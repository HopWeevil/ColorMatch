using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Sprite _pauseIcon;
        [SerializeField] private Sprite _playIcon;
        [SerializeField] private Image _buttonIcon;

        private bool _isPaused = false;

        private void Start()
        {
            _pauseButton.onClick.AddListener(TogglePause);
        }

        private void OnDestroy()
        {
            _pauseButton.onClick.RemoveListener(TogglePause);
        }

        private void TogglePause()
        {
            _isPaused = !_isPaused;

            if (_isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }

        private void PauseGame()
        {
            _buttonIcon.sprite = _playIcon;
            Time.timeScale = 0f;
        }

        private void ResumeGame()
        {
            _buttonIcon.sprite = _pauseIcon;
            Time.timeScale = 1f;
        }
    }
}