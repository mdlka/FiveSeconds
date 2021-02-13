using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCondition : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Timer _timer;
    [SerializeField] private GameObject _loseMenu;
    [SerializeField] private Menu _menu;

    private void OnEnable()
    {
        _player.Died += OpenLoseMenu;
        _timer.TimeOver += OpenLoseMenu;
    }

    private void OnDisable()
    {
        _player.Died -= OpenLoseMenu;
        _timer.TimeOver -= OpenLoseMenu;
    }

    private void OpenLoseMenu()
    {
        _menu.OpenPanel(_loseMenu);
    }
}
