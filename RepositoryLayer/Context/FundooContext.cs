using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<User> UserTables { get; set; }

        public DbSet<Notes> Notes { get; set; }
        public DbSet<Collaborator> CollabTable { get; set; }
    }
}
