using System.Collections;
using UnityEngine;

namespace GestureClash
{

    public enum CompetitorType
    {
        Player,
        Bot
    }
    public class OnGameStartedSignal { }
    public class OnGameEndSignal
    {

        public GameResult GameResult;
        public OnGameEndSignal(GameResult gameResult)
        {
            GameResult = gameResult;
        }
    }
    public class OnPlayerInputReceivedSignal
    {
        public GestureType GestureType;

        public OnPlayerInputReceivedSignal(GestureType gestureType)
        {
            GestureType = gestureType;
        }

    }
    public class OnInputsReceivedSignal
    {
        public CompetitorType CompetitorType;
        public GestureType GestureType;

        public OnInputsReceivedSignal(CompetitorType competitorType, GestureType gestureType)
        {
            CompetitorType = competitorType;
            GestureType = gestureType;
        }
    }

    public class GameManager : MonoBehaviour
    {
        private BasePlayerInput _playerInput;
        private BasePlayerInput _botInput;

        #region UnityMethods
        private void OnEnable()
        {
            AddListeners();
            SetUpGame();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }
        #endregion UnityMethods 


        private void AddListeners()
        {
            ASignal<OnTimerEndSignal>.AddListener(OnTimerEnd);
            ASignal<OnPlayerInputReceivedSignal>.AddListener(OnPlayerInputReceived);
        }

        private void RemoveListeners()
        {
            ASignal<OnTimerEndSignal>.RemoveListener(OnTimerEnd);
            ASignal<OnPlayerInputReceivedSignal>.RemoveListener(OnPlayerInputReceived);

        }
        private void SetUpGame()
        {
            _playerInput = InputHandler.GetInputType(false);
            _botInput = InputHandler.GetInputType(true);
            ASignal<ShowScreenById>.Dispatch(new ShowScreenById(ScreenId.GamePlayScreen, null));
            ASignal<OnGameStartedSignal>.Dispatch(new OnGameStartedSignal());

        }

        private void DetermineWinner()
        {
            GestureType playerInput = _playerInput.GetSelectedGesture();
            GestureType botInput = _botInput.GetSelectedGesture();

            var result =  playerInput == botInput ? GameResult.Tie :
                   (5 + playerInput - botInput) % 5 % 2 == 1 ? GameResult.Win : GameResult.Lost;

            ASignal<OnGameEndSignal>.Dispatch(new OnGameEndSignal(result));
            StartCoroutine(RestartGame());
        }

        private void OnTimerEnd(OnTimerEndSignal data)
        {
            CheckForGameResult();
        }

        private void OnPlayerInputReceived(OnPlayerInputReceivedSignal data)
        {
            _playerInput.SetGestureType(data.GestureType);
            CheckForGameResult();
        }

        private void CheckForGameResult()
        {
            ASignal<OnInputsReceivedSignal>.Dispatch(new OnInputsReceivedSignal(CompetitorType.Bot, _botInput.GetSelectedGesture()));
            ASignal<OnInputsReceivedSignal>.Dispatch(new OnInputsReceivedSignal(CompetitorType.Player, _playerInput.GetSelectedGesture()));

            DetermineWinner();
        }

        private IEnumerator RestartGame()
        {
            yield return new WaitForSeconds(3.0f);
            ASignal<ShowScreenById>.Dispatch(new ShowScreenById(ScreenId.GameRestartScreen, null));
        }

    }
}