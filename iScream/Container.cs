using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    class Container
    {
        List<User> Users;

        List<Game> Games;

        Container(List<User> users, List<Game> games)
        {
            this.Users = users;

            this.Games = games;
        }
    }
}
