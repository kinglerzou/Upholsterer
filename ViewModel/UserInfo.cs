using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Upholsterer.Models;

namespace Upholsterer.ViewModel
{
    /// <summary>
    /// 个人主页 资料大整合
    /// </summary>
    public class UserInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string ImgUrl { get; set; }
        public BaseInfo BaseInfo { get; set; }
        public DetailInfo DetailInfo { get; set; }
        public Requirement Requirement { get; set; }
        public string LastState { get; set; }// 最后一条状态
        public int Enable { get; set; }
    }

    public class AdminViewModel
    {
        public List<User> UncheckedUsers { get; set; }//管理用户
        public Admin Admin { get; set; }//操作者本人
    }

    public class Admin
    {
        public Role Role { get; set; }
        public string UserName { get; set; }
        public string ImgUrl { get; set; }
    }
}