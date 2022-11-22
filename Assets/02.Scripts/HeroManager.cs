using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    public List<Hero> HeroList = new List<Hero>();
    public List<GlobalObject> ShopHeroList = new List<GlobalObject>();
    public List<GlobalObject> ShopItemList = new List<GlobalObject>();

    public void Test()
    {
        
        Debug.Log("Test�� ���� Hero �ӽ÷� ����մϴ�");
        EnrollHero(1);
        EnrollHero(5);
        EnrollHero(7);

        AddHeroItem(5, 200, 0);
        AddHeroItem(5, 205, 1);
        AddHeroItem(5, 205, 2);

        GameManager.Relation.GetTeamScore();
    }

    public void Init()
    {
        Debug.Log("Hero Manager Init");

        DontDestroyOnLoad(this);
    }

    public void EnrollHero(uint guid)
    {
        // �̹� �ش� GUID�� Hero�� ��ϵ�
        foreach (Hero hero in GameManager.Hero.HeroList)
        {
            if (hero.GUID == guid)
                return;
        }

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

    public List<Item> GetHeroItem(uint HeroGuid, uint ItemGuid)
    {
        List<Item> SameItem = new List<Item>();

        if (GetHero(HeroGuid).ItemNum > 0)
        {
            foreach (Item i in GetHeroItemList(HeroGuid))
            {
                if (i != null && i.GUID == ItemGuid)
                {
                        SameItem.Add(i);
                }
            }
        }
        return SameItem;
    }

    public Item[] GetHeroItemList(uint guid)
    {
        Item[] Items = HeroList.Find(
            delegate (Hero _hero)
            {
                return _hero == GetHero(guid);
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

    public void AddHeroItem(uint HeroGUID, uint ItemGUID, uint order)
    {
        Item[] Items = GetHeroItemList(HeroGUID);
        
        if (GetHero(HeroGUID).ItemNum < 3)
        {
            foreach (GlobalObject g in GameManager.Data.ObjectCodex.Values)
            {
                Item item = g as Item;
                
                if (item != null && item.GUID == ItemGUID)
                {
                    // �ش� �������� �ߺ��ؼ� ������ Ȯ���Ѵ�
                    List<Item> HeroItem = GetHeroItem(HeroGUID, ItemGUID);

                    // �׷��� �ʴٸ� ������
                    if (HeroItem.Count == 0)
                    {
                        item.OwnHeroGUID = HeroGUID;
                        item.InventoryOrder = order;
                        Items[order] = item;
                    }
                    else
                    {
                        foreach (Item i in HeroItem)
                        {
                            // �ش� ��ġ�� �̹� ����� ���� Item�̶��
                            if (i.InventoryOrder == order)
                            {
                                return;
                            }
                            // �ش� ��ġ�� �ƴ�, ���� Item�� ���ο� ��ġ�� ������ ���̶��
                            else
                            {
                                Item item2 = i.DeepCopy();
                                item2.OwnHeroGUID = HeroGUID;
                                item2.InventoryOrder = order;
                                Items[order] = item2;
                                break;
                            }
                        }
                    }

                    Debug.Log(order + "��°��, " + ItemGUID + " ������ ����");
                    GetHero(HeroGUID).ItemNum += 1;
                    ADDItemBasicEffect(GetHero(HeroGUID), item);
                }
            }
        }
        else
        {
            Debug.Log("��� ������ ������ ������ �ִ� 3���Դϴ�.");
            return;
        }
    }

    public void RemoveHeroItem(uint HeroGUID, uint ItemGUID, uint order)
    {
        Item[] Items = GetHeroItemList(HeroGUID);
        if (Items[order].GUID == ItemGUID)
        {
            SUBItemBasicEffect(GetHero(HeroGUID), Items[order]);
            Items[order] = new Item();
            GetHero(HeroGUID).ItemNum-=1;
        }
    }

    public void SetGuildHero()
    {
        ShopHeroList.Clear();

        // ���� ��ϵ��� ���� Hero
        int LeftHero = 20 - GameManager.Hero.GetHeroList().Count;

        if (LeftHero >= 3)
        {
            LeftHero = 3;
        }

        while (ShopHeroList.Count < LeftHero)
        {
            if (LeftHero == 0)
                break;

            uint guid = (uint)Random.Range(0, 20);
            bool CanActive = true;

            foreach (Hero hero in GameManager.Hero.GetHeroList())
            {
                if (hero.GUID == guid)
                {
                    CanActive = false;
                    break;
                }
            }

            foreach (Hero hero in GameManager.Hero.GetShopHeroList())
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
                ShopHeroList.Add(hero);
            }
        }
    }

    

    public List<GlobalObject> GetShopHeroList()
    {
        return ShopHeroList;
    }

    // �������� �⺻ ȿ���� �뺴���� �����
    public void ADDItemBasicEffect(Hero hero, Item item)
    {
        switch (item.BasicType)
        {
            case GameManager.ItemType.AttackDamage:
                {
                    hero.AttackDamage += item.BasicNum;
                    break;
                }
            case GameManager.ItemType.MoveSpeed:
                {
                    hero.MoveSpeed += item.BasicNum;
                    break;
                }
            case GameManager.ItemType.DefensePoint:
                {
                    hero.DefensePoint += item.BasicNum;
                    break;
                }
            case GameManager.ItemType.MaxHP:
                {
                    hero.MaxHP += item.BasicNum;
                    break;
                }
                
        }
    }

    // �������� �⺻ ȿ���� �뺴���Լ� �����
    public void SUBItemBasicEffect(Hero hero, Item item)
    {
        switch (item.BasicType)
        {
            case GameManager.ItemType.AttackDamage:
                {
                    hero.AttackDamage -= item.BasicNum;
                    break;
                }
            case GameManager.ItemType.MoveSpeed:
                {
                    hero.MoveSpeed -= item.BasicNum;
                    break;
                }
            case GameManager.ItemType.DefensePoint:
                {
                    hero.DefensePoint -= item.BasicNum;
                    break;
                }
            case GameManager.ItemType.MaxHP:
                {
                    hero.MaxHP -= item.BasicNum;
                    break;
                }

        }
    }
    public void SetShopItem()
    {
        ShopItemList.Clear();

        while (ShopItemList.Count < 3)
        {
            uint guid = (uint)Random.Range(200, 220);
            Item item = (Item)GameManager.Data.LoadObject(guid, GameManager.ObjectType.Item);

            if (ShopItemList.Contains(item))
                continue;

            // Cost�� Star�� ���� �����ȴ�
            if (item.Star ==1)
                item.Cost = 50;
            else if (item.Star == 2)
                item.Cost = 150;
            else if (item.Star == 3)
                item.Cost = 400;
            else if (item.Star == 4)
                item.Cost = 950;
            else if (item.Star == 5)
                item.Cost = 2000;

            ShopItemList.Add(item);
        }
    }

    public List<GlobalObject> GetShopItemList()
    {
        return ShopItemList;
    }

}
