namespace CasinoGame
{
    public struct Card
    {
        private readonly Suit _suit;
        private readonly CardValue _value;

        public Suit Suit { get { return _suit; } }
        public CardValue Value { get { return _value; } }

        public Card(Suit suit, CardValue value)
        {
            _suit = suit;
            _value = value;
        }

        public override string ToString()
        {
            return $"{_value} of {_suit}";
        }
    }

    public enum Suit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }

    public enum CardValue
    {
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 10,  
        Queen = 10, 
        King = 10,  
        Ace = 11   
    }
}