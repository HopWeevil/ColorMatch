using CodeBase.Logic;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Elements
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private Gradient _textGradient;
        [SerializeField] private Timer _timer;

        private float _totalTime;

        private void OnEnable()
        {
            _timer.RemainingTimeChanged += OnRemainingTimeChange;
            _timer.Started += OnTimerStarted;
        }

        private void OnDisable()
        {
            _timer.RemainingTimeChanged -= OnRemainingTimeChange;
            _timer.Started -= OnTimerStarted;
        }

        public void UpdateTimerView(float remainingTime)
        {
            UpdateTimerText(remainingTime);
            UpdateTextColor(remainingTime);
        }

        private void UpdateTimerText(float remainingTime)
        {
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        private void UpdateTextColor(float remainingTime)
        {
            float timeFraction = Mathf.Clamp01(1 - (remainingTime / _totalTime));
            _timerText.color = _textGradient.Evaluate(timeFraction);
        }

        private void OnRemainingTimeChange(float remainingTime)
        {
            UpdateTimerView(remainingTime);
        }

        private void OnTimerStarted(float totalTime)
        {
            _totalTime = totalTime;
        }
    }
}