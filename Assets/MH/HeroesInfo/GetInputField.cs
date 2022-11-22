using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetInputField : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Name, MBTI;
    public Sprite CheckUI;
    public GameObject Q1, Q2;
    public GameObject StartButton;

    void Start()
    {
        StartButton.SetActive(false);
    }

    public void OnValueChangeEvent_Name(string str)
    {
        Name.text = $"ÀÌ¸§: {str}";
    }

    public void OnValueChangeEvent_Mbti(string str)
    {
        MBTI.text = $"MBTI: {str}";
    }

    public void OnEndEditEvent_Name(string str)
    {
        if (str.Length > 0)
            Q1.transform.GetChild(1).GetComponent<Image>().sprite = CheckUI;
    }
    public void OnEndEditEvent_Mbti(string str)
    {
        if (str.Length > 0)
        {
            Q2.transform.GetChild(1).GetComponent<Image>().sprite = CheckUI;

            if (Name.text == "ÀÌ¸§:")
            {
                Name.text = "ÀÌ¸§: ¸ú¸ú¸ú";
            }
            StartButton.SetActive(true);
        }
    }

    public void EnrollUser()
    {
        Hero UserHero = (Hero)GameManager.Data.LoadObject(01, GameManager.ObjectType.Hero);

        GameManager.Data.Save();
    }

    public void OnSelectEvent(string str)
    {

    }

}
