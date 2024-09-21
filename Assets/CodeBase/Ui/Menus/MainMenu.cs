using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Infrastructure.States;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.UI.Elements;
using CodeBase.SO;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Menus
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private GameDifficultyToggle _difficultyToggle;

        private IStaticDataService _staticData;
        private IGameStateMachine _gameStateMachine;
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IStaticDataService staticData, IGameStateMachine stateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoad)
        {
            _staticData = staticData;
            _gameStateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoad;
        }

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(ExitGame);
            _playButton.onClick.AddListener(StartGame);
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(ExitGame);
            _playButton.onClick.RemoveListener(StartGame);
        }

        private void StartGame()
        {
            //A blank for selecting levels;
            LevelStaticData data = _staticData.ForLevel("Level1");
            _gameStateMachine.Enter<LoadLevelState, LevelStaticData>(data);

            _progressService.Progress.LastPlayerDifficulty = _difficultyToggle.GameDifficulty;
            _saveLoadService.SaveProgress();
        }

        private void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}