using PartyWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PartyWebAPI.Services
{
    public interface IDrinksService
    {
        DataTable getAllDrinks();

        bool AddDrink(DrinkModel drink);

    }
}
