using UnityEngine;
using System;
using UnityEngine.Events;
using Zenject;
using CodeBase.Infrastructure.Factories;

namespace CodeBase.Logic
{
    public class Timer : MonoBehaviour
    {
        private bool _isRunning;
        private float _timeRemaining;
        private IUIFactory _factory;

        public UnityAction<float> RemainingTimeChanged;
        public UnityAction<float> Started;

        [Inject]
        private void Construct(IUIFactory factory)
        {
            _factory = factory;
        }

        public void Begin(float countdownTime)
        {
            _timeRemaining = countdownTime;
            Started?.Invoke(countdownTime);
            _isRunning = true;
        }

        private void Update()
        {
            if (_isRunning)
            {
                UpdateTimer();
            }
        }

        private void UpdateTimer()
        {
            if (_timeRemaining > 0)
            {
                CalculateRemainingTime();
            }
            else
            {
                _isRunning = false;
                _factory.CreateTimesUpPanel();
            }

            RemainingTimeChanged?.Invoke(_timeRemaining);
        }

        private void CalculateRemainingTime()
        {
            _timeRemaining -= Time.deltaTime;
            _timeRemaining = Mathf.Max(_timeRemaining, 0);
        }
    }
}