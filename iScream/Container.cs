﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    public class Container
    {
        List<User> Users;

        List<Game> Games;

        public Container(List<User> users, List<Game> games)
        {
            this.Users = users;

            this.Games = games;
        }

        public Game Game
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public User User
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
