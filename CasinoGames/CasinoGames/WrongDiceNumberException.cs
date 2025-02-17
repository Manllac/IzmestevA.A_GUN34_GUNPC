using System;

namespace CasinoGame
{
    public class WrongDiceNumberException : Exception
    {
        public WrongDiceNumberException(string message) : base(message) { }
    }
}
