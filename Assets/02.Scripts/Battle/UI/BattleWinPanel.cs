using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BattleWinPanel : MonoBehaviour
{
    public Button nextSceneBtn;
    public TMPro.TextMeshProUGUI moneyText;

    public GameObject infoPanel;
    public GameObject heroInfoPrefab;

    float animTime = 1.0f;
    public void Initalize(List<Hero> BattleHerosBefore, List<Hero> BattleHerosAfter)
    {
        uint earnedMoney = GameManager.Battle.GetEarnedMoney();
        uint currentMoney = GameManager.Data.Money;

        if (nextSceneBtn == null)
            throw new System.Exception("nextSceneBtn didnt assigned");
        if (moneyText == null)
            throw new System.Exception("moneyText didnt assigned");

        nextSceneBtn.gameObject.SetActive(false);

        for(int i = 0; i < BattleHerosBefore.Count; i++)
        {
            GameObject g = Instantiate(heroInfoPrefab, infoPanel.transform);
            g.GetComponent<EndUI_HeroInfo>().Initalize(BattleHerosBefore[i], BattleHerosAfter[i], animTime);
        }

        StartCoroutine(MoneyIncrease(currentMoney,earnedMoney, animTime));
    }

    IEnumerator MoneyIncrease(uint from, uint amount, float time)
    {
        float stepTime = 0.05f;
        int stepAmount = (int)(amount * stepTime);
        for (uint i = 0; i < (int)(time/stepTime); i++)
        {
            moneyText.text = (from + stepAmount).ToString() + "G";
            yield return new WaitForSeconds(stepTime);
        }
        moneyText.text = (from + amount).ToString() + "G";
        nextSceneBtn.gameObject.SetActive(true);
        yield break;
    }
}
