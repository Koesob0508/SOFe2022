using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    public List<Hero> HeroList = new List<Hero>();
    public List<GlobalObject> ShopHeroList = new List<GlobalObject>();
    // public List<GlobalObject> GuildList = new List<GlobalObject>();

    public void Test()
    {
        Debug.Log("Test를 위해 Hero 임시로 등록합니다");
        EnrollHero(0);
        EnrollHero(1);
        EnrollHero(2);
        EnrollHero(3);

        // !! 임시 !!
        SetShopHero();

        GameManager.Relation.GetTeamScore();
    }

    public void Init()
    {
        Debug.Log("Hero Manager Init");

        DontDestroyOnLoad(this);

        GameObject obj = GameObject.Find("Hero Manager");
        if (obj == null)
        {
            obj = new GameObject { name = "Hero Manager" };
            obj.AddComponent<HeroManager>();
        }

        DontDestroyOnLoad(obj);


        // === 임시 코드 ===
        //Hero hero1 = new Hero();
        //Hero hero2 = new Hero();
        //Hero hero3 = new Hero();
        //Hero hero4 = new Hero();

        //hero1 = GameManager.Instance.LoadObject(1, GameManager.ObjectType.Hero) as Hero;
        //hero2 = GameManager.Instance.LoadObject(2, GameManager.ObjectType.Hero) as Hero;
        //hero3 = GameManager.Instance.LoadObject(3, GameManager.ObjectType.Hero) as Hero;
        //hero4 = GameManager.Instance.LoadObject(4, GameManager.ObjectType.Hero) as Hero;

        //HeroList.Add(hero1);
        //HeroList.Add(hero2);
        //HeroList.Add(hero3);
        //HeroList.Add(hero4);
        // ==================


        // GameManager로부터 csv파일의 Hero 정보 받아온 뒤 Instance
        //for (uint guid = 1; guid <= 17; guid++)
        //{
        //    // Prefab 폴더에서 load 
        //    GameObject Prefab = Resources.Load("Prefabs/Monsters/" + guid) as GameObject;
        //    GameObject HeroObj = Instantiate(Prefab);
        //    Hero LoadHero = HeroObj.GetComponent<Hero>();
        //    // Load된 게임 Data에서 해당 Hero의 속성 가져옴
        //    // LoadObject?
        //    Hero HeroData = GameManager.Instance.LoadObject(guid,ObjectType.Hero) as Hero;
        //    // Hero의 속성 적용
        //    LoadHero.IsActive = false;
        //    LoadHero.MaxHP = HeroData.MaxHP;
        //    LoadHero.AttackDamage = HeroData.AttackDamage;
        //    LoadHero.AttackSpeed = HeroData.AttackSpeed;
        //    LoadHero.DefensePoint = HeroData.DefensePoint;
        //    LoadHero.MaxMana = HeroData.MaxMana;
        //    LoadHero.MoveSpeed = HeroData.MoveSpeed;
        //    LoadHero.AttackRange = HeroData.AttackRange;
        //}
    }

    public void EnrollHero(uint guid)
    {
        // 해당 guid의 hero를 setActive, List에 등록한다 -> 이는 Battle Manager에 넘어감

        //foreach (GameObject g in GameObject.FindGameObjectsWithTag("Hero")) 
        //{
        //    if (g.GetComponent<Hero>().GUID == guid)
        //    {
        //        g.GetComponent<Hero>().IsActive = true;
        //        HeroList.Add(g.GetComponent<Hero>());
        //        break;
        //    }
        //}

        foreach (Hero hero in GameManager.Data.ObjectCodex.Values)
        {
            if (hero.GUID == guid)
            {
                if (hero.IsActive)
                    break;

                hero.IsActive = true;

                // !! 임시 !!
                hero.MBTI = (GameManager.MbtiType)Random.Range(0, 16);

                // Team Score에 해당 Hero를 Update
                GameManager.Relation.NewHeroScore(hero);

                HeroList.Add(hero);
                // Debug.Log("Enroll " + hero.GUID + hero.Name);
                break;
            }
        }
    }

    public Hero GetHero(uint guid)
    {
        Hero hero = HeroList.Find(
            delegate (Hero _hero)
            {
                return _hero.GUID == guid;
            }
            );

        if (hero == null)
        {
            Debug.Log("등록되지 않은 용병입니다.");
            return null;
        }
        else
        {
            return hero;
        }
    }

    public List<Hero> GetHeroList()
    {
        return HeroList;
    }

    public List<Item> GetHeroItemList(Hero hero)
    {
        List<Item> Items = HeroList.Find(
            delegate (Hero _hero)
            {
                return _hero == hero;
            }
            ).Items;

        if (Items == null)
        {
            Debug.Log("등록되지 않은 용병입니다.");
            return null;
        }
        else
        {
            return Items;
        }
    }

    public void AddHeroItem(Hero hero, Item item)
    {
        List<Item> Items = GetHeroItemList(hero);

        item.HeroGUID = hero.GUID;
        item.InventoryOrder = Items.Count;

        if (Items.Count > 3)
        {
            Debug.Log("등록 가능한 아이템 갯수는 최대 3개입니다.");
        }
        else
        {
            Items.Add(item);
        }
    }

    public void RemoveHeroItem(Hero hero, Item item)
    {
        List<Item> Items = GetHeroItemList(hero);
        Items.Remove(item);
    }

    public void SetShopHero()
    {
        ShopHeroList.Clear();

        while (ShopHeroList.Count < 3)
        {
            uint guid = (uint)Random.Range(0, 17);
            bool CanActive = true;

            foreach (Hero hero in ShopHeroList)
            {
                if (hero.GUID == guid)
                {
                    CanActive = false;
                    break;
                }
            }

            foreach (Hero hero in GameManager.Hero.GetHeroList())
            {
                if (hero.GUID == guid)
                {
                    CanActive = false;
                    break;
                }
            }

            if (CanActive)
            {
                Hero hero = (Hero)GameManager.Data.LoadObject(guid, GameManager.ObjectType.Hero);

                // 용병의 MBTI Random으로 지정
                hero.MBTI = (GameManager.MbtiType)Random.Range(0, 16);
                // Debug.Log(hero.Name + "랜덤으로 등록");
                ShopHeroList.Add(hero);
            }
        }
    }

    public List<GlobalObject> GetShopHeroList()
    {
        return ShopHeroList;
    }

}
