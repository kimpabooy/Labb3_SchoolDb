using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3_SchoolDb.Models
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }
        public string Name { get; set; }
        public ICollection Staffs { get; set; }
        public ICollection Students { get; set; }

        [ForeignKey("StaffId")]
        public int StaffId { get; set; }
        public Staff Staff { get; set; }

    }
}
