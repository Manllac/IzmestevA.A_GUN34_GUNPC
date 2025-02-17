using System;
using System.Collections.Generic;
using System.Linq;

namespace CasinoGame
{
    public class Blackjack : CasinoGameBase
    {
        private Player _player;
        private List<Card> _deck;
        private int _bet;

        public Blackjack(Player player, int bet)
        {
            _player = player;
            _bet = bet;
            FactoryMethod(); 
        }

        public override void PlayGame()
        {
            int playerScore = _deck.Take(2).Sum(card => card.Value);
            int computerScore = _deck.Skip(2).Take(2).Sum(card => card.Value);

            Console.WriteLine($"Your score: {playerScore}");
            Console.WriteLine($"Computer's score: {computerScore}");

            if (playerScore > computerScore)
                OnWinInvoke();
            else if (playerScore < computerScore)
                OnLooseInvoke();
            else
                OnDrawInvoke();
        }

        protected override void FactoryMethod()
        {
            _deck = new List<Card>
            {
                new Card(Suit.Clubs, 10), new Card(Suit.Diamonds, 9), 
                new Card(Suit.Hearts, 6), new Card(Suit.Spades, 7)
            };
        }
    }
}
