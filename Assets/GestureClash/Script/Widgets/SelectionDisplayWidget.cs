using GestureClash;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.Rendering;
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
        ResetUIState();
        ASignal<OnInputsReceivedSignal>.AddListener(OnInputsReceived);
    }

    private void OnDisable()
    {
        ASignal<OnInputsReceivedSignal>.RemoveAllListener();
    }

    #endregion UnityMethods 
    private void ResetUIState()
    {
        SetInfoText(string.Format("{0} choice : ", _competitorType));
        SetIconVisibility(false);
    }
    private void OnInputsReceived(OnInputsReceivedSignal data)
    {
        if (_competitorType == data.CompetitorType && data.GestureType.HasValue)
        {
            SetGestureIcon(data.GestureType.Value);

            var gestureElementName = _gestureCollectionData.GetGestureData(data.GestureType.Value).GestureName;
            SetInfoText(string.Format("{0} chose {1}", data.CompetitorType, gestureElementName));
        }
    }
    private void SetInfoText(string infoText)
    {
        _infoText.text = infoText;
    }

    private void SetGestureIcon(GestureType gestureType)
    {
        SetIconVisibility(true);
        Sprite icon = _gestureCollectionData.GetGestureData(gestureType).GestureIcon;
        _gestureIcon.sprite = icon;
    }
    
    private void SetIconVisibility(bool isVisible)
    {
        _gestureIcon.gameObject.SetActive(isVisible);
    }
}
