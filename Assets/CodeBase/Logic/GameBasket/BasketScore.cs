using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace CodeBase.Logic.GameBasket
{
    public class BasketScore : MonoBehaviour
    {
        private int _incrementAmount;
        private int _decrementAmount;

        private int _currentScore;

        public event UnityAction<int> ScoreChanged;

        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Initialize(int incrementAmount, int decrementAmount)
        {
            _incrementAmount = incrementAmount;
            _decrementAmount = decrementAmount;
        }

        public void Increment()
        {
            _currentScore += _incrementAmount;
            ScoreChanged?.Invoke(_currentScore);
            TryUpdateMaxScore();
        }

        public void Decrement()
        {
            _currentScore = Mathf.Max(0, _currentScore - _decrementAmount);
            ScoreChanged?.Invoke(_currentScore);
        }

        private void TryUpdateMaxScore()
        {
            if (_currentScore > _progressService.Progress.MaxScore)
            {
                _progressService.Progress.MaxScore = _currentScore;
                _saveLoadService.SaveProgress();
            }
        }
    }
}