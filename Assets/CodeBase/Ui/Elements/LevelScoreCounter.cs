using TMPro;
using UnityEngine;
using DG.Tweening;
using CodeBase.Logic.GameBasket;

namespace CodeBase.UI.Elements
{
    public class LevelScoreCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _scaleAnimationDuration = 0.25f;
        [SerializeField] private float _scaleAnimationScale = 1.2f;
        private BasketScore _score;

        public void Initialize(BasketScore score)
        {
            _score = score;
        }

        private void Start()
        {
            _score.ScoreChanged += OnScoreChanged;
        }

        private void OnDestroy()
        {
            _score.ScoreChanged -= OnScoreChanged;
        }

        private void OnScoreChanged(int currentScore)
        {
            _text.text = currentScore.ToString();

            Sequence sequence = DOTween.Sequence();
            sequence.Append(_text.transform.DOScale(Vector3.one * _scaleAnimationScale, _scaleAnimationDuration));
            sequence.Append(_text.transform.DOScale(Vector3.one, _scaleAnimationDuration));
            sequence.SetLink(gameObject);
            sequence.Play();
        }
    }
}