using System;
using UnityEngine;

namespace Statics
{
    public static class CurrencyHandler
    {
        private static int _currentMoney;
        private static int _thisLevelCollected;

        public static int CurrentMoney => _currentMoney;
        public static int ThisLevelCollected => _thisLevelCollected;
        public static Action<int, Vector3, bool> onValueChanged;

        public static void Initialize()
        {
            _currentMoney = Prefs.Money;
        }

        public static void IncreaseMoney(int value, Vector3 position, bool haveFadingText)
        {
            _thisLevelCollected += value;
            _currentMoney += value;
            SaveMoney();
            onValueChanged?.Invoke(value, position, haveFadingText);
        }

        private static void SaveMoney()
        {
            Prefs.Money = _currentMoney;
        }

        public static void DecreaseMoney(int value)
        {
            _currentMoney -= value;
            SaveMoney();
            onValueChanged?.Invoke(value, Vector3.zero, false);
        }

        public static bool CanDecrease(int value)
        {
            return _currentMoney - value >= 0;
        }

        public static void ResetData()
        {
            _thisLevelCollected = 0;
        }
    }
}