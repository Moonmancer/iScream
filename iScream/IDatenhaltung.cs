using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iScream
{
    interface IDatenhaltung
    {
        #region Get
        int GetNextUser_id();
        int GetNextGame_id();

        User GetUser(int user_id);
        List<User> GetUser();

        Game GetGame(int game_id);
        Game GetGame(string name);
        List<Game> GetGame();

        Link GetLink(int user_id, int game_id);
        Link GetLink(Link link);
        List<Link> GetLink();

        List<Game> GetGamesOfUser(int user_id);
        List<Game> GetGamesOfUser(User user);

        List<User> GetUserOfGame(int game_id);
        List<User> GetUserOfGame(Game game);
        #endregion

        #region Add
        bool AddUser(string firstname, string lastname, int user_id);
        bool AddUser(string firstname, string lastname);
        bool AddUser(User user);
        void AddUser(List<User> user);

        bool AddGame(string name, int game_id);
        bool AddGame(string name);
        bool AddGame(Game game);
        void AddGame(List<Game> games);

        bool AddLink(int user_id, int game_id);
        bool AddLink(Link link);
        void AddLink(List<Link> links);
        #endregion

        #region Delete
        bool DeleteUser(int user_id);
        bool DeleteUser(User user);
        void DeleteUser(List<User> user);

        bool DeleteGame(int game_id);
        bool DeleteGame(Game game);
        void DeleteGame(List<Game> games);

        bool DeleteLink(int user_id, int game_id);
        bool DeleteLink(Link link);
        #endregion

        #region Update
        bool UpdateUser(int user_id, string firstname, string lastname);
        bool UpdateUser(User user);

        bool UpdateGame(int game_id, string name);
        bool UpdateGame(Game game);
        #endregion

        #region Search
        List<User> SearchUser(string firstname, string lastname);
        List<Game> SearchGame(string name);
        #endregion
    }
}