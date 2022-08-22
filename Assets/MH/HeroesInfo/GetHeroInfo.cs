using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetHeroInfo : MonoBehaviour
{
    public List<Hero> HeroList = GameManager.Hero.GetHeroList();
    public Dictionary<uint, GameObject> HeroUIList = new Dictionary<uint, GameObject>();

    public GameObject HeroUIObject;
    GameObject HeroUI;

    // Status
    public Gradient gradient;

    // �������� ������� ����� HeroList�� HeroManager���� ���� (?)
    //  �� ü��, ���, ���� ������ Hero�� ���� ... ��

    public uint GetHeroUIOrder(GameObject _hero)
    {
        uint guid = HeroUIList.FirstOrDefault(x => x.Value == _hero).Key;
        return guid;
    }


    // Init()�� ������ ��
    void Start()
    {
        int HeroNUM = HeroList.Count;

        for (int i = 0; i < HeroNUM; i++)
        {
            HeroUI = Instantiate(HeroUIObject, transform.position, Quaternion.identity);
            HeroUI.transform.parent = transform;
            HeroUIList.Add(HeroList[i].GUID, HeroUI);
        }

        SetStatus();
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
        GameObject HeroInfo, AbilityInfo, HealthBar, HungerBar;
        Slider HealthSlider, HungerSlider;
        Image fill;

        foreach (KeyValuePair<uint, GameObject> _Hero in HeroUIList)
        {
            uint HeroGuid = _Hero.Key;
            GameObject HeroUI = _Hero.Value;
            Hero hero = null;

            foreach (Hero _hero in HeroList)
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
                HeroMbti.text = hero.MBTI.ToString();

                // ü�� �� ��� ��
                HealthBar = GetChildWithName(HeroUI, "HealthBar");
                HealthSlider = HealthBar.transform.GetComponent<Slider>();
                HealthSlider.maxValue = hero.MaxHP;
                HealthSlider.value = hero.CurrentHP;
                fill = GetChildWithName(HealthBar, "Fill").transform.GetComponent<Image>();
                fill.color = gradient.Evaluate(HealthSlider.normalizedValue);

                HungerBar = GetChildWithName(HeroUI, "HungerBar");
                HungerSlider = GetChildWithName(HeroUI, "HungerBar").transform.GetComponent<Slider>();
                HungerSlider.value = hero.CurHunger;
                fill = GetChildWithName(HungerBar, "Fill").transform.GetComponent<Image>();
                fill.color = gradient.Evaluate(HungerSlider.normalizedValue);

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

                // Items ��
            }
        }
    

    }    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
