using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "NewLevelsData", menuName = "LevelsData", order = 51)]
public class LevelsData : ScriptableObject
{
    [SerializeField] private Level[] _levels;

    public int Lenght => _levels.Length;

    public Level GetLevelByIndex(int index)
    {
        return _levels[index];
    }

    public void OpenLevel(string sceneName)
    {
        _levels.First(level => level.SceneName == sceneName).IsOpen = true;
    }

    public string GetNextLevelSceneName(string sceneName)
    {
        string nextLevelSceneName = null;
        var levels = _levels.SkipWhile(level => level.SceneName != sceneName);

        if(levels.Count() != 1)
        {
            nextLevelSceneName = levels.First(level => level.SceneName != sceneName).SceneName;
        }

        return nextLevelSceneName;
    }

    public string GetLastOpenLevelSceneName()
    {
        return _levels.Last(level => level.IsOpen).SceneName;
    }
}

[System.Serializable]
public class Level
{
    public string SceneName;
    public bool IsOpen;
}
