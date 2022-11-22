using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
                foreach(var button in GameObject.FindObjectsOfType<Button>())
                {
                    button.onClick.AddListener(delegate { GameManager.Sound.PlayButtonSound(); });
                }
                break;

            case "01.UserInputScene":
                Debug.Log("This is User Input Scene");
                GameManager.Stage.HideStageMap();
                foreach (var button in GameObject.FindObjectsOfType<Button>())
                {
                    button.onClick.AddListener(delegate { GameManager.Sound.PlayButtonSound(); });
                }
                break;

            case "02.StageSelectScene":
                Debug.Log("This is Stage Select Scene");
                // ���� ����ٰ� Stage Show �Լ� �־�θ� �ɵ�
                if(GameManager.Stage.IsToNextStage())
                {
                    GameManager.Stage.ToNextStage();
                }
                GameManager.Stage.ShowStageMap();
                foreach (var button in GameObject.FindObjectsOfType<Button>())
                {
                    button.onClick.AddListener(delegate { GameManager.Sound.PlayButtonSound(); });
                }
                break;

            case "03.BattleSelectScene":
                Debug.Log("This is Battle Scene");

                // GameManager.Stage.Test_SetBattleStage();
                GameManager.Stage.HideStageMap();
                foreach (var button in GameObject.FindObjectsOfType<Button>())
                {
                    button.onClick.AddListener(delegate { GameManager.Sound.PlayButtonSound(); });
                }
                GameObject obj = new GameObject("BattleManager");
                GameManager.Battle = obj.AddComponent<BattleSceneManager>();
                obj.AddComponent<Path.PathManager>();
                Hero h1 = (Hero)GameManager.Data.ObjectCodex[0];
                Hero h2 = (Hero)GameManager.Data.ObjectCodex[1];
                Hero h3 = (Hero)GameManager.Data.ObjectCodex[2];
                Hero h4 = (Hero)GameManager.Data.ObjectCodex[3];
                Hero h5 = (Hero)GameManager.Data.ObjectCodex[4];
                Hero h6 = (Hero)GameManager.Data.ObjectCodex[5];
                Hero h7 = (Hero)GameManager.Data.ObjectCodex[6];
                Hero h8 = (Hero)GameManager.Data.ObjectCodex[7];
                Hero h9 = (Hero)GameManager.Data.ObjectCodex[8];
                Hero h10 = (Hero)GameManager.Data.ObjectCodex[9];
                Hero h11 = (Hero)GameManager.Data.ObjectCodex[10];
                Hero h12 = (Hero)GameManager.Data.ObjectCodex[11];
                Hero h13 = (Hero)GameManager.Data.ObjectCodex[12];
                Hero h14 = (Hero)GameManager.Data.ObjectCodex[13];
                Hero h15 = (Hero)GameManager.Data.ObjectCodex[14];
                Hero h16 = (Hero)GameManager.Data.ObjectCodex[15];
                Hero h17 = (Hero)GameManager.Data.ObjectCodex[16];
                Hero h18 = (Hero)GameManager.Data.ObjectCodex[17];
                Hero h19 = (Hero)GameManager.Data.ObjectCodex[18];
                Hero h20 = (Hero)GameManager.Data.ObjectCodex[19];
                List<Hero> hlist = new List<Hero>();
                hlist.Add(h1);
                hlist.Add(h2);
                hlist.Add(h3);
                hlist.Add(h4);
                hlist.Add(h5);
                hlist.Add(h6);
                hlist.Add(h7);
                hlist.Add(h8);
                hlist.Add(h9);
                hlist.Add(h10);
                hlist.Add(h11);
                hlist.Add(h12);
                hlist.Add(h13);
                hlist.Add(h14);
                hlist.Add(h15);
                hlist.Add(h16);
                hlist.Add(h17);
                hlist.Add(h18);
                hlist.Add(h19);
                hlist.Add(h20);

                List<Enemy> elist = GameManager.Stage.GetEnemies();

                GameManager.MapType mapType = GameManager.Stage.GetMapType();
                GameManager.Battle.Init(hlist, elist, mapType);
                break;

            case "04.TownScene":
                foreach (var button in GameObject.FindObjectsOfType<Button>())
                {
                    button.onClick.AddListener(delegate { GameManager.Sound.PlayButtonSound(); });
                }
                Debug.Log("This is Town Scene");
                GameManager.Stage.HideStageMap();
                break;

            case "05.EventScene":
                foreach (var button in GameObject.FindObjectsOfType<Button>())
                {
                    button.onClick.AddListener(delegate { GameManager.Sound.PlayButtonSound(); });
                }
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

    public void ToUserInputScene()
    {
        SceneManager.LoadScene("01.UserInputScene");
    }

    public void ToStageSelectScene()
    {
        SceneManager.LoadScene("02.StageSelectScene");
    }

    public void ToBattleScene()
    {
        SceneManager.LoadScene("03.BattleSelectScene");
    }

    public void ToTownScene()
    {
        SceneManager.LoadScene("04.TownScene");

        // Guild의 Random으로 3명의 Hero Set
        GameManager.Hero.SetGuildHero();

        // ItemShop의 오늘의 아이템 Lisr Set
        GameManager.Hero.SetShopItem();
    }

    public void ToEventScene()
    {
        SceneManager.LoadScene("05.EventScene");
    }
}
