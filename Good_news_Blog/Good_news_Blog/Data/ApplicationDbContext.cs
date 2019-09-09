﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Good_news_Blog.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<News> News { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
    }
}
