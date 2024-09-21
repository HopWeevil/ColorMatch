using CodeBase.Infrastructure.Factories;
using UnityEngine;
using Zenject;
using System.Collections;
using CodeBase.Data;

namespace CodeBase.Logic.Spawner
{
    public class ShapeSpawner : MonoBehaviour
    {
        [SerializeField] private float _initialDelay = 0.5f;

        private float _spawnInterval;
        private float _fallSpeedModifier;
        private Camera _mainCamera;
        private Palette _palette;
        private bool _isSpawning;
        private float _shapeWidth;

        private IGameFactory _gameFactory;

        [Inject]
        private void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void Initialize(Palette palette, float spawnInterval, float fallSpeed)
        {
            _palette = palette;
            _spawnInterval = spawnInterval;
            _fallSpeedModifier = fallSpeed;
        }

        private void Start()
        {
            _mainCamera = Camera.main;
            StartSpawning();
        }

        private void StartSpawning()
        {
            if (!_isSpawning)
            {
                _isSpawning = true;
                StartCoroutine(SpawnShapesCoroutine());
            }
        }

        private IEnumerator SpawnShapesCoroutine()
        {
            yield return new WaitForSeconds(_initialDelay);

            while (_isSpawning)
            {
                SpawnShapeAsync();
                yield return new WaitForSeconds(_spawnInterval);
            }
        }

        private async void SpawnShapeAsync()
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();
            Shape shape = await _gameFactory.CreateRandomShape(spawnPosition, _fallSpeedModifier, _palette);
            _shapeWidth = shape.Width;

        }

        private Vector2 GetRandomSpawnPosition()
        {
            Vector2 screenTopLeft = _mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0));
            Vector2 screenTopRight = _mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

            float randomX = Random.Range(screenTopLeft.x + GetShapeWidth() / 2, screenTopRight.x - GetShapeWidth() / 2);
            return new Vector2(randomX, screenTopLeft.y);
        }

        private float GetShapeWidth()
        {
            return _shapeWidth == 0 ? 1 : _shapeWidth;
        }
    }
}