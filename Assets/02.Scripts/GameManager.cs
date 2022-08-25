using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public enum ObjectType
    {
        Hero,
        Enemy,
        Item
    }

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }

            return instance;
        }
    }

    #region Managers

    [SerializeField] private StageManager _stage;
    public static StageManager Stage { get { return Instance._stage; } }

    [SerializeField] private CustomSceneManager _scene = new CustomSceneManager();
    public static CustomSceneManager Scene { get { return Instance._scene; } }

    [SerializeField] private HeroManager _hero; // HeroManager가 MonoBehaviour를 상속 받고 있기 때문에 Scene에서 직접 할당 필요
    public static HeroManager Hero { get { return Instance._hero; } }

    public static BattleSceneManager Battle = null; // Battle 돌입 때마다 새로 할당

    #endregion

    public Dictionary<uint,GlobalObject> ObjectCodex = new Dictionary<uint,GlobalObject>();

    public enum MapType
    {
        Jungle,
        Dessert,
        Boss
    }

    private void Awake()
    {
        ImportCharData();
    }

    private void Start()
    {
        Init();
        // Test();
    }

    private void Init()
    {
        if (instance == null)
        {
            #region Initialize GameManager

            GameObject obj = GameObject.Find("Game Manager");

            if (obj == null)
            {
                obj = new GameObject { name = "Game Manager" };
                obj.AddComponent<GameManager>();
            }

            DontDestroyOnLoad(obj);
            instance = obj.GetComponent<GameManager>();

            #endregion
            Stage.Init();
            Scene.Init();
            Hero.Init();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Test()
    {
        Debug.Log("테스트를 진행합니다.");
        // 이 밑으로 진행할 Test 코드를 입력한 후, Start 함수에 가서 Test의 주석 처리를 해제하면 됩니다. 
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

            ObjectCodex.Add(hero.GUID,hero);
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

            ObjectCodex.Add(enemy.GUID,enemy);
        }
    }

    public GlobalObject LoadObject(uint guid,GameManager.ObjectType type)
    {
        GlobalObject obj = ObjectCodex[guid];
        if (obj.Type != type)
            throw new System.Exception("GUID and Type Didn't matched!");
        else 
            return obj;
    }

    public void TestFunc()
    {
        Scene.ToBattleScene();
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
    public Sprite LoadSprite(string path)
    {
        byte[] bytes = Instance.LoadFile(path);
        Sprite Image = null;
        if (bytes.Length > 0)
        {
            Texture2D tex = new Texture2D(0, 0);
            tex.LoadImage(bytes);

            Image = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }
        return Image;
    }
    public Sprite LoadSprite(uint guid)
    {
        if (ObjectCodex[guid].UI_Image != null)
            return ObjectCodex[guid].UI_Image;
        else
        {
            string pathByType = "";
            switch (ObjectCodex[guid].Type)
            {
                case ObjectType.Hero:
                    pathByType = "/Sprites/HeroUI/";
                    break;
                case ObjectType.Enemy:
                    pathByType = "/Sprites/MonsterUI/";
                    break;
                case ObjectType.Item:
                    break;
            }
            byte[] bytes = Instance.LoadFile(pathByType + guid + "_UI.png");
            Sprite Image = null;
            if (bytes.Length > 0)
            {
                Texture2D tex = new Texture2D(0, 0);
                tex.LoadImage(bytes);
                Image = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            }
            ObjectCodex[guid].UI_Image = Image;
            return Image;

        }
    }
}
