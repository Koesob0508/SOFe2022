using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetRandomObject : MonoBehaviour
{
    public GameObject EnrollHeroUI;
    GameObject ObjectUI;
    public GameManager.ObjectType Type;

    public List<GlobalObject> EnrollList = new List<GlobalObject>();
    public Dictionary<uint, GameObject> ObjectUIList = new Dictionary<uint, GameObject>();

    public uint GetHeroUIOrder(GameObject _hero)
    {
        uint guid = ObjectUIList.FirstOrDefault(x => x.Value == _hero).Key;
        return guid;
    }

    public void SetRandomList()
    {
        switch (Type)
        {
            case GameManager.ObjectType.Hero:
                {
                    EnrollList = GetRandomHero();
                    break;
                }
            case GameManager.ObjectType.Item:
                {

                    break;
                }
            default:
                throw new System.Exception("ObjectType Not Defined");

        }

    }

    public List<GlobalObject> GetRandomHero()
    {
        List<GlobalObject> HeroList = new List<GlobalObject>();

        while (HeroList.Count < 3)
        {
            uint guid = (uint)Random.Range(0, 17);
            bool CanActive = true;

            foreach (Hero hero in HeroList)
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
                GlobalObject hero = GameManager.Instance.LoadObject(guid, GameManager.ObjectType.Hero);

                // �뺴�� MBTI Random���� ����
                // Debug.Log(hero.Name + "�������� ���");
                HeroList.Add(hero);
            }

        }

        return HeroList;
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

    private void SetStatus()
    {
        // Status
        Image HeroImage;
        TextMeshProUGUI HeroName, HeroMbti;
        GameObject HeroInfo, AbilityInfo;

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

            // HeroUI�� ���� �Է�
            if (hero != null)
            {
                // Info ��
                HeroInfo = GetChildWithName(HeroUI, "Info");
                HeroImage = GetChildWithName(HeroInfo, "HeroImage").transform.GetComponent<Image>();
                // HeroImage.sprite = 

                HeroName = GetChildWithName(HeroInfo, "Name").transform.GetComponent<TextMeshProUGUI>();
                HeroName.text = hero.Name;

                HeroMbti = GetChildWithName(HeroInfo, "MBTI").transform.GetComponent<TextMeshProUGUI>();
                // HeroMbti.text = hero.


                // Ability ��
                AbilityInfo = GetChildWithName(HeroUI, "AbilityInfo").transform.GetChild(0).gameObject;
                AbilityInfo.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().SetText("�ִ� ü�� : " + hero.MaxHP);
                AbilityInfo.transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>().SetText("�ִ� ���� : " + hero.MaxMana);
                AbilityInfo.transform.GetChild(2).transform.GetComponent<TextMeshProUGUI>().SetText("���ݷ� : " + hero.AttackDamage);
                AbilityInfo.transform.GetChild(3).transform.GetComponent<TextMeshProUGUI>().SetText("���� : " + hero.DefensePoint);
                AbilityInfo.transform.GetChild(4).transform.GetComponent<TextMeshProUGUI>().SetText("�̵� �ӵ� : " + hero.MoveSpeed);
                AbilityInfo.transform.GetChild(5).transform.GetComponent<TextMeshProUGUI>().SetText("���� �ӵ� : " + hero.AttackSpeed);
                AbilityInfo.transform.GetChild(6).transform.GetComponent<TextMeshProUGUI>().SetText("���� ���� : " + hero.AttackRange);
                // AbilityInfo.transform.GetChild(7).transform.GetComponent<TextMeshProUGUI>().SetText("��ų : " + hero.);

            }
        }
    }

    void Start()
    {
        switch (Type)
        {
            case GameManager.ObjectType.Hero:
                {
                    if (EnrollList.Count < 3)
                        SetRandomList();

                    for (int i = 0; i < 3; i++)
                    {
                        ObjectUI = Instantiate(EnrollHeroUI, transform.position, Quaternion.identity);
                        ObjectUI.transform.parent = transform;
                        ObjectUIList.Add(EnrollList[i].GUID, ObjectUI);

                        SetStatus();
                    }
                    break;
                }
            case GameManager.ObjectType.Item:
                {

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