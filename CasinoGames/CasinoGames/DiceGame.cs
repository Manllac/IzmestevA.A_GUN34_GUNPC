using System;
using System.Collections.Generic;
using System.Linq;

namespace CasinoGame
{
    public class DiceGame : CasinoGameBase
    {
        private int _diceCount;
        private int _minValue;
        private int _maxValue;
        private List<Dice> _dice;

        public DiceGame(Player player, int bet, int diceCount = 2, int minValue = 1, int maxValue = 6) : base(player, bet)
        {
            _diceCount = diceCount;
            _minValue = minValue;
            _maxValue = maxValue;
            _dice = new List<Dice>();
            FactoryMethod();
        }

        protected override void FactoryMethod()
        {
            Random random = new Random();

            for (int i = 0; i < _diceCount; i++)
            {
                _dice.Add(new Dice(_minValue, _maxValue, random));
            }
        }

        public override void PlayGame()
        {
            int playerScore = _dice.Sum(d => d.Roll());
            int computerScore = _dice.Sum(d => d.Roll());

            Console.WriteLine($"Your dice sum: {playerScore}");
            Console.WriteLine($"Computer's dice sum: {computerScore}");

            if (playerScore > computerScore)
            {
                OnWinInvoke();
            }
            else if (playerScore < computerScore)
            {
                OnLooseInvoke();
            }
            else
            {
                OnDrawInvoke();
            }
        }
    }
}