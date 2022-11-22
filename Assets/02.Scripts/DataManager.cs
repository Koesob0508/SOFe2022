using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;

public class DataManager
{
    public Dictionary<uint, GlobalObject> ObjectCodex = new Dictionary<uint, GlobalObject>();
    public Dictionary<uint, Sprite> UI_Img = new Dictionary<uint, Sprite>();
    public List<StageData> StageData = new List<StageData>();
    public uint Money = 3000;

    [Serializable]
    private struct SaveFormat
    {
        public List<Hero> HeroData;
        public string MapData;
        public uint Money;
        public string Version;
    }

    public void Init()
    {
        LoadInitialEnemyData();
        LoadInitalItemData();
        LoadInitialStageData();

        if(IsExistSaveData())
        {
            Load();
        }
        else
        {
            LoadInitialHeroData();
            Hero[] startingHeros = { ObjectCodex[9] as Hero, ObjectCodex[10] as Hero, ObjectCodex[14] as Hero, ObjectCodex[15] as Hero };
            for(int i = 0; i < startingHeros.Length; i++)
            {
                startingHeros[i].IsActive = true;
            }
            GameManager.Stage.SetStageData(StageData);
        }
    }

    void LoadInitialHeroData()
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
            hero.Type = (GameManager.ObjectType)Int32.Parse(elems[2]);
            hero.MaxHP = float.Parse(elems[3]);
            hero.AttackDamage = float.Parse(elems[4]);
            hero.AttackSpeed = float.Parse(elems[5]);
            hero.DefensePoint = float.Parse(elems[6]);
            hero.MaxMana = float.Parse(elems[7]);
            hero.MoveSpeed = float.Parse(elems[8]);
            hero.AttackRange = float.Parse(elems[9]);
            hero.Type = GameManager.ObjectType.Hero;
            hero.CurrentHP = hero.MaxHP;
            // Hard-Coding Part
            hero.CurHunger = 100;
            hero.IsActive = false;
            line = csvImp.Readline();

