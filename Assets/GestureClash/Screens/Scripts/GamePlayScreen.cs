using UnityEngine;

namespace GestureClash
{
    public enum GameResult
    {
        Win,
        Lost,
        Tie
    }
    public class GamePlayScreen : MonoBehaviour
    {
        [SerializeField] private TimerWidget _timerWidget;
        [SerializeField] private GestureWidgetsPanel _gesturePanel;

        #region UnityMethods
        private void OnEnable()
        {
            ASignal<OnGameStartedSignal>.AddListener(OnGameStarted);
        }

        private void OnDisable()
        {
            ASignal<OnGameStartedSignal>.RemoveListener(OnGameStarted);

        }
        #endregion UnityMethods 

        private void SetUpGameUI()
        {
            _gesturePanel.InitializeGamePlayElements();
            _timerWidget.InitializeTimer(4);
        }

        private void OnGameStarted(OnGameStartedSignal data)
        {
            SetUpGameUI();
        }
    }
}