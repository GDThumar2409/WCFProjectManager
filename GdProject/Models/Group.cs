using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GdProject.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string AdminId { get; set; }
    }
}
