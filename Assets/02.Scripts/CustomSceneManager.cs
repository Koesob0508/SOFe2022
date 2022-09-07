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
                GameManager.Stage.HideStageMap();
                break;

            case "01.StageSelectScene":
                Debug.Log("This is Stage Select Scene");
                // ���� ����ٰ� Stage Show �Լ� �־�θ� �ɵ�
                GameManager.Stage.ShowStageMap();
                break;

            case "02.BattleSelectScene":
                Debug.Log("This is Battle Scene");

                GameManager.Stage.Test_SetBattleStage();
                GameManager.Stage.HideStageMap();

                GameObject obj = new GameObject("BattleManager");
                GameManager.Battle = obj.AddComponent<BattleSceneManager>();
                obj.AddComponent<Path.PathManager>();
                Hero h1 = (Hero)GameManager.Data.ObjectCodex[0];
                Hero h2 = (Hero)GameManager.Data.ObjectCodex[1];
                Hero h3 = (Hero)GameManager.Data.ObjectCodex[2];
                List<Hero> hlist = new List<Hero>();
                hlist.Add(h1);
                hlist.Add(h2);
                hlist.Add(h3);
                List<Enemy> elist = GameManager.Stage.GetEnemies();
                GameManager.Battle.Init(hlist, elist, GameManager.MapType.Boss);
                break;

            case "03.TownScene":
                Debug.Log("This is Town Scene");
                GameManager.Stage.HideStageMap();
                break;

            case "04.EvevntScene":
                Debug.Log("This is Event Scene");
                GameManager.Stage.HideStageMap();
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
