using System;
using UnityEngine;
namespace GestureClash.UI
{
    public abstract class BaseScreen : MonoBehaviour
    {
        public ScreenId ScreenId => Enum.Parse<ScreenId>(gameObject.name);
        public virtual void Show(object screenData)
        {
            gameObject.SetActive(true);
            OnScreenShown(screenData);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
            OnHideScreen();
        }

        protected abstract void OnScreenShown(object screenData = null);
        protected abstract void OnHideScreen();

    }
}