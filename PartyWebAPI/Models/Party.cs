using System;

namespace PartyWebAPI.Models
{
    public class PartyModel
    {
       public int PartyId { get; set; }
       public string PartyName { get; set; }
       public string Location { get; set; }
       public DateTime StartTime { get; set; }
    }
}
