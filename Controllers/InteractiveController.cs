﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Upholsterer.Models;
using Upholsterer.ViewModel;
using System.Web.Script.Serialization;

namespace Upholsterer.Controllers
{
    public class InteractiveController : BaseController
    {
        //
        // GET: /Interactive/

        public ActionResult Index()
        {
            var id = CheckValid();
            if (id == -1 || Session["uid"] == null)
            {
                return RedirectToAction("Logon", "User");
            }
            return View();
        }

        /// <summary>
        /// 个人消息统计块
        /// </summary>
        /// <returns></returns>
        public ActionResult InfoBlock()
        {
            var id = CheckValid();
            if (id == -1 || Session["uid"] == null)
            {
                return RedirectToAction("Logon", "User");
            }
            ViewBag.User = GetMyself().User;
            ViewBag.TopicCount = PrivateDb.TopicAll().Count(m => m.UserId == id);
            ViewBag.EnjoyCount = PrivateDb.EnjoyTopicAll().Count(n => n.UserId == id);
            ViewBag.ReplyCount = PrivateDb.CommentAll().Where(n => n.UserId == id).Select(n=>n.TopicId).Distinct().Count();
            return PartialView();
        }

        #region 评论区域
        /// <summary>
        /// 回复 不带图片
        /// </summary>
        /// <returns></returns>
        public ActionResult Coments(int topicId)
        {
            var coms = PrivateDb.CommentAll().Where(n => n.TopicId == topicId).OrderBy(n => n.Id).ToList();
            var listcoms = new List<UninComments>();
            foreach (var comment in coms)
            {
                var uncom = new UninComments {Comment = comment,ReComment = null,User = PrivateDb.GetUninUser(comment.UserId),Sex = GetIdSex(comment.UserId)};
                if (comment.ReplyId != 0)
                {
                    Comment comment1 = comment;
                    uncom.ReComment = PrivateDb.One((Comment c) => c.Id == comment1.ReplyId);
                }
                listcoms.Add(uncom);
            }
            //前台隐藏值
            ViewBag.TopicId = topicId;
            ViewBag.SefeId = CheckValid();
            ViewBag.TopicUserId = PrivateDb.One((Topic t) => t.Id == topicId).UserId;
            return PartialView(listcoms);
        }

