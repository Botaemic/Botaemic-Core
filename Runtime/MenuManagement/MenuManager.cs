using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Botaemic.Core.MenuManagement
{
    public class MenuManager : Botaemic.Utils.Singleton<MenuManager>
    {
        #region Inspector
        [SerializeField] private Menu _openingMenu = null;
        [SerializeField] private MenuOpenCloseEvent _menuOpenCloseEvent = null;
        #endregion

        #region Private variables
        #endregion

        #region Unity methodes
        private void Start()
        {
            OpenMenus(new Menu[] { _openingMenu });
        }

        private void OnEnable()
        {
            _menuOpenCloseEvent.openCloseEvent += OpenCloseMenus;
        }

        private void OnDisable()
        {
            _menuOpenCloseEvent.openCloseEvent -= OpenCloseMenus;
        }
        #endregion

        #region Public Methodes
        #endregion

        #region Private Methodes
        private void OpenCloseMenus(Menu[] menusToOpen, Menu[] MenusToClose)
        {
            CloseMenus(MenusToClose);
            OpenMenus(menusToOpen);
        }

        private void CloseMenus(Menu[] menus)
        {
            if (menus == null) { return; }
            foreach (Menu menu in menus)
            {
                if(menu == null) { continue; }
                menu.Hide(0);
            }
        }

        private void OpenMenus(Menu[] menus)
        {
            if (menus == null) { return; }
            foreach (Menu menu in menus)
            {
                if (menu == null) { continue; }
                menu.Show(0);
            }
        }
        #endregion

    }
}