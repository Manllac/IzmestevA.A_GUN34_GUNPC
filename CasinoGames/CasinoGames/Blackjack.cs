using System;
using System.Collections.Generic;
using System.Linq;

namespace CasinoGame
{
    public class Blackjack : CasinoGameBase
    {
        private Queue<Card> _deck;

        public Blackjack(Player player, int bet) : base(player, bet)
        {
            FactoryMethod();
        }

        public override void PlayGame()
        {
            List<Card> playerCards = new List<Card> { _deck.Dequeue(), _deck.Dequeue() };
            List<Card> computerCards = new List<Card> { _deck.Dequeue(), _deck.Dequeue() };

            int playerScore = playerCards.Sum(card => (int)card.Value); 
            int computerScore = computerCards.Sum(card => (int)card.Value); 

            Console.WriteLine($"Your score: {playerScore} (Cards: {string.Join(", ", playerCards.Select(c => c.ToString()))})");
            Console.WriteLine($"Computer's score: {computerScore} (Cards: {string.Join(", ", computerCards.Select(c => c.ToString()))})");

            if (playerScore == 21 && computerScore == 21)
            {
                OnDrawInvoke();
            }
            else if (playerScore == 21 || computerScore > 21 || playerScore > computerScore)
            {
                OnWinInvoke();
            }
            else if (computerScore == 21 || playerScore > 21 || computerScore > playerScore)
            {
                OnLooseInvoke();
            }
            else
            {
                while (playerScore <= 21 && computerScore <= 21 && playerScore == computerScore)
                {
                    playerCards.Add(_deck.Dequeue());
                    computerCards.Add(_deck.Dequeue());

                    playerScore = playerCards.Sum(card => (int)card.Value); 
                    computerScore = computerCards.Sum(card => (int)card.Value);

                    Console.WriteLine($"Your score: {playerScore} (Cards: {string.Join(", ", playerCards.Select(c => c.ToString()))})");
                    Console.WriteLine($"Computer's score: {computerScore} (Cards: {string.Join(", ", computerCards.Select(c => c.ToString()))})");
                }

                if (playerScore > 21 || (computerScore <= 21 && computerScore > playerScore))
                {
                    OnLooseInvoke();
                }
                else if (computerScore > 21 || playerScore > computerScore)
                {
                    OnWinInvoke();
                }
                else
                {
                    OnDrawInvoke();
                }
            }
        }
        protected override void FactoryMethod()
        {
            _deck = new Queue<Card>();

            List<Suit> suits = new List<Suit>
    {
        Suit.Clubs,
        Suit.Diamonds,
        Suit.Hearts,
        Suit.Spades
    };
            List<CardValue> values = Enum.GetValues(typeof(CardValue)).Cast<CardValue>().ToList(); 

            Random random = new Random();

            foreach (var suit in suits)
            {
                foreach (var value in values)
                {
                    _deck.Enqueue(new Card(suit, value));
                }
            }

            Shuffle();
        }

        private void Shuffle()
        {
            Random rand = new Random();
            var shuffledDeck = _deck.OrderBy(c => rand.Next()).ToList();
            _deck = new Queue<Card>(shuffledDeck);
        }
    }
}