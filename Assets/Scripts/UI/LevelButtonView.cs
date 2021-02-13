using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class LevelButtonView : MonoBehaviour
{
    [SerializeField] private Button _levelButton;
    [SerializeField] private TMP_Text _name;

    private Level _level;

    public event UnityAction<Level, LevelButtonView> LevelButtonClick;

    private void OnEnable()
    {
        _levelButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _levelButton.onClick.RemoveListener(OnButtonClick);
    }

    private void TryLockButton()
    {
        if (_level.IsOpen == false)
            _levelButton.interactable = false;
    }

    public void Render(Level level)
    {
        _level = level;
        _name.text = level.SceneName;

        TryLockButton();
    }

    private void OnButtonClick()
    {
        LevelButtonClick?.Invoke(_level, this);
    }
}
