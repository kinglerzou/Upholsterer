using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Schema;
using Upholsterer.ViewModel;
using NoteSystem.Helper;

namespace Upholsterer.Models
{
    public class PrivateDbRepository
    {
        private static PrivateDbRepository _loveDb;

        private PrivateDbRepository()
        {
        }

        public static PrivateDbRepository GetIntance()
        {
            return _loveDb ?? (_loveDb = new PrivateDbRepository());
        }

        #region  常规方法 Add Delete All One  Ones LastOne

        #region Add
        
        public void Add<T>(T t)
        {
            using(var PrivateDb = new PrivateDb())
            {
                switch (t.GetType().Name)
                {
                    case "User":
                        PrivateDb.Users.Add(t as User);
                        break;
                    case "BaseInfo":
                        PrivateDb.BaseInfos.Add(t as BaseInfo);
                        break;
                    case "Requirement":
                        PrivateDb.Requirements.Add(t as Requirement);
                        break;
                    case "DetailInfo":
                        PrivateDb.DetailInfos.Add(t as DetailInfo);
                        break;
                    case "InfoStatistic":
                        PrivateDb.InfoStatistics.Add(t as InfoStatistic);
                        break;
                    case "UserHot":
                        PrivateDb.UserHots.Add(t as UserHot);
                        break;
                    case "Praise":
                        PrivateDb.Praises.Add(t as Praise);
                        break;
                    case "State":
                        PrivateDb.States.Add(t as State);
                        break;
                    case "Iamgbox":
                        PrivateDb.Iamgboxes.Add(t as Iamgbox);
                        break;
                    case "Message":
                        PrivateDb.Messages.Add(t as Message);
                        break;
                    case "LoginLog":
                        PrivateDb.LoginLogs.Add(t as LoginLog);
                        break;
                    case "Role":
                        PrivateDb.Roles.Add(t as Role);
                        break;
                    case "Hello":
                        PrivateDb.Hellos.Add(t as Hello);
                        break;
                    case "RoleLog":
                        PrivateDb.RoleLogs.Add(t as RoleLog);
                        break;
                    case "Authority":
                        PrivateDb.Authoritys.Add(t as Authority);
                        break;
                    case "AdminStatistic":
                        PrivateDb.AdminStatistics.Add(t as AdminStatistic);
                        break;
                    case "VisitLog":
                        PrivateDb.VisitLogs.Add(t as VisitLog);
                        break;
                    case "Report":
                        PrivateDb.Reports.Add(t as Report);
                        break;
                    case "ReportLog":
                        PrivateDb.ReportLogs.Add(t as ReportLog);
                        break;
                    case "MyLove":
                        PrivateDb.MyLoves.Add(t as MyLove);
                        break;
                    case "DisLove":
                        PrivateDb.DisLoves.Add(t as DisLove);
                        break;
                    case "Topic":
                        PrivateDb.Topics.Add(t as Topic);
                        break;
                    case "EnjoyTopic":
                        var en = t as EnjoyTopic;
                        PrivateDb.EnjoyTopics.Add(en);
                        if (en != null)
                        {
                            var top = PrivateDb.Topics.Find(en.TopicId);
                            top.LikeCount += 1;
                        }
                        break;
                    case "Comment":
                        var com = t as Comment;
                        PrivateDb.Comments.Add(com);
                        //让话题评论数加1 
                        if (com != null)
                        {
                            var top = PrivateDb.Topics.Find(com.TopicId);
                            top.ReplyCount += 1;
                            top.UpDataTime = DateTime.Now;
                        }


                        break;
                }
                PrivateDb.SaveChanges();
            }
        }
        
        #endregion
        
        #region Delete
        
