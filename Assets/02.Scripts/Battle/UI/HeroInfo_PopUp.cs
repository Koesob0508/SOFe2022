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
        HealthBar.fillAmount = hero.CurrentHP / (float)hero.MaxHP;
        HungerBar.fillAmount = hero.CurHunger / 100.0f;
    }

    public void UpdateInfo()
    {
        SetUpData(heroData);
    }
}
