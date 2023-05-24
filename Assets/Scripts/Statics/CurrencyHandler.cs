using System;

namespace Statics
{
    public static class CurrencyHandler
    {
        private static int _currentMoney;

        public static int CurrentMoney => _currentMoney;
        public static Action<bool> onValueChanged;

        public static void Initialize()
        {
            _currentMoney = Prefs.Money;
        }

        public static void IncreaseMoney(int value)
        {
            _currentMoney += value;
            SaveMoney();
            onValueChanged?.Invoke(true);
        }

        private static void SaveMoney()
        {
            Prefs.Money = _currentMoney;
        }

        public static void DecreaseMoney(int value)
        {
            _currentMoney -= value;
            onValueChanged?.Invoke(false);
        }

        public static bool CanDecrease(int value)
        {
            return _currentMoney - value >= 0;
        }
    }
}