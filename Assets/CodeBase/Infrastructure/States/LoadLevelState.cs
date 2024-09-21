using CodeBase.Services.PersistentProgress;
using CodeBase.Infrastructure.Sceneloader;
using CodeBase.Infrastructure.Factories;
using CodeBase.Services.StaticData;
using CodeBase.Logic.GameBasket;
using System.Threading.Tasks;
using CodeBase.UI.Elements;
using CodeBase.UI.Curtain;
using CodeBase.Logic;
using CodeBase.SO;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<LevelStaticData>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uIFactory;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentProgressService _progressService;
        private LevelStaticData _levelToLoad;

        public LoadLevelState(
            IGameStateMachine gameStateMachine,
            ISceneLoader sceneLoader,
            ILoadingCurtain loadingCurtain,
            IGameFactory gameFactory,
            IUIFactory uIFactory,
            IStaticDataService staticData,
            IPersistentProgressService progressService)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _uIFactory = uIFactory;
            _staticData = staticData;
            _progressService = progressService;
        }

        public void Enter(LevelStaticData configuration)
        {
            _levelToLoad = configuration;
            PrepareForLevelLoading();
            LoadSceneAsync();
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
            _uIFactory.CleanUp();
            _gameFactory.CleanUp();
        }

        private void PrepareForLevelLoading()
        {
            _loadingCurtain.Show();
            _uIFactory.WarmUp();
            _gameFactory.WarmUp();
        }

        private void LoadSceneAsync()
        {
            _sceneLoader.Load(_levelToLoad.SceneName, OnLoadedAsync);
        }

        private async void OnLoadedAsync()
        {
            await _uIFactory.CreateUIRoot();
            await InitializeGameWorld();
            _stateMachine.Enter<GameLoopState>();
        }

        private async Task InitializeGameWorld()
        {
            GameObject basket = await CreateBasket();
            await _gameFactory.CreateShapeSpawner(_levelToLoad.Palette);
            await SetupHud(basket);
        }

        private async Task<GameObject> CreateBasket()
        {
            Basket basket = await _gameFactory.CreateBasket(_levelToLoad.InitialBasketPosition, _levelToLoad.Palette);

            GameDifficultyStaticData data = _staticData.ForGameDifficult(_progressService.Progress.LastPlayerDifficulty);
            basket.GetComponentInChildren<BasketScore>().Initialize(data.ScoreIncreaseAmount, data.ScoreDecreaseAmount);

            return basket.gameObject;
        }

        private async Task SetupHud(GameObject basket)
        {
            GameObject hud = await _uIFactory.CreateHud();
            InitializeScoreCounter(hud, basket);
            InitializeTimer(hud);
        }

        private void InitializeScoreCounter(GameObject hud, GameObject basket)
        {
            var scoreCounter = hud.GetComponentInChildren<LevelScoreCounter>();
            scoreCounter.Initialize(basket.GetComponent<BasketScore>());
        }

        private void InitializeTimer(GameObject hud)
        {
            Timer timer = hud.GetComponentInChildren<Timer>();
            timer.Begin(_levelToLoad.TimeLimit);
        }
    }
}