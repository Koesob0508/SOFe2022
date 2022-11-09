using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalEI : CustomSignal
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


            //if (prehero.name != posthero.name && object.referenceequals(preenemy, postenemy))
            //{
            //    debug.log("���� �ٸ� �����, ���� �� ����");

            //    testgamemanager.instance.eventlog.text = "e&i event �߻� : ���� �ٸ� �����, ���� �� ����";
            //    return true;
            //}
            //else
            //{
            //    testgamemanager.instance.eventlog.text = "e&i event �߻� ���� x";
            //}


        }
        else
        {
            ///TestGameManager.Instance.eventLog.text = "E&I event �߻� ���� X";
        }

        return false;
    }

    protected override void Apply()
    {
        Debug.Log("E�� I �� Event �߻�");

        //if(TestGameManager.Relation.IsI(preHero))
        //{
        //    TestGameManager.Relation.SetChangeRelationship(preHero, postHero, -10);
        //    TestGameManager.Instance.heroALog.text = "Hero A : ���� ����ε�...(-10)";
        //}
        //else // I�� �ƴϸ� E�̱� ������...
        //{
        //    TestGameManager.Relation.SetChangeRelationship(preHero, postHero, 10);
        //    TestGameManager.Instance.heroALog.text = "Hero A : �̰�... �ο�?(+10)";
        //}

        //if (TestGameManager.Relation.IsI(postHero))
        //{
        //    TestGameManager.Relation.SetChangeRelationship(postHero, preHero, -10);
        //    TestGameManager.Instance.heroBLog.text = "Hero B : ���� ����ε�...(-10)";
        //}
        //else // I�� �ƴϸ� E�̱� ������...
        //{
        //    TestGameManager.Relation.SetChangeRelationship(postHero, preHero, 10);
        //    TestGameManager.Instance.heroBLog.text = "Hero B : �̰�... �ο�?(+10)";
        //}
    }
}
