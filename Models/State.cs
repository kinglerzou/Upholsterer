﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Upholsterer.Models
{
    /// <summary>
    /// 个人状态
    /// </summary>
    public class State
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        /// <summary>
        /// 状态类型
        /// </summary>
        [MaxLength(20)]
        public string StateType { get; set; }
        [MaxLength(400)]
        public string Content { get; set; }
        public int PraiseCount { get; set; }
        public DateTime ActionTime { get; set; }
    }
}