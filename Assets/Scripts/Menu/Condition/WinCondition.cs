using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [SerializeField] private Finish _finish;
    [SerializeField] private GameObject _winMenu;
    [SerializeField] private Menu _menu;

    private void OnEnable()
    {
        _finish.PlayerFinished += OpenWinMenu;
    }

    private void OnDisable()
    {
        _finish.PlayerFinished += OpenWinMenu;
    }

    private void OpenWinMenu()
    {
        _menu.OpenPanel(_winMenu);
    } 
}
