using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneManager : MonoBehaviour
{
    private Canvas battleUI = null;
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

    private BattleLogPanel LogPanel;

    private uint hCount = 0;
    private uint eCount = 0;

    private Dictionary<uint, Sprite> unitUIImage = new Dictionary<uint, Sprite>();

    /// <summary>
    /// Call When BattleSelectScene Loaded to Initalize BattleSceneManager
    /// </summary>
    /// <param name="Heros"> Heros List that player owns</param>
    /// <param name="Enemies">Enemy List of this stage</param>
    public void Init(GameManager.MapType mapType)
    {
        Debug.Log("BattleManager Initalized");
        SetBackground(mapType);

        battleUI = FindObjectOfType<Canvas>();

        PathMgr = new Path.PathManager();
        PathMgr.Init(backImg);
    }
    public void Init(List<Hero> Heros, List<Enemy> Enemies, GameManager.MapType mapType)
    {

        Debug.Log("BattleManager Initalized");

        LogPanel = GameObject.Find("BattleLogPanel").GetComponent<BattleLogPanel>();
        tmpPosHero.Add(new Vector2(-3.8f, 0f));
        //tmpPosHero.Add(new Vector2(-8f, -2.5f));
        //tmpPosHero.Add(new Vector2(-4f, -4f));
        tmpPosEnemy.Add(new Vector2(1.8f, 0f));
        tmpPosEnemy.Add(new Vector2(4.5f, -2.5f));
        //tmpPosEnemy.Add(new Vector2(8f, -4f));

        HeroList = Heros;
        EnemyList = Enemies;

        hCount = (uint)HeroList.Count;
        eCount = (uint)EnemyList.Count;

        Debug.Log("Heros: " + hCount  + "Enemys: " + eCount);

        for(int i = 0; i < hCount; i++)
        {
            GameObject h = Resources.Load<GameObject>("Prefabs/GlobalObjects/" + HeroList[i].GUID);
            GameObject hTemp = Instantiate(h, tmpPosHero[i], new Quaternion());
            Battle_Heros tempU = hTemp.GetComponent<Battle_Heros>();
            tempU.Initalize(HeroList[i]);
            heroObjects.Add(hTemp);
            Debug.Log(tempU.charData.GUID);
            unitUIImage.Add(tempU.charData.GUID, LoadSprite("/Sprites/HeroUI/" + tempU.charData.GUID + "_UI.png"));
        }

        for(int i =0; i<eCount; i++)
        {
            GameObject e = Resources.Load<GameObject>("Prefabs/GlobalObjects/" + EnemyList[i].GUID);
            GameObject eTemp = Instantiate(e, tmpPosEnemy[i], new Quaternion());
            Battle_Enemys tempU2 = eTemp.GetComponent<Battle_Enemys>();
            tempU2.Initalize(EnemyList[i]);
            enemyObjects.Add(eTemp);
            if (!unitUIImage.ContainsKey(tempU2.charData.GUID))
                unitUIImage.Add(tempU2.charData.GUID, LoadSprite("/Sprites/MonsterUI/" + tempU2.charData.GUID + "_UI.png"));
        }

        SetBackground(mapType);

        battleUI = FindObjectOfType<Canvas>();
        startBtn = GameObject.Find("StartBtn").GetComponent<Button>();
        transition = GameObject.Find("Transition").GetComponent<Image>();
        startBtn.onClick.AddListener( delegate { StartBattle(); });
        PathMgr = GetComponent<Path.PathManager>();
        PathMgr.Init(backImg);

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
    
    Sprite LoadSprite(string path)
    {
        byte[] bytes = GameManager.Instance.LoadFile(path);
        Sprite Image = null;
        if (bytes.Length > 0)
        {
            Texture2D tex = new Texture2D(0, 0);
            tex.LoadImage(bytes);

            Image = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }
        return Image;
    }
    public Sprite GetUIImage(uint guid)
    {
        if (unitUIImage.ContainsKey(guid))
            return unitUIImage[guid];
        else
            throw new System.Exception(guid + "_UI.png Image Doenst Exist");
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
        backImg = LoadSprite("/Sprites/Maps/" + mapName);
        //byte[] bytes = GameManager.Instance.LoadFile();

        //if (bytes.Length > 0)
        //{
        //    Texture2D tex = new Texture2D(0, 0);
        //    tex.LoadImage(bytes);
            
        //    backImg = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        //}
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
    }
    public void GenerateHit(GameObject Causer, GameObject Target, float Dmg)
    {
        var targetUnitComp = Target.GetComponent<Units>();
        var causerUnitComp = Causer.GetComponent<Units>();
        if (targetUnitComp != null)
        {
            causerUnitComp.Attack();
            targetUnitComp.Hit(Dmg);
        }
        LogPanel.AddLog(new System.Tuple<Character, float, Character>(targetUnitComp.charData, Dmg, causerUnitComp.charData));
    }

    /// <summary>
    /// Damage POPUP_UI Instantiate ( OBJ Pool 적용예정 )
    /// </summary>
    /// <param name="pos">만들위치</param>
    /// <param name="dur">지속시간</param>
    /// <param name="Dmg">데미지 양</param>
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
