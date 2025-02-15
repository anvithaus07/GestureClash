using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GestureClash.UI
{
    public class OnTimerEndSignal { }

    public class TimerWidget : MonoBehaviour
    {
        [SerializeField] private Slider _timer;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI _timerValueText;
        [SerializeField] private Color _defaultFontColor;
        [SerializeField] private Color _warningFontColor;

        #region Unity Methods
        private void OnDisable()
        {
            _timer.DOKill();
        }

        #endregion Unity Methods
        public void InitializeTimer(float duration)
        {
            StartTimer(duration);
        }
        private void StartTimer(float duration)
        {
            _timer.value = 1.0f;
            _timer.DOValue(0, duration).SetEase(Ease.Linear).OnUpdate(() =>
            {
                OnTimerValueUpdated(duration * _timer.value);
            }).OnComplete(() =>
            {
                ASignal<OnTimerEndSignal>.Dispatch(new OnTimerEndSignal());
            });
        }

        private void OnTimerValueUpdated(float remainingTime)
        {
            _timerValueText.text = remainingTime.ToString("F0") + "s";
            _timerValueText.color = remainingTime <= 3f ? _warningFontColor : _defaultFontColor;
        }
    }
}