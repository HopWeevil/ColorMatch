using DG.Tweening;
using UnityEngine;

namespace CodeBase.Logic
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Shape : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private float _rotationSpeed;
        private Camera _mainCamera;
        private float _fallSpeed;
        private Vector3 _currentVelocity;

        public float Width => _spriteRenderer.bounds.size.x;
        public Color Color => _spriteRenderer.color;

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        public void SetFallSpeed(float speed)
        {
            _fallSpeed = speed;
        }

        public void SetRotationSpeed(float speed)
        {
            _rotationSpeed = speed;
        }

        private void Start()
        {
            _mainCamera = Camera.main;
            StartRotation();
        }
        private void Update()
        {
            UpdateVelocity();
            UpdatePosition();
            CheckIfOutOfBounds();
        }

        private void StartRotation()
        {
            transform.DORotate(new Vector3(0, 0, 360), 360f / _rotationSpeed, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental)
                .SetLink(gameObject)
                .SetLink(gameObject);
        }

        private void UpdateVelocity()
        {
            _currentVelocity += Physics.gravity * _fallSpeed * Time.deltaTime;
        }

        private void UpdatePosition()
        {
            transform.position += _currentVelocity * Time.deltaTime;
        }


        private void CheckIfOutOfBounds()
        {
            float cameraBottomY = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
            float destroyThresholdY = cameraBottomY - (_mainCamera.orthographicSize * 2);

            if (transform.position.y < destroyThresholdY)
            {
                Destroy(gameObject);
            }
        }
    }
}