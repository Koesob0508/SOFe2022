using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{

    // 원래 정의된 관계 점수
    public sbyte[,] MBTIScore = new sbyte[,] {  {+3, +3, +3, +5, +3, +5, +3, +3, -3, -3, -3, -3, -3, -3, -3, -3 },
                                                {+3, +3, +5, +3, +5, +3, +3, +3, -3, -3, -3, -3, -3, -3, -3, -3 },
                                                {+3, +5, +3, +3, +3, +3, +3, +5, -3, -3, -3, -3, -3, -3, -3, -3 },
                                                {+5, +3, +3, +3, +3, +3, +3, +3, +5, -3, -3, -3, -3, -3, -3, -3 },
                                                {+3, +5, +3, +3, +3, +3, +3, +5, +1, +1, +1, +1, 0, 0, 0, 0, },
                                                {+5, +3, +3, +3, +3, +3, +5, +3, +1, +1, +1, +1, +1, +1, +1, +1 },
                                                {+3, +3, +3, +3, +3, +5, +3, +3, +1, +1, +1, +1, 0, 0, 0, +5 },
                                                {+3, +3, +5, +3, +5, +5, +3, +3, +1, +1, +1, +1, 0, 0, 0, 0 },
                                                {-3, -3, -3, +5, +1, +1, +1, +1, 0, 0, 0, 0, +1, +5, +1, +5 },
                                                {-3, -3, -3, -3, +1, +1, +1, +1, 0, 0, 0, 0, +5, +1, +5, +1 },
                                                {-3, -3, -3, -3, +1, +1, +1, +1, 0, 0, 0, 0, +1, +5, +1, +5 },
                                                {-3, -3, -3, -3, +1, +1, +1, +1, 0, 0, 0, 0, +5, +1, +5, +1 },
                                                {-3, -3, -3, -3, 0, +1, 0, 0, +1, +5, +1, +5, +3, +3, +3, +3 },
                                                {-3, -3, -3, -3, 0, +1, 0, 0, +5, +1, +5, +1, +3, +3, +3, +3 },
                                                {-3, -3, -3, -3, 0, +1, 0, 0, +1, +5, +1, +5, +3, +3, +3, +3 },
                                                {-3, -3, -3, -3, 0, +1, +5, 0, +5, +1, +5, +1, +3, +3, +3, +3 } };


    public List<Hero> HeroList = new List<Hero>();
    // public List<GlobalObject> GuildList = new List<GlobalObject>();
    private void Test()
    {
        Debug.Log("Test를 위해 Hero 임시로 등록합니다");
        EnrollHero(0);
        EnrollHero(1);
        EnrollHero(2);
        EnrollHero(3);
    }

    public void Init()
    {

        Debug.Log("Hero Manager Init");

        GameObject obj = GameObject.Find("Hero Manager");
        if (obj == null)
        {
            obj = new GameObject { name = "Hero Manager" };
            obj.AddComponent<HeroManager>();
        }

        DontDestroyOnLoad(obj);

        
        // === 임시 코드 ===
        Test();
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

    public void GetTeamScore()
    {
        // TeamScore를 Return
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
        
        foreach (Hero hero in GameManager.Instance.ObjectCodex.Values)
        {
            if (hero.GUID == guid)
            {
                hero.IsActive = true;
                // 임시로 등록하기 위해서 mbti 여기서도 등록할 수 있게 해놓음
                // hero.MBTI = (GameManager.MbtiType)Random.Range(0, 16);

                HeroList.Add(hero);
                Debug.Log("Enroll " + hero.GUID + hero.Name);
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
}
