namespace CasinoGame
{
    public struct Card
    {
        public readonly Suit Suit;
        public readonly int Value;

        public Card(Suit suit, int value)
        {
            Suit = suit;
            Value = value;
        }
    }

    public enum Suit
    {
        Clubs, Diamonds, Hearts, Spades
    }
}
