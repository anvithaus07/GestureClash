using GestureClash;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class SelectionDisplayWidget : MonoBehaviour
{
    [SerializeField] private CompetitorType _competitorType;
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private Image _gestureIcon;
    [SerializeField] private GestureCollectionData _gestureCollectionData;

    #region UnityMethods
    private void OnEnable()
    {
        ASignal<OnInputsReceivedSignal>.AddListener(OnInputsReceived);
    }

    private void OnDisable()
    {
        ASignal<OnInputsReceivedSignal>.RemoveListener(OnInputsReceived);
    }

    #endregion UnityMethods 
    public void Initialize(string infoText, GestureType gestureType)
    {
        SetInfoText(infoText);
    }

    private void SetInfoText(string infoText)
    {
        _infoText.text = infoText;
    }

    private void SetGestureIcon(GestureType gestureType)
    {
        Sprite icon = _gestureCollectionData.GetGestureData(gestureType).GestureIcon;
        _gestureIcon.sprite = icon;
    }

    private void OnInputsReceived(OnInputsReceivedSignal data)
    {
        if (_competitorType == data.CompetitorType)
        {
            SetGestureIcon(data.GestureType);

            var gestureElementName = _gestureCollectionData.GetGestureData(data.GestureType).GestureName;
            SetInfoText(string.Format("{0} has chosen {1}", data.CompetitorType, gestureElementName));
        }
    }
}
