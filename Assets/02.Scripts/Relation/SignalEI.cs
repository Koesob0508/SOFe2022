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
        // 일단 subject가 Hero여야 실행
        if (_logInfo.Subjective is Hero && _logInfo.Objective is Enemy)
        {
            preHero = postHero;
            preEnemy = postEnemy;
            postHero = (Hero)_logInfo.Subjective;
            postEnemy = (Enemy)_logInfo.Objective;


            //if (prehero.name != posthero.name && object.referenceequals(preenemy, postenemy))
            //{
            //    debug.log("서로 다른 히어로, 같은 적 공격");

            //    testgamemanager.instance.eventlog.text = "e&i event 발생 : 서로 다른 히어로, 같은 적 공격";
            //    return true;
            //}
            //else
            //{
            //    testgamemanager.instance.eventlog.text = "e&i event 발생 조건 x";
            //}


        }
        else
        {
            ///TestGameManager.Instance.eventLog.text = "E&I event 발생 조건 X";
        }

        return false;
    }

    protected override void Apply()
    {
        Debug.Log("E와 I 간 Event 발생");

        //if(TestGameManager.Relation.IsI(preHero))
        //{
        //    TestGameManager.Relation.SetChangeRelationship(preHero, postHero, -10);
        //    TestGameManager.Instance.heroALog.text = "Hero A : 아직 어사인데...(-10)";
        //}
        //else // I가 아니면 E이기 때문에...
        //{
        //    TestGameManager.Relation.SetChangeRelationship(preHero, postHero, 10);
        //    TestGameManager.Instance.heroALog.text = "Hero A : 이건... 인연?(+10)";
        //}

        //if (TestGameManager.Relation.IsI(postHero))
        //{
        //    TestGameManager.Relation.SetChangeRelationship(postHero, preHero, -10);
        //    TestGameManager.Instance.heroBLog.text = "Hero B : 아직 어사인데...(-10)";
        //}
        //else // I가 아니면 E이기 때문에...
        //{
        //    TestGameManager.Relation.SetChangeRelationship(postHero, preHero, 10);
        //    TestGameManager.Instance.heroBLog.text = "Hero B : 이건... 인연?(+10)";
        //}
    }
}
