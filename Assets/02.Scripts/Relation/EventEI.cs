using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEI : CustomEvent
{
    [SerializeField] private Hero preHero = null;
    [SerializeField] private string preValue;
    [SerializeField] private Hero postHero = null;
    [SerializeField] private string postValue;
    [SerializeField] private Enemy preEnemy = null;
    [SerializeField] private Enemy postEnemy = null;

    protected override bool Condition(LogInfo _logInfo)
    {
        // �ϴ� subject�� Hero���� ����
        if (_logInfo.Subjective is Hero && _logInfo.Objective is Enemy)
        {
            preHero = postHero;
            preEnemy = postEnemy;
            postHero = (Hero)_logInfo.Subjective;
            postEnemy = (Enemy)_logInfo.Objective;

            if (preHero.Name != postHero.Name && object.ReferenceEquals(preEnemy, postEnemy))
            {
                Debug.Log("���� �ٸ� �����, ���� �� ����");
                return true;
            }
        }

        return false;
    }

    protected override void Apply()
    {
        Debug.Log("E�� I �� Event �߻�");
        switch(preHero.MBTI)
        {
            case GameManager.MbtiType.INFP:
                TestGameManager.Relation.SetChangeRelationship(preHero, postHero, -10);
                break;
            case GameManager.MbtiType.ENFP:
                TestGameManager.Relation.SetChangeRelationship(preHero, postHero, 10);
                break;
            case GameManager.MbtiType.INFJ:
                TestGameManager.Relation.SetChangeRelationship(preHero, postHero, -10);
                break;
            case GameManager.MbtiType.ENFJ:
                TestGameManager.Relation.SetChangeRelationship(preHero, postHero, 10);
                break;
            case GameManager.MbtiType.INTJ:
                TestGameManager.Relation.SetChangeRelationship(preHero, postHero, -10);
                break;
            case GameManager.MbtiType.ENTJ:
                TestGameManager.Relation.SetChangeRelationship(preHero, postHero, 10);
                break;
            case GameManager.MbtiType.INTP:
                TestGameManager.Relation.SetChangeRelationship(preHero, postHero, -10);
                break;
            case GameManager.MbtiType.ENTP:
                TestGameManager.Relation.SetChangeRelationship(preHero, postHero, 10);
                break;
            case GameManager.MbtiType.ISFP:
                TestGameManager.Relation.SetChangeRelationship(preHero, postHero, -10);
                break;
            case GameManager.MbtiType.ESFP:
                TestGameManager.Relation.SetChangeRelationship(preHero, postHero, 10);
                break;
            case GameManager.MbtiType.ISTP:
                TestGameManager.Relation.SetChangeRelationship(preHero, postHero, -10);
                break;
            case GameManager.MbtiType.ESTP:
                TestGameManager.Relation.SetChangeRelationship(preHero, postHero, 10);
                break;
            case GameManager.MbtiType.ISFJ:
                TestGameManager.Relation.SetChangeRelationship(preHero, postHero, -10);
                break;
            case GameManager.MbtiType.ESFJ:
                TestGameManager.Relation.SetChangeRelationship(preHero, postHero, 10);
                break;
            case GameManager.MbtiType.ISTJ:
                TestGameManager.Relation.SetChangeRelationship(preHero, postHero, -10);
                break;
            case GameManager.MbtiType.ESTJ:
                TestGameManager.Relation.SetChangeRelationship(preHero, postHero, 10);
                break;
        }

        switch (postHero.MBTI)
        {
            case GameManager.MbtiType.INFP:
                TestGameManager.Relation.SetChangeRelationship(postHero, preHero, -10);
                break;
            case GameManager.MbtiType.ENFP:
                TestGameManager.Relation.SetChangeRelationship(postHero, preHero, 10);
                break;
            case GameManager.MbtiType.INFJ:
                TestGameManager.Relation.SetChangeRelationship(postHero, preHero, -10);
                break;
            case GameManager.MbtiType.ENFJ:
                TestGameManager.Relation.SetChangeRelationship(postHero, preHero, 10);
                break;
            case GameManager.MbtiType.INTJ:
                TestGameManager.Relation.SetChangeRelationship(postHero, preHero, -10);
                break;
            case GameManager.MbtiType.ENTJ:
                TestGameManager.Relation.SetChangeRelationship(postHero, preHero, 10);
                break;
            case GameManager.MbtiType.INTP:
                TestGameManager.Relation.SetChangeRelationship(postHero, preHero, -10);
                break;
            case GameManager.MbtiType.ENTP:
                TestGameManager.Relation.SetChangeRelationship(postHero, preHero, 10);
                break;
            case GameManager.MbtiType.ISFP:
                TestGameManager.Relation.SetChangeRelationship(postHero, preHero, -10);
                break;
            case GameManager.MbtiType.ESFP:
                TestGameManager.Relation.SetChangeRelationship(postHero, preHero, 10);
                break;
            case GameManager.MbtiType.ISTP:
                TestGameManager.Relation.SetChangeRelationship(postHero, preHero, -10);
                break;
            case GameManager.MbtiType.ESTP:
                TestGameManager.Relation.SetChangeRelationship(postHero, preHero, 10);
                break;
            case GameManager.MbtiType.ISFJ:
                TestGameManager.Relation.SetChangeRelationship(postHero, preHero, -10);
                break;
            case GameManager.MbtiType.ESFJ:
                TestGameManager.Relation.SetChangeRelationship(postHero, preHero, 10);
                break;
            case GameManager.MbtiType.ISTJ:
                TestGameManager.Relation.SetChangeRelationship(postHero, preHero, -10);
                break;
            case GameManager.MbtiType.ESTJ:
                TestGameManager.Relation.SetChangeRelationship(postHero, preHero, 10);
                break;
        }
    }
}
