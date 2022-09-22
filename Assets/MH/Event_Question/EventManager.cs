using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EventManager : MonoBehaviour
{
    #region Enum Types
    public enum EventType
    {
        LifeLake,
        CampFire,
        TelltheTruth,
        Merchant,
        Villain,
        QnA
    }
    #endregion

    // 해당 이벤트 지역에 나타날 지역 유형
    private EventType Type;

    public GameObject QuestionBox, MBTIQnABox;
    private GameObject EventPanel, Name, Ask, choiceA, choiceB;
    public int ChoiceMode;

    void Start()
    {
        ChoiceMode = 0;
        // Type = (EventType)5;
        Type = (EventType)Random.Range(0, 5);

        if ((int)Type < 5)
        {
            EventPanel = Instantiate(QuestionBox, new Vector3(1200, 540, 0), Quaternion.identity);
            EventPanel.transform.SetParent(GameObject.FindWithTag("UI").transform);

            Name = EventPanel.transform.GetChild(0).gameObject;
            Ask = EventPanel.transform.GetChild(1).gameObject;
            choiceA = EventPanel.transform.GetChild(2).gameObject;
            choiceB = EventPanel.transform.GetChild(3).gameObject;

            choiceA.GetComponent<Button>().onClick.AddListener(ChoiceOptionA);
            //choiceA.GetComponent<Button>().onClick.RemoveListener(ChoiceOptionA);
            choiceB.GetComponent<Button>().onClick.AddListener(ChoiceOptionB);
            //choiceB.GetComponent<Button>().onClick.RemoveListener(ChoiceOptionB);
        }

        // Ramdom으로 선택된 Event 유형에 따라서 Set
        SetEvent(Type);
    }

    public void SetEvent(EventType type)
    {
        switch (Type)
        {
            case EventType.LifeLake:
                {
                    Name.GetComponent<TextMeshProUGUI>().text = "생명의 샘";
                    Ask.GetComponent<TextMeshProUGUI>().text = "용병들은 숲을 지나가다 생명의 샘을 발견했습니다! \n하지만 너무 조그마해서 모두가 충분히 마실 순 없습니다.";
                    choiceA.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "모든 용병의 생명력을 조금씩 회복시킨다";
                    choiceB.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "한 용병의 생명력을 완전히 회복시킨다";
                    break;
                }
            case EventType.CampFire:
                {
                    Name.GetComponent<TextMeshProUGUI>().text = "캠프파이어";
                    Ask.GetComponent<TextMeshProUGUI>().text = "용병들이 오늘 밤 캠프파이어를 원합니다 \n어떤 활동을 하는 것이 좋을까요?";
                    choiceA.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "진실게임 \n서로가 하지 못했던 속 깊은 대화를 나눕니다\n팀워크 포인트가 영구적으로 2 증가합니다.";
                    choiceB.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "휴식\n모닥불에 모여 앉아 하루를 마무리합니다\n모든 용병의 생명력을 10%씩 회복합니다.";
                    break;
                }
            case EventType.TelltheTruth:
                {
                    Name.GetComponent<TextMeshProUGUI>().text = "진실의 방으로";
                    Ask.GetComponent<TextMeshProUGUI>().text = "우리 팀 중 누군가는 거짓말을 하고 있을지도 모릅니다.\n당신은 진실을 원하십니까?";
                    choiceA.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "진실을 원한다.";
                    choiceB.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "진실을 원하지 않는다.";
                    break;
                }
            case EventType.Merchant:
                {
                    Name.GetComponent<TextMeshProUGUI>().text = "떠돌이 상인";
                    Ask.GetComponent<TextMeshProUGUI>().text = "무언가를 잔뜩 실은 마차를 끌고 허름한 차림의 누군가가 지나갑니다.\n근방에서 유명한 떠돌이 상인입니다.";
                    choiceA.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "금화 100을 지불하고, 일반 장비를 구매합니다.";
                    choiceB.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "금화 1000을 지불하고, 특수 장비를 구매합니다.";
                    break;
                }
            case EventType.Villain:
                {
                    Name.GetComponent<TextMeshProUGUI>().text = "악당 출현";
                    Ask.GetComponent<TextMeshProUGUI>().text = "용병들은 숲을 지나가다 악당의 습격을 받았습니다.\n다들 지쳐있는데 어떡하지?";
                    choiceA.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "이보 전진을 위한 일보 후퇴...\n도망갑니다.\n그러나 모든 용병의 허기가 일부 감소합니다.";
                    choiceB.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "맞서 싸웁니다.\n금화 500을 획득할 수 있으나, 모든 용병이 체력을 일부 잃습니다.";
                    break;
                }
            case EventType.QnA:
                {
                    int option = 0;
                    SetQnA(option);
                    break;
                }
        }
    }

    public void ChoiceOptionA()
    {
        ChoiceMode = 1;
    }

    public void ChoiceOptionB()
    {
        ChoiceMode = 2;
    }


    private void LifeLake(int mode)
    {
        // 선택지
        Debug.Log("LifeLake " + mode + " 선택지의 기능을 수행합니다");
        Destroy(EventPanel);

        // 선택에 따른 결과를 띄워준다

        ChoiceMode = 0;
    }

    private void CampFire(int mode)
    {
        // 선택지
        Debug.Log("CampFire " + mode + " 선택지의 기능을 수행합니다");
        Destroy(EventPanel);

        // 선택에 따른 결과를 띄워준다

        ChoiceMode = 0;
    }
    private void TelltheTruth(int mode)
    {
        // 선택지
        Debug.Log("TelltheTruth " + mode + " 선택지의 기능을 수행합니다");
        Destroy(EventPanel);

        // 선택에 따른 결과를 띄워준다

        ChoiceMode = 0;
    }
    private void Merchant(int mode)
    {
        // 선택지
        Debug.Log("Merchant " + mode + " 선택지의 기능을 수행합니다");
        Destroy(EventPanel);

        // 선택에 따른 결과를 띄워준다

        ChoiceMode = 0;
    }
    private void Villain(int mode)
    {
        // 선택지
        Debug.Log("Villain " + mode + " 선택지의 기능을 수행합니다");
        Destroy(EventPanel);

        // 선택에 따른 결과를 띄워준다

        ChoiceMode = 0;
    }

    private void SetQnA(int option)
    {
        EventPanel = Instantiate(MBTIQnABox, new Vector3(1200, 540, 0), Quaternion.identity);
        EventPanel.transform.SetParent(GameObject.FindWithTag("UI").transform);

        Name = EventPanel.transform.GetChild(0).gameObject;
        Ask = EventPanel.transform.GetChild(1).gameObject;
        Name.GetComponent<TextMeshProUGUI>().text = "Q n A";
        Ask.GetComponent<TextMeshProUGUI>().text = MBTIQnABox.GetComponent<ChioiceController>().Ask;
        switch (option)
        {
            case 0:
                {
                    for (int i = 0; i < MBTIQnABox.GetComponent<ChioiceController>().choiceNum; i++)
                    {
                        // EventPanel.transform.GetChild(2).GetChild(0).GetChild(0).gameObject;
                        GameObject choice = EventPanel.transform.GetChild(2).GetChild(0).GetChild(i).gameObject;
                        choice.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = choice.GetComponent<Choice>().text;
                    }
                    break;
                }
        }
    }

    private void QnA(int mode)
    {
        Choice choice1 = new Choice();
        choice1.text = "";
        choice1.num = 3;
        choice1.TargetMbti = new List<GameManager.MbtiType> { 
            GameManager.MbtiType.ESFP, 
            GameManager.MbtiType.ESFP, 
            GameManager.MbtiType.ESFP 
        };


        ChioiceController QnA = new ChioiceController();

    }

    void Update()
    {
        if (ChoiceMode > 0)
        {
            switch (Type)
            {
                case EventType.LifeLake:
                    {
                        LifeLake(ChoiceMode);
                        break;
                    }
                case EventType.CampFire:
                    {
                        CampFire(ChoiceMode);
                        break;
                    }
                case EventType.TelltheTruth:
                    {
                        TelltheTruth(ChoiceMode);
                        break;
                    }
                case EventType.Merchant:
                    {
                        Merchant(ChoiceMode);
                        break;
                    }
                case EventType.Villain:
                    {
                        Villain(ChoiceMode);
                        break;
                    }
                case EventType.QnA:
                    {
                        break;
                    }
            }
        }
    }
}

