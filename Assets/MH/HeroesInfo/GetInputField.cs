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

    [Header("플레이어 관련 오브젝트")]
    [SerializeField] private Image userImage;
    [SerializeField] private TMP_InputField inputUserName;
    [SerializeField] private TMP_InputField inputUserMBTI;
    [SerializeField] private TextMeshProUGUI outputUserName, outputUserMBTI;
    [SerializeField] private Sprite checkUI, uncheckUI;
    [SerializeField] private GameObject q1, q2;

    [Header("동료 영웅 관련 오브젝트")]
    [SerializeField] private Image heroImage;
    [SerializeField] private TextMeshProUGUI outputHeroName, outputHeroMBTI;
    [SerializeField] private Button leftButton, rightButton;
    [SerializeField] private Button selectHeroButton;
    [SerializeField] private GameObject q3;

    [Header("시작 버튼")]
    [SerializeField] private Button startButton;

    private List<uint> startHerosGUIDs;
    private List<Sprite> heroSprites;
    private List<Hero> heroes;
    private int currentStartHeros = 0;

    private bool q1Condition = false, q2Condition = false, q3Condition = false;

    void Start()
    {
        // UserImage 처리
        //userImage.sprite = GameManager.Data.LoadSprite("/Resources/PlayerImages/1");

        // InputField의 연결 처리
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

        // LeftButton과 RightButton에 대한 처리
        leftButton.onClick.AddListener(OnClickLeft);
        rightButton.onClick.AddListener(OnClickRight);
        selectHeroButton.onClick.AddListener(() => EnrollHero(heroes[currentStartHeros]));

        // 시작 버튼 처리
        startButton.onClick.AddListener(EnrollUser);
        startButton.onClick.AddListener(GameManager.Scene.ToStageSelectScene);
        startButton.gameObject.SetActive(false);

        startHerosGUIDs = new List<uint>
        {
            9,
            10,
            14,
            15
        };
        heroSprites = new List<Sprite>();
        heroes = InitRandomHeroes(startHerosGUIDs);

        UpdateHeroInfo();

        for (int i = 0; i < startHerosGUIDs.Count; i++)
        {
            heroSprites.Add(GameManager.Data.LoadSprite((uint)startHerosGUIDs[i]));
        }

        print($"startHeros: {startHerosGUIDs.Count}");
    }

    public void OnValueChangeEvent_Name(string str)
    {
        outputUserName.text = $"이름: {str}";
        userName = str;
    }

    public void OnEndEditEvent_Name(string str)
    {
        if (str.Length > 0)
        {
            q1.transform.GetChild(1).GetComponent<Image>().sprite = checkUI;
            userName = str;

            q1Condition = true;
            UpdateStartButton();
        }
        else
        {
            q1.transform.GetChild(1).GetComponent<Image>().sprite = uncheckUI;
            q1Condition = false;
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

            if (outputUserName.text == "이름:")
            {
                outputUserName.text = "이름: 뫄뫄뫄";
                userName = "뫄뫄뫄"; // 어떻게 사람 이름이 뫄뫄뫄
                q1.transform.GetChild(1).GetComponent<Image>().sprite = checkUI;
            }
            userMBTI = (GameManager.MbtiType)Enum.Parse(typeof(GameManager.MbtiType), str.ToUpper());

            // 입력한 MBTI가 대소문자 상관없이 정상적이라면 알잘딱깔센으로 대문자로 바꿔줌
            inputUserMBTI.text = str.ToUpper();
            outputUserMBTI.text = "MBTI: " + str.ToUpper();
            q2Condition = true;
            UpdateStartButton();
        }
        else
        {
            q2.transform.GetChild(1).GetComponent<Image>().sprite = uncheckUI;
            q2Condition = false;
        }
    }
    public void OnClickRight()
    {
        currentStartHeros = (currentStartHeros + 1) % startHerosGUIDs.Count;

        heroImage.sprite = heroSprites[currentStartHeros];

        UpdateHeroInfo();

    }
    public void OnClickLeft()
    {
        currentStartHeros = (currentStartHeros - 1);
        if (currentStartHeros < 0)
        {
            currentStartHeros = startHerosGUIDs.Count - 1;
        }

        heroImage.sprite = heroSprites[currentStartHeros];

        UpdateHeroInfo();
    }

    private List<Hero> InitRandomHeroes(List<uint> _indices)
    {
        List<Hero> result = new List<Hero>();

        foreach(uint index in _indices)
        {
            Hero initHero = (Hero)GameManager.Data.LoadObject(index, GameManager.ObjectType.Hero);
            initHero.MBTI = (GameManager.MbtiType)UnityEngine.Random.Range(0, 16);

            result.Add(initHero);
        }

        return result;
    }

    private void UpdateHeroInfo()
    {
        outputHeroName.text = "이름: " + heroes[currentStartHeros].Name;
        outputHeroMBTI.text = "MBTI: " + heroes[currentStartHeros].MBTI;
    }

    private void EnrollHero(Hero _hero)
    {
        _hero.IsActive = true;
        GameManager.Hero.HeroList.Add(_hero);
        GameManager.Relation.NewHeroScore(_hero);

        q3.transform.GetChild(0).GetComponent<Image>().sprite = checkUI;
        q3Condition = true;

        selectHeroButton.gameObject.SetActive(false);

        UpdateStartButton();
    }

    private void UpdateStartButton()
    {
        if (!q1Condition) return;
        if (!q2Condition) return;
        if (!q3Condition) return;

        startButton.gameObject.SetActive(true);
    }

    private void EnrollUser()
    {
        // Hero UserHero = (Hero)GameManager.Data.LoadObject((uint)startHerosGUIDs[currentStartHeros], GameManager.ObjectType.Hero);
        GameManager.Data.UserName = userName;
        GameManager.Data.UserGuid = (uint)startHerosGUIDs[currentStartHeros];
        GameManager.Data.UserMbti = userMBTI;

        // Enroll User Hero
        // UserHero.IsActive = false;
        // UserHero.MBTI = userMBTI;
        // GameManager.Hero.HeroList.Add(UserHero);

        //// Team Score에 해당 Hero를 Update
        // GameManager.Relation.NewHeroScore(UserHero);

        //GameManager.Data.Save();

    }


}
