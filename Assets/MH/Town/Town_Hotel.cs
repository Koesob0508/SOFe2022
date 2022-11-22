using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Town_Hotel : MonoBehaviour
{
    private enum HotelType
    {
        Sleep,
        Eat
    }
    public GameObject EnrollHeroUI, PlaceUI;
    public GameObject Silder, MoneyObject;
    public Sprite Bed, Table;

    public List<Hero> EnrollList = new List<Hero>();
    public List<Hero> TargetList = new List<Hero>();
    public Dictionary<uint, GameObject> ObjectUIList = new Dictionary<uint, GameObject>();

    private HotelType Type = HotelType.Sleep;
    private int NeedMoney = 0;

    // Status
    public Gradient gradient;

    private int Time = -1; // Hotel 효과는 한번 사용 가능하다

    public uint GetHeroUIOrder(GameObject _hero)
    {
        uint guid = ObjectUIList.FirstOrDefault(x => x.Value == _hero).Key;
        return guid;
    }
    
    public void SetTarget(Hero _hero)
    {
        if (TargetList.Contains(_hero))
        {
            TargetList.Remove(_hero);
            GameObject InfoUI = Resources.Load<GameObject>("Prefabs/UI/HeroImageUI");
            InfoUI = Instantiate(InfoUI, new Vector3(0, 20, 0), Quaternion.identity);
            InfoUI.transform.SetParent(PlaceUI.transform);
            InfoUI.transform.localPosition = new Vector3(0, 0, 0);
            InfoUI.transform.localScale=new Vector3(0, 0, 0);
            InfoUI.GetComponent<Image>().sprite = GameManager.Data.LoadSprite(_hero.GUID);
            LeanTween.moveLocal(InfoUI, new Vector3(0f, +4f, +2f), 1f).setDelay(0.1f).setEase(LeanTweenType.easeOutCirc);
            LeanTween.scale(InfoUI, new Vector3(1f, 1f, 1f), 1f).setDelay(0.5f).setEase(LeanTweenType.easeOutCirc);
            Destroy(InfoUI, 2f);
        }
        else
        {
            TargetList.Add(_hero);

            // Hotel에 Hero 등록
            GameObject InfoUI = Resources.Load<GameObject>("Prefabs/UI/HeroImageUI");
            InfoUI = Instantiate(InfoUI, new Vector3(0, 20, 0), Quaternion.identity);
            InfoUI.transform.SetParent(PlaceUI.transform);
            InfoUI.GetComponent<Image>().sprite = GameManager.Data.LoadSprite(_hero.GUID);
            LeanTween.moveLocal(InfoUI, new Vector3(0f, -4f, -2f), 1f).setDelay(0.1f).setEase(LeanTweenType.easeOutCirc);
            LeanTween.scale(InfoUI, new Vector3(0f, 0f, 0f), 1f).setDelay(0.5f).setEase(LeanTweenType.easeOutCirc);
            Destroy(InfoUI, 3f);
        }
    }

    // Button누르면 Target Hero에게 Hotel 효과 Get
    public void GetHotel()
    {
        if (TargetList.Count() >= 0 && Time < 0)
        {
            GameObject.Find("Place").GetComponent<Image>().color = new Color(133 / 255f, 133 / 255f, 133 / 255f);
            LeanTween.scale(PlaceUI, new Vector3(1.2f, 1.2f, 1.2f), 0.5f).setDelay(0.2f).setEase(LeanTweenType.easeOutCirc);
            LeanTween.scale(PlaceUI, new Vector3(1.0f, 1.0f, 1.0f), 0.5f).setDelay(0.9f).setEase(LeanTweenType.easeOutCirc);
            LeanTween.scale(PlaceUI, new Vector3(1.2f, 1.2f, 1.2f), 0.5f).setDelay(1.6f).setEase(LeanTweenType.easeOutCirc);
            LeanTween.scale(PlaceUI, new Vector3(1.0f, 1.0f, 1.0f), 0.5f).setDelay(2.3f).setEase(LeanTweenType.easeOutCirc);
            if (NeedMoney <= GameManager.Data.Money)
            {
                switch (Type)
                {
                    case HotelType.Sleep:
                        {
                            foreach (Hero _hero in TargetList)
                            {
                                _hero.CurrentHP += _hero.MaxHP / 2;
                                if (_hero.CurrentHP > _hero.MaxHP)
                                    _hero.CurrentHP = _hero.MaxHP;
                            }

                            GameManager.Data.Money -= (uint)NeedMoney;
                            TargetList.Clear();
                            Time =+ 1;

                            break;
                        }
                    case HotelType.Eat:
                        {
                            foreach (Hero _hero in TargetList)
                            {
                                _hero.CurHunger = 100;
                            }

                            GameManager.Data.Money -= (uint)NeedMoney;
                            TargetList.Clear();
                            Time =+ 1;
                            break;
                        }
                }
            }
            else
            {
                Debug.Log("돈이 부족합니다");
            }
        }
        else
        {
            Debug.Log("Can't Do Hotel Thing");
        }
    }

    public void SetHeroList()
    {
        EnrollList = GameManager.Hero.GetHeroList();

        for (int i = 0; i < EnrollList.Count(); i++)
        {
            GameObject ObjectUI = Instantiate(EnrollHeroUI, transform.position, Quaternion.identity);
            ObjectUI.transform.SetParent(transform);
            ObjectUIList.Add(EnrollList[i].GUID, ObjectUI);

            SetStatus();
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
        GameObject HeroInfo, HealthBar, HungerBar;
        Slider HealthSlider, HungerSlider;
        Image fill;

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

                // 체력 및 허기 값
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

            }
        }
    }

    public void SleepOrEat()
    {
        if (Silder.transform.GetComponent<Slider>().value == 0)
        {
            Type = HotelType.Sleep;
            PlaceUI.transform.GetComponent<Image>().sprite = Bed;
            PlaceUI.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Let's SLEEP!");
        }
        else
        {
            Type = HotelType.Eat;
            PlaceUI.transform.GetComponent<Image>().sprite = Table;
            PlaceUI.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Let's EAT!");
        }
    }

    void Start()
    {
        SetHeroList();
    }

    void Update()
    {
        SetStatus();

        switch (Type)
        {
            case HotelType.Sleep:
                {
                    NeedMoney = TargetList.Count() * 100;
                    break;
                }
            case HotelType.Eat:
                {
                    NeedMoney = TargetList.Count() * 50;
                    break;
                }

        }
        MoneyObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText("Need " + NeedMoney + "G");
    }

}
