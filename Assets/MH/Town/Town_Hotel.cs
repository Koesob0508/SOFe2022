using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Town_Hotel : MonoBehaviour
{

    public GameObject EnrollHeroUI;
    GameObject ObjectUI;

    public List<Hero> EnrollList = new List<Hero>();
    public Dictionary<uint, GameObject> ObjectUIList = new Dictionary<uint, GameObject>();

    // Status
    public Gradient gradient;

    public uint GetHeroUIOrder(GameObject _hero)
    {
        uint guid = ObjectUIList.FirstOrDefault(x => x.Value == _hero).Key;
        return guid;
    }
    public void SetHeroList()
    {
        EnrollList = GameManager.Hero.GetHeroList();

        for (int i = 0; i < EnrollList.Count(); i++)
        {
            ObjectUI = Instantiate(EnrollHeroUI, transform.position, Quaternion.identity);
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

    void Start()
    {
        SetHeroList();
    }


}
