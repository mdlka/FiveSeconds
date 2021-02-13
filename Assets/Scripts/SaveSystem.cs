using System.IO;
using UnityEngine;
using System.Linq;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private LevelsData _levelsData;

    private Save _save = new Save();
    private string _path;

    private void Start()
    {
        _path = Path.Combine(Application.dataPath, "Save.json");

        if (File.Exists(_path))
        {
            _save = JsonUtility.FromJson<Save>(File.ReadAllText(_path));

            var openLevels = _save.Levels.TakeWhile(level => level.IsOpen == true);

            foreach (var level in openLevels)
            {
                _levelsData.OpenLevel(level.SceneName);
            }
        }
    }

    private void Save()
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
        Save();
    }
}

[System.Serializable]
public class Save
{
    public Level[] Levels;
}
