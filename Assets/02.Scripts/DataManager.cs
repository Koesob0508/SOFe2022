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
    public uint Money = 0;

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
        LoadInitalEnemyData();
        if(IsExistSaveData())
            Load();
        else
            LoadInitalHeroData();
    }

    void LoadInitalHeroData()
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
            line = csvImp.Readline();

            ObjectCodex.Add(hero.GUID, hero);
        }

       
    }
    void LoadInitalEnemyData()
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
            enemy.Type = (GameManager.ObjectType)Int32.Parse(elems[2]);
            enemy.MaxHP = float.Parse(elems[3]);
            enemy.AttackDamage = float.Parse(elems[4]);
            enemy.AttackSpeed = float.Parse(elems[5]);
            enemy.DefensePoint = float.Parse(elems[6]);
            enemy.MaxMana = float.Parse(elems[7]);
            enemy.MoveSpeed = float.Parse(elems[8]);
            enemy.AttackRange = float.Parse(elems[9]);
            enemy.Type = GameManager.ObjectType.Enemy;
            line1 = csvImp1.Readline();

            ObjectCodex.Add(enemy.GUID, enemy);
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
        string path = Application.streamingAssetsPath + "/Save/s_" + GameManager.Instance.GetVersion();
        return System.IO.File.Exists(path);
    }
    public void Save()
    {
        Debug.Log(SystemInfo.deviceUniqueIdentifier);
        SaveFormat saveData;
        saveData.HeroData = new List<Hero>();
        saveData.MapData = GameManager.Stage.SerializeStageMap();
        saveData.Version = GameManager.Instance.GetVersion();
        saveData.Money = Money;

        foreach(var obj in ObjectCodex.Values)
        {
            if(((GlobalObject)obj).Type == GameManager.ObjectType.Hero)
            {
                Hero h = (Hero)obj;
                saveData.HeroData.Add(h);
            }
        }
        string path = Application.streamingAssetsPath + "/Save/s_" + GameManager.Instance.GetVersion();

        System.IO.File.Create(path).Close();
        System.IO.FileStream fStream = System.IO.File.Open(path, System.IO.FileMode.OpenOrCreate);

        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fStream, saveData);
        fStream.Close();
        Load();
    }

    public void Load()
    {
        string path = Application.streamingAssetsPath + "/Save/s_" + GameManager.Instance.GetVersion();

        System.IO.FileStream fStream = System.IO.File.Open(path, System.IO.FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();
        SaveFormat savedData = (SaveFormat)formatter.Deserialize(fStream);
        fStream.Close();
        foreach (Hero h in savedData.HeroData)
        {
            ObjectCodex.Add(h.GUID, h);
        }
    }
}
