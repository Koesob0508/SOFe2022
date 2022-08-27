using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataManager
{
    public Dictionary<uint, GlobalObject> ObjectCodex = new Dictionary<uint, GlobalObject>();

    public void Init()
    {
        ImportCharData();
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
            hero.Type = GameManager.ObjectType.Hero;
            line = csvImp.Readline();

            ObjectCodex.Add(hero.GUID, hero);
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
        if (ObjectCodex[guid].UI_Image != null)
            return ObjectCodex[guid].UI_Image;
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
            ObjectCodex[guid].UI_Image = Image;
            return Image;

        }
    }
}
