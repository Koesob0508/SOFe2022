using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    public List<Hero> HeroList = new List<Hero>();
    
    public void Init()
    {
        // GameManager�κ��� csv������ Hero ���� �޾ƿ� �� Instance
        Debug.Log("Hero Manager Init");

        for (uint guid = 1; guid <= 17; guid++)
        {
            // Prefab �������� load 
            GameObject Prefab = Resources.Load("Prefabs/Monsters/" + guid) as GameObject;
            GameObject HeroObj = Instantiate(Prefab);
            Hero LoadHero = HeroObj.GetComponent<Hero>();

            // Load�� ���� Data���� �ش� Hero�� �Ӽ� ������
            // LoadObject?
            Hero HeroData = GameManager.Instance.LoadObject(guid);

            // Hero�� �Ӽ� ����
            LoadHero.IsActive = false;
            LoadHero.MaxHP = HeroData.MaxHP;
            LoadHero.AttackDamage = HeroData.AttackDamage;
            LoadHero.AttackSpeed = HeroData.AttackSpeed;
            LoadHero.DefensePoint = HeroData.DefensePoint;
            LoadHero.MaxMana = HeroData.MaxMana;
            LoadHero.MoveSpeed = HeroData.MoveSpeed;
            LoadHero.AttackRange = HeroData.AttackRange;
        }
    }

    public void EnrollHero(uint guid)
    {
        // �ش� guid�� hero�� setActive, List�� ����Ѵ� -> �̴� Battle Manager�� �Ѿ
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Hero"))
        {
            if (g.GetComponent<Hero>().GUID == guid)
            {
                g.GetComponent<Hero>().IsActive = true;
                HeroList.Add(g.GetComponent<Hero>());
                break;
            }
        }
    }

    public List<Hero> GetHeroList()
    {
        return HeroList;
    }

    

}
