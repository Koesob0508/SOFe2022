using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpdateBasicUI : MonoBehaviour
{


    public Gradient gradient;
    public GameObject User, Money, TeamStatus;

    private Slider TeamSlider; // Team의 체력 상태
    private Image fill;

    // Start is called before the first frame update
    void Start()
    {
        //  User 정보 Set
        // 임시로 Data 지정
        User.transform.GetChild(0).GetComponent<Image>().sprite = GameManager.Data.LoadSprite(0);
        User.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText("엠비티아이 맹신론자");
        User.transform.GetChild(3).GetComponent<TextMeshProUGUI>().SetText("ENFP");
        User.transform.GetChild(4).GetComponent<TextMeshProUGUI>().SetText("Stage: 00stage..");

        // 보유 Gold Set
        Money.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(GameManager.Data.Money.ToString() +"G");

        TeamSlider = TeamStatus.transform.GetChild(0).GetComponent<Slider>();
        foreach (Hero hero in GameManager.Hero.GetHeroList())
        {
            TeamSlider.maxValue += hero.MaxHP;
            TeamSlider.value += hero.CurrentHP;
        }

        fill = TeamSlider.transform.GetChild(0).GetComponent<Image>();
        fill.color = gradient.Evaluate(TeamSlider.normalizedValue);
    }

    // Update is called once per frame
    void Update()
    {
        // 보유 Gold Set
        Money.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(GameManager.Data.Money.ToString() + "G");

    }
}
