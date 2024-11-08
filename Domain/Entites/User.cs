using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public partial class User
    {
        public int UserID { get; set; }
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }

    }
}
