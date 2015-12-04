using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTeachingSystem.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public string NickName { get; set; }
        public string Mail { get; set; }
        public bool IsAdmin { get; set; }
        public string Message { get; set; }
        public string AlertType { get; set; }
    }
}