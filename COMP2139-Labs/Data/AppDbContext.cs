﻿using Microsoft.EntityFrameworkCore;
using COMP2139_Labs.Models;

namespace COMP2139_Labs.Data {
    public class AppDbContext : DbContext {
        public DbSet<Project> Projects { get; set; }
        //Add dbset for other entities like tasks in the future
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
            
        }
    }
}