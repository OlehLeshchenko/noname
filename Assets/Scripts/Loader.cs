using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene {
        MainMenuScene,
        GameScene,
        LoadingScene
    }
    public static int targetSceneIndex;

    public static void Load(string targetSceneName)
    {
        SceneManager.LoadScene(targetSceneName);
    }
}
