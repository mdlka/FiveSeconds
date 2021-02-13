using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevelMenu : MonoBehaviour
{
    [SerializeField] private LevelsData _levelsData;
    [SerializeField] private Menu _menu;
    [SerializeField] private LevelButtonView _template;
    [SerializeField] private GameObject _itemContainer;

    private void Start()
    {
        for (int i = 0; i < _levelsData.Lenght; i++)
        {
            AddItem(_levelsData.GetLevelByIndex(i));
        }
    }

    private void AddItem(Level level)
    {
        var view = Instantiate(_template, _itemContainer.transform);

        view.LevelButtonClick += OnLevelButtonClick;
        view.Render(level);
    }

    private void OnLevelButtonClick(Level level, LevelButtonView view)
    {
        LoadLevel(level, view);
    }

    private void LoadLevel(Level level, LevelButtonView view)
    {
        _menu.LoadScene(level.SceneName);

        view.LevelButtonClick -= OnLevelButtonClick;
    }
}
