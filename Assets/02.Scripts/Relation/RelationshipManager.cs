using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RelationshipManager : MonoBehaviour
{
    public List<Hero> HeroList = new List<Hero>();
    public sbyte RelationScore;
    [SerializeField] private List<CustomEvent> events;

    public void Init()
    {
        Debug.Log("Relationship Manager Init");
        DontDestroyOnLoad(this);
    }

    // ���� ���ǵ� �⺻ ���� ����
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

    // �뺴 GUID�� ����. �� ������ ���� ����
    public sbyte[,] MBTIScore = new sbyte[20, 20];

    // ���ο� �뺴�� ����� ���, ���� Team�� ���� ������ Update�Ѵ�.
    public void NewHeroScore(Hero A_Hero)
    {
        HeroList = GameManager.Hero.GetHeroList();

        if (HeroList.Count == 0)
            return;

        foreach (Hero B_Hero in HeroList)
        {
            // �⺻ ���� ����
            sbyte Score = OriginScore[(int)A_Hero.MBTI, (int)B_Hero.MBTI];

            // �� ������ ���� ���� Update
            MBTIScore[(int)A_Hero.GUID, (int)B_Hero.GUID] = Score;
            MBTIScore[(int)B_Hero.GUID, (int)A_Hero.GUID] = Score;

            RelationScore += Score;
        }
    }

    // �� �뺴 ������ ���� Score�� Get
    public sbyte GetBetweenScore(Hero A_Hero, Hero B_Hero)
    {
        return MBTIScore[(int)A_Hero.MBTI, (int)B_Hero.MBTI];
    }

    // ���� TeamScore�� Get
    public sbyte GetTeamScore()
    {
        // TeamScore�� Return
        Debug.Log("���� TeamScore��: " + RelationScore);
        return RelationScore;
    }

    // �� �뺴�� ������ ��ȭ��(��� or ��ȭ)���� ������ Get
    public sbyte GetDevelopmentScore(Hero A_Hero, Hero B_Hero)
    {
        sbyte Score = OriginScore[(int)A_Hero.MBTI, (int)B_Hero.MBTI];
        Score -= MBTIScore[(int)A_Hero.GUID, (int)B_Hero.GUID];
        return Score;
    }

    // �� �뺴 ������ ���� ���� ��ȭ
    public void SetChangeRelationship(Hero A_Hero, Hero B_Hero, sbyte var)
    {
        MBTIScore[(int)A_Hero.GUID, (int)B_Hero.GUID] += var;
        MBTIScore[(int)B_Hero.GUID, (int)A_Hero.GUID] += var;
    }

    /// <summary>
    /// �α׸� �޾Ƶ��̴� �κ�. ��� ó�������� ���� ����
    /// </summary>
    public void ApplyLog(LogInfo _logInfo)
    {
        ReadEvents(_logInfo);
    }

    #region �α� �о���̴� �κ� ����
    private void ReadEvents(LogInfo _logInfo)
    {
        foreach(CustomEvent customEvent in events)
        {
            // customEvent�� log ������ �Ѹ��ϴ�. �װ� ��
            // �׷��� customEvent�� ���� log�� �������� ������ �Ǵ��ϰ� �ߵ��ؾ��Ѵٸ� ����� �ߵ���ŵ�ϴ�.
            customEvent.Judge(_logInfo);
        }
    }
    #endregion

}
