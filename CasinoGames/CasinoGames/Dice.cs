using System;

namespace CasinoGame
{
    public struct Dice
    {
        private readonly int _number;
        private readonly int _min;
        private readonly int _max;

        public int Number { get { return _number; } }
        public int Min { get { return _min; } }
        public int Max { get { return _max; } }

        public Dice(int min, int max)
        {
            if (min < 1 || max > int.MaxValue || min > max)
            {
                throw new WrongDiceNumberException("Invalid dice range. Min must be >= 1, Max must be <= int.MaxValue, and Min must be <= Max.");
            }

            _min = min;
            _max = max;
            Random random = new Random();
            _number = random.Next(min, max + 1); 
        }

        public int Roll()
        {
            return _number;
        }
    }
}