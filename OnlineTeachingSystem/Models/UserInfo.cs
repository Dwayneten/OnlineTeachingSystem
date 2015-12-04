using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineTeachingSystem.Models
{
    public class UserInfo
    {
        [Key]
        public int UserInfoID { get; set; }
        public string NickName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public int Grade { set; get; }
    }
}