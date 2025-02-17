namespace CasinoGame
{
    public class Player
    {
        public string Name { get; set; }
        public int Bank { get; set; }

        public Player(string name, int bank)
        {
            Name = name;
            Bank = bank;
        }
    }
}
