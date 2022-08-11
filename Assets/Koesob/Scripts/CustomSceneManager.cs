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
                GameObject obj = new GameObject("BattleManager");
                GameManager.Battle = obj.AddComponent<BattleSceneManager>();
                GameManager.Battle.Init(GameManager.MapType.Boss);
                break;

            case "TestInitScene":
                Debug.Log("This is Init Game Scene");
                break;

            case "TestStageSelectScene":
                Debug.Log("This is Stage Select Scene");
                // 이제 여기다가 Stage Show 함수 넣어두면 될듯
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
        SceneManager.LoadScene("GameStartScene");
    }

    public void ToBattleScene()
    {
        SceneManager.LoadScene("BattleSelectScene");
    }
}
