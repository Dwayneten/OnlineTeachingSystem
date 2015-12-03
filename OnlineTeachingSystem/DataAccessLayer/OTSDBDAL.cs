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
        public DbSet<Article> ArticleList { get; set; }
        public DbSet<Exam> Exam { get; set; }
        public DbSet<ExamList> ExamList { get; set; }
        public DbSet<Comment> CommentList { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>().ToTable("UserInfo");
            modelBuilder.Entity<Article>().ToTable("Article");
            modelBuilder.Entity<Exam>().ToTable("Exam");
            modelBuilder.Entity<ExamList>().ToTable("ExamList");
            modelBuilder.Entity<Comment>().ToTable("Comment");
            base.OnModelCreating(modelBuilder);
        }
    }
}