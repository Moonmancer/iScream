using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    public class Container
    {
        public List<User> Users;

        public List<Game> Games;

        public Container(List<User> users, List<Game> games)
        {
            this.Users = users;

            this.Games = games;
        }
    }
}
