using GestureClash.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GestureClash.GamePlay
{
    [CreateAssetMenu(fileName = "GestureWidgetData", menuName = "Scriptable Objects/GestureClash/GestureWidgetData")]
    public class GestureCollectionData : ScriptableObject
    {
        [SerializeField] private List<GestureData> _gesturesData;

        public List<GestureData> GetAllGestures()
        {
            return _gesturesData;
        }
        public GestureData GetGestureData(GestureType gestureType)
        {
            return _gesturesData.FirstOrDefault(x => x.GestureType == gestureType);
        }
    }

    [System.Serializable]
    public class GestureData
    {
        [SerializeField] private GestureType _gestureType;
        [SerializeField] private Sprite _gestureIcon;
        [SerializeField] private string _gestureName;

        public GestureType GestureType => _gestureType;
        public string GestureName => _gestureName;
        public Sprite GestureIcon => _gestureIcon;
    }
}