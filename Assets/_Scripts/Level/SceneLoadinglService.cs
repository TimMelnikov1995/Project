using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadinglService
{
    LevelInfo[] _levels;

    public SceneLoadinglService()
    {
        _levels = Resources.Load<LevelsPool>("LevelsPool").Levels;
    }

    public async UniTask<bool> TryLoadLevelByIndex(int index)
    {
        if (index >= _levels.Length)
            return false;

        var sceneLoading = SceneManager.LoadSceneAsync(_levels[index].SceneBuildName, LoadSceneMode.Single);

        if (sceneLoading.isDone)
            return true;

        return false;

    }

    public async UniTask LoadRandomLevel()
    {
        int rand = UnityEngine.Random.Range(0, _levels.Length);
        await TryLoadLevelByIndex(rand);
    }
}

[Serializable]
public class LevelInfo
{
    public string SceneBuildName;
}