using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public partial class RelationshipManager : MonoBehaviour
{
    public List<Hero> HeroList = new List<Hero>();
    public sbyte RelationScore;
    [SerializeField] private List<CustomSignal> signals;
    [SerializeField] private GameObject noticeBallon;
    [SerializeField] private TMP_Text noticeText;
    [SerializeField] private TMP_Text explainText;

    public void Init()
    {
        Debug.Log("Relationship Manager Init");
        DontDestroyOnLoad(this);

        foreach (CustomSignal customSignal in signals)
        {
            customSignal.Init();
        }
    }

    // 원래 정의된 기본 관계 점수
    public sbyte[,] OriginScore = new sbyte[,] {{+3, +3, +3, +5, +3, +5, +3, +3, -3, -3, -3, -3, -3, -3, -3, -3 },
                                                {+3, +3, +5, +3, +5, +3, +3, +3, -3, -3, -3, -3, -3, -3, -3, -3 },
                                                {+3, +5, +3, +3, +3, +3, +3, +5, -3, -3, -3, -3, -3, -3, -3, -3 },
                                                {+5, +3, +3, +3, +3, +3, +3, +3, +5, -3, -3, -3, -3, -3, -3, -3 },
                                                {+3, +5, +3, +3, +3, +3, +3, +5, +1, +1, +1, +1, 0, 0, 0, 0, },
                                                {+5, +3, +3, +3, +3, +3, +5, +3, +1, +1, +1, +1, +1, +1, +1, +1 },
                                                {+3, +3, +3, +3, +3, +5, +3, +3, +1, +1, +1, +1, 0, 0, 0, +5 },
                                                {+3, +3, +5, +3, +5, +5, +3, +3, +1, +1, +1, +1, 0, 0, 0, 0 },
                                                {-3, -3, -3, +5, +1, +1, +1, +1, 0, 0, 0, 0, +1, +5, +1, +5 },
                                                {-3, -3, -3, -3, +1, +1, +1, +1, 0, 0, 0, 0, +5, +1, +5, +1 },
                                                {-3, -3, -3, -3, +1, +1, +1, +1, 0, 0, 0, 0, +1, +5, +1, +5 },
                                                {-3, -3, -3, -3, +1, +1, +1, +1, 0, 0, 0, 0, +5, +1, +5, +1 },
                                                {-3, -3, -3, -3, 0, +1, 0, 0, +1, +5, +1, +5, +3, +3, +3, +3 },
                                                {-3, -3, -3, -3, 0, +1, 0, 0, +5, +1, +5, +1, +3, +3, +3, +3 },
                                                {-3, -3, -3, -3, 0, +1, 0, 0, +1, +5, +1, +5, +3, +3, +3, +3 },
                                                {-3, -3, -3, -3, 0, +1, +5, 0, +5, +1, +5, +1, +3, +3, +3, +3 } };

    // 용병 GUID로 접근. 둘 사이의 관계 점수
    public sbyte[,] MBTIScore = new sbyte[20, 20];

    // 새로운 용병을 등록할 경우, 현재 Team과 관계 점수를 Update한다.
    public void NewHeroScore(Hero A_Hero)
    {
        HeroList = GameManager.Hero.GetHeroList();

        if (HeroList.Count == 0)
            return;

        foreach (Hero B_Hero in HeroList)
        {
            // 기본 관계 점수
            sbyte Score = OriginScore[(int)A_Hero.MBTI, (int)B_Hero.MBTI];

            // 둘 사이의 관계 점수 Update
            MBTIScore[(int)A_Hero.GUID, (int)B_Hero.GUID] = Score;
            MBTIScore[(int)B_Hero.GUID, (int)A_Hero.GUID] = Score;

            RelationScore += Score;
        }
    }

    // 두 용병 사이의 현재 Score를 Get
    public sbyte GetBetweenScore(Hero A_Hero, Hero B_Hero)
    {
        return MBTIScore[(int)A_Hero.MBTI, (int)B_Hero.MBTI];
    }

    // 현재 TeamScore를 Get
    public sbyte GetTeamScore()
    {
        // TeamScore를 Return
        Debug.Log("현재 TeamScore은: " + RelationScore);
        return RelationScore;
    }

    // 두 용병의 사이의 변화된(향상 or 악화)관계 점수를 Get
    public sbyte GetDevelopmentScore(Hero A_Hero, Hero B_Hero)
    {
        sbyte Score = OriginScore[(int)A_Hero.MBTI, (int)B_Hero.MBTI];
        Score -= MBTIScore[(int)A_Hero.GUID, (int)B_Hero.GUID];
        return Score;
    }

    // 두 용병 사이의 관계 점수 변화
    public void SetChangeRelationship(Hero A_Hero, Hero B_Hero, sbyte var)
    {
        MBTIScore[(int)A_Hero.GUID, (int)B_Hero.GUID] += var;
        // MBTIScore[(int)B_Hero.GUID, (int)A_Hero.GUID] += var;
    }

    public bool IsI(Hero _hero)
    {
        switch (_hero.MBTI)
        {
            case GameManager.MbtiType.INFJ:
                return true;
            case GameManager.MbtiType.INFP:
                return true;
            case GameManager.MbtiType.INTP:
                return true;
            case GameManager.MbtiType.INTJ:
                return true;
            case GameManager.MbtiType.ISFJ:
                return true;
            case GameManager.MbtiType.ISFP:
                return true;
            case GameManager.MbtiType.ISTJ:
                return true;
            case GameManager.MbtiType.ISTP:
                return true;
            default:
                break;
        }

        return false;
    }

    public bool IsE(Hero _hero)
    {
        switch (_hero.MBTI)
        {
            case GameManager.MbtiType.ENFJ:
                return true;
            case GameManager.MbtiType.ENFP:
                return true;
            case GameManager.MbtiType.ENTJ:
                return true;
            case GameManager.MbtiType.ENTP:
                return true;
            case GameManager.MbtiType.ESFJ:
                return true;
            case GameManager.MbtiType.ESFP:
                return true;
            case GameManager.MbtiType.ESTJ:
                return true;
            case GameManager.MbtiType.ESTP:
                return true;
            default:
                break;
        }

        return false;
    }

    /// <summary>
    /// 로그를 받아들이는 부분. 어떻게 처리할지는 이후 구현
    /// </summary>
    public void ApplyLog(BattleLogPanel.Log _log)
    {
        ReadEvents(_log);
    }

    #region 로그 읽어들이는 부분 구현
    private void ReadEvents(BattleLogPanel.Log _log)
    {
        if(noticeBallon.activeSelf == false)
        {
            foreach (CustomSignal customSignal in signals)
            {
                // customEvent에 log 정보를 뿌립니다. 그게 끝
                // 그러면 customEvent는 받은 log를 바탕으로 조건을 판단하고 발동해야한다면 어련히 발동시킵니다.
                (string, string) noticeLog = customSignal.Judge(_log);

                if (noticeLog.Item1 != null)
                {
                    noticeText.text = noticeLog.Item1;
                    explainText.text = noticeLog.Item2;

                    noticeBallon.SetActive(true);
                    StartCoroutine(SetDisable(noticeBallon));
                }
            }
        }
    }
    #endregion


    private IEnumerator SetDisable(GameObject _gameObject)
    {
        yield return new WaitForSeconds(1.5f);

        _gameObject.SetActive(false);
    }
}
