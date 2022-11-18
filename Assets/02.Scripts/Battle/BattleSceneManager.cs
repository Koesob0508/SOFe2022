using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class BattleSceneManager : MonoBehaviour
{
    private Canvas BattleCanvas = null;
    private Button startBtn = null;
    private Sprite backImg = null;
    private Image transition = null;

    private List<Hero> hDataList = new List<Hero>();
    private List<Hero> hDataList_Original = new List<Hero>();

    private List<Enemy> EnemyList = new List<Enemy>();


    /// <summary>
    /// Heros in scene
    /// </summary>
    public List<GameObject> heroObjects = new List<GameObject>();
    /// <summary>
    /// Heros in either scene and battle
    /// </summary>
    public List<GameObject> enemyObjects = new List<GameObject>();
    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    public Path.PathManager PathMgr = null;

    private BattleLogPanel bLogPanel;
    private HeroInvenPanel hInvenPanel;
    private BattleEndUI bEndPanel;
    private SynergyPanel synergyPanel;
    private uint hCount = 0;
    private uint eCount = 0;

    IObjectPool<GameObject> dmgPopupPool;

    bool bBattleStarted = false;

    public Action<BattleLogPanel.Log> LogDelegate;

    List<ObserverBase> Observers = new List<ObserverBase>();

    GameObject infoPopUp_Prefab;
    GameObject infoPopUp_Object;
    Hero curHeroInfoOpened;
    bool bIsInfoOpened;

    #region Initalize
    /// <summary>
    /// Call When BattleSelectScene Loaded to Initalize BattleSceneManager
    /// </summary>
    /// <param name="Heros"> Heros List that player owns</param>
    /// <param name="Enemies">Enemy List of this stage</param>
    public void Init(List<Hero> Heros, List<Enemy> Enemies, GameManager.MapType mapType)
    {
        Debug.Log("BattleManager Initalized");

        // Get UI Elements
        BattleCanvas = GameObject.FindGameObjectWithTag("UI").GetComponent<Canvas>();
        bLogPanel = BattleCanvas.GetComponentInChildren<BattleLogPanel>();
        hInvenPanel = BattleCanvas.GetComponentInChildren<HeroInvenPanel>();
        bEndPanel = BattleCanvas.GetComponentInChildren<BattleEndUI>();
        synergyPanel = BattleCanvas.GetComponentInChildren<SynergyPanel>();
        if (bEndPanel != null)
        {
            bEndPanel.gameObject.SetActive(false);
        }
        else
            Debug.Log("EndPanel Didnt Initalized");
        if(bLogPanel != null)
        {
            bLogPanel.gameObject.SetActive(false);
        }
        else
            Debug.Log("LogPanel Didnt Initalized");

        infoPopUp_Prefab = Resources.Load<GameObject>("Prefabs/UI/HeroInfo_PopUp");
        if (infoPopUp_Prefab == null)
            Debug.Log("InfoPopUp Didnt Initalized");
        else
        {
            infoPopUp_Object = Instantiate(infoPopUp_Prefab);
            infoPopUp_Object.transform.SetParent(GameObject.Find("Canvas_AboveChar").transform);


            RectTransform rt = infoPopUp_Object.GetComponent<RectTransform>();
            rt.localScale = Vector2.one;
            rt.anchorMin = new Vector2(0.1f, 0.7f);
            rt.anchorMax = new Vector2(0.3f, 1f);
            rt.offsetMax = Vector2.zero;
            rt.offsetMin = Vector2.zero;

            infoPopUp_Object.SetActive(false);
        }


        //Init Hero
        hInvenPanel.Initalize(Heros);
        //Setup Start Btn
        startBtn = GameObject.Find("StartBtn").GetComponent<Button>();
        startBtn.onClick.AddListener(delegate { StartBattle(); });
        startBtn.gameObject.SetActive(false);


        dmgPopupPool = new ObjectPool<GameObject>(
            Create_DmgPopup,
            OnGet_DmgPopup,
            OnRelease__DmgPopup,
            OnDestroy__DmgPopup);

        //Get Hero and Enemy
        hDataList = Heros;
        foreach(var h in hDataList)
        {
            hDataList_Original.Add(h.DeepCopy());
        }
        EnemyList = Enemies;
        eCount = (uint)EnemyList.Count;

        //Init Enemy
        for (int i = 0; i < eCount; i++)
        {
            GameObject e = Resources.Load<GameObject>("Prefabs/GlobalObjects/" + EnemyList[i].GUID);
            GameObject eTemp = Instantiate(e, EnemyList[i].Position, new Quaternion());
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
        var ob = new Observer_Battle();
        ob.Init();
        Observers.Add(ob);
        LogDelegate += AddLog;
    }

    #endregion
    #region Publlic Methods
    
    /// <summary>
    /// Apply Buff to Hero
    /// </summary>
    /// <param name="type">Attack or AttackSpeed</param>
    /// <param name="hero"></param>
    /// <param name="value"></param>
    /// <param name="remainTime"></param>
    public void ApplyBuff(string type, Hero hero, float value, float remainTime = 0.0f)
    {
        GameObject g = heroObjects.Find((h) => { return h.GetComponent<Battle_Heros>().charData.GUID == hero.GUID; });
        g.GetComponent<Battle_Heros>().Buff(type,value);
    }

    public void HeroInvenItemClicked(Hero heroData, Vector2 pos)
    {
        if(curHeroInfoOpened != heroData)
        {
            curHeroInfoOpened = heroData;
            infoPopUp_Object.SetActive(true);
            RectTransform rt = infoPopUp_Object.GetComponent<RectTransform>();
            float offset = Screen.height - pos.y - rt.rect.width / 2;
            if (offset < 0)
                offset = 0;
            rt.offsetMax = new Vector2(0, -offset);
            rt.offsetMin = new Vector2(0, -offset);
            infoPopUp_Object.GetComponent<HeroInfo_PopUp>().SetUpData(heroData);
            bIsInfoOpened = true;
        }
        else
        {
            if (!bIsInfoOpened)
            {
                curHeroInfoOpened = heroData;
                infoPopUp_Object.SetActive(true);
                RectTransform rt = infoPopUp_Object.GetComponent<RectTransform>();
                float offset = Screen.height - pos.y - rt.rect.width / 2;
                rt.offsetMax = new Vector2(0, -offset);
                rt.offsetMin = new Vector2(0, -offset);
                infoPopUp_Object.GetComponent<HeroInfo_PopUp>().SetUpData(heroData);
                bIsInfoOpened = true;
            }
            else
            {
                ClosePopUp();
            }
        }
    }
    public void ClosePopUp()
    {
        infoPopUp_Object.SetActive(false);
        bIsInfoOpened = false;
    }
    public void RestoreHeroData(Hero hero)
    {
        Hero hData = hDataList_Original.Find((obj) => { return obj.GUID == hero.GUID; });
        Hero hDataToChange = hDataList.Find((obj) => { return obj.GUID == hero.GUID; });
        hDataToChange.MaxHP = hData.MaxHP;
        hDataToChange.CurrentHP = hData.CurrentHP;
    }
    public GameObject GetHeroGameObject(Character Hero)
    {
        return heroObjects.Find((h) => { return h.GetComponent<Units>().charData.GUID == Hero.GUID; });
    }
    public void SetHeroOnBattle(GameObject Hero)
    {
        hCount++;
        heroObjects.Add(Hero);
        startBtn.gameObject.SetActive(true);

        foreach (var obj in Observers)
            obj.onNotify(ObserverBase.EventType.R_Enroll, new UnityEngine.Object[] { Hero });

    }
    public void DeleteHeroOnBattle(GameObject Hero)
    {
        hCount--;
        heroObjects.Remove(Hero);
        if (heroObjects.Count == 0)
            startBtn.gameObject.SetActive(false);
        foreach (var obj in Observers)
            obj.onNotify(ObserverBase.EventType.R_Dismiss, new UnityEngine.Object[] { Hero });
    }
    public void GenerateHitEvent(GameObject Causer, GameObject Target, float Dmg)
    {
        var targetUnitComp = Target.GetComponent<Units>();
        var causerUnitComp = Causer.GetComponent<Units>();

        // 얘네도 피격 시점으로 수정 해줘야 함
        // bLogPanel.AddLog(new System.Tuple<Character, float, Character>(causerUnitComp.charData, Dmg, targetUnitComp.charData));
        MakeDamagePopup(Target.transform.position, Dmg);
    }
    public GameObject CreateHero(Hero heroData)
    {
        GameObject h = Resources.Load<GameObject>("Prefabs/GlobalObjects/" + heroData.GUID);
        GameObject hObj = Instantiate(h);
        Units tempU = hObj.GetComponent<Units>();
        tempU.Initalize(heroData);
        hObj.SetActive(false);
        return hObj;
    }
    public void DeadProcess(GameManager.ObjectType type, GameObject target, Character causer)
    {
        spriteRenderers.Remove(target.GetComponent<SpriteRenderer>());
        switch (type)
        {
            case GameManager.ObjectType.Hero:
                {
                    bLogPanel.AddLog(new BattleLogPanel.Log(causer, target.GetComponent<Units>().charData, BattleLogPanel.LogType.Kill));
                    hCount -= 1;
                    break;
                }
            case GameManager.ObjectType.Enemy:
                {
                    bLogPanel.AddLog(new BattleLogPanel.Log(causer, target.GetComponent<Units>().charData, BattleLogPanel.LogType.Dead));
                    eCount -= 1;
                    break;
                }
            default:
                break;
        }
        if (hCount == 0)
        {
            StartCoroutine("ActivateBattleEndUI", false);
        }
        if (eCount == 0)
        {
            StartCoroutine("ActivateBattleEndUI", true);
        }
    }
    public void FinishBattle(bool bIsWin)
    {
        // GameManager.Data.Save();

        bBattleStarted = false;
        foreach(GameObject g in heroObjects)
        {
            g.GetComponent<Battle_Heros>().ReduceHunger(10);
        }
        if (bIsWin)
        {
            UpdateHeroData();
            GameManager.Stage.CompleteStage();
            GameManager.Scene.ToStageSelectScene();
        }
        else
        {
            GameManager.Scene.ToTownScene();
        }

        GameManager.Data.Save();
    }
    public void AddSynergyUIText(Observer_Battle.SynergyEvent type)
    {
        synergyPanel.AddSynergy(type);
    }
    public void RemoveSynergyUIText(Observer_Battle.SynergyEvent type)
    {
        synergyPanel.RemoveSynergy(type);
    }
    #endregion
    #region Private Methods
    #region DamagePopup
    void MakeDamagePopup(Vector2 pos, float Damage)
    {
        GameObject obj = dmgPopupPool.Get();

        RectTransform rt = obj.GetComponent<RectTransform>();
        DamagePopUp dmgPopup = obj.GetComponent<DamagePopUp>();
        TMPro.TextMeshProUGUI text = obj.GetComponent<TMPro.TextMeshProUGUI>();


        Vector2 AnchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(BattleCanvas.gameObject.GetComponent<RectTransform>(), Camera.main.WorldToScreenPoint(pos), Camera.main, out AnchoredPos);
        rt.anchoredPosition = AnchoredPos;
        rt.localScale = Vector3.one;

        text.SetText("-" + Damage);
        text.color = Color.red;
        text.alignment = TMPro.TextAlignmentOptions.Center;

        dmgPopup.SetPool(dmgPopupPool);
        dmgPopup.StartAnimation();

    }
    GameObject Create_DmgPopup()
    {
        GameObject obj = new GameObject("Dmg_Popup");
        obj.transform.SetParent(BattleCanvas.transform);

        obj.AddComponent<DamagePopUp>();
        obj.AddComponent<RectTransform>();
        obj.AddComponent<TMPro.TextMeshProUGUI>();

        obj.SetActive(false);
        return obj;
    }
    void OnGet_DmgPopup(GameObject popup)
    {
        popup.SetActive(true);
    }
    void OnRelease__DmgPopup(GameObject popup)
    {
        popup.SetActive(false);
    }
    void OnDestroy__DmgPopup(GameObject popup)
    {
        Destroy(popup);
    }
    #endregion

    void AddLog(BattleLogPanel.Log log)
    {
        bLogPanel.AddLog(log);
    }
    void AlignUnitsByY()
    {
        spriteRenderers.Sort((lhs, rhs) => { return rhs.gameObject.transform.position.y.CompareTo(lhs.gameObject.transform.position.y); });
        for (int i = 0; i < spriteRenderers.Count; i++)
            spriteRenderers[i].sortingOrder = -100 + i;
    }
    void UpdateHeroData()
    {
        foreach (Hero h in hDataList)
        {
            Hero originData = hDataList_Original.Find((obj) => { return obj.GUID == h.GUID; });
            if (originData != null)
                h.MaxHP = originData.MaxHP;
            GameManager.Data.ObjectCodex[h.GUID] = h;
        }
    }
    void SetBackground(GameManager.MapType mapType)
    {
        string mapName = "";
        switch (mapType)
        {
            case GameManager.MapType.Boss:
                {
                    mapName = "StatueForest_Pale.png";
                    break;
                }
            case GameManager.MapType.Dessert:
                {
                    mapName = "Dessert_Pale.png";
                    break;
                }
            case GameManager.MapType.Jungle:
                {
                    mapName = "BigTreeForest_Pale.png";
                    break;
                }
            default:
                throw new System.Exception("Undefined Map Type!");
        }
        backImg = GameManager.Data.LoadSprite("/Sprites/Maps/" + mapName);
    }

    void StartBattle()
    {
        foreach (var hero in heroObjects)
        {
           
            spriteRenderers.Add(hero.GetComponentInChildren<SpriteRenderer>());
            hero.GetComponent<Units>().StartBattle();
            for (int i = 0; i < hero.transform.childCount; i++)
            {
                Transform tempG = hero.transform.GetChild(i);
                if (tempG != null & tempG.gameObject.CompareTag("CharOffset"))
                {
                    GameManager.Battle.PathMgr.AddObstacle(tempG.gameObject.GetComponent<Collider2D>());
                }
            }
        }
        foreach (var enemy in enemyObjects)
        {
            spriteRenderers.Add(enemy.GetComponent<SpriteRenderer>());
            enemy.GetComponent<Units>().StartBattle();
        }
        startBtn.gameObject.SetActive(false);
        hInvenPanel.gameObject.SetActive(false);
        bLogPanel.gameObject.SetActive(true);

        ClosePopUp();

        bBattleStarted = true;
    }


  
    void Update()
    {
        if(bBattleStarted)
            AlignUnitsByY();
    }
    #endregion
    #region Coroutine
    IEnumerator ActivateBattleEndUI(bool bIsWin)
    {
        Debug.Log("Started");
        Time.timeScale = 0.3f;
        yield return new WaitForSeconds(1);
        Time.timeScale = 1f;
        bEndPanel.gameObject.SetActive(true);
        bEndPanel.Initalize(bIsWin);
        yield break;
    }
    IEnumerator FadeInTransition(Color col)
    {
        Material mat = transition.material;
        mat.SetColor("_NoiseCol", col);
        float prog = 0.0f;
        while (prog < 1.5f)
        {
            prog += 0.03f;
            mat.SetFloat("_Progress", prog);
            // Debug.Log(prog);
            yield return new WaitForSeconds(0.01f);
        }
        transition.gameObject.SetActive(false);
        yield break;
    }
    #endregion
}
