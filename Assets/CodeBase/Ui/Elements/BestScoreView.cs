using CodeBase.Services.PersistentProgress;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Elements
{
    public class BestScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private IPersistentProgressService _progressService;

        [Inject]
        private void Construct(IPersistentProgressService progressService)
        {
            _progressService = progressService;
        }

        private void Start()
        {
            _text.text = string.Format(_text.text, _progressService.Progress.MaxScore.ToString());
        }
    }
}