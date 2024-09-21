using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.Services.Randomizer;
using System.Collections.Generic;
using CodeBase.Logic.GameBasket;
using CodeBase.Logic.Spawner;
using System.Threading.Tasks;
using CodeBase.Logic;
using CodeBase.Data;
using UnityEngine;
using CodeBase.SO;
using Zenject;

namespace CodeBase.Infrastructure.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;
        private readonly IStaticDataService _staticData;
        private readonly IRandomService _randomService;
        private readonly IPersistentProgressService _persistentProgress;
        private readonly DiContainer _container;
   
        public GameFactory(DiContainer container, IAssetProvider assets, IStaticDataService staticDataService, IRandomService randomService, IPersistentProgressService persistentProgress)
        {
            _assets = assets;
            _staticData = staticDataService;
            _randomService = randomService;
            _persistentProgress = persistentProgress;
            _container = container;
        }

        public async Task WarmUp()
        {
            await _assets.Load<GameObject>(AssetAddress.Shape);
        }

        public void CleanUp()
        {
            _assets.CleanUp();
        }

        public async Task<Shape> CreateRandomShape(Vector2 position, float speedModifier, Palette palette) 
        {
            List<ShapeStaticData> shapes = _staticData.GetAllShapes();
            ShapeStaticData shapeData = _staticData.ForShape(shapes[_randomService.Next(0, shapes.Count)].TypeId);

            GameObject shapeAsset = await _assets.Load<GameObject>(AssetAddress.Shape);
            Shape shape = Object.Instantiate(shapeAsset, position, Quaternion.identity).GetComponent<Shape>();
            shape.SetColor(palette.Colors[_randomService.Next(0, palette.Colors.Length)]);
            shape.SetFallSpeed(shapeData.FallingSpeed * speedModifier);
            shape.SetRotationSpeed(shapeData.RotationSpeed);
            shape.SetSprite(shapeData.Image);
            return shape;
        }

        public async Task<Basket> CreateBasket(Vector3 at, Palette palette)
        {       
            GameObject basketAsset = await _assets.Load<GameObject>(AssetAddress.Basket);
            Basket basket = Object.Instantiate(basketAsset, at, Quaternion.identity).GetComponent<Basket>();

            GameDifficultyStaticData difficult = _staticData.ForGameDifficult(_persistentProgress.Progress.LastPlayerDifficulty);
            basket.Initialize(palette, difficult.BasketColorChangeInterval, difficult.BasketColorTransitionDuration);

            _container.InjectGameObject(basket.gameObject);
            return basket;
        }

        public async Task<ShapeSpawner> CreateShapeSpawner(Palette palette)
        {
            GameObject spawnerAsset = await _assets.Load<GameObject>(AssetAddress.ShapeSpawner);
            ShapeSpawner spawner = Object.Instantiate(spawnerAsset, Vector2.zero, Quaternion.identity).GetComponent<ShapeSpawner>();
            
            GameDifficultyStaticData difficult = _staticData.ForGameDifficult(_persistentProgress.Progress.LastPlayerDifficulty);
            spawner.Initialize(palette, difficult.ShapeSpawnInterval, difficult.ShapeFallSpeedMultiplayer);
           
            _container.InjectGameObject(spawner.gameObject);
            return spawner;
        }
    }
}