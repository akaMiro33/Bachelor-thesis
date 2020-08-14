using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspBlog.Models
{
    public class ApiKey
    {
        [Key]
        public int Id { get; set; }
        public string Key { get; set; }
        public string Note { get; set; }
        public DateTime ExpirationDate { get; set; }
        // [ForeignKey("Standard")]
        // public int PracaId { get; set; }

    }
}
