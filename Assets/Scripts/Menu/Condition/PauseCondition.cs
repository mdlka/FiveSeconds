using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCondition : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Menu _menu;

    private bool _isPause = false;
    private MainInputAction _input;

    private void Awake()
    {
        _input = new MainInputAction();
    }

    private void OnEnable()
    {
        _input.Enable();

        _input.Menu.Pause.performed += context => OnPause();
    }

    private void OnDisable()
    {
        _input.Disable();

        _input.Menu.Pause.performed -= context => OnPause();
    }

    private void OnPause()
    {
        if (_isPause == false && Time.timeScale == 1f)
        {
            _menu.OpenPanel(_pauseMenu);
            _isPause = true;
        }
        else if (_isPause && Time.timeScale == 0f)
        {
            _menu.ClosePanel(_pauseMenu);
            _isPause = false;       
        }
    }
}