        public void Delete<T>(int id)
        {
            using (var PrivateDb = new PrivateDb())
            {
                switch (typeof(T).Name)
                {
                    case "User":
                        PrivateDb.Users.Remove(PrivateDb.Users.SingleOrDefault(m => m.UserId == id));
                        break;
                    case "BaseInfo":
                        PrivateDb.BaseInfos.Remove(PrivateDb.BaseInfos.SingleOrDefault(m => m.Id == id));
                        break;
                    case "Requirement":
                        PrivateDb.Requirements.Remove(PrivateDb.Requirements.SingleOrDefault(m => m.Id == id));
                        break;
                    case "DetailInfo":
                        PrivateDb.DetailInfos.Remove(PrivateDb.DetailInfos.SingleOrDefault(m => m.Id == id));
                        break;
                  
                    case "InfoStatistic":
                        PrivateDb.InfoStatistics.Remove(PrivateDb.InfoStatistics.SingleOrDefault(m => m.Id == id));
                        break;
                    case "UserHot":
                        PrivateDb.UserHots.Remove(PrivateDb.UserHots.SingleOrDefault(m => m.Id == id));
                        break;
                    case "Praise":
                        PrivateDb.Praises.Remove(PrivateDb.Praises.SingleOrDefault(m => m.Id == id));
                        break;
                    case "State":
                        PrivateDb.States.Remove(PrivateDb.States.SingleOrDefault(m => m.Id == id));
                        break;
                    case "Iamgbox":
                        PrivateDb.Iamgboxes.Remove(PrivateDb.Iamgboxes.SingleOrDefault(m => m.Id == id));
                        break;
                    case "Message":
                        PrivateDb.Messages.Remove(PrivateDb.Messages.SingleOrDefault(m => m.Id == id));
                        break;
                    case "LoginLog":
                        PrivateDb.LoginLogs.Remove(PrivateDb.LoginLogs.SingleOrDefault(m => m.Id == id));
                        break;
                    case "Role":
                        PrivateDb.Roles.Remove(PrivateDb.Roles.SingleOrDefault(m => m.Id == id));
                        break;
                    case "Hello":
                        PrivateDb.Hellos.Remove(PrivateDb.Hellos.SingleOrDefault(m => m.Id == id));
                        break;
                    case "RoleLog":
                        PrivateDb.RoleLogs.Remove(PrivateDb.RoleLogs.SingleOrDefault(m => m.Id == id));
                        break;
                    case "Authority":
                        PrivateDb.Authoritys.Remove(PrivateDb.Authoritys.SingleOrDefault(m => m.Id == id));
                        break;
                    case "AdminStatistic":
                        PrivateDb.AdminStatistics.Remove(PrivateDb.AdminStatistics.SingleOrDefault(m => m.Id == id));
                        break;
                    case "VisitLog":
                        PrivateDb.VisitLogs.Remove(PrivateDb.VisitLogs.SingleOrDefault(m => m.Id == id));
                        break;
                    case "Report":
                        PrivateDb.Reports.Remove(PrivateDb.Reports.SingleOrDefault(m => m.Id == id));
                        break;
                    case "ReportLog":
                        PrivateDb.ReportLogs.Remove(PrivateDb.ReportLogs.SingleOrDefault(m => m.Id == id));
                        break;
                    case "MyLove":
                        PrivateDb.MyLoves.Remove(PrivateDb.MyLoves.SingleOrDefault(m => m.Id == id));
                        break;
                    case "DisLove":
                        PrivateDb.DisLoves.Remove(PrivateDb.DisLoves.SingleOrDefault(m => m.Id == id));
                        break;
                    case "Topic":
                        PrivateDb.Topics.Remove(PrivateDb.Topics.SingleOrDefault(m => m.Id == id));
                        break;
                    case "EnjoyTopic":
                     
                        var ens = PrivateDb.EnjoyTopics.SingleOrDefault(m => m.Id == id);
                        PrivateDb.EnjoyTopics.Remove(ens);
                        if (ens != null)
                        {
                            var top = PrivateDb.Topics.Find(ens.TopicId);
                            top.LikeCount -= 1;
                        }
                        break;
                    case "Comment":
                        var com = PrivateDb.Comments.SingleOrDefault(m => m.Id == id);
                        PrivateDb.Comments.Remove(com);
                        if (com != null)
                        {
                            var top = PrivateDb.Topics.Find(com.TopicId);
                            top.ReplyCount -= 1;
                        }
                        break;
                }
                PrivateDb.SaveChanges();
            }
        }

        #endregion
      
        #region All
        public List<User> UserAll()
        {
            using (var db = new PrivateDb())
            {
                return db.Users.ToList();
            }
        }
        public List<EnjoyTopic> EnjoyTopicAll()
        {
            using (var db = new PrivateDb())
            {
                return db.EnjoyTopics.ToList();
            }
        }

