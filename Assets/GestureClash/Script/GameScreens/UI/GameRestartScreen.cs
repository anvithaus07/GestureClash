using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GestureClash
{
    public class GameRestartScreen : BaseScreen
    {
        [SerializeField] private Image _BG;
        [SerializeField] private TextMeshProUGUI _countdownText;
        [SerializeField] private int _countdownValue;

        private Tween _countDownTween;
        protected override void OnScreenShown(object data = null)
        {
            StartTimer();
        }

        protected override void OnHideScreen()
        {
            KillCountDownTween();
        }

        public void StartTimer()
        {
            KillCountDownTween();
            int startValue = _countdownValue;
            _countDownTween = DOTween.To(() => startValue, x => startValue = x, 0, 2.0f)
                 .SetEase(Ease.Linear)
                 .OnUpdate(() =>
                 {
                     _countdownText.text = startValue.ToString();
                 })
                 .OnComplete(() =>
                 {
                     _countdownText.text = "0";
                     OnGameRestart();
                 });
        }
        private void OnGameRestart()
        {
            Hide();
            ASignal<OnGameRestartedSignal>.Dispatch(new OnGameRestartedSignal());
        }

        private void KillCountDownTween()
        {
            if (_countDownTween != null)
                _countDownTween.Kill();
        }
    }
}