using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    public List<Hero> HeroList = new List<Hero>();
    public List<GlobalObject> ShopHeroList = new List<GlobalObject>();

    public void Test()
    {
        Debug.Log("Test�� ���� Hero �ӽ÷� ����մϴ�");
        EnrollHero(0);
        EnrollHero(1);
        EnrollHero(2);
        EnrollHero(3);
        EnrollHero(4);
        EnrollHero(5);
        EnrollHero(6);
        EnrollHero(7);
        EnrollHero(8);
        EnrollHero(9);
        EnrollHero(10);
        EnrollHero(11);
        EnrollHero(12);
        EnrollHero(13);
        EnrollHero(14);
        EnrollHero(15);
        EnrollHero(16);
        EnrollHero(17);
        EnrollHero(18);
        EnrollHero(19);

        GameManager.Relation.GetTeamScore();
    }

    public void Init()
    {
        Debug.Log("Hero Manager Init");

        DontDestroyOnLoad(this);
    }

    public void EnrollHero(uint guid)
    {

        foreach (GlobalObject g in GameManager.Data.ObjectCodex.Values)
        {
            Hero hero = g as Hero;

            if (hero != null && hero.GUID == guid)
            {
                if (hero.IsActive)
                    break;

                hero.IsActive = true;
                // !! �ӽ� !!
                hero.MBTI = (GameManager.MbtiType)Random.Range(0, 16);

                // Team Score�� �ش� Hero�� Update
                GameManager.Relation.NewHeroScore(hero);

                HeroList.Add(hero);
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
            Debug.Log("��ϵ��� ���� �뺴�Դϴ�.");
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
            Debug.Log("��ϵ��� ���� �뺴�Դϴ�.");
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
            Debug.Log("��� ������ ������ ������ �ִ� 3���Դϴ�.");
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

    public void SetGuildHero()
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

                // �뺴�� MBTI Random���� ����
                hero.MBTI = (GameManager.MbtiType)Random.Range(0, 16);
                // Debug.Log(hero.Name + "�������� ���");
                ShopHeroList.Add(hero);
            }
        }
    }

    public List<GlobalObject> GetShopHeroList()
    {
        return ShopHeroList;
    }

}