        public UninUser GetUninUser(int id)
        {
           
            using (var db = new PrivateDb())
            {
                var userinfo = new UninUser
                    {
                        User = db.Users.FirstOrDefault(n => n.UserId == id) ,
                        BaseInfo = db.BaseInfos.FirstOrDefault(n => n.UserId == id),
                        DetailInfo = db.DetailInfos.FirstOrDefault(n => n.UserId == id)
                    };
                return userinfo;
            }
        }

        public List<MyLove> MyLoveAll()
        {
            using (var db = new PrivateDb())
            {
                return db.MyLoves.ToList();
            }
        }

        public List<Topic> TopicAll()
        {
            using (var db = new PrivateDb())
            {
                return db.Topics.ToList();
            }
        }

        public List<DisLove> DisLoveAll()
        {
            using (var db = new PrivateDb())
            {
                return db.DisLoves.ToList();
            }
        }
        public List<State> StateAll()
        {
            using (var db = new PrivateDb())
            {
                return db.States.ToList();
            }
        }

        public List<Iamgbox> IamgAll()
        {
            using (var db = new PrivateDb())
            {
                return db.Iamgboxes.ToList();
            }
        }

        public List<Role> RoleAll()
        {
            using (var db = new PrivateDb())
            {
                return db.Roles.ToList();
            }
        }
        public List<ReportLog> ReportLogAll()
        {
            using (var db = new PrivateDb())
            {
                return db.ReportLogs.ToList();
            }
        }

        public List<Message> MessageAll()
        {
            using (var db = new PrivateDb())
            {
                return db.Messages.ToList();
            }
        }

        public List<Comment> CommentAll()
        {
            using (var db = new PrivateDb())
            {
                return db.Comments.ToList();
            }
        }

        public List<AdminStatistic> AdminStatisticAll()
        {
            using (var db = new PrivateDb())
            {
                return db.AdminStatistics.ToList();
            }
        }

        public List<VisitLog> VisitorAll()
        {
            using (var db = new PrivateDb())
            {
                return db.VisitLogs.ToList();
            }
        }

        public List<Praise> PraiseAll()
        {
            using (var db = new PrivateDb())
            {
                return db.Praises.ToList();
            }
        }

        public List<Hello> HelloAll()
        {
            using (var db = new PrivateDb())
            {
                return db.Hellos.ToList();
            }
        }

        /// <summary>
        /// 已经倒叙
        /// </summary>
        /// <returns></returns>
        public List<Report> ReportAll()
        {
            using (var db = new PrivateDb())
            {
                return db.Reports.OrderByDescending(n=>n.Id).ToList();
            }
        } 



        public List<UserHot> UserHotAllDes()
        {
            using (var db = new PrivateDb())
            {
                return db.UserHots.OrderByDescending(n=>n.HotValue).ToList();
            }
        }

        #endregion

