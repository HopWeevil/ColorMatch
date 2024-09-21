using CodeBase.Services.Randomizer;
using UnityEngine;
using Zenject;
using DG.Tweening;
using System.Collections;
using UnityEngine.EventSystems;
using CodeBase.Data;

namespace CodeBase.Logic.GameBasket
{
    public class Basket : MonoBehaviour
    {
        [SerializeField] private BasketScore _score;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private float _colorChangeInterval;
        private float _colorTransitionDuration;

        private Color _basketColor;
        private Camera _mainCamera;
        private float _halfBasketWidth;
        private Palette _palette;
        private IRandomService _randomService;

        [Inject]
        private void Construct(IRandomService randomService)
        {
            _randomService = randomService;
        }

        public void Initialize(Palette palette, float colorChangeInterval, float colorTransitionDuration)
        {
            _palette = palette;
            _colorChangeInterval = colorChangeInterval;
            _colorTransitionDuration = colorTransitionDuration;
        }

        private void Start()
        {
            _mainCamera = Camera.main;
            _halfBasketWidth = _spriteRenderer.bounds.extents.x;

            StartCoroutine(ChangeBasketColorRoutine());
        }

        private void Update()
        {
            HandleBasketMovement();
        }

        private void HandleBasketMovement()
        {
            if (CanMove())
            {
                Vector3 newPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                newPosition.y = transform.position.y;
                newPosition.z = 0;

                float screenLeft = _mainCamera.ViewportToWorldPoint(Vector3.zero).x;
                float screenRight = _mainCamera.ViewportToWorldPoint(Vector3.right).x;

                newPosition.x = Mathf.Clamp(newPosition.x, screenLeft + _halfBasketWidth, screenRight - _halfBasketWidth);

                transform.position = newPosition;
            }
        }

        private static bool CanMove()
        {
            return Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && Time.timeScale != 0;
        }

        private IEnumerator ChangeBasketColorRoutine()
        {
            while (true)
            {
                ChangeBasketColor();
                yield return new WaitForSeconds(_colorChangeInterval);
            }
        }

        private void ChangeBasketColor()
        {
            Color newColor = _palette.Colors[_randomService.Next(0, _palette.Colors.Length)];
            _spriteRenderer.DOColor(newColor, _colorTransitionDuration).SetLink(gameObject).OnComplete(() =>
            {
                _basketColor = newColor;
            });
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Shape shape))
            {
                if (shape.Color == _basketColor)
                {
                    _score.Increment();
                }
                else
                {
                    _score.Decrement();
                }

                Destroy(collision.gameObject);
            }
        }
    }
}