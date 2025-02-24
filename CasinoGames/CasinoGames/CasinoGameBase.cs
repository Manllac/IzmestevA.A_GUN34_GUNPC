using System;

namespace CasinoGame
{
    public abstract class CasinoGameBase
    {
        public event Action OnWin;
        public event Action OnLoose;
        public event Action OnDraw;
        protected Player _player;
        protected int _bet;

        public CasinoGameBase(Player player, int bet)
        {
            _player = player;
            _bet = bet;

            if (_bet > _player.Bank)
            {
                throw new InvalidOperationException("Bet cannot exceed player's bank.");
            }
        }

        public abstract void PlayGame();
        protected void OnWinInvoke()
        {
            _player.Bank += _bet;
            OnWin?.Invoke();
            CheckBankLimits();
        }

        protected void OnLooseInvoke()
        {
            _player.Bank -= _bet;
            OnLoose?.Invoke();
            CheckBankLimits();
        }

        protected void OnDrawInvoke()
        {
            OnDraw?.Invoke();
            CheckBankLimits();
        }

        private void CheckBankLimits()
        {
            if (_player.Bank <= 0)
            {
                Console.WriteLine("No money? Kicked!");
                throw new InvalidOperationException("Player has no money left.");
            }
        }

        protected abstract void FactoryMethod();
    }
}