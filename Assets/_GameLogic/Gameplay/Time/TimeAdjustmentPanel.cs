using System;
using _GameLogic.Extensions.Patterns;
using Scellecs.Morpeh;
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
        [SerializeField] private TextMeshProUGUI _modeText;
        
        private void OnEnable()
        {
            _playButton.onClick.AddListener(OnPlayPauseButtonClicked);
            _pauseButton.onClick.AddListener(OnPlayPauseButtonClicked);
            _timeAccelerationButton.onClick.AddListener(OnTimeAccelerationButtonClicked);
            _timeDecelerationButton.onClick.AddListener(OnTimeDecelerationButtonClicked);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(OnPlayPauseButtonClicked);
            _pauseButton.onClick.RemoveListener(OnPlayPauseButtonClicked);
            _timeAccelerationButton.onClick.RemoveListener(OnTimeAccelerationButtonClicked);
            _timeDecelerationButton.onClick.RemoveListener(OnTimeDecelerationButtonClicked);
        }
        
        public void SetDate(DateTime dateTime)
        {
            _dateText.SetText("{0:0}.{1:0}.{2:0}", dateTime.Year, dateTime.Month, dateTime.Day);
        }

        public void SetTimeLapseMode(TimeLapseMode mode)
        {
            _modeText.SetText(mode.ToString());
        }

        public void SetTimeState(bool isPaused)
        {
            _playButton.gameObject.SetActive(isPaused);
            _pauseButton.gameObject.SetActive(!isPaused);
        }
        
        private void OnTimeAccelerationButtonClicked()
        {
            var clickEvent = World.Default.GetEvent<TimeAccelerationButtonClickEvent>();
            clickEvent.NextFrame(new TimeAccelerationButtonClickEvent());
        }
        
        private void OnTimeDecelerationButtonClicked()
        {
            var clickEvent = World.Default.GetEvent<TimeDecelerationButtonClickEvent>();
            clickEvent.NextFrame(new TimeDecelerationButtonClickEvent());
        }

        private void OnPlayPauseButtonClicked()
        {
            var clickEvent = World.Default.GetEvent<TimeStateSwitchingButtonClickEvent>();
            clickEvent.NextFrame(new TimeStateSwitchingButtonClickEvent());
        }
    }
}