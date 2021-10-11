
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities.Identity
{
    public class User : BaseEntity
    {
        public string DisplayName { get; set; }
        public string Email{ get; set; }
        public ICollection<Adress> Adress { get; set; }
        public Authority Authority { get; set; }        
    }
}
