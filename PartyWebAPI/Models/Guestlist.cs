using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartyWebAPI.Models
{
    public class GuestlistModel
    {
        public int PartyId { get; set; }
        public int PersonId { get; set; }
        public int DrinkId { get; set; }
    }
}
