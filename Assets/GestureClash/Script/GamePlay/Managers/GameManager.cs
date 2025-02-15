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
    public class OnGameRestartedSignal { }
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
        public GestureType? GestureType;

        public OnInputsReceivedSignal(CompetitorType competitorType, GestureType? gestureType)
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
            StartGame();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }
        #endregion UnityMethods 


        private void AddListeners()
        {
            RemoveListeners();
            ASignal<OnTimerEndSignal>.AddListener(OnTimerEnd);
            ASignal<OnPlayerInputReceivedSignal>.AddListener(OnPlayerInputReceived);
            ASignal<OnGameRestartedSignal>.AddListener(OnGameRestarted);
        }

        private void RemoveListeners()
        {
            ASignal<OnTimerEndSignal>.RemoveAllListener();
            ASignal<OnPlayerInputReceivedSignal>.RemoveAllListener();
            ASignal<OnGameRestartedSignal>.RemoveAllListener();
        }
        private void StartGame()
        {
            AddListeners();

            _playerInput = InputHandler.GetInputType(false);
            _botInput = InputHandler.GetInputType(true);
            ASignal<ShowScreenById>.Dispatch(new ShowScreenById(ScreenId.GamePlayScreen, null));
            ASignal<OnGameStartedSignal>.Dispatch(new OnGameStartedSignal());

        }

        private void DetermineWinner()
        {
            GestureType playerInput = _playerInput.GetSelectedGesture().Value;
            GestureType botInput = _botInput.GetSelectedGesture().Value;

            var result =  playerInput == botInput ? GameResult.Tie :
                   (5 + playerInput - botInput) % 5 % 2 == 1 ? GameResult.Win : GameResult.Lost;

            ASignal<OnGameEndSignal>.Dispatch(new OnGameEndSignal(result));
            StartCoroutine(RestartGame());
        }

        private void OnTimerEnd(OnTimerEndSignal data)
        {
            _playerInput.SetGestureType(null);
            ASignal<OnInputsReceivedSignal>.Dispatch(new OnInputsReceivedSignal(CompetitorType.Bot, _botInput?.GetSelectedGesture()));
            StartCoroutine(RestartGame());
        }

        private void OnPlayerInputReceived(OnPlayerInputReceivedSignal data)
        {
            _playerInput.SetGestureType(data.GestureType);
            CheckForGameResult();
        }

        private void CheckForGameResult()
        {
            ASignal<OnInputsReceivedSignal>.Dispatch(new OnInputsReceivedSignal(CompetitorType.Bot, _botInput?.GetSelectedGesture()));
            ASignal<OnInputsReceivedSignal>.Dispatch(new OnInputsReceivedSignal(CompetitorType.Player, _playerInput?.GetSelectedGesture()));

            DetermineWinner();
        }

        private IEnumerator RestartGame()
        {
            yield return new WaitForSeconds(2.0f);

            ASignal<HideScreenWithID>.Dispatch(new HideScreenWithID(ScreenId.GamePlayScreen));
            ASignal<ShowScreenById>.Dispatch(new ShowScreenById(ScreenId.GameRestartScreen, null));
        }

        private void OnGameRestarted(OnGameRestartedSignal data)
        {
            StartGame();
        }
    }
}