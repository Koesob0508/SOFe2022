using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetInputField : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Name, MBTI;
    public Sprite CheckUI, UnCheckUI;
    public GameObject Q1, Q2;
    public GameObject StartButton;
    private string User_Name;
    private GameManager.MbtiType User_Mbti;

    void Start()
    {
        StartButton.SetActive(false);
    }

    public void OnValueChangeEvent_Name(string str)
    {
        Name.text = $"ÀÌ¸§: {str}";
        User_Name = str;
    }

    public void OnValueChangeEvent_Mbti(string str)
    {
        MBTI.text = $"MBTI: {str}";
    }

    public void OnEndEditEvent_Name(string str)
    {
        if (str.Length > 0)
        {
            Q1.transform.GetChild(1).GetComponent<Image>().sprite = CheckUI;
            User_Name = str;
        }
        else
        {
            Q1.transform.GetChild(1).GetComponent<Image>().sprite = UnCheckUI;
        }
    }
    public void OnEndEditEvent_Mbti(string str)
    {
        if (str.Length > 0 && Enum.IsDefined(typeof(GameManager.MbtiType), str))
        {
            Q2.transform.GetChild(1).GetComponent<Image>().sprite = CheckUI;

            if (Name.text == "ÀÌ¸§:")
            {
                Name.text = "ÀÌ¸§: ¸ú¸ú¸ú";
                User_Name = "¸ú¸ú¸ú";
                Q1.transform.GetChild(1).GetComponent<Image>().sprite = CheckUI;
            }
            User_Mbti = (GameManager.MbtiType)Enum.Parse(typeof(GameManager.MbtiType), str);
            StartButton.SetActive(true);
        }
        else
        {
            Q2.transform.GetChild(1).GetComponent<Image>().sprite = UnCheckUI;
        }
    }

    public void EnrollUser()
    {
        Hero UserHero = (Hero)GameManager.Data.LoadObject(00, GameManager.ObjectType.Hero);
        GameManager.Data.UserName = User_Name;
        GameManager.Data.UserMbti = User_Mbti;

        // Enroll User Hero
        UserHero.IsActive = true;
        UserHero.MBTI = User_Mbti;
        GameManager.Hero.HeroList.Add(UserHero);

        // Team Score¿¡ ÇØ´ç Hero¸¦ Update
        GameManager.Relation.NewHeroScore(UserHero);

        //GameManager.Data.Save();
    }

    public void OnSelectEvent(string str)
    {

    }

}
