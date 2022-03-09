//-----------------------------------------------------------------------
// <copyright file="Collaborator.cs" company="Aniket">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepositoryLayer.Entity
{
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Collaborator Entity
    /// </summary>
    public class Collaborator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        /// <summary>Gets or sets the COLLAB identifier.</summary>
        /// <value>The COLLAB identifier.</value>
        public long CollabId { get; set; }

        /// <summary>Gets or sets the COLLAB email.</summary>
        /// <value>The COLLAB email.</value>
        public string CollabEmail { get; set; }

        [ForeignKey("user")]

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public long Id { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        public User user { get; set; }

        [ForeignKey("notes")]

        /// <summary>Gets or sets the notes identifier.</summary>
        /// <value>The notes identifier.</value>
        public long NotesId { get; set; }

        /// <summary>Gets or sets the notes.</summary>
        /// <value>The notes.</value>
        public Notes notes { get; set; }
    }
}
