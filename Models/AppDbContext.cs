﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AuthApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }

}
