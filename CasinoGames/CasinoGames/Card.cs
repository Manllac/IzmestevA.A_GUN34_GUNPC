namespace CasinoGame
{
    public class Card
    {
        public Suit Suit { get; set; }
        public int Value { get; set; }

        public Card(Suit suit, int value)
        {
            Suit = suit;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Value} of {Suit}";
        }
    }

    // Перечисление мастей
    public enum Suit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }
}

