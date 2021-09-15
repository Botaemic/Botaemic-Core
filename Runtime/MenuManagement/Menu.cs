using UnityEngine;

namespace Botaemic.Core.MenuManagement
{
    public abstract class Menu : MonoBehaviour
    {
        [SerializeField] protected MenuOpenCloseEvent _menuOpenCloseEvent;

        public abstract void Show(float delay);
        public abstract void Hide(float delay);
    }
}