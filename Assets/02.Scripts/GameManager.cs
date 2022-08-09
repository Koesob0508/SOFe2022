using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    public  static GameManager Instance { get { Init(); return instance; } }



    // ������ ���� ����ø� �˴ϴ�.
    #region Core
    [SerializeField] private StageManager _stage = new StageManager();
    [SerializeField] private BattleSceneManager _battle;
    [SerializeField] private HeroManager _hero;

    public static StageManager Stage { get { return Instance._stage; } }
    public static BattleSceneManager Battle { get { return Instance._battle; } }
    public static HeroManager Hero { get { return Instance._hero; } }



    #endregion

    private List<GlobalObject> ObjectCodex = new List<GlobalObject>();

    public enum MapType
    {
        Jungle,
        Dessert,
        Boss
    }

    private void Awake()
    {
        ImportCharData();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        Init();
    }

    private static void Init()
    {
        if(instance == null)
        {
            GameObject obj = GameObject.Find("Game Manager");

            if(obj == null)
            {
                obj = new GameObject { name = "Game Manager" };
                obj.AddComponent<GameManager>();
            }

            DontDestroyOnLoad(obj);
            instance = obj.GetComponent<GameManager>();

            // ���⼭���� ������ Manager�� Init�ϸ� �˴ϴ�.
            Stage.Init();
            Hero.Init();

        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "BattleSelectScene")
        {
            GameObject obj = new GameObject("BattleManager");
            _battle = obj.AddComponent<BattleSceneManager>();
            Battle.Init(MapType.Dessert);
        }
    }

    void ImportCharData()
    {
        CSVImporter csvImp = new CSVImporter();
        csvImp.OpenFile("Data/Heros_values");
        csvImp.ReadHeader();
        string line = csvImp.Readline();

        while (line != null)
        {
            string[] elems = line.Split(',');

            Hero hero = new Hero();
            hero.GUID = uint.Parse(elems[0]);
            hero.Name = elems[1];
            hero.MaxHP = float.Parse(elems[2]);
            hero.AttackDamage = float.Parse(elems[3]);
            hero.AttackSpeed = float.Parse(elems[4]);
            hero.DefensePoint = float.Parse(elems[5]);
            hero.MaxMana = float.Parse(elems[6]);
            hero.MoveSpeed = float.Parse(elems[7]);
            hero.AttackRange = float.Parse(elems[8]);
            hero.Type = ObjectType.Hero;
            line = csvImp.Readline();

            ObjectCodex.Add(hero);
        }

        CSVImporter csvImp1 = new CSVImporter();
        csvImp1.OpenFile("Data/Monsters_values");
        csvImp1.ReadHeader();
        string line1 = csvImp1.Readline();

        while (line1 != null)
        {
            string[] elems = line1.Split(',');

            Enemy enemy = new Enemy();
            enemy.GUID = uint.Parse(elems[0]);
            enemy.Name = elems[1];
            enemy.MaxHP = float.Parse(elems[2]);
            enemy.AttackDamage = float.Parse(elems[3]);
            enemy.AttackSpeed = float.Parse(elems[4]);
            enemy.DefensePoint = float.Parse(elems[5]);
            enemy.MaxMana = float.Parse(elems[6]);
            enemy.MoveSpeed = float.Parse(elems[7]);
            enemy.AttackRange = float.Parse(elems[8]);
            enemy.Type = ObjectType.Enemy;
            line1 = csvImp1.Readline();

            ObjectCodex.Add(enemy);
        }
    }

    public GlobalObject LoadObject(uint guid,ObjectType type)
    {
        GlobalObject obj = ObjectCodex.Find((elem) => { return elem.GUID == guid; });
        if (obj.Type != type)
            throw new System.Exception("GUID and Type Didn't matched!");
        else 
            return obj;
    }

    public void TestFunc()
    {
        SceneManager.LoadScene("BattleSelectScene");
    }

    
    /// <summary>
    /// Load File From Andriod
    /// </summary>
    /// <param name="FilePath">FilePath begins from under the StreamingAssets folder</param>
    /// <returns> Readed Bytes</returns>
    public byte[] LoadFile(string FilePath)
    {
        byte[] data;
        string path = Application.streamingAssetsPath+FilePath;
        UnityWebRequest www = UnityWebRequest.Get(path);
        www.SendWebRequest();
        while(!www.isDone)
        {}
        if (www.error == null)
            data = www.downloadHandler.data;
        else
            throw new System.Exception("Data cannot Arrive");
        return data;
    }
}
