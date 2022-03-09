using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Entity
{
   public class Labels
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public long LabelId { get; set; }

        public string LabelName { get; set; }

        [ForeignKey("user")]
        public long Id { get; set; }

        public User user { get; set; }

        [ForeignKey("notes")]
        public long NotesId { get; set; }

        public Notes notes { get; set; }
    }
}
