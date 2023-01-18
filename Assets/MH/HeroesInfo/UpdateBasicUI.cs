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
        User.transform.GetChild(0).GetComponent<Image>().sprite = GameManager.Data.LoadSprite(GameManager.Data.UserGuid);
        User.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText(GameManager.Data.UserName);
        User.transform.GetChild(3).GetComponent<TextMeshProUGUI>().SetText(GameManager.Data.UserMbti.ToString());
        User.transform.GetChild(4).GetComponent<TextMeshProUGUI>().SetText("Stage: 00stage..");
        //User.transform.GetChild(4).GetComponent<TextMeshProUGUI>().SetText(GameManager.Stage.GetLastStage());

        // 보유 Gold Set
        Money.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(GameManager.Data.Money.ToString() + "G");

        // Team Point
        TeamSlider = TeamStatus.transform.GetChild(0).GetComponent<Slider>();
        int TeamScore = GameManager.Relation.GetTeamScore();

        TeamSlider.maxValue = 150;
        TeamSlider.minValue = -150;
        TeamSlider.value = GameManager.Relation.GetTeamScore();

        fill = TeamSlider.transform.GetChild(0).GetComponent<Image>();
        fill.color = gradient.Evaluate(TeamSlider.normalizedValue);

        if (TeamScore <= 0)
            TeamStatus.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().SetText("F");
        else if (1<= TeamScore && TeamScore <= 5)
            TeamStatus.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().SetText("E");
        else if (6 <= TeamScore && TeamScore <= 10)
            TeamStatus.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().SetText("D");
        else if (11 <= TeamScore && TeamScore <= 15)
            TeamStatus.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().SetText("C");
        else if (16 <= TeamScore && TeamScore <= 20)
            TeamStatus.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().SetText("B");
        else if (21 <= TeamScore && TeamScore <= 25)
            TeamStatus.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().SetText("A");
        else if (26 <= TeamScore)
            TeamStatus.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().SetText("S");
    }

    // Update is called once per frame
    void Update()
    {
        // 보유 Gold Set
        Money.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(GameManager.Data.Money.ToString() + "G");

    }
}
