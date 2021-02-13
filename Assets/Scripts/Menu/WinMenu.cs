using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private LevelsData _levelsData;
    [SerializeField] private Menu _menu;

    private string _nextLevelSceneName;

    private void OnEnable()
    {
        _nextLevelSceneName = _levelsData.GetNextLevelSceneName(SceneManager.GetActiveScene().name);

        if (_nextLevelSceneName != null)
        {
            _levelsData.OpenLevel(_nextLevelSceneName);
        }
        else
        {
            _nextLevelButton.interactable = false;
        }

        _nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);
    }

    private void OnDisable()
    {
        _nextLevelButton.onClick.RemoveListener(OnNextLevelButtonClick);
    }

    private void OnNextLevelButtonClick()
    {
        _menu.LoadScene(_nextLevelSceneName);
    }
}