            ObjectCodex.Add(hero.GUID, hero);
        }

       
    }
    void LoadInitialEnemyData()
    {
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
            enemy.Type = GameManager.ObjectType.Enemy;
            enemy.MaxHP = float.Parse(elems[2]);
            enemy.AttackDamage = float.Parse(elems[3]);
            enemy.AttackSpeed = float.Parse(elems[4]);
            enemy.DefensePoint = float.Parse(elems[5]);
            enemy.MaxMana = float.Parse(elems[6]);
            enemy.MoveSpeed = float.Parse(elems[7]);
            enemy.AttackRange = float.Parse(elems[8]);
            enemy.min_Coin = uint.Parse(elems[9]);
            enemy.max_Coin = uint.Parse(elems[10]);
            line1 = csvImp1.Readline();

            ObjectCodex.Add(enemy.GUID, enemy);
        }
    }
    void LoadInitalItemData()
    {
        CSVImporter csvImp2 = new CSVImporter();
        csvImp2.OpenFile("Data/Items_values");
        csvImp2.ReadHeader();
        string line2 = csvImp2.Readline();

        while (line2 != null)
        {
            string[] elems = line2.Split(',');

            if (elems.Length < 2) // ?�� ���� �ϳ� �� ��?
                break;

            Item item = new Item();
            item.GUID = uint.Parse(elems[0]);
            item.Name = elems[1];
            item.Type = (GameManager.ObjectType)Int32.Parse(elems[2]);
            item.Star = uint.Parse(elems[3]);
            item.BasicType = (GameManager.ItemType)Int32.Parse(elems[4]);
            item.BasicNum = float.Parse(elems[5]);
            // item.SpecialType = (GameManager.ItemTyoe)Int32.Parse(elems[6]);
            item.SpeicalNum = float.Parse(elems[7]);
            item.Info = elems[7];
            item.Type = GameManager.ObjectType.Item;
            line2 = csvImp2.Readline();

            ObjectCodex.Add(item.GUID, item);
        }
    }

    private void LoadInitialStageData()
    {
        CSVImporter csvStage = new CSVImporter();
        csvStage.OpenFile("Data/Stages_values");
        csvStage.ReadHeader();
        string line = csvStage.Readline();

        while (line != null)
        {
            string[] elems = line.Split(',');

            if(elems[0] == "")
            {
                break;
            }

            var stageData = new StageData();
           
            stageData.stage = int.Parse(elems[0]);
            stageData.map = (GameManager.MapType)int.Parse(elems[1]);
            stageData.step = int.Parse(elems[2]);
            stageData.case_ = int.Parse(elems[3]);
            stageData.ruid = int.Parse(elems[4]);
            stageData.count = int.Parse(elems[5]);
            line = csvStage.Readline();

            StageData.Add(stageData);
        }
    }
    public GlobalObject LoadObject(uint guid, GameManager.ObjectType type)
    {
        GlobalObject obj = ObjectCodex[guid];
        if (obj.Type != type)
            throw new System.Exception("GUID and Type Didn't matched!");
        else
            return obj;
    }

    /// <summary>
    /// Load File From Andriod
    /// </summary>
    /// <param name="FilePath">FilePath begins from under the StreamingAssets folder</param>
    /// <returns> Readed Bytes</returns>
    public byte[] LoadFile(string FilePath)
    {
        byte[] data;
        string path = Application.streamingAssetsPath + FilePath;
        UnityWebRequest www = UnityWebRequest.Get(path);
        www.SendWebRequest();
        while (!www.isDone)
        { }
        if (www.error == null)
            data = www.downloadHandler.data;
        else
            throw new System.Exception("Data cannot Arrive");
        return data;
    }
    public Sprite LoadSprite(string path)
    {
        byte[] bytes = LoadFile(path);
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
        Sprite sprite = null;
        if (UI_Img.TryGetValue(guid, out sprite))
            return sprite;
        else
        {
            string pathByType = "";
            switch (ObjectCodex[guid].Type)
            {
                case GameManager.ObjectType.Hero:
                    pathByType = "/Sprites/HeroUI/";
                    break;
                case GameManager.ObjectType.Enemy:
                    pathByType = "/Sprites/MonsterUI/";
                    break;
                case GameManager.ObjectType.Item:
                    pathByType = "/Sprites/ItemUI/";
                    break;
            }
            byte[] bytes = LoadFile(pathByType + guid + "_UI.png");
            Sprite Image = null;
            if (bytes.Length > 0)
            {
                Texture2D tex = new Texture2D(0, 0);
                tex.LoadImage(bytes);
                Image = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            }
            UI_Img[guid] = Image;
            return Image;

        }
    }
    
    private bool IsExistSaveData()
    {
        if (!System.IO.Directory.Exists(Application.persistentDataPath + "/Save"))
        {
            return false;
        }
        string path = Application.persistentDataPath + "/Save/s_" + GameManager.Instance.GetVersion();
        return System.IO.File.Exists(@path);
    }
    public void Save()
    {
        SaveFormat saveData;
        saveData.HeroData = new List<Hero>();
        saveData.MapData = GameManager.Stage.SerializeStageMap();
        saveData.Version = GameManager.Instance.GetVersion();
        saveData.Money = Money;

        foreach (var obj in ObjectCodex.Values)
        {
            if (((GlobalObject)obj).Type == GameManager.ObjectType.Hero)
            {
                Hero h = (Hero)obj;
                saveData.HeroData.Add(h);
            }
        }
        if(!System.IO.Directory.Exists(Application.persistentDataPath + "/Save"))
        {
            System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/Save");
        }

        string path = Application.persistentDataPath + "/Save/s_" + GameManager.Instance.GetVersion();
        System.IO.File.Create(path).Close();
        System.IO.FileStream fStream = System.IO.File.Open(path, System.IO.FileMode.OpenOrCreate);

        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fStream, saveData);
        fStream.Close();
    }

    public void Load()
    {
        string path = Application.persistentDataPath + "/Save/s_" + GameManager.Instance.GetVersion();

        System.IO.FileStream fStream = System.IO.File.Open(path, System.IO.FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();
        SaveFormat savedData = (SaveFormat)formatter.Deserialize(fStream);
        fStream.Close();
        Money = savedData.Money;
        GameManager.Stage.LoadStageSaveData(savedData.MapData);

        foreach (Hero h in savedData.HeroData)
        {
            ObjectCodex.Add(h.GUID, h);
            GameManager.Hero.HeroList.Add(h);
        }
    }

    public void Delete()
    {
        if(IsExistSaveData())
        {
            string path = Application.persistentDataPath + "/Save/s_" + GameManager.Instance.GetVersion();

            System.IO.File.Delete(path);

            Debug.Log(IsExistSaveData());
        }

        ObjectCodex.Clear();
        StageData.Clear();
        GameManager.Stage.SetStageData(StageData);
    }
}
