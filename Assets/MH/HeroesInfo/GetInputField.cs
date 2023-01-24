using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetInputField : MonoBehaviour
{
    private string userName;
    private GameManager.MbtiType userMBTI;

    [Header("�÷��̾� ���� ������Ʈ")]
    [SerializeField] private Image userImage;
    [SerializeField] private TMP_InputField inputUserName;
    [SerializeField] private TMP_InputField inputUserMBTI;
    [SerializeField] private TextMeshProUGUI outputUserName, outputUserMBTI;
    [SerializeField] private Sprite checkUI, uncheckUI;
    [SerializeField] private GameObject q1, q2;

    [Header("���� ���� ���� ������Ʈ")]
    [SerializeField] private Image heroImage;
    [SerializeField] private TextMeshProUGUI outputHeroName, outputHeroMBTI;
    [SerializeField] private Button leftButton, rightButton;

    [Header("���� ��ư")]
    [SerializeField] private GameObject startButton;

    private List<int> startHerosGUIDs = new List<int>();
    private List<Sprite> heroSprites;
    private int currentStartHeros = 0;

    void Start()
    {
        // UserImage ó��
        userImage.sprite = GameManager.Data.LoadSprite(uint.MaxValue);

        // InputField�� ���� ó��
        inputUserName.onValueChanged.AddListener((value) =>
        {
            OnValueChangeEvent_Name(value);
            OnEndEditEvent_Name(value);
        });

        inputUserMBTI.onValueChanged.AddListener((value) =>
        {
            OnValueChangeEvent_MBTI(value);
            OnEndEditEvent_MBTI(value);
        });

        // LeftButton�� RightButton�� ���� ó��
        leftButton.onClick.AddListener(OnClickLeft);
        rightButton.onClick.AddListener(OnClickRight);

        // ���� ��ư ó��
        startButton.SetActive(false);

        startHerosGUIDs.Add(9);
        startHerosGUIDs.Add(10);
        startHerosGUIDs.Add(14);
        startHerosGUIDs.Add(15);

        heroSprites = new List<Sprite>();

        for (int i = 0; i < startHerosGUIDs.Count; i++)
        {
            heroSprites.Add(GameManager.Data.LoadSprite((uint)startHerosGUIDs[i]));
        }

        print($"startHeros: {startHerosGUIDs.Count}");

    }

    public void OnValueChangeEvent_Name(string str)
    {
        outputUserName.text = $"�̸�: {str}";
        userName = str;
    }

    public void OnEndEditEvent_Name(string str)
    {
        if (str.Length > 0)
        {
            q1.transform.GetChild(1).GetComponent<Image>().sprite = checkUI;
            userName = str;
        }
        else
        {
            q1.transform.GetChild(1).GetComponent<Image>().sprite = uncheckUI;
        }
    }

    public void OnValueChangeEvent_MBTI(string str)
    {
        outputUserMBTI.text = $"MBTI: {str}";
    }

    public void OnEndEditEvent_MBTI(string str)
    {
        if (str.Length > 0 && Enum.IsDefined(typeof(GameManager.MbtiType), str.ToUpper()))
        {
            q2.transform.GetChild(1).GetComponent<Image>().sprite = checkUI;

            if (outputUserName.text == "�̸�:")
            {
                outputUserName.text = "�̸�: ������";
                userName = "������"; // ��� ��� �̸��� ������
                q1.transform.GetChild(1).GetComponent<Image>().sprite = checkUI;
            }
            userMBTI = (GameManager.MbtiType)Enum.Parse(typeof(GameManager.MbtiType), str.ToUpper());

            // �Է��� MBTI�� ��ҹ��� ������� �������̶�� ���ߵ������� �빮�ڷ� �ٲ���
            inputUserMBTI.text = str.ToUpper();
            outputUserMBTI.text = "MBTI: " + str.ToUpper();

            startButton.SetActive(true);
        }
        else
        {
            q2.transform.GetChild(1).GetComponent<Image>().sprite = uncheckUI;
        }
    }

    public void EnrollUser()
    {
        //Hero UserHero = (Hero)GameManager.Data.LoadObject(00, GameManager.ObjectType.Hero);
        Hero UserHero = (Hero)GameManager.Data.LoadObject((uint)startHerosGUIDs[currentStartHeros], GameManager.ObjectType.Hero);
        GameManager.Data.UserName = userName;
        GameManager.Data.UserGuid = uint.MaxValue;
        GameManager.Data.UserMbti = userMBTI;

        // Enroll User Hero
        UserHero.IsActive = true;
        UserHero.MBTI = userMBTI;
        GameManager.Hero.HeroList.Add(UserHero);

        // Team Score�� �ش� Hero�� Update
        GameManager.Relation.NewHeroScore(UserHero);

        //GameManager.Data.Save();

    }

    public void OnClickRight()
    {
        currentStartHeros = (currentStartHeros + 1) % startHerosGUIDs.Count;

        heroImage.sprite = heroSprites[currentStartHeros];

    }
    public void OnClickLeft()
    {
        currentStartHeros = (currentStartHeros - 1);
        if (currentStartHeros < 0)
        {
            currentStartHeros = startHerosGUIDs.Count - 1;
        }

        heroImage.sprite = heroSprites[currentStartHeros];
    }

}
