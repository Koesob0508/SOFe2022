using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EndUI_HeroInfo : MonoBehaviour
{
    public Image heroImg;
    public Image hungerFG;
    public Image healthFG;
    public TMPro.TextMeshProUGUI hungerTxt;
    public TMPro.TextMeshProUGUI healthTxt;
    public void Initalize(Hero before, Hero after, float animTime = 1.0f)
    {
        heroImg.sprite = GameManager.Data.LoadSprite(before.GUID);
        healthFG.fillAmount = before.CurrentHP / before.MaxHP;
        hungerFG.fillAmount = before.CurHunger / 100;
        healthTxt.text = before.CurrentHP.ToString() + "(" + before.MaxHP + ")";
        hungerTxt.text = before.CurHunger.ToString() + "(100)";

        StartCoroutine(AnimateHealthAndHunger(before, after, animTime));
    }

    IEnumerator AnimateHealthAndHunger(Hero before, Hero after, float animTime)
    {
        float stepTime = animTime / 0.05f;
        int step = (int)stepTime;
        for(int i = 0; i < step; i++)
        {
            
        }
        yield break;
    }
}
