using UnityEngine;

namespace GestureClash
{
    public class GamePlayScreen : MonoBehaviour
    {
        [SerializeField] private TimerWidget _timerWidget;
        [SerializeField] private GestureWidgetsPanel _gesturePanel;

        #region UnityMethods
        private void OnEnable()
        {
            SetUpGameUI();
        }
        #endregion UnityMethods 

        private void SetUpGameUI()
        {
            _gesturePanel.InitializeGamePlayElements();
            _timerWidget.InitializeTimer(10);
        }
    }
}