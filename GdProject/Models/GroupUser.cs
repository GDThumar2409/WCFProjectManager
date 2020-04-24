using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GdProject.Models
{
    public class GroupUser
    {
        public string UserId { get; set; }
        public int GroupId { get; set; }
    }
}
