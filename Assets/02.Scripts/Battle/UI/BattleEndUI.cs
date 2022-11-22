using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleEndUI : MonoBehaviour
{
    public GameObject WinPanel;
    public GameObject LosePanel;
    public void Initalize(bool bIsWin, List<Hero> BattleHerosBefore, List<Hero> BattleHerosAfter)
    {
        WinPanel.GetComponentInChildren<Button>().onClick.AddListener(delegate { GameManager.Battle.FinishBattle(bIsWin); });
        LosePanel.GetComponentInChildren<Button>().onClick.AddListener(delegate { GameManager.Battle.FinishBattle(bIsWin); });

        if (bIsWin)
        {
            GetComponent<Image>().color = new Color(0.1f,0.1f,0.1f,0.9f);

            WinPanel.gameObject.SetActive(true);
            LosePanel.gameObject.SetActive(false);
            WinPanel.gameObject.GetComponent<BattleWinPanel>().Initalize(BattleHerosBefore, BattleHerosAfter);
            
        }
        else
        {

            WinPanel.gameObject.SetActive(false);
            LosePanel.gameObject.SetActive(true);
        }
    }
}
