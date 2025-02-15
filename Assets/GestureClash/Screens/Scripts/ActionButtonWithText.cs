using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GestureClash
{
    public class ActionButtonWithText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _buttonText;
        [SerializeField] private Button _actionButton;


        private Action _onButtonClick;

        #region UnityMethods

        private void OnEnable()
        {
            _actionButton.onClick.AddListener(OnActionButtonClicked);
        }

        private void OnDisable()
        {
            _actionButton.onClick.RemoveAllListeners();

        }
        #endregion UnityMethods

        public void InitializeActionButton(string buttonText, Action onButtonClick)
        {
            _onButtonClick = onButtonClick;
        }
        private void OnActionButtonClicked()
        {
            _onButtonClick?.Invoke();
        }

    }
}