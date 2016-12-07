﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    class Fachkonzept1 : IFachkonzept
    {
        private bool sortDescending
        {
            get
            {
                return sortDescending;
            }
            set
            {
                Console.WriteLine("Fachkonzept1 unterstützt keine sortierung.");
            }
        }
        // unsortiert
        IDatenhaltung datenhaltung;
        public Fachkonzept1(IDatenhaltung datenhaltung)
        {
            this.datenhaltung = datenhaltung;
        }

        #region Details

        public Container detailsUser(User user)
        {
            Container details;
            List<User> users = new List<User> { user };
            List<Game> games = new List<Game>();

            games = datenhaltung.GetGamesOfUser(user);

            details = new Container(users, games);

            return details;
        }
        public Container detailsGame(Game game)
        {
            Container details;
            List<User> users = new List<User>();
            List<Game> games = new List<Game> { game };

            users = datenhaltung.GetUserOfGame(game);

            details = new Container(users, games);

            return details;
        }

        #endregion

        #region Create

        public bool createUser(User user)
        {
            return datenhaltung.AddUser(user);
        }
        public bool createGame(Game game)
        {
            return datenhaltung.AddGame(game);
        }

        #endregion

        #region Delete

        public bool deleteUser(User user)
        {
            return datenhaltung.DeleteUser(user);
        }

        public bool deleteUser(int user_id)
        {
            return datenhaltung.DeleteUser(user_id);
        }

        public bool deleteGame(Game game)
        {
            return datenhaltung.DeleteGame(game);
        }
        public bool deleteGame(int game_id)
        {
            return datenhaltung.DeleteGame(game_id);
        }

        #endregion

        #region Search

        public List<Game> searchGame(String name)
        {
            return datenhaltung.SearchGame(name);
        }
        public List<User> searchUser(String firstname, String lastname)
        {
            return datenhaltung.SearchUser(firstname, lastname);
        }

        #endregion
    }
}
