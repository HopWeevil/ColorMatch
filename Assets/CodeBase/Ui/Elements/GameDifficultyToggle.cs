using CodeBase.Services.StaticData;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System.Collections.Generic;
using CodeBase.SO;
using CodeBase.Enums;
using CodeBase.Services.PersistentProgress;

namespace CodeBase.UI.Elements
{
    public class GameDifficultyToggle : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _buttonImage;

        private int _currentDifficultyIndex = 0;
        private List<GameDifficultyStaticData> _gameDifficulties;

        private IStaticDataService _staticData;
        private IPersistentProgressService _progressService;

        public GameDifficultyId GameDifficulty => _gameDifficulties[_currentDifficultyIndex].Id;

        [Inject]
        private void Construct(IStaticDataService staticData, IPersistentProgressService progressService)
        {
            _staticData = staticData;
            _progressService = progressService;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(ChangeDifficultyOnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ChangeDifficultyOnClick);
        }

        private void Start()
        {
            _gameDifficulties = _staticData.GetGameDifficulties();

            GameDifficultyId savedDifficultyId = _progressService.Progress.LastPlayerDifficulty;
            _currentDifficultyIndex = _gameDifficulties.FindIndex(difficulty => difficulty.Id == savedDifficultyId);
            _buttonImage.sprite = _gameDifficulties[_currentDifficultyIndex].Icon;

        }

        private void ChangeDifficultyOnClick()
        {
            _currentDifficultyIndex = (_currentDifficultyIndex + 1) % _gameDifficulties.Count;

            _buttonImage.sprite = _gameDifficulties[_currentDifficultyIndex].Icon;
        }
    }
}