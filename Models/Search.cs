using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Upholsterer.Models
{
    public class Search
    {

        public int Id { get; set; }

        public int UserId { get; set; }

        /// <summary>
        ///所在地省份
        /// </summary>
        [MaxLength(20)]
        [Display(Name = "所在地省份")]
        public string ProvBelong { get; set; }

        /// <summary>
        /// 所在地城市
        /// </summary>
        [MaxLength(20)]
        [Display(Name="所在地城市")]
        public string CityBelong { get; set; }


        /// <summary>
        /// age
        /// </summary>
        [Display(Name = "年龄")]
        public int AgeUp { get; set; }

        public int AgeLow { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public string Education { get; set; }

        //是否是及以上 学历
        //public int EduUp { get; set; }

        [Display(Name = "月收入")]
        public string MonthlyIncome { get; set; }

        /// <summary>
        /// 住房情况
        /// </summary>
        [MaxLength(10)]
        [Display(Name = "住房情况")]
        public string Housing { get; set; }

        [MaxLength(10)]
        [Display(Name = "购车情况")]
        public string Car { get; set; }

        [Display(Name = "身高")]
        public int HightUp { get; set; }

        public int HightLow { get; set; }

        /// <summary>
        /// 星座
        /// </summary>
        [MaxLength(10)]
        [Display(Name = "星座")]
        public string Constellation { get; set; }


        /// 民族
        [MaxLength(10)]
        [Display(Name = "民族")]
        public string People { get; set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        [MaxLength(20)]
        [Display(Name = "当前状态")]
        public string State { get; set; }

    }
}