        #region One
        public User One(Func<User, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.Users.ToList().Where(predicate).FirstOrDefault();
            }
        }
        public BaseInfo One(Func<BaseInfo, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.BaseInfos.ToList().Where(predicate).FirstOrDefault();
            }
        }
        public Iamgbox One(Func<Iamgbox, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.Iamgboxes.ToList().Where(predicate).FirstOrDefault();
            }
        }
        
        public Requirement One(Func<Requirement, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.Requirements.ToList().Where(predicate).FirstOrDefault();
            }
        }
        public DetailInfo One(Func<DetailInfo, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.DetailInfos.ToList().Where(predicate).FirstOrDefault();
            }
        }
        public Comment One(Func<Comment, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.Comments.ToList().Where(predicate).FirstOrDefault();
            }
        }
        public MyLove One(Func<MyLove, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.MyLoves.ToList().Where(predicate).FirstOrDefault();
            }
        }
        public DisLove One(Func<DisLove, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.DisLoves.ToList().Where(predicate).FirstOrDefault();
            }
        }
        public InfoStatistic One(Func<InfoStatistic, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.InfoStatistics.ToList().Where(predicate).FirstOrDefault();
            }
        }
        public State One(Func<State, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.States.ToList().Where(predicate).FirstOrDefault();
            }
        }
        public Hello One(Func<Hello, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.Hellos.ToList().Where(predicate).FirstOrDefault();
            }
        }
        public Role One(Func<Role, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.Roles.ToList().Where(predicate).FirstOrDefault();
            }
        }
        public EnjoyTopic One(Func<EnjoyTopic, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.EnjoyTopics.ToList().Where(predicate).FirstOrDefault();
            }
        }
        public RoleLog One(Func<RoleLog, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.RoleLogs.ToList().Where(predicate).FirstOrDefault();
            }
        }
        public UserHot One(Func<UserHot, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.UserHots.ToList().Where(predicate).FirstOrDefault();
            }
        }

        public Authority One(Func<Authority, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.Authoritys.ToList().Where(predicate).FirstOrDefault();
            }
        }
        public Topic One(Func<Topic, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.Topics.ToList().Where(predicate).FirstOrDefault();
            }
        }
        public AdminStatistic One(Func<AdminStatistic, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.AdminStatistics.ToList().Where(predicate).FirstOrDefault();
            }
        }
        public Message One(Func<Message, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.Messages.ToList().Where(predicate).FirstOrDefault();
            }
        }

        public Praise One(Func<Praise, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.Praises.ToList().Where(predicate).FirstOrDefault();
            }
        }
        public Report One(Func<Report, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.Reports.ToList().Where(predicate).FirstOrDefault();
            }
        }
        public ReportLog One(Func<ReportLog, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.ReportLogs.ToList().Where(predicate).FirstOrDefault();
            }
        }
        #endregion 

        #region LastOne
        public Message LastOne(Func<Message, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.Messages.ToList().Where(predicate).LastOrDefault();
            }
        }
        public Comment LastOne(Func<Comment, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.Comments.ToList().Where(predicate).LastOrDefault();
            }
        
        }
        public Topic LastOne(Func<Topic, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.Topics.ToList().Where(predicate).LastOrDefault();
            }
        }
 
        public State LastOne(Func<State, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                var st=db.States.ToList().Where(predicate).LastOrDefault();
                return st;
            }
        }

        public State GetOneState(Func<State, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                var state = db.States.ToList().Where(predicate).LastOrDefault() ?? new State
                {
                    Content = "我刚来到Upholeterer,赶快发现我吧",
                };
                return state;
            }
        }

        public VisitLog LastOne(Func<VisitLog, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.VisitLogs.ToList().Where(predicate).LastOrDefault();
            }
        }

        public LoginLog LastOne(Func<LoginLog, bool> predicate)
        {
            using (var db = new PrivateDb())
            {
                return db.LoginLogs.ToList().Where(predicate).LastOrDefault();
            }
        }

        #endregion

        #endregion

        #region 定制方法,Update相关,logon,check
        /// <summary>
        /// 更新消息资料表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="info"></param>
        public void UpdateBaseInfo(int id, BaseInfo info)
        {
            using (var db = new PrivateDb())
            {
                var dst = db.BaseInfos.SingleOrDefault(n => n.UserId == id);
                if (dst == null) return;
                var i = 0;
                dst.Height = Modifycount(info.Height, ref i);
                dst.Education = Modifycount(info.Education, ref i);
                dst.MonthlyIncome = Modifycount(info.MonthlyIncome, ref i);
                dst.ResidenceProvince = Modifycount(info.ResidenceProvince, ref i);
                dst.ResidenceCity = Modifycount(info.ResidenceCity, ref i);
                if (info.ResidenceCity != "选择城市")
                {
                    i--;
                }
                dst.School = Modifycount(info.School, ref i);
                dst.Vocation = Modifycount(info.Vocation, ref i);
                dst.Position = Modifycount(info.Position, ref i);
                dst.CompanyNature = Modifycount(info.CompanyNature, ref i);
                dst.State = Modifycount(info.State, ref i);

                var statistic = db.InfoStatistics.SingleOrDefault(b => b.UserId == id);
                if (statistic != null)
                {
                    statistic.BaseInfoReal = i;
                    var real = statistic.DetialsInfoReal + statistic.BaseInfoReal + statistic.LoveViewsReal;
                    var imgsum = db.Iamgboxes.Count(n => n.UserId == id);
                    imgsum = imgsum > 22 ? 22 : imgsum;
                    real += imgsum;
                    //还差一条标准 .. 回答问题 或者别的。  准备18道题目。
                    var x = (float)real / 50;
                    statistic.Percent = x; // (float)Math.Round(x, 2);
                }
                db.SaveChanges();
            }

        }

        public double GetPercent(int userid)
        {
            using (var db = new PrivateDb())
            {
                var statistic = db.InfoStatistics.SingleOrDefault(b => b.UserId == userid);
                if (statistic != null)
                {
                    var real = statistic.DetialsInfoReal + statistic.BaseInfoReal + statistic.LoveViewsReal;
                    var imgsum = db.Iamgboxes.Count(n => n.UserId == userid);
                    imgsum = imgsum > 22 ? 22 : imgsum;
                    real += imgsum;
                    var x = (float)real / 50;
                    statistic.Percent = x; // (float)Math.Round(x, 2);
                    db.SaveChanges();
                    return x;
                }
            }
            return 0;
        }

        public void UpdateTopic(Topic model)
        {
            using (var db = new PrivateDb())
            {
                var one = db.Topics.Find(model.Id);
                if (one != null)
                {
                    one.Title = model.Title;
                    one.Content = model.Content;
                    one.UpDataTime = DateTime.Now;

                    db.SaveChanges();
                }

            }
        }

        /// <summary>
        /// 将IsRead=false的消息设置为true
        /// </summary>
        public void ReadMessage<T>(int id)
        {
            using (var db=new PrivateDb())
            {
                switch (typeof(T).Name)
                {
                        //消息
                    case "Message":
                        db.Messages.Find(id).IsReaded = true;
                        break;
                    case "MyLove":
                        db.MyLoves.Find(id).IsRead = true;
                        break;
                    case "VisitLog":
                        db.VisitLogs.Find(id).IsRead = true;
                        break;
                    case "EnjoyTopic":
                        db.EnjoyTopics.Find(id).IsRead = true;
                        break;
                }
                db.SaveChanges();
            }
        }


        /// <summary>
        /// 更新消息资料表
        /// </summary>
        /// <param name="info"></param>
        public void UpdateRequirement(Requirement info)
        {
            using (var db = new PrivateDb())
            {
                var dst = db.Requirements.SingleOrDefault(n => n.UserId == info.UserId);
                if (dst == null) return;
                dst.AgeLl = info.AgeLl;
                dst.AgeUl = info.AgeUl;
                dst.HightLl = info.HightLl;
                dst.HightUl = info.HightUl;
                dst.ResidenceCity = info.ResidenceCity;
                dst.Education = info.Education;
                dst.MonthlyIncomeLl = info.MonthlyIncomeLl;
                dst.MonthlyIncomeUl = info.MonthlyIncomeUl;
                dst.ResidenceProvince = info.ResidenceProvince;
                db.SaveChanges();
            }

        }
        /// <summary>
        /// 更新详细资料表
        /// </summary>
        /// <param name="id"></param>
        /// <param name="detail"></param>
        public void UpdateDetail(int id, DetailInfo detail)
        {
            using (var db = new PrivateDb())
            {
                var dst = db.DetailInfos.SingleOrDefault(n => n.UserId == id);
                if (dst == null) return;
                var i = 0;

                dst.Housing = Modifycount(detail.Housing, ref i);
                dst.Car = Modifycount(detail.Car, ref i);
                dst.People = Modifycount(detail.People, ref i);
                dst.Constellation = Modifycount(detail.Constellation, ref i);
                dst.BloodType = Modifycount(detail.BloodType, ref i);
                dst.ResidenceCity = Modifycount(detail.ResidenceCity, ref i);
                dst.NativeCity = Modifycount(detail.NativeCity, ref i);

                if (detail.NativeCity != "请选择" && detail.NativeCity!="")
                {
                    i--;
                }
                dst.Weight = Modifycount(detail.Weight, ref i);

                if (!string.IsNullOrEmpty(detail.DouBan))
                {
                    dst.DouBan = detail.DouBan.Trim();
                    i++;
                }
                else
                {
                    dst.DouBan = null;
                }

                if (!string.IsNullOrEmpty(detail.MicroBlog))
                {
                    dst.MicroBlog = detail.MicroBlog.Trim();
                    i++;
                }
                else
                {
                    dst.MicroBlog = null;
                }

                var statistic = db.InfoStatistics.SingleOrDefault(b => b.UserId == id);
                if (statistic != null)
                {
                    statistic.DetialsInfoReal = i;
                    var real = statistic.DetialsInfoReal + statistic.BaseInfoReal + statistic.LoveViewsReal;
                    var imgsum = db.Iamgboxes.Count(n => n.UserId == id);
                    imgsum = imgsum > 22 ? 22 : imgsum;
                    //还差一条标准 .. 回答问题 或者别的。  准备18道题目。
                    statistic.Percent = (float)(real + imgsum) / 50;
                }

                db.SaveChanges();
            }
        }
        //更改密码
        public bool ModifyPassword(int id,ModifyPassword modify)
        {
            using (PrivateDb db = new PrivateDb())
            {
                var user = db.Users.SingleOrDefault(n=>n.UserId==id);
                if (user == null) return false;

                if (user.Password != Helper.Helpers.GetMd5Code(modify.OldPassword)) return false;
                 user.Password = Helper.Helpers.GetMd5Code(modify.ConfirmPassword);
                 db.SaveChanges();
            }
            return true;
        }


        /// <summary>
        /// 将举报信息处理为已处理
        /// </summary>
        /// <param name="id"></param>
        public void DoneReport(int id)
        {
            using (var db = new PrivateDb())
            {
                var rep = db.Reports.Find(id);
                rep.IsDone = true;
                db.SaveChanges();
            }
        }

       
        /// <summary>
        /// 审核资料或图片
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="result"></param>
        /// <param name="type">0表示图片,1表示资料</param>
        /// <returns></returns>
        public bool CheckImgOrInfo(int userid, bool result,int type)
        {
            using (var db = new PrivateDb())
            {
                var user = db.Users.SingleOrDefault(n => n.UserId == userid);
                if (user == null) return false;
                if (type == 0)
                {
                    user.IsCheckedImg = true;
                    user.IsVerifiedImg = result;
                }
                else
                {
                    user.IsChecked = true;
                    user.IsVerified = result;
                }
              
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 封号
        /// </summary>
        /// <param name="userId"></param>
        public void ForbidUser(int userId)
        {
            using (var db = new PrivateDb())
            {
                var u = db.Users.Find(userId);
                if (u != null)
                {
                    u.Enable = 0;
                    db.SaveChanges();
                }
            }
        }
        /// <summary>
        /// 解封
        /// </summary>
        /// <param name="userId"></param>
        public void UnForbidUser(int userId)
        {
            using (var db = new PrivateDb())
            {
                var u = db.Users.Find(userId);
                if (u != null)
                {
                    u.Enable =1;
                    db.SaveChanges();
                }
            }
        }
        
        /// <summary>
        /// 统计管理员数据
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="type">0图片,1资料,2登录最近一天,签到</param>
        public void AdminStatistics(int userid, int type)
        {
            //经验值如何算
            using (var db = new PrivateDb())
            {
                var role = db.AdminStatistics.SingleOrDefault(n => n.UserId == userid);
                if (role != null)
                {
                    switch (type)
                    {
                        case 0:
                            role.CheckImgCount += 1;
                            break;
                        case 1:
                            role.CheckInfoCount += 1;
                            break;
                        case 2:
                            if (DateTime.Now.DayOfYear - role.LastLogin.DayOfYear > 1)// 需要判断是否重复 
                            {
                                role.SignInDays = 1;
                            }
                            else
                            {
                                role.SignInDays += 1;
                            }
                            role.LastLogin = DateTime.Now;
                            break;
                    }
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// 访问记录
        /// </summary>
        /// <param name="user"></param>
        /// <param name="visitor"></param>
        public void VisitCount(User user, User visitor)
        {
            // 记录访问次数， 记录访问人物。
            using (var db = new PrivateDb())
            {
                var userhot = db.UserHots.SingleOrDefault(n => n.UserId == user.UserId);
                if (userhot == null) return;
               
                //为防止刷屏，最近两次访问要超过30分钟
                var visitlog = db.VisitLogs.SingleOrDefault(n => n.VisitorId == visitor.UserId && n.UserId == user.UserId);
                if (visitlog == null)
                {
                    var newlog = new VisitLog
                    {
                        UserId = user.UserId,
                        VisitorId = visitor.UserId,
                        VisitorName = visitor.UserName,
                        ActionTime = DateTime.Now,
                        IsRead = false,
                        Count = 1,
                    };
                    db.VisitLogs.Add(newlog);
                    Logger.Debug(visitor.UserName + "(Id:" + visitor.UserId + ") 访问了 " + user.UserName + "(Id:" + user.UserId + ")");
                }
                if ((visitlog != null && DiffMinute(visitlog.ActionTime, DateTime.Now) >= 30) )
                {
                    userhot.VistitCount += 1;
                    userhot.HotValue += 1;
                    visitlog.Count += 1;
                    visitlog.IsRead = false;//这时候相当于一个新的消息。
                    visitlog.ActionTime = DateTime.Now;
                  Logger.Debug(visitor.UserName+"(Id:"+visitor.UserId+") 访问了 "+user.UserName+"(Id:"+user.UserId+")");
                } 
                
                db.SaveChanges();
            }
        }

        public int IamgeCount(int userId)
        {
            using (var db = new PrivateDb())
            {
                return db.Iamgboxes.Count(n => n.UserId == userId) + 1;
            }
        }

        #region 用户热度相关  登录次数，打招呼，喜欢，赞
        /// <summary>
        /// 登陆处理 次数加1，并写入日志
        /// </summary>
        /// <param name="id"></param>
        public void LoginCountAdd(int id)
        {
            using (var db = new PrivateDb())
            {
                var user = db.Users.FirstOrDefault(n => n.UserId == id);
                if (user == null) return;
                var visitlog = db.LoginLogs.ToList().LastOrDefault(n => n.UserId==id);
                if ((visitlog != null && DiffMinute(visitlog.LoginTime, DateTime.Now) >= 600) || visitlog == null)
                {
                    var hot = db.UserHots.FirstOrDefault(n => n.UserId == id);
                    if (hot != null)
                    {
                        hot.LogCount += 1;
                        hot.HotValue += 1;
                    }

                    var userlog = new LoginLog
                    {
                        UserId = user.UserId,
                        LoginTime = DateTime.Now,
                        IpAddress = Dns.GetHostName(),
                        LogoutTime=DateTime.Now

                    };
                    db.LoginLogs.Add(userlog);
                    Logger.Debug(user.UserName + "(Id:" + user.UserId + ") 登录了");
                } 
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 打招呼加1 
        /// </summary>
        /// <param name="id"></param>
        public void HelloCountAdd(int id)
        {
            using (var db = new PrivateDb())
            {
                var hot = db.UserHots.FirstOrDefault(n => n.UserId == id);
                if (hot != null)
                {
                    hot.HelloCount += 1;
                    hot.HotValue += 1;
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 赞加一
        /// </summary>
        /// <param name="id"></param>
        public void PraiseCountAdd(int id)
        {
            using (var db = new PrivateDb())
            {
                var hot = db.UserHots.FirstOrDefault(n => n.UserId == id);
                if (hot != null)
                {
                    hot.PraiseCount += 1;
                    hot.HotValue += 1;
                }
                db.SaveChanges();
            }
        }

        public void LoveCountAdd(int id)
        {
            using (var db = new PrivateDb())
            {
                var hot = db.UserHots.FirstOrDefault(n => n.UserId == id);
                if (hot != null)
                {
                    hot.CollectCount += 1;
                    hot.HotValue += 1;
                }
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 修正状态的赞
        /// </summary>
        /// <param name="stateId"></param>
        /// <param name="stateType"></param>
        public void StateOrImagePraiseCountAdd(int stateId, StateType stateType)
        {
            using (var db = new PrivateDb())
            {
                if (stateType == StateType.Personal)
                {
                    var s = db.States.Find(stateId);
                    if (s != null)
                    {
                        s.PraiseCount++;
                    }
                }
                if (stateType==StateType.Image)
                {
                    var i = db.Iamgboxes.Find(stateId);
                    if (i != null)
                    {
                        i.PraiseCount++;
                    }
                }
                db.SaveChanges();
            }
        }

        #endregion


        /// <summary>
        /// 退出 换成logger
        /// </summary>
        /// <param name="userid"></param>
        public void Logoff(int userid)
        {
            using (var db = new PrivateDb())
            {
                var user = db.Users.SingleOrDefault(n => n.UserId == userid);
                if (user == null) return;
                var userlog =
                    db.LoginLogs.Where(n => n.UserId == user.UserId)
                    .OrderByDescending(n => n.Id)
                    .FirstOrDefault();
                if (userlog != null)
                {
                    userlog.LogoutTime = DateTime.Now;
                    //DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                }
                db.SaveChanges();
            }
        }



        #endregion

        #region 辅助方法

        /// <summary>
        /// 计算两个时间差
        /// </summary>
        /// <param name="beforeTime"></param>
        /// <param name="afterTime"></param>
        /// <returns>返回分钟数</returns>
        public double DiffMinute(DateTime beforeTime, DateTime afterTime)
        {
            TimeSpan timeSpan = afterTime - beforeTime;
            return timeSpan.TotalMinutes;
        }

        private static string Modifycount(string str, ref int i)
        {
            if (!string.IsNullOrEmpty(str) && str != "请选择"&& str != "选择城市")
            {
                i++;
                return str;
            }
            return null;
        }
        private float Modifycount(float str, ref int i)
        {
            if (str > 0)
            {
                i++;
                return str;
            }
            return 0;
        }

        /// <summary>
        /// 字典
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="dictionary"></param>
        public void Update<T>(int id, Dictionary<string, object> dictionary)
        {
            using (var PrivateDb = new PrivateDb())
            {
                object obj = null;
                switch (typeof(T).Name)
                {
                    case "User":
                        obj = PrivateDb.Users.SingleOrDefault(n => n.UserId == id);
                        break;
                    case "BaseInfo":
                        obj = PrivateDb.BaseInfos.SingleOrDefault(n => n.Id == id);
                        break;
                    case "Requirement":
                        obj = PrivateDb.Requirements.SingleOrDefault(n => n.Id == id);
                        break;
                    case "DetailInfo":
                        obj = PrivateDb.DetailInfos.SingleOrDefault(n => n.Id == id);
                        break;
                    case "InfoStatistic":
                        obj = PrivateDb.InfoStatistics.SingleOrDefault(n => n.Id == id);
                        break;
                    case "UserHot":
                        obj = PrivateDb.UserHots.SingleOrDefault(n => n.Id == id);
                        break;
                    case "Praise":
                        obj = PrivateDb.Praises.SingleOrDefault(n => n.Id == id);
                        break;
                    case "State":
                        obj = PrivateDb.States.SingleOrDefault(n => n.Id == id);
                        break;
                    case "Iamgbox":
                        obj = PrivateDb.Iamgboxes.SingleOrDefault(n => n.Id == id);
                        break;
                    case "Message":
                        obj = PrivateDb.Messages.SingleOrDefault(n => n.Id == id);
                        break;
                    case "LoginLog":
                        obj = PrivateDb.LoginLogs.SingleOrDefault(n => n.Id == id);
                        break;
                    case "Role":
                        obj = PrivateDb.Roles.SingleOrDefault(n => n.Id == id);
                        break;
                    case "Hello":
                        obj = PrivateDb.Hellos.SingleOrDefault(n => n.Id == id);
                        break;
                    case "RoleLog":
                        obj = PrivateDb.RoleLogs.SingleOrDefault(n => n.Id == id);
                        break;
                    case "Authority":
                        obj = PrivateDb.Authoritys.SingleOrDefault(n => n.Id == id);
                        break;
                    case "AdminStatistic":
                        obj = PrivateDb.AdminStatistics.SingleOrDefault(n => n.Id == id);
                        break;  
                    case "Topic":
                        obj = PrivateDb.Topics.SingleOrDefault(n => n.Id == id);
                        break;
                   
                }
                if (obj == null) return;
                foreach (var element in dictionary)
                {
                    ObjectCopier.CopyProperty(obj, element.Key, element.Value);
                }
                PrivateDb.SaveChanges();
            }

        }
       
        public static PrivateDbRepository GetInstance()
        {
            return _loveDb ?? (_loveDb = new PrivateDbRepository());
        }
        #endregion

       
    }

}