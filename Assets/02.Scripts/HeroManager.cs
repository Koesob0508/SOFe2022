using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    public List<Hero> HeroList = new List<Hero>();
    public List<GlobalObject> ShopHeroList = new List<GlobalObject>();

    public void Test()
    {
        Debug.Log("Test를 위해 Hero 임시로 등록합니다");
        EnrollHero(0);
        EnrollHero(1);
        EnrollHero(11);
        EnrollHero(9);
        EnrollHero(14);
        EnrollHero(18);
        EnrollHero(19);

        AddHeroItem(0, 200, 0);
        AddHeroItem(0, 205, 1);

        GameManager.Relation.GetTeamScore();
    }

    public void Init()
    {
        Debug.Log("Hero Manager Init");

        DontDestroyOnLoad(this);
    }

    public void EnrollHero(uint guid)
    {
        // 이미 해당 GUID의 Hero가 등록됨
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
                // !! 임시 !!
                hero.MBTI = (GameManager.MbtiType)Random.Range(0, 16);

                // Team Score에 해당 Hero를 Update
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

    public Item GetHeroItem(uint HeroGuid, uint ItemGuid)
    {
        if (GetHero(HeroGuid).ItemNum > 0)
        {
            foreach (Item i in GetHeroItemList(HeroGuid))
            {
                if (i != null && i.GUID == ItemGuid)
                {
                        return i;
                }
            }
        }
        return null;
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
            Debug.Log("등록되지 않은 용병입니다.");
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
                    // 해당 아이템을 중복해서 갖는지 확인한다
                    Item HeroItem = GetHeroItem(HeroGUID, ItemGUID);

                    // 그렇지 않다면 저장함
                    if (HeroItem == null)
                    {
                        Items[order] = item;
                    }
                    // 이미 저장된 Item이라면
                    else if (HeroItem.InventoryOrder == order)
                    {
                        return;
                    }
                    else
                    {
                        Items[order] = item;
                    }

                    GetHero(HeroGUID).ItemNum += 1;
                    item.OwnHeroGUID = HeroGUID;
                    item.InventoryOrder = order;

                    ADDItemBasicEffect(GetHero(HeroGUID), item);
                }
            }
        }
        else
        {
            Debug.Log("등록 가능한 아이템 갯수는 최대 3개입니다.");
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
                ShopHeroList.Add(hero);
            }
        }
    }

    public List<GlobalObject> GetShopHeroList()
    {
        return ShopHeroList;
    }

    // 아이템의 기본 효과가 용병에게 적용됨
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

    // 아이템의 기본 효과가 용병에게서 사라짐
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


}