        public ActionResult CreateComment(Comment model)
        {
            try
            {
                //产生评论
                var id = CheckValid();
                var name = GetUserNameById(id);
                
                var com = new Comment
                {
                    ActionTime = DateTime.Now,
                    Content = model.Content,
                    TopicId = model.TopicId,
                    ReplyId = model.ReplyId,
                    UserId = id,
                    UserName = name
                };
                //修复第一次评论时，ActionName出现null的bug
                
                //var mm = string.IsNullOrEmpty(LoveDb.LastOne((Comment c) => c.UserId == id).ActionTime);
                var mm = PrivateDb.LastOne((Comment c) => c.Id == id);
                if (string.IsNullOrEmpty(mm.ActionTime.ToString()))
                {

                var min = PrivateDb.DiffMinute(mm.ActionTime, DateTime.Now);
                if (min<=1)
                {
                    return Json(0);
                }
                }              
                PrivateDb.Add(com);
                
                var topic = PrivateDb.One((Topic t) => t.Id == model.TopicId);
                if (topic != null)
                {

                    if (topic.UserId != id)//回复这个话题不是楼主本人就通知楼主。
                    {
                        var msg = new Message
                        {
                            ActionTime = DateTime.Now,
                            Content =
                                string.Format(
                                    "<a href='/User/Index?id={0}'>{1}</a>回复了你的话题:<a href='/Interactive/Detail?topicId={2}'>{3}</a>"
                                    , id, name, topic.Id, topic.Title),
                            FromUserId = 1,
                            FromUserName = "Upholsterer管理员",
                            IsReaded = false,
                            MegType = MegType.System,
                            StateId = topic.Id,
                            StateType = StateType.Topic,
                            ToUserId = topic.UserId,
                        };
                        PrivateDb.Add(msg);
                    }

                    if (com.ReplyId != 0)
                    {
                        var recom = PrivateDb.One((Comment c) => c.Id == com.ReplyId);
                        if (recom != null)
                        {
                            var msg = new Message
                            {
                                ActionTime = DateTime.Now,
                                Content =
                                    string.Format(
                                        "<a href='/User/Index?id={0}'>{1}</a>回复了你的,话题:<a href='/Interactive/Detail?topicId={2}'>{3}</a>"
                                        , id, name, topic.Id, topic.Title),
                                FromUserId = 1,
                                FromUserName = "Upholsterer管理员",
                                IsReaded = false,
                                MegType = MegType.System,
                                StateId = recom.Id,
                                StateType = StateType.Comment,
                                ToUserId = recom.UserId,
                            };
                            PrivateDb.Add(msg);
                        }
                    }

                
               
                }
                return Json(1);
            }
            catch (Exception)
            {

                return Json(0);
            }
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="id"></param>
        public void DeleteComment(int id)
        {
            PrivateDb.Delete<Comment>(id);
        }
        #endregion

        #region 话题的喜欢处理

        public JsonResult EnjoyTopic(int topicid)
        {
            var id = CheckValid();
            var name = GetUserNameById(id);
            var topic = PrivateDb.One((Topic t) => t.Id == topicid);
            if (topic != null)
            {
                if (id==topic.UserId)
                {
                    return Json(0);
                }


            var love = PrivateDb.One((EnjoyTopic m) => m.TopicId == topicid && m.UserId == id);
            
            if (love != null)//存在 就删除 
            {
                PrivateDb.Delete<EnjoyTopic>(love.Id);
                var msg1 =
                    PrivateDb.One(
                        (Message m) =>
                            m.StateType == StateType.Topic && m.StateId == topicid && m.Content.Contains(name));
                if (msg1 != null)
                {
                    PrivateDb.Delete<Message>(msg1.Id);
                }

                return Json(0);//已经取消喜欢
            }

            love = new EnjoyTopic
            {
                TopicId = topicid,
                UserId = id,
                ActionTime = DateTime.Now,
                IsRead = false,
            };
            PrivateDb.Add(love);

            //系统消息通知下
        
                var msg = new Message
                {
                    ActionTime = DateTime.Now,
                    Content =
                        string.Format(
                            "<a href='/User/Index?id={0}'>{1}</a>赞了你的话题:<a href='/Interactive/Detail?topicId={2}'>{3}</a>"
                            , id,name , topicid, topic.Title),
                    FromUserId = 1,
                    FromUserName = "Upholester管理员",
                    IsReaded = false,
                    MegType = MegType.System,
                    StateId = topicid,
                    StateType = StateType.Topic,
                    ToUserId = topic.UserId,
                };
                PrivateDb.Add(msg);
                return Json(1);
            }
            return Json(0);
        }

        /// <summary>
        /// 是否已经赞过这个话题
        /// </summary>
        /// <returns></returns>
        public JsonResult IsEnjoyed()
        {
            var id = CheckValid();
            var enjoys = PrivateDb.EnjoyTopicAll().Where(n => n.UserId == id).Select(n => n.TopicId).ToList();
            return Json(enjoys);
        }
        #endregion

        /// <summary>
        /// 我的话题
        /// </summary>
        /// <param name="id"></param>
        /// <param name="typeid">1为我喜欢的 2为我回应的 0|null表示不管</param>
        /// <returns></returns>
        public ActionResult MyTopics(int? id,int? typeid)
        {
            if (id == null) id = CheckValid();
            ViewBag.TypeId = typeid;// 0|null表示不管
            var tops =new List<Topic>();
            switch (typeid)
            {
                case null:
                case 0:
                    tops = PrivateDb.TopicAll().Where(n => n.UserId == id).OrderByDescending(n => n.Id).ToList();
                    break;
                case 1:
                    var likes = PrivateDb.EnjoyTopicAll().Where(n => n.UserId == id).ToList();
                    tops.AddRange(likes.Select(topic => PrivateDb.One((Topic t) => t.Id == topic.TopicId)));
                    break;
                case 2:
                    var replys =
                        PrivateDb.CommentAll()
                            .Where(n => n.UserId == id)
                            .Select(n => new SimpleTopic {TopId = n.TopicId})
                            .Distinct();
                    foreach (var one in replys.Select(simpleTopic => PrivateDb.One((Topic t) => t.Id == simpleTopic.TopId)).Where(one => one != null&&tops.All(n => n.Id != one.Id)))
                    {
                        tops.Add(one);
                    }
                    break;
            }
          
            return View(tops);
        }

        /// <summary>
        /// 首页话题列表
        /// </summary>
        /// <returns></returns>
        public ActionResult TopicList()
        {
            var tops = PrivateDb.TopicAll().Where(n=>n.IsValid).OrderByDescending(n => n.UpDataTime);
       
            return PartialView(tops);
        }
        /// <summary>
        /// 搜索话题
        /// </summary>
        /// <returns></returns>
        public JsonResult EnterSearch(string keyword)
        { 
           
           var keys=PrivateDb.TopicAll().Where(n=>n.Title==keyword).OrderByDescending(n=>n.UpDataTime);
           return Json(keys,JsonRequestBehavior.AllowGet);
        
        }

        /// <summary>
        /// 一个话题明细
        /// </summary>
        /// <param name="topicId"></param>
        /// <returns></returns>
        public ActionResult Detail(int topicId)
        {
            var tp= PrivateDb.One((Topic t) => t.Id == topicId);
            if (tp == null)
            {
                return RedirectToAction("ErroResult");
            }
            tp.Content = Helper.HtmlHelper.TransStringToHtml(tp.Content);
            var tu = new TopicUser
            {
                Topic = tp,
                UninUser = PrivateDb.GetUninUser(tp.UserId),
            };
            ViewBag.UserId = tp.UserId;
            ViewBag.CurrentUserId = CheckValid();
            ViewBag.Sex = GetIdSex(tp.UserId);
            
            return View(tu);
        }

        public ActionResult ErroResult()
        {
            return View();
        }

       
        /// <summary>
        /// 删除话题
        /// </summary>
        /// <param name="id"></param>
        public void DeleteTopic(int id)
        {
            PrivateDb.Delete<Topic>(id);     
        }

        #region Edit topic

        public ActionResult Edit(int id)
        {
            var top = PrivateDb.One((Topic t) => t.Id == id);
            top.Content = Helper.HtmlHelper.TransStringToHtml(top.Content);
            return View(top);
        }
        [HttpPost]
        public ActionResult Edit(Topic model)
        {
            var str = Helper.HtmlHelper.TransStringToHtml(model.Content);
            str = Helper.HtmlHelper.TransHtmlToString(str).Trim();
            if (str != ""&&model.Title.Trim()!=""&&str.Length>5)
            {
                PrivateDb.UpdateTopic(model);
                return Json(model.Id);
            }
            return Json("内容或者标题不能太短哦");
        }

        #endregion
       
        #region Create topic
        /// <summary>
        /// 创建一个话题
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateTopic()
        {
            return View();
        }

        /// <summary>
        /// 导航页
        /// </summary>
        /// <returns></returns>
        public ActionResult TopicNav()
        {
            return PartialView();
        }
        /// <summary>
        /// 创建话题
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateTopic(Topic model)
        {
            if (!Helper.HtmlHelper.IsEmpty(model.Content)&&model.Title.Length>=2)
            {
                var id = CheckValid();
                var lastone = PrivateDb.LastOne((Topic t) => t.UserId == id);
                if (lastone!=null&&Helper.Helpers.DiffMinute(lastone.ActionTime, DateTime.Now) < 1)
                {
                    return Json("ORZ,歇会儿再发起话题吧...");
                }

                var topic = new Topic
                {
                    ActionTime = DateTime.Now,
                    UserId = id,
                    Content = model.Content,
                    Title = model.Title,
                    UpDataTime = DateTime.Now,
                    IsValid = true,
                    UserName = GetUserNameById(id)
                };
                PrivateDb.Add(topic);
                var one = PrivateDb.LastOne((Topic t) => t.UserId == id);
                return Json(one.Id);

            }
            return Json("内容或者标题不能太短哦");
        }

        
        #endregion
    }
}
