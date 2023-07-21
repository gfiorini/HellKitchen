using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene {
        MainMenuScene,
        GameScene,
        LoadingScene,
    }

    private static Scene targetScene;

    public static void Load(Scene targetScene) {
        Loader.targetScene = targetScene;
        
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
     }
    //chiamato da LoaderCallback
    public static void Callback() {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
