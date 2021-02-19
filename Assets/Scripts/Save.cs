using System.IO;
using UnityEngine;
using System.Linq;

public class Save : MonoBehaviour
{
    [SerializeField] private LevelsData _levelsData;

    private SaveData _save = new SaveData();
    private string _path;

    private void Start()
    {
        _path = Path.Combine(Application.dataPath, "Save.json");

        if (File.Exists(_path))
        {
            _save = JsonUtility.FromJson<SaveData>(File.ReadAllText(_path));

            var openLevels = _save.Levels.TakeWhile(level => level.IsOpen == true);

            foreach (var level in openLevels)
            {
                _levelsData.OpenLevel(level.SceneName);
            }
        }
    }

    private void SaveGame()
    {
        Level[] levels = new Level[_levelsData.Lenght];

        for (int i = 0; i < levels.Length; i++)
        {
            levels[i] = _levelsData.GetLevelByIndex(i);
        }

        _save.Levels = levels;
        File.WriteAllText(_path, JsonUtility.ToJson(_save));
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}

[System.Serializable]
public class SaveData
{
    public Level[] Levels;
}
