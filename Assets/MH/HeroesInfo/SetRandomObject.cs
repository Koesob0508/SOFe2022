using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetRandomObject : MonoBehaviour
{
    public GameObject EnrollUI;
    GameObject ObjectUI;
    public GameManager.ObjectType Type;

    public List<GlobalObject> EnrollList = new List<GlobalObject>();
    public Dictionary<uint, GameObject> ObjectUIList = new Dictionary<uint, GameObject>();

    public uint GetHeroUIOrder(GameObject _hero)
    {
        uint guid = ObjectUIList.FirstOrDefault(x => x.Value == _hero).Key;
        return guid;
    }

    public void SetShopList()
    {
        switch (Type)
        {
            case GameManager.ObjectType.Hero:
                {
                    EnrollList = GameManager.Hero.GetShopHeroList();

                    if (EnrollList.Count == 0)
                    {
                        this.transform.parent.parent.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().SetText("해당 마을에서 더 이상 용병을 구할 수 없습니다");
                    }

                    for (int i = 0; i < EnrollList.Count(); i++)
                    {
                        ObjectUI = Instantiate(EnrollUI, transform.position, Quaternion.identity);
                        ObjectUI.transform.SetParent(transform);
                        ObjectUIList.Add(EnrollList[i].GUID, ObjectUI);
                    }
                    SetStatus();
                    break;
                }
            case GameManager.ObjectType.Item:
                {
                    EnrollList = GameManager.Hero.GetShopItemList();
                    for (int i = 0; i < EnrollList.Count(); i++)
                    {
                        ObjectUI = Instantiate(EnrollUI, transform.position, Quaternion.identity);
                        transform.GetChild(i).GetComponent<Image>().sprite = GameManager.Data.LoadSprite(EnrollList[i].GUID);
                        ObjectUI.transform.SetParent(transform.GetChild(i));
                        ObjectUI.transform.GetComponent<GetItemInfo>().SetInfo((Item)EnrollList[i]);
                        ObjectUIList.Add(EnrollList[i].GUID, ObjectUI);
                    }
                    break;
                }
            default:
                throw new System.Exception("ObjectType Not Defined");

        }

    }

    private GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }

    public void SetStatus()
    {
        // Status
        Image HeroImage;
        TextMeshProUGUI HeroName, HeroMbti;
        GameObject HeroInfo, AbilityInfo, CostInfo;

        foreach (KeyValuePair<uint, GameObject> _Hero in ObjectUIList)
        {
            uint HeroGuid = _Hero.Key;
            GameObject HeroUI = _Hero.Value;
            Hero hero = null;

            foreach (Hero _hero in EnrollList)
            {
                if (_hero.GUID == HeroGuid)
                {
                    hero = _hero;
                    break;
                }
            }

            // HeroUI에 정보 입력
            if (hero != null)
            {
                // Info 값
                HeroInfo = GetChildWithName(HeroUI, "Info");
                HeroImage = GetChildWithName(HeroInfo, "HeroImage").transform.GetComponent<Image>();
                HeroImage.sprite = GameManager.Data.LoadSprite(hero.GUID);

                HeroName = GetChildWithName(HeroInfo, "Name").transform.GetComponent<TextMeshProUGUI>();
                HeroName.text = hero.Name;

                HeroMbti = GetChildWithName(HeroInfo, "MBTI").transform.GetComponent<TextMeshProUGUI>();
                HeroMbti.text = hero.MBTI.ToString();


                // Ability 값
                AbilityInfo = GetChildWithName(HeroUI, "AbilityInfo").transform.GetChild(0).gameObject;
                AbilityInfo.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().SetText("최대체력\n" + hero.MaxHP);
                AbilityInfo.transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>().SetText("최대마나\n" + hero.MaxMana);
                AbilityInfo.transform.GetChild(2).transform.GetComponent<TextMeshProUGUI>().SetText("공격력\n" + hero.AttackDamage);
                AbilityInfo.transform.GetChild(3).transform.GetComponent<TextMeshProUGUI>().SetText("방어력\n" + hero.DefensePoint);
                AbilityInfo.transform.GetChild(4).transform.GetComponent<TextMeshProUGUI>().SetText("이동속도\n" + hero.MoveSpeed);
                AbilityInfo.transform.GetChild(5).transform.GetComponent<TextMeshProUGUI>().SetText("공격속도\n" + hero.AttackSpeed);
                AbilityInfo.transform.GetChild(6).transform.GetComponent<TextMeshProUGUI>().SetText("공격범위\n" + hero.AttackRange);
                //AbilityInfo.transform.GetChild(7).transform.GetComponent<TextMeshProUGUI>().SetText("스킬:\n" + hero.Skill);
                GetChildWithName(HeroUI, "AbilityInfo").transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>().SetText("스킬\n" + hero.Skill);
                // Cost 값
                CostInfo = GetChildWithName(HeroUI, "Button").transform.GetChild(0).gameObject;
                CostInfo.transform.GetComponent<TextMeshProUGUI>().SetText(hero.Cost.ToString()+"G");
            }
        }
    }

    void Start()
    {
        switch (Type)
        {
            case GameManager.ObjectType.Hero:
                {
                    SetShopList();
                    break;
                }
            case GameManager.ObjectType.Item:
                {
                    SetShopList();
                    break;
                }
            default:
                throw new System.Exception("ObjectType Not Defined");

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
