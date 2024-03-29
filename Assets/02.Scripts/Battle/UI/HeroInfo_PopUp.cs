using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroInfo_PopUp : MonoBehaviour
{
    public TMPro.TextMeshProUGUI NameTXT;
    public TMPro.TextMeshProUGUI PersonalityTXT;
    public GameObject ItemPanel;
    public Image HealthBar;
    public TMPro.TextMeshProUGUI HpValTxt;
    public TMPro.TextMeshProUGUI HungerValTxt;
    public Image HungerBar;
    public TMPro.TextMeshProUGUI HpTXT;
    public TMPro.TextMeshProUGUI ManaTXT;
    public TMPro.TextMeshProUGUI AdTxt;
    public TMPro.TextMeshProUGUI DpTxt;
    public TMPro.TextMeshProUGUI MsTxt;
    public TMPro.TextMeshProUGUI AsTxt;
    public TMPro.TextMeshProUGUI ArTxt;
    public TMPro.TextMeshProUGUI SkillTxt;
    Hero heroData;
    public void SetUpData(Hero hero)
    {
        heroData = hero;
        NameTXT.text = hero.Name;
        PersonalityTXT.text = hero.MBTI.ToString();

        Color color = Color.black;
        switch (hero.MBTI)
        {
            case GameManager.MbtiType.INFP:
                ColorUtility.TryParseHtmlString("#66C8BB", out color);
                break;
            case GameManager.MbtiType.ENFP:
                ColorUtility.TryParseHtmlString("#F5C012", out color);
                break;
            case GameManager.MbtiType.INFJ:
                ColorUtility.TryParseHtmlString("#64ADCB", out color);
                break;
            case GameManager.MbtiType.ENFJ:
                ColorUtility.TryParseHtmlString("#13A5B4", out color);
                break;
            case GameManager.MbtiType.INTJ:
                ColorUtility.TryParseHtmlString("#C3C3C3", out color);
                break;
            case GameManager.MbtiType.ENTJ:
                ColorUtility.TryParseHtmlString("#37373F", out color);
                break;
            case GameManager.MbtiType.INTP:
                ColorUtility.TryParseHtmlString("#91ADB9", out color);
                break;
            case GameManager.MbtiType.ENTP:
                ColorUtility.TryParseHtmlString("#B62135", out color);
                break;
            case GameManager.MbtiType.ISFP:
                ColorUtility.TryParseHtmlString("#C8E682", out color);
                break;
            case GameManager.MbtiType.ESFP:
                ColorUtility.TryParseHtmlString("#F62662", out color);
                break;
            case GameManager.MbtiType.ISTP:
                ColorUtility.TryParseHtmlString("#363E62", out color);
                break;
            case GameManager.MbtiType.ESTP:
                ColorUtility.TryParseHtmlString("#C82F37", out color);
                break;
            case GameManager.MbtiType.ISFJ:
                ColorUtility.TryParseHtmlString("#F4BF59", out color);
                break;
            case GameManager.MbtiType.ESFJ:
                ColorUtility.TryParseHtmlString("#DA5A8B", out color);
                break;
            case GameManager.MbtiType.ISTJ:
                ColorUtility.TryParseHtmlString("#55545A", out color);
                break;
            case GameManager.MbtiType.ESTJ:
                ColorUtility.TryParseHtmlString("#5CA286", out color);
                break;
        }
        PersonalityTXT.color = color;

        HealthBar.fillAmount = hero.CurrentHP / (float)hero.MaxHP;
        HungerBar.fillAmount = hero.CurHunger / 100.0f;

        HpValTxt.text = ((int)hero.CurrentHP).ToString() + "(" + ((int)hero.MaxHP).ToString() + ")";
        HungerValTxt.text = ((int)hero.CurHunger).ToString() + "(100)";

        HpTXT.text = "HP : " + hero.MaxHP.ToString();
        ManaTXT.text = "에너지 : " + hero.MaxMana.ToString();
        AdTxt.text = "공격력 : " + hero.AttackDamage.ToString();
        DpTxt.text = "방어력 : " + hero.AttackDamage.ToString();
        MsTxt.text = "이동속도 : " + hero.MoveSpeed.ToString();
        AsTxt.text = "공격속도 : " + hero.AttackSpeed.ToString();
        ArTxt.text = "공격범위 : " + hero.AttackRange.ToString();
        SkillTxt.text = "스킬 : ";

        Image[] m_ItemImages = ItemPanel.GetComponentsInChildren<Image>();
        //for(int i = 0; i < hero.Items.Count; i++)
        //{
        //    m_ItemImages[i].sprite = GameManager.Data.LoadSprite(hero.Items[i].GUID);
        //}
        for (int i = 0; i < 3; i++)
        {
            if (hero.Items[i] != null)
            {
                m_ItemImages[i + 1].color = new Color32(255, 255, 255, 255);
                m_ItemImages[i+1].sprite = GameManager.Data.LoadSprite(hero.Items[i].GUID);
            }
            else
            {
                m_ItemImages[i+1].color = new Color32(0,0,0,0); 
            }
        }
    }

    public void UpdateInfo()
    {
        SetUpData(heroData);
    }
}
