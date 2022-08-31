using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleEndUI : MonoBehaviour
{
    public GameObject WinPanel;
    public GameObject LosePanel;
    public void Initalize(bool bIsWin)
    {
        WinPanel.GetComponentInChildren<Button>().onClick.AddListener(delegate { GameManager.Battle.FinishBattle(bIsWin); });
        LosePanel.GetComponentInChildren<Button>().onClick.AddListener(delegate { GameManager.Battle.FinishBattle(bIsWin); });

        if (bIsWin)
        {
            WinPanel.gameObject.SetActive(true);
            LosePanel.gameObject.SetActive(false);
        }
        else
        {
            WinPanel.gameObject.SetActive(false);
            LosePanel.gameObject.SetActive(true);
        }
    }
}
