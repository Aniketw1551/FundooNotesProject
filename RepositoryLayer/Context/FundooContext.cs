using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{
    //DbContext is used to communicate with Database
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions options)
           : base(options)
        {
        }
        //DbSet is used to view Database and interact with Table/s in Database
        public DbSet<User> UserTable { get; set; }
        public DbSet<Notes> NotesTable { get; set; }
        public DbSet<Collaborator> CollabTable { get; set; }
    }
}