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
        int step = (int)(animTime / 0.1f);
        float stepTime = 0.1f / animTime;
        float healthStep = (after.CurrentHP - before.CurrentHP) / step;
        float hungerStep = 10 / step;

        float health = before.CurrentHP;
        float hunger = before.CurHunger;

        for(int i = 0; i < step; i++)
        {
            health += healthStep;
            hunger -= hungerStep;

            healthFG.fillAmount = health / before.MaxHP;
            hungerFG.fillAmount = hunger / 100;

            healthTxt.text = ((int)health).ToString() + "(" + before.MaxHP + ")";
            hungerTxt.text = ((int)hunger).ToString() + "(100)";

            yield return new WaitForSeconds(stepTime);
        }
        healthFG.fillAmount = after.CurrentHP / after.MaxHP;
        hungerFG.fillAmount = after.CurHunger / 100;

        if((int)after.CurrentHP < 0)
        {
            healthTxt.text = "0(" + after.MaxHP.ToString() + ")";
        }
        else
            healthTxt.text = ((int)after.CurrentHP).ToString() + "(" + after.MaxHP.ToString() + ")";
        hungerTxt.text = after.CurHunger.ToString() + "(100)";
        yield break;
    }
}
