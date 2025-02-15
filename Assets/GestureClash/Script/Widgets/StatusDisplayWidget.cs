using GestureClash;
using TMPro;
using UnityEngine;

public class StatusDisplayWidget : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statusText;

    #region UnityMethods
    private void OnEnable()
    {
        ASignal<OnGameStartedSignal>.AddListener(OnGameStarted);
        ASignal<OnPlayerInputReceivedSignal>.AddListener(OnInputsReceived);
        ASignal<OnTimerEndSignal>.AddListener(OnTimerEnd);
        ASignal<OnGameEndSignal>.AddListener(OnGameEnd);
    }

    private void OnDisable()
    {
        ASignal<OnGameStartedSignal>.RemoveListener(OnGameStarted);
        ASignal<OnPlayerInputReceivedSignal>.RemoveListener(OnInputsReceived);
        ASignal<OnTimerEndSignal>.RemoveListener(OnTimerEnd);
        ASignal<OnGameEndSignal>.RemoveListener(OnGameEnd);

    }

    #endregion UnityMethods 

    private void OnGameStarted(OnGameStartedSignal data)
    {
        _statusText.text = "Waiting for input...";
    }
    private void OnInputsReceived(OnPlayerInputReceivedSignal data)
    {
        _statusText.text = "Checking results...";
    }

    private void OnTimerEnd(OnTimerEndSignal data)
    {
        _statusText.text = "Time Complete!!!";
    }

    private void OnGameEnd(OnGameEndSignal data)
    {
        switch(data.GameResult)
        {
            case GameResult.Tie:
                _statusText.text = "IT'S A TIE!!";
                return;

            case GameResult.Win:
                _statusText.text = "YOU BEAT THE BOT!!";
                return;

            case GameResult.Lost:
                _statusText.text = "BOT BEATS YOU!!";
                return;


        }
    }
}
