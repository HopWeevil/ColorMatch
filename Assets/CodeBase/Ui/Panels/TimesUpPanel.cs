using CodeBase.Infrastructure.States;
using CodeBase.Services.StaticData;
using UnityEngine.SceneManagement;
using CodeBase.SO;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Panels
{
    public class TimesUpPanel : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        private IGameStateMachine _gameStateMachine;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(IGameStateMachine gameStateMachine, IStaticDataService staticData)
        {
            _gameStateMachine = gameStateMachine;
            _staticDataService = staticData;
        }

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClick);
            _exitButton.onClick.AddListener(OnExitButtonClick);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClick);
            _exitButton.onClick.RemoveListener(OnExitButtonClick);
        }

        private void Start()
        {
            Time.timeScale = 0;
        }
        private void OnDestroy()
        {
            Time.timeScale = 1;
        }

        private void OnRestartButtonClick()
        {
            LevelStaticData data = _staticDataService.ForLevel(SceneManager.GetActiveScene().name);
            _gameStateMachine.Enter<LoadLevelState, LevelStaticData>(data);
        }

        private void OnExitButtonClick()
        {
            _gameStateMachine.Enter<MainMenuState>();
        }
    }
}