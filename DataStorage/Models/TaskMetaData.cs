using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspBlog.Models
{
    public class TaskMetaData
    {
        [Key]
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public Guid TaskUuid { get; set; }
       // [ForeignKey("Standard")]
       // public int PracaId { get; set; }
        public virtual Task task { get; set; }
    }
}
