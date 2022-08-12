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
                {
                    GameObject obj = new GameObject("BattleManager");
                    obj.AddComponent<Path.PathManager>();
                    GameManager.Battle = obj.AddComponent<BattleSceneManager>();
                    Hero h1 = (Hero)GameManager.Instance.ObjectCodex.Find((elem) => { return elem.GUID == 0; });
                    Hero h2 = (Hero)GameManager.Instance.ObjectCodex.Find((elem) => { return elem.GUID == 1; });
                    Enemy e1 = (Enemy)GameManager.Instance.ObjectCodex.Find((elem) => { return elem.GUID == 100; });
                    Enemy e2 = (Enemy)GameManager.Instance.ObjectCodex.Find((elem) => { return elem.GUID == 100; });
                    List<Hero> hlist = new List<Hero>();
                    List<Enemy> elist = new List<Enemy>();
                    hlist.Add(h1);
                    hlist.Add(h2);
                    elist.Add(e1);
                    elist.Add(e2);
                    GameManager.Battle.Init(hlist,elist,GameManager.MapType.Boss);

                }
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
        SceneManager.LoadScene("GameStartScene");
    }

    public void ToBattleScene()
    {
        SceneManager.LoadScene("BattleSelectScene");
    }
}
