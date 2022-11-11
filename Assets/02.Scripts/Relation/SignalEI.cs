using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalEI : CustomSignal
{
    [SerializeField] private Hero preHero = null;
    [SerializeField] private Hero postHero = null;
    [SerializeField] private Character preCharacter = null;
    [SerializeField] private Character postCharacter = null;

    protected override bool Condition(BattleLogPanel.Log _log)
    {
        // 일단 subject가 Hero여야 실행
        if (_log.Causer is Hero)
        {
            preHero = postHero;
            preCharacter = postCharacter;
            postHero = (Hero)_log.Causer;
            postCharacter = _log.Target;


            if (preHero.Name != postHero.Name && object.ReferenceEquals(preCharacter, postCharacter))
            {
                Debug.Log("서로 다른 히어로, 같은 대상에 대해 행동");
                return true;
            }
        }

        return false;
    }

    protected override void Apply()
    {
        Debug.Log("E와 I 간 Event 발생");

        if (GameManager.Relation.IsI(preHero))
        {
            GameManager.Relation.SetChangeRelationship(preHero, postHero, -10);
            Debug.Log(preHero.Name + " : 아직... 어사인데...");
        }
        else // I가 아니면 E이기 때문에...
        {
            GameManager.Relation.SetChangeRelationship(preHero, postHero, 10);
            Debug.Log(preHero.Name + " : 이것도 천생연분?");
        }

        if (GameManager.Relation.IsI(postHero))
        {
            GameManager.Relation.SetChangeRelationship(postHero, preHero, -10);
            Debug.Log(postHero.Name + " : 아직... 어사인데...");
        }
        else // I가 아니면 E이기 때문에...
        {
            GameManager.Relation.SetChangeRelationship(postHero, preHero, 10);
            Debug.Log(postHero.Name + " : 이것도 천생연분?");
        }
    }
}
