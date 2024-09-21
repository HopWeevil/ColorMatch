using CodeBase.Data;
using CodeBase.Enums;
using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.Sceneloader;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Services.PersistentProgress;
using CodeBase.UI.Curtain;
using System.Threading.Tasks;

namespace CodeBase.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly ISaveLoadService _saveLoad;
        private readonly IPersistentProgressService _progressService;
        private readonly IUIFactory _factory;

        private const string MenuSceneName = "MainMenu";

        public MainMenuState(
            IGameStateMachine stateMachine,
            IUIFactory factory,
            ISceneLoader sceneLoader,
            ILoadingCurtain loadingCurtain,
            ISaveLoadService saveLoad,
            IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _factory = factory;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _saveLoad = saveLoad;
            _progressService = progressService;
        }

        public void Exit()
        {

        }

        public void Enter()
        {
            LoadProgressOrInitNew();
            LoadMenuScene();
        }

        private async void OnLoadedAsync()
        {
            await InitUiRoot();
            await InitMenu();
            _loadingCurtain.Hide();
        }

        private async Task InitUiRoot()
        {
            await _factory.CreateUIRoot();
        }

        private async Task InitMenu()
        {
            await _factory.CreateMainMenu();
        }
        private void LoadMenuScene()
        {
            _sceneLoader.Load(MenuSceneName, OnLoadedAsync);
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.Progress = _saveLoad.LoadProgress() ?? new PlayerProgress
            {
                LastPlayerDifficulty = GameDifficultyId.Easy
            };
        }
    }
}