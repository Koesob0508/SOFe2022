using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager
{
    public void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("Load");
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "BattleSelectScene":
                GameManager.Battle = new BattleSceneManager();
                break;

            case "TestInitScene":
                Debug.Log("This is Init Game Scene");
                break;

            case "TestStageSelectScene":
                Debug.Log("This is Stage Select Scene");
                break;

            case "TestBattleScene":
                Debug.Log("This is Battle Scene");
                break;

            default:
                break;
        }
    }

    public void ToStageSelectScene()
    {
        SceneManager.LoadScene("TestStageSelectScene");
    }
    public void ToInitGameScene()
    {
        SceneManager.LoadScene("TestInitScene");
    }

    public void ToBattleScene()
    {
        SceneManager.LoadScene("TestBattleScene");
    }
}
