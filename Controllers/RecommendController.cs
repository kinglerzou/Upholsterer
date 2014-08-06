using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Web.Mvc;
using Upholsterer.Models;
using Upholsterer.ViewModel;

namespace Upholsterer.Controllers
{
    public class RecommendController : BaseController 
    {
       
        // 推荐首页
        public ActionResult Index()
        {
            var id = CheckValid();
            if (id == -1 || Session["uid"]==null)
            {
                return RedirectToAction("Logon", "User");
            }
            return View();
        }

        //推荐的页面 
        public ActionResult RecommendPage()
        {
            var id = CheckValid();
            if (id == -1 || Session["uid"] == null)
            {
                return RedirectToAction("Logon", "User");
            }
            var require = PrivateDb.One((Requirement r) => r.UserId == id);
            var baseusers = GetBaseUsers(id).Where(n => PartMathUser(require, PrivateDb.GetUninUser(n.UserId)));//找出所有正规的异性符合需求的。 
            ViewBag.Sex = GetMyself().User.Sex == "man" ? "她" : "他";
            var userlist = baseusers.Select(baseuser => GetRecommendUser(baseuser.UserId)).ToList();
            return PartialView(userlist.OrderByDescending(n => n.Rate.TotalRate));
        }

        public ActionResult Visited()
        {
            var id = CheckValid();
            if (id == -1 || Session["uid"] == null)
            {
                return RedirectToAction("Logon", "User");//不知道有没有效果
            }

            return View();
        }

     

        public ActionResult VisitedUsers()
        {
            var id = CheckValid();
            if (id == -1 || Session["uid"] == null)
            {
                return RedirectToAction("Logon", "User");
            }
        
            //ViewBag.Sex = GetMyself().User.Sex == "man" ? "她" : "他";

            var mylist = PrivateDb.VisitorAll().Where(n => n.UserId == id).ToList();
            foreach (var visitLog in mylist.Where(m=>!m.IsRead))
            {
                PrivateDb.ReadMessage<VisitLog>(visitLog.Id);
            }
            ViewBag.VisitorCount = mylist.Sum(n => n.Count);// 需要修正！

            var visitusrs = mylist.Select(obj => new VisitUser
            {
                Visitor = PrivateDb.GetUninUser(obj.VisitorId),
                LastVisitTime = obj.ActionTime,
                Times = obj.Count 
            }).ToList();
            

            return PartialView(visitusrs.OrderByDescending(n=>n.LastVisitTime));
        }

        /// <summary>
        /// 获取推荐列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        private RecommendUser GetRecommendUser(int userid)
        {
            var uin = PrivateDb.GetUninUser(userid);
            var ruser = new RecommendUser
            {
                User = uin,
                LastSate = LastStateStr(userid),
                Rate = GetRecommendRate(CheckValid(),userid),
                InfoStr = GetInfoStr(uin)
            };
            return ruser;
        }
        /// <summary>
        /// 最新的状态 转化为字符串.  可以统计照片,需求,默认 这三种
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        private string LastStateStr(int userid)
        {
            var ls = PrivateDb.LastOne((State u) => u.UserId == userid);
            if (ls != null)
            {
                return ls.Content;
            }
            return "我刚来到Upholester";
        }

        private string GetInfoStr(UninUser user)
        {
            var sb=new StringBuilder();
            sb.Append(user.User.Age+"岁 | ");
            if (!string.IsNullOrEmpty(user.BaseInfo.Height))
            {
                sb.Append(user.BaseInfo.Height + " | ");
            }
            if (!string.IsNullOrEmpty(user.BaseInfo.Education))
            {
                sb.Append(user.BaseInfo.Education + " | ");
            }
            if (!string.IsNullOrEmpty(user.BaseInfo.MonthlyIncome))
            {
                sb.Append(user.BaseInfo.MonthlyIncome );
            }
            sb.Append("<br/>");
            sb.Append(PrivateDb.IamgeCount(user.User.UserId) + "张照片 | ");
            sb.Append("资料完整度" + GetPersent(user.User.UserId)*100 + "%");
            sb.Append("<br/>");
            sb.Append(LastLoginstr(user.User.UserId));
            return sb.ToString();
        }
    }
}
