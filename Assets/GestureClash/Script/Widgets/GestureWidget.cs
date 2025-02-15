using UnityEngine;
using UnityEngine.UI;
namespace GestureClash
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

        public void InitializeGestureElement(GestureData gestureType)
        {
            SetGestureIcon(gestureType.GestureIcon);
            SetGestureName();
        }

        private void SetGestureIcon(Sprite icon)
        {
            _gestureIcon.sprite = icon;
        }

        private void SetGestureName()
        {
        }
    }
}