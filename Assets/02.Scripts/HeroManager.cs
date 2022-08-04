using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    public List<Hero> HeroList = new List<Hero>();
    
    public void Init()
    {
        // GameManager로부터 csv파일의 Hero 정보 받아온 뒤 Instance
        Debug.Log("Hero Manager Init");

        for (uint guid = 1; guid <= 17; guid++)
        {
            // Prefab 폴더에서 load 
            GameObject Prefab = Resources.Load("Prefabs/Monsters/" + guid) as GameObject;
            GameObject HeroObj = Instantiate(Prefab);
            Hero LoadHero = HeroObj.GetComponent<Hero>();

            // Load된 게임 Data에서 해당 Hero의 속성 가져옴
            // LoadObject?
            Hero HeroData = GameManager.Instance.LoadObject(guid);

            // Hero의 속성 적용
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
        // 해당 guid의 hero를 setActive, List에 등록한다 -> 이는 Battle Manager에 넘어감
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
