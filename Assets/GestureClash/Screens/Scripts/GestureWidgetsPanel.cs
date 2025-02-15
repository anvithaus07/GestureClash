using GestureClash;
using UnityEngine;
using UnityEngine.UI;

public class GestureWidgetsPanel : MonoBehaviour
{
    [SerializeField] private GestureCollectionData _gestureCollectionData;
    [SerializeField] private GestureWidget _gestureWidget;
    [SerializeField] private HorizontalLayoutGroup _widgetHolder;

    public void InitializeGamePlayElements()
    {
        var gestureData = _gestureCollectionData.GetAllGestures();
        foreach(GestureData gesture in gestureData)
        {
            GestureWidget gestureElement = Instantiate(_gestureWidget, _widgetHolder.transform);
            gestureElement.InitializeGestureElement(gesture,OnGestureButtonClick);
        }
    }

    private void OnGestureButtonClick(GestureType gestureType)
    {
        ASignal<OnPlayerInputReceivedSignal>.Dispatch(new OnPlayerInputReceivedSignal(gestureType));
    }
}
