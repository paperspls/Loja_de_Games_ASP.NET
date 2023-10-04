namespace LojaGames.Security
{
    public class Settings
    {
        private static string secret = "46d200be485697ce0565155314a3eefd371087d7922da5b5054a411dbc9cde48";

        public static string Secret { get => secret; set => secret = value; }
    }
}