using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager
{
    public void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("SceneLoaded Event Init");
    }

    public void Clear()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Debug.Log("SceneLoaded Evetn Clear");
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "00.StartScene":
                Debug.Log("This is Start Scene");
                break;

            case "01.StageSelectScene":
                Debug.Log("This is Stage Select Scene");
                // 이제 여기다가 Stage Show 함수 넣어두면 될듯
                break;

            case "02.BattleSelectScene":
                Debug.Log("This is Battle Scene");
                GameObject obj = new GameObject("BattleManager");
                GameManager.Battle = obj.AddComponent<BattleSceneManager>();
                obj.AddComponent<Path.PathManager>();

                Hero h1 = (Hero)GameManager.Instance.ObjectCodex.Find((elem) => { return elem.GUID == 0; });
                Hero h2 = (Hero)GameManager.Instance.ObjectCodex.Find((elem) => { return elem.GUID == 1; });
                Hero h3 = (Hero)GameManager.Instance.ObjectCodex.Find((elem) => { return elem.GUID == 12; });
                Enemy e1 = (Enemy)GameManager.Instance.ObjectCodex.Find((elem) => { return elem.GUID == 100; });
                Enemy e2 = (Enemy)GameManager.Instance.ObjectCodex.Find((elem) => { return elem.GUID == 100; });
                Enemy e3 = (Enemy)GameManager.Instance.ObjectCodex.Find((elem) => { return elem.GUID == 100; });
                List<Hero> hlist = new List<Hero>();
                List<Enemy> elist = new List<Enemy>();
                hlist.Add(h1);
                hlist.Add(h2);
                hlist.Add(h3);
                elist.Add(e1);
                elist.Add(e2);
                elist.Add(e3);
                GameManager.Battle.Init(hlist, elist, GameManager.MapType.Jungle);

                break;

            case "03.TownScene":
                Debug.Log("This is Town Scene");
                break;

            case "04.EvevntScene":
                Debug.Log("This is Event Scene");
                break;

            default:
                break;
        }
    }

    public void ToInitGameScene()
    {
        SceneManager.LoadScene("00.StartScene");
    }

    public void ToStageSelectScene()
    {
        SceneManager.LoadScene("01.StageSelectScene");
    }

    public void ToBattleScene()
    {
        SceneManager.LoadScene("02.BattleSelectScene");
    }

    public void ToTownScene()
    {
        SceneManager.LoadScene("03.TownScene");
    }

    public void ToEventScene()
    {
        SceneManager.LoadScene("04.EventScene");
    }
}
