using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTeachingSystem.ViewModels
{
    public class SignUpViewModel
    {
        public string NickName { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public string ErrorMessage { get; set; }
    }
}