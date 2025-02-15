using GestureClash.GamePlay;
using System;
using UnityEngine;
using UnityEngine.UI;
namespace GestureClash.UI
{
    public enum GestureType
    {
        Rock,
        Paper,
        Scissor,
        Lizard,
        Spock
    }
    public class GestureWidget : MonoBehaviour
    {
        [SerializeField] private Image _gestureIcon;
        [SerializeField] private Button _gestureButton;

        private Action<GestureType> OnGestureSelected;
        private GestureType _gestureType;


        #region UnityMethods
        private void OnEnable()
        {
            _gestureButton.onClick.AddListener(OnGestureButtonClick);
        }

        private void OnDisable()
        {
            _gestureButton.onClick.RemoveAllListeners();
        }

        #endregion UnityMethods 

        public void InitializeGestureElement(GestureData gestureData,Action<GestureType> onGestureSelected)
        {
            OnGestureSelected = onGestureSelected;
            _gestureType = gestureData.GestureType;

            SetGestureIcon(gestureData.GestureIcon);
            SetGestureName();
        }

        private void SetGestureIcon(Sprite icon)
        {
            _gestureIcon.sprite = icon;
        }

        private void SetGestureName()
        {
        }

        private void OnGestureButtonClick()
        {
            OnGestureSelected?.Invoke(_gestureType);
        }
    }
}