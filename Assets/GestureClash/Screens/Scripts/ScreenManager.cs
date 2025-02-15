using System.Collections.Generic;
using UnityEngine;

namespace GestureClash
{
    public class ScreenManager : MonoBehaviour
    {
        [SerializeField] private List<BaseScreen> _screens;
        public IReadOnlyList<BaseScreen> Screens => _screens;
        private readonly Dictionary<ScreenId, BaseScreen> _screenInstances = new();

        private Transform _screenHolder;

        private void Awake()
        {
            _screenHolder = FindFirstObjectByType<Canvas>().transform;
            ASignal<ShowScreenById>.AddListener(OnShowScreenRequested);
            ASignal<HideScreenWithID>.AddListener(OnHideScreenRequested);
        }

        private void OnDestroy()
        {
            ASignal<ShowScreenById>.RemoveListener(OnShowScreenRequested);
            ASignal<HideScreenWithID>.RemoveListener(OnHideScreenRequested);
        }


        private void OnShowScreenRequested(ShowScreenById request)
        {
            ShowScreen(request.ScreenId, request.ScreenData);
        }

        private void OnHideScreenRequested(HideScreenWithID request)
        {
            HideScreen(request.ScreenID);
        }

        public void HideScreen(ScreenId screenId)
        {
            if (_screenInstances.TryGetValue(screenId, out var screen))
            {
                screen.Hide();
            }
        }

        public void ShowScreen(ScreenId screenId, object screenData)
        {
            if (!_screenInstances.TryGetValue(screenId, out var screen))
            {
                screen = InstantiateScreen(screenId);
            }

            screen.Show(screenData);
        }

        private BaseScreen InstantiateScreen(ScreenId screenId)
        {
            var prefab = _screens.Find(s => s.ScreenId == screenId);
            if (prefab == null)
            {
                Debug.LogError($"Screen {screenId} not found in ScreenManager!");
                return null;
            }

            var screenInstance = Instantiate(prefab, _screenHolder);
            _screenInstances[screenId] = screenInstance;
            return screenInstance;
        }
    }

    public class ShowScreenById
    {
        public ScreenId ScreenId { get; }
        public object ScreenData { get; }

        public ShowScreenById(ScreenId screenId, object screenData)
        {
            ScreenId = screenId;
            ScreenData = screenData;
        }
    }
    public class HideScreenWithID
    {
        public ScreenId ScreenID { get; }

        public HideScreenWithID(ScreenId screenId)
        {
            ScreenID = screenId;
        }
    }
}