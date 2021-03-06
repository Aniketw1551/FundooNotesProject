//-----------------------------------------------------------------------
// <copyright file="Labels.cs" company="Aniket">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepositoryLayer.Entity
{
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Labels Entity
    /// </summary>
    public class Labels
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        /// <summary>Gets or sets the label identifier.</summary>
        /// <value>The label identifier.</value>
        public long LabelId { get; set; }

        /// <summary>Gets or sets the name of the label.</summary>
        /// <value>The name of the label.</value>
        public string LabelName { get; set; }

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
