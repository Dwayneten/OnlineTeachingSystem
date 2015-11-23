using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using OnlineTeachingSystem.Models;


namespace OnlineTeachingSystem.DataAccessLayer
{
    public class OTSDBDAL:DbContext
    {
        public DbSet<UserInfo> UserInfoList { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>().ToTable("UserInfo");
            base.OnModelCreating(modelBuilder);
        }
    }
}