using UnityEngine;

namespace GestureClash
{

    public enum CompetitorType
    {
        Player,
        Bot
    }
    public class OnGameStartedSignal { }
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
        }

        private void RemoveListeners()
        {
            ASignal<OnTimerEndSignal>.RemoveListener(OnTimerEnd);

        }
        private void SetUpGame()
        {
            _playerInput = InputHandler.GetInputType(false);
            _botInput = InputHandler.GetInputType(true);
            ASignal<OnGameStartedSignal>.Dispatch(new OnGameStartedSignal());

        }

        private GameResult DetermineWinner()
        {
            GestureType playerInput = _playerInput.GetSelectedGesture();
            GestureType botInput = _botInput.GetSelectedGesture();

            return playerInput == botInput ? GameResult.Tie :
                   (5 + playerInput - botInput) % 5 % 2 == 1 ? GameResult.Win : GameResult.Lost;
        }

        private void OnTimerEnd(OnTimerEndSignal data)
        {
            ASignal<OnInputsReceivedSignal>.Dispatch(new OnInputsReceivedSignal(CompetitorType.Bot, _botInput.GetSelectedGesture()));
            ASignal<OnInputsReceivedSignal>.Dispatch(new OnInputsReceivedSignal(CompetitorType.Player, _playerInput.GetSelectedGesture()));

            DetermineWinner();
        }

    }
}