using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AspBlog.Models
{
    public class Task
    {

        [Key]
        public Guid Uuid { get; set; } 
        public string NameOfTask { get; set; }
        public DateTime DateOfInsertion { get; set; }
        public virtual ICollection<TaskMetaData> Task_MetaData { get; set; }
        // [ForeignKey("PracaTagy")]
        // public virtual List<int> TagyIds { get; set; }
        //public ICollection<PracaTag> pracaTags { get; set; }

        public Task()
        {
          Task_MetaData = new List<TaskMetaData>();
            //pracaTags.Add();
        }

    }
}
