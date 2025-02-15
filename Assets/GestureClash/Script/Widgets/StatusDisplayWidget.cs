using GestureClash.GamePlay;
using TMPro;
using UnityEngine;
namespace GestureClash.UI
{
    public class StatusDisplayWidget : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _statusText;

        #region UnityMethods
        private void OnEnable()
        {
            ASignal<OnGameStartedSignal>.AddListener(OnGameStarted);
            ASignal<OnInputsReceivedSignal>.AddListener(OnInputsReceived);
            ASignal<OnTimerEndSignal>.AddListener(OnTimerEnd);
            ASignal<OnGameEndSignal>.AddListener(OnGameEnd);
        }

        private void OnDisable()
        {
            ASignal<OnGameStartedSignal>.RemoveListener(OnGameStarted);
            ASignal<OnInputsReceivedSignal>.RemoveListener(OnInputsReceived);
            ASignal<OnTimerEndSignal>.RemoveListener(OnTimerEnd);
            ASignal<OnGameEndSignal>.RemoveListener(OnGameEnd);

        }

        #endregion UnityMethods 

        private void OnGameStarted(OnGameStartedSignal data)
        {
            _statusText.text = "Waiting for input...";
        }
        private void OnInputsReceived(OnInputsReceivedSignal data)
        {
            _statusText.text = "Checking results...";
        }

        private void OnTimerEnd(OnTimerEndSignal data)
        {
            _statusText.text = "Time Complete!!!";
        }

        private void OnGameEnd(OnGameEndSignal data)
        {
            switch (data.GameResult)
            {
                case GameResult.Tie:
                    _statusText.text = "It's a tie!!";
                    return;

                case GameResult.Win:
                    _statusText.text = "You Win!";
                    return;

                case GameResult.Lost:
                    _statusText.text = "You lost!";
                    return;


            }
        }
    }
}