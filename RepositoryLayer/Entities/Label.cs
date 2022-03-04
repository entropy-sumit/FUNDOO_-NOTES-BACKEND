using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class Label
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LabelId { get; set; }
        public string LabelName { get; set; }

        [ForeignKey("user")]
        public long UserId { get; set; }
        public virtual User user { get; set; }

        [ForeignKey("notes")]
        public long NotesId { get; set; }
        public virtual Notes notes { get; set; }
    }
}