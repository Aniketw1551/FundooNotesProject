//-----------------------------------------------------------------------
// <copyright file="FundooContext.cs" company="Aniket">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepositoryLayer.Context
{
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;

    ////DbContext is used to communicate with Database
    
    /// <summary>
    /// FundooContext 
    /// </summary>
    public class FundooContext : DbContext
    {
        /// <summary>Initializes a new instance of the <see cref="FundooContext" /> class.</summary>
        /// <param name="options">The options for this context.</param>
        public FundooContext(DbContextOptions options)
           : base(options)
        { 
        }
        ////DbSet is used to view Database and interact with Table/s in Database
        
        /// <summary>Gets or sets the user table.</summary>
        /// <value>The user table.</value>
        public DbSet<User> UserTable { get; set; }

        /// <summary>Gets or sets the notes table.</summary>
        /// <value>The notes table.</value>
        public DbSet<Notes> NotesTable { get; set; }

        /// <summary>Gets or sets the collaborator table.</summary>
        /// <value>The collaborator table.</value>
        public DbSet<Collaborator> CollabTable { get; set; }

        /// <summary>Gets or sets the label table.</summary>
        /// <value>The label table.</value>
        public DbSet<Labels> LabelTable { get; set; }
    }
}