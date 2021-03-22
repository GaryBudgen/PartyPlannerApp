using PartyWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PartyWebAPI.Services
{
    public interface IPartyService
    {
        DataTable getAllParties();
        DataTable getPartiesByPerson(int PersonId);
        bool AddParty(PartyModel party);
        bool AddPersonToParty(GuestlistModel guestlist);
    }
}
