using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GestureClash {
    public class GameRestartScreen : BaseScreen
    {
        [SerializeField] private Image _BG;
        [SerializeField] private TextMeshProUGUI _countdownText;
        [SerializeField] private int _countdownValue;

        protected override void OnScreenShown(object data = null)
        {
            StartTimer();
        }

        public void StartTimer()
        {
            int startValue = _countdownValue;
            DOTween.To(() => startValue, x => startValue = x, 0, 2.0f)
                .SetEase(Ease.Linear)
                .OnUpdate(() =>
                {
                    _countdownText.text = startValue.ToString();
                })
                .OnComplete(() =>
                {
                    _countdownText.text = "0";
                    ASignal<OnGameStartedSignal>.Dispatch(new OnGameStartedSignal());
                    OnGameRestart();
                });
        }
        private void OnGameRestart()
        {
            Hide();
        }
    }
}