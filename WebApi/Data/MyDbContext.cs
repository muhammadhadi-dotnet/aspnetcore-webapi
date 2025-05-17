﻿using Microsoft.EntityFrameworkCore;
using WebApi.Model;

namespace WebApi.Data
{
    public class MyDbContext : DbContext
    {
    
        public MyDbContext(DbContextOptions<MyDbContext> options): base(options)
        {

            
        }
        public DbSet<Product> Products { get; set; } = null!;
    }



    

}

