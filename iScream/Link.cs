namespace iScream
{
    public class Link
    {
        private int user_id;
        public int User_id
        {
            get { return user_id; }
            set { user_id = value; }
        }

        private int game_id;
        public int Game_id
        {
            get { return game_id; }
            set { game_id = value; }
        }

        public Link()
        {
            user_id = new int();
            game_id = new int();
        }

        public Link(int user_id, int game_id)
        {
            this.user_id = user_id;
            this.game_id = game_id;
        }
    }
}
