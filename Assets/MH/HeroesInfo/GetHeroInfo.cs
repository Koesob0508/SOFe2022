using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetHeroInfo : MonoBehaviour
{
    //public List<Hero> HeroList = GameManager.Hero.GetHeroList();
    public List<Hero> HeroList = new List<Hero>();
    public Dictionary<uint, GameObject> HeroUIList = new Dictionary<uint, GameObject>();

    public GameObject HeroUIObject;
    GameObject HeroUI;

    // Status
    public Gradient gradient;

    // Choose Stage Scene 시작할 때 Init()
    // 마지막에 변경사항 적용된 HeroList를 HeroManager에게 전달 (?)
    //  ㄴ 체력, 장비, 새로 영입한 Hero의 정보 ... 등

    void Test()
    {
        Debug.Log("Hero Info Test");

        Hero hero1 = new Hero();
        Hero hero2 = new Hero();
        Hero hero3 = new Hero();
        Hero hero4 = new Hero();
        hero1 = GameManager.Instance.LoadObject(1, GameManager.ObjectType.Hero) as Hero;
        hero2 = GameManager.Instance.LoadObject(2, GameManager.ObjectType.Hero) as Hero;
        hero3 = GameManager.Instance.LoadObject(3, GameManager.ObjectType.Hero) as Hero;
        hero4 = GameManager.Instance.LoadObject(4, GameManager.ObjectType.Hero) as Hero;

        HeroList.Add(hero1);
        HeroList.Add(hero2);
        HeroList.Add(hero3);
        HeroList.Add(hero4);
    }

    // Init()로 변경할 것
    void Start()
    {
        Test();

        int HeroNUM = HeroList.Count;

        for (int i = 0; i < HeroNUM; i++)
        {
            HeroUI = Instantiate(HeroUIObject, transform.position, Quaternion.identity);
            HeroUI.transform.parent = transform;
            HeroUIList.Add(HeroList[i].GUID, HeroUI);
        }

        Debug.Log("Start Set Status");
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
            Debug.Log("오잉");
            return null;
        }
    }

    private void SetStatus()
    {
        // Status
        Image HeroImage;
        TextMeshProUGUI HeroName, HeroMbti;
        GameObject HealthBar, HungerBar;
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

            // HeroUI에 정보 입력
            if (hero != null)
            {
                HeroImage = GetChildWithName(HeroUI, "HeroImage").transform.GetComponent<Image>();
                // HeroImage.sprite = 

                Debug.Log(hero.Name);
                HeroName = GetChildWithName(HeroUI, "Name").transform.GetComponent<TextMeshProUGUI>();
                HeroName.text = hero.Name;
                

                HeroMbti = GetChildWithName(HeroUI, "MBTI").transform.GetComponent<TextMeshProUGUI>();
                // HeroMbti.text = hero.

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
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
