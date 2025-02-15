using UnityEngine;

namespace GestureClash
{
    public enum GameResult
    {
        Win,
        Lost,
        Tie
    }
    public class GamePlayScreen : BaseScreen
    {
        [SerializeField] private TimerWidget _timerWidget;
        [SerializeField] private GestureWidgetsPanel _gesturePanel;

       
        protected override void OnScreenShown(object screenData = null)
        {
            SetUpGameUI();
        }
        private void SetUpGameUI()
        {
            _gesturePanel.InitializeGamePlayElements();
            _timerWidget.InitializeTimer(4);
        }       
    }
}