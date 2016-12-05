using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    class SortAscending : ISort
    {

        List<User> users = new List<User>();
        List<Game> games = new List<Game>();
        Container sort(Container unsorted)
        {
            return new Container(users, games);
        }
    }
}
