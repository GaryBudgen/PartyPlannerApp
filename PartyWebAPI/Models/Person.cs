using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartyWebAPI.Models
{
    public class PersonModel
    {
        public int? PersonId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
