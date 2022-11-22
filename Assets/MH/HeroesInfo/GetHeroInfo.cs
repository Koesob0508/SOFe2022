using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetHeroInfo : MonoBehaviour
{
    public List<Hero> HeroList = new List<Hero>();
    public Dictionary<uint, GameObject> HeroUIList = new Dictionary<uint, GameObject>();

    public GameObject HeroUIObject;
    public GameObject ItemUIObject;
    GameObject HeroUI, ItemUI;

    // Status 변수
    public Gradient gradient;
    private Image HeroImage, Item;
    private TextMeshProUGUI HeroName, HeroMbti;
    private GameObject HeroInfo, AbilityInfo, Items, HealthBar, HungerBar;
    private Slider HealthSlider, HungerSlider;
    private Image fill;

    // UI에 있는 Hero의 GUID
    public uint GetHeroUIOrder(GameObject _hero)
    {
        uint guid = HeroUIList.FirstOrDefault(x => x.Value == _hero).Key;
        return guid;
    }

    void Start()
    {
        HeroList = GameManager.Hero.GetHeroList();
        int HeroNUM = HeroList.Count;
        for (int i = 0; i < HeroNUM; i++)
        {
            if (HeroUIList.ContainsKey(HeroList[i].GUID) == false)
            {
                HeroUI = Instantiate(HeroUIObject, transform.position, Quaternion.identity);
                HeroUI.transform.SetParent(transform);
                HeroUIList.Add(HeroList[i].GUID, HeroUI);
            }
        }

        SetStatus();
        UpdateItems();
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

                // Ability 값
                AbilityInfo = GetChildWithName(HeroUI, "AbilityInfo").transform.GetChild(0).gameObject;
                AbilityInfo.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().SetText("최대 체력 : " + hero.MaxHP);
                AbilityInfo.transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>().SetText("최대 마나 : " + hero.MaxMana);
                AbilityInfo.transform.GetChild(2).transform.GetComponent<TextMeshProUGUI>().SetText("공격력 : " + hero.AttackDamage);
                AbilityInfo.transform.GetChild(3).transform.GetComponent<TextMeshProUGUI>().SetText("방어력 : " + hero.DefensePoint);
                AbilityInfo.transform.GetChild(4).transform.GetComponent<TextMeshProUGUI>().SetText("이동 속도 : " + hero.MoveSpeed);
                AbilityInfo.transform.GetChild(5).transform.GetComponent<TextMeshProUGUI>().SetText("공격 속도 : " + hero.AttackSpeed);
                AbilityInfo.transform.GetChild(6).transform.GetComponent<TextMeshProUGUI>().SetText("공격 범위 : " + hero.AttackRange);
                AbilityInfo.transform.GetChild(7).transform.GetComponent<TextMeshProUGUI>().SetText("스킬 : " + hero.Skill);

                // Items 값
                Items = GetChildWithName(HeroUI, "Items").transform.gameObject;
                for (int i = 0; i < 3; i++)
                {
                    if (hero.Items[i] != null && hero.Items[i].GUID != 0)
                    {
                        ItemUI = Instantiate(ItemUIObject, transform.position, Quaternion.identity);
                        ItemUI.transform.SetParent(Items.transform.GetChild(i), false);
                        ItemUI.transform.localPosition = new Vector3(0, 0, 0);

                        Item = ItemUI.GetComponent<Image>();
                        Item.sprite = GameManager.Data.LoadSprite(hero.Items[i].GUID);
                        //Item.GetComponent<GetItemInfo>().SetItem(hero.GUID, hero.Items[i].GUID);
                        Item.GetComponent<GetItemInfo>().SetInfo(hero.Items[i]);
                    }
                }
            }
        }
    }


    public void UpdateHealthandHunger(Hero hero)
    {
        // 체력 및 허기 값
        HealthSlider.maxValue = hero.MaxHP;
        HealthSlider.value = hero.CurrentHP;
        fill.color = gradient.Evaluate(HealthSlider.normalizedValue);
        HungerSlider.value = hero.CurHunger;
        fill.color = gradient.Evaluate(HungerSlider.normalizedValue);

    }

    public void UpdateAbility(Hero hero)
    {
        AbilityInfo.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().SetText("최대 체력 : " + hero.MaxHP);
        AbilityInfo.transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>().SetText("최대 마나 : " + hero.MaxMana);
        AbilityInfo.transform.GetChild(2).transform.GetComponent<TextMeshProUGUI>().SetText("공격력 : " + hero.AttackDamage);
        AbilityInfo.transform.GetChild(3).transform.GetComponent<TextMeshProUGUI>().SetText("방어력 : " + hero.DefensePoint);
        AbilityInfo.transform.GetChild(4).transform.GetComponent<TextMeshProUGUI>().SetText("이동 속도 : " + hero.MoveSpeed);
        AbilityInfo.transform.GetChild(5).transform.GetComponent<TextMeshProUGUI>().SetText("공격 속도 : " + hero.AttackSpeed);
        AbilityInfo.transform.GetChild(6).transform.GetComponent<TextMeshProUGUI>().SetText("공격 범위 : " + hero.AttackRange);
        // AbilityInfo.transform.GetChild(7).transform.GetComponent<TextMeshProUGUI>().SetText("스킬 : " + hero.);
    }

    public void UpdateItems()
    {
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

            if (hero != null && hero.ItemNum > 0)
            {
                GameObject Items = GetChildWithName(HeroUI, "Items").transform.gameObject;
                for (int i = 0; i < 3; i++)
                {
                    if (hero.Items[i] != null && hero.Items[i].GUID != 0)
                    {
                        hero.Items[i].InventoryOrder = (uint)i;
                        Items.transform.GetChild(i).GetChild(0).GetComponent<GetItemInfo>().SetInfo(hero.Items[i]);
                    }
                }
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        // 체력 및 허기, Ability 변화를 UI에 적용함
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

            // HeroUI에 정보 입력
            if (hero != null)
            {
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

                // Ability 값
                AbilityInfo = GetChildWithName(HeroUI, "AbilityInfo").transform.GetChild(0).gameObject;
                AbilityInfo.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().SetText("최대 체력 : " + hero.MaxHP);
                AbilityInfo.transform.GetChild(1).transform.GetComponent<TextMeshProUGUI>().SetText("최대 마나 : " + hero.MaxMana);
                AbilityInfo.transform.GetChild(2).transform.GetComponent<TextMeshProUGUI>().SetText("공격력 : " + hero.AttackDamage);
                AbilityInfo.transform.GetChild(3).transform.GetComponent<TextMeshProUGUI>().SetText("방어력 : " + hero.DefensePoint);
                AbilityInfo.transform.GetChild(4).transform.GetComponent<TextMeshProUGUI>().SetText("이동 속도 : " + hero.MoveSpeed);
                AbilityInfo.transform.GetChild(5).transform.GetComponent<TextMeshProUGUI>().SetText("공격 속도 : " + hero.AttackSpeed);
                AbilityInfo.transform.GetChild(6).transform.GetComponent<TextMeshProUGUI>().SetText("공격 범위 : " + hero.AttackRange);
                // AbilityInfo.transform.GetChild(7).transform.GetComponent<TextMeshProUGUI>().SetText("스킬 : " + hero.);
            }
        }
    }
}
