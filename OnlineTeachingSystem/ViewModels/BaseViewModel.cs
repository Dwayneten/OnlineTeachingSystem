using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTeachingSystem.ViewModels
{
    public class BaseViewModel
    {
        public NavStatusViewModel NavStatusData { get; set; }
        public SideBarViewModel SideBarData { get; set; }
    }
}