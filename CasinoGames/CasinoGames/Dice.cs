using System;

namespace CasinoGame
{
    public class Dice
    {
        private Random _random;
        private int _minValue;
        private int _maxValue;

        public Dice(int minValue, int maxValue, Random random)
        {
            if (minValue < 1 || maxValue > int.MaxValue || minValue > maxValue)
                throw new ArgumentOutOfRangeException(nameof(minValue), "Invalid dice range.");

            _minValue = minValue;
            _maxValue = maxValue;
            _random = random;
        }

        public int Roll()
        {
            return _random.Next(_minValue, _maxValue + 1);
        }
    }
}
