using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    private List<Hero> HeroList = new List<Hero>();
    private List<Enemy> EnemyList = new List<Enemy>();

    public List<GameObject> heroObjects = new List<GameObject>();
    public List<GameObject> enemyObjects = new List<GameObject>();

    public Path.PathManager PathMgr = null;
    Sprite backImg = null;
    private List<Vector2> tmpPosHero = new List<Vector2>();
    private List<Vector2> tmpPosEnemy = new List<Vector2>();


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

        PathMgr = new Path.PathManager();
        PathMgr.Init(backImg);
    }
    public void Init(List<Hero> Heros, List<Enemy> Enemies, GameManager.MapType mapType)
    {
        Debug.Log("BattleManager Initalized");
        tmpPosHero.Add(new Vector2(-3.8f, 0f));
        tmpPosHero.Add(new Vector2(-8f, -2.5f));
        tmpPosEnemy.Add(new Vector2(3.8f, 0f));
        tmpPosEnemy.Add(new Vector2(8f, -2.5f));

        HeroList = Heros;
        EnemyList = Enemies;

        hCount = (uint)HeroList.Count;
        eCount = (uint)EnemyList.Count;

        for(int i = 0; i < Heros.Count; i++)
        {
            GameObject h = Resources.Load<GameObject>("Prefabs/GlobalObjects/" + HeroList[i].GUID);
            GameObject e = Resources.Load<GameObject>("Prefabs/GlobalObjects/" + EnemyList[i].GUID);

            GameObject hTemp = Instantiate(h, tmpPosHero[i], new Quaternion());
            GameObject eTemp = Instantiate(e, tmpPosEnemy[i], new Quaternion());

            Units tempU = hTemp.GetComponent<Units>();
            Units tempU2 = eTemp.GetComponent<Units>();

            tempU.Initalize(HeroList[i]);
            tempU2.Initalize(EnemyList[i]);

            heroObjects.Add(hTemp);
            enemyObjects.Add(eTemp);
        }

        SetBackground(mapType);

        PathMgr = GetComponent<Path.PathManager>();
        PathMgr.Init(backImg);


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

        byte[] bytes = GameManager.Instance.LoadFile("/Sprites/Maps/" + mapName);

        if (bytes.Length > 0)
        {
            Texture2D tex = new Texture2D(0, 0);
            tex.LoadImage(bytes);
            
            backImg = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }
    }


    public void GenerateHit(GameObject Causer, GameObject Target, float Dmg)
    {
        var targetUnitComp = Target.GetComponent<Units>();
        if (targetUnitComp != null)
        {
            targetUnitComp.GetDamage(Dmg);
        }
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

    public void DeadProcess(Character charData)
    {
        switch (charData.Type)
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
        Debug.Log("HERO LIST HP : " + HeroList[0].CurrentHP);
    }



}
