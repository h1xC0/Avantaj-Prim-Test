using System;
using BaseInfrastructure;
using TMPro;
using UnityEngine;

namespace MainComponents.GameplayUI
{
    public class LevelDataView : BaseView, ILevelDataView
    {
        [SerializeField] private TMP_Text playerCurrency;
        [SerializeField] private TMP_Text customersCount;
        [SerializeField] private TMP_Text levelTimer;

        public void SetCustomersCount(int value) => SetText(customersCount, value);
        public void SetPlayerCurrency(int value) => SetText(playerCurrency, value);
        public void SetLevelTimer(TimeSpan timeSpan) => levelTimer.text = timeSpan.ToString("mm':'ss");

        private void SetText(TMP_Text tmp, int value) => tmp.text = value.ToString();
    }
}