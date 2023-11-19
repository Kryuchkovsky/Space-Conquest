using System;
using System.Globalization;
using _GameLogic.Extensions.Patterns;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GameLogic.Gameplay.Time
{
    public class TimeAdjustmentPanel : Singleton<TimeAdjustmentPanel>
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _timeAccelerationButton;
        [SerializeField] private Button _timeDecelerationButton;
        [SerializeField] private TextMeshProUGUI _dateText;
        
        private TimeLapseMode[] _timeLapseModes;
        //private int _


        public void Initialize(TimeLapseMode[] timeLapseModes)
        {
            _timeLapseModes = timeLapseModes;
        }

        //TODO check allocations
        public void SetDate(DateTime dateTime)
        {
            _dateText.SetText(dateTime.ToString(CultureInfo.InstalledUICulture));
        }
    }
}