using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneManager : MonoBehaviour
{
    private Canvas BattleCanvas = null;
    private Button startBtn = null;
    private Sprite backImg = null;
    private Image transition = null;

    private List<Hero> HeroList = new List<Hero>();
    private List<Enemy> EnemyList = new List<Enemy>();

    public List<GameObject> heroObjects = new List<GameObject>();
    public List<GameObject> enemyObjects = new List<GameObject>();

    public Path.PathManager PathMgr = null;
    private List<Vector2> tmpPosHero = new List<Vector2>();
    private List<Vector2> tmpPosEnemy = new List<Vector2>();


    public BattleLogPanel bLogPanel;
    public HeroInvenPanel hInvenPanel;


    private uint hCount = 0;
    private uint eCount = 0;




    /// <summary>
    /// Call When BattleSelectScene Loaded to Initalize BattleSceneManager
    /// </summary>
    /// <param name="Heros"> Heros List that player owns</param>
    /// <param name="Enemies">Enemy List of this stage</param>
    public void Init(GameManager.MapType mapType)
    {
        Debug.Log("BattleManager Initalized");
        SetBackground(mapType);

        BattleCanvas = FindObjectOfType<Canvas>();

        PathMgr = new Path.PathManager();
        PathMgr.Init(backImg);
    }
    /// <summary>
    /// Call When BattleSelectScene Loaded to Initalize BattleSceneManager
    /// </summary>
    /// <param name="Heros"> Heros List that player owns</param>
    /// <param name="Enemies">Enemy List of this stage</param>
    public void Init(List<Hero> Heros, List<Enemy> Enemies, GameManager.MapType mapType)
    {

        Debug.Log("BattleManager Initalized");

        // Get UI Elements
        BattleCanvas = FindObjectOfType<Canvas>();

        bLogPanel = BattleCanvas.GetComponentInChildren<BattleLogPanel>();
        hInvenPanel = BattleCanvas.GetComponentInChildren<HeroInvenPanel>();

        bLogPanel.gameObject.SetActive(false);

        //Init Hero
        hInvenPanel.Initalize(Heros);

        //Setup Start Btn
        startBtn = GameObject.Find("StartBtn").GetComponent<Button>();
        startBtn.onClick.AddListener(delegate { StartBattle(); });

        //temp
        tmpPosHero.Add(new Vector2(-3.8f, 0f));
        tmpPosHero.Add(new Vector2(-8f, -2.5f));
        //tmpPosHero.Add(new Vector2(-4f, -4f));
        tmpPosEnemy.Add(new Vector2(1.8f, 0f));
        tmpPosEnemy.Add(new Vector2(4.5f, -2.5f));
        tmpPosEnemy.Add(new Vector2(8f, -4f));

        //Get Hero and Enemy
        HeroList = Heros;
        EnemyList = Enemies;

        hCount = (uint)HeroList.Count;
        eCount = (uint)EnemyList.Count;

        //Init Enemy
        for(int i = 0; i < eCount; i++)
        {
            GameObject e = Resources.Load<GameObject>("Prefabs/GlobalObjects/" + EnemyList[i].GUID);
            GameObject eTemp = Instantiate(e, tmpPosEnemy[i], new Quaternion());
            Units tempU = eTemp.GetComponent<Units>();
            tempU.Initalize(EnemyList[i]);
            enemyObjects.Add(eTemp);
        }


        // Set Background
        SetBackground(mapType);


        // Setup PathManager
        PathMgr = GetComponent<Path.PathManager>();
        PathMgr.Init(backImg);


  

        // Setup and Play Transition
        transition = GameObject.Find("Transition").GetComponent<Image>();
        Color fadeColor;
        switch (mapType)
        {
            case GameManager.MapType.Jungle:
                fadeColor = new Color(0.71f, 0.9f, 0.11f);
                break;
            case GameManager.MapType.Dessert:
                fadeColor = new Color(0.9f, 0.87f, 0.48f);
                break;
            case GameManager.MapType.Boss:
                fadeColor = new Color(0.9f, 0.25f, 0.22f);
                break;
            default:
                fadeColor = Color.white;
                break;
        }
        StartCoroutine(FadeInTransition(fadeColor));
    }
   
    IEnumerator FadeInTransition(Color col)
    {
        Material mat = transition.material;
        mat.SetColor("_NoiseCol",col);
        float prog = 0.0f;
        while(prog < 1.5f)
        {
            prog += 0.03f;
            mat.SetFloat("_Progress", prog);
            // Debug.Log(prog);
            yield return new WaitForSeconds(0.01f);
        }
        transition.gameObject.SetActive(false);
        yield break;
    }

    void SetBackground(GameManager.MapType mapType)
    {
        string mapName = "";

        switch (mapType)
        {
            case GameManager.MapType.Boss:
                {
                    mapName = "Forest_Dark.png";
                    break;
                }
            case GameManager.MapType.Dessert:
                {
                    mapName = "Dessert.png";
                    break;
                }
            case GameManager.MapType.Jungle:
                {
                    mapName = "Forest_Bright.png";
                    break;
                }
            default:
                throw new System.Exception("Undefined Map Type!");
        }
        backImg = GameManager.Data.LoadSprite("/Sprites/Maps/" + mapName);
      
    }

    void StartBattle()
    {
        foreach(var hero in heroObjects)
        {
            hero.GetComponent<Units>().StartBattle();
        }
        foreach (var enemy in enemyObjects)
        {
            enemy.GetComponent<Units>().StartBattle();
        }
        startBtn.gameObject.SetActive(false);
        hInvenPanel.gameObject.SetActive(false);
        bLogPanel.gameObject.SetActive(true);
        
    }

    public GameObject CreateHero(Hero heroData)
    {
        GameObject h = Resources.Load<GameObject>("Prefabs/GlobalObjects/" + heroData.GUID);
        GameObject hObj =  Instantiate(h);
        Units tempU = hObj.GetComponent<Units>();
        tempU.Initalize(heroData);

        hObj.SetActive(false);
        return hObj;
    }

    public void SetHeroOnBattle(GameObject Hero)
    {
        heroObjects.Add(Hero);
    }
    public void DeleteHeroOnBattle(GameObject Hero)
    {
        heroObjects.Remove(Hero);
    }
    public void GenerateHit(GameObject Causer, GameObject Target, float Dmg)
    {
        var targetUnitComp = Target.GetComponent<Units>();
        var causerUnitComp = Causer.GetComponent<Units>();
        //if (targetUnitComp != null)
        //{
        //    causerUnitComp.Attack();
        //    targetUnitComp.Hit(Dmg);
        //}
        bLogPanel.AddLog(new System.Tuple<Character, float, Character>(causerUnitComp.charData, Dmg, targetUnitComp.charData));
    }

    /// <summary>
    /// Damage POPUP_UI Instantiate ( OBJ Pool ���뿹�� )
    /// </summary>
    /// <param name="pos">������ġ</param>
    /// <param name="dur">���ӽð�</param>
    /// <param name="Dmg">������ ��</param>
    /// <returns></returns>
    IEnumerator DamageUI(Vector2 pos, float dur, float Dmg)
    {

        yield break;
    }

    public void DeadProcess(GameManager.ObjectType type)
    {
        switch (type)
        {
            case GameManager.ObjectType.Hero:
                hCount -= 1;
                break;
            case GameManager.ObjectType.Enemy:
                eCount -= 1;
                break;
            default:
                break;
        }

        if(hCount == 0)
        {
            FinishBattle(false);
        }
        if(eCount == 0)
        {
            FinishBattle(true);
        }
    }
    void FinishBattle(bool bIsWin)
    {
        if(bIsWin)
        {

        }
        else
        {

        }

    }
    void Update()
    {
    }

}
