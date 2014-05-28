using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Upholsterer.ViewModel
{
    public class ModifyPassword
    {
        [Required]
        [Display(Name="原始密码")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Required]
        [Display(Name="新密码")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [Display(Name="确认新密码")]
        [System.Web.Mvc.Compare("NewPassword",ErrorMessage="两次密码输入不一致")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}