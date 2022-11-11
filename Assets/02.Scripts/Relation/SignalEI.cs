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
        // �ϴ� subject�� Hero���� ����
        if (_log.Causer is Hero)
        {
            preHero = postHero;
            preCharacter = postCharacter;
            postHero = (Hero)_log.Causer;
            postCharacter = _log.Target;


            if (preHero.Name != postHero.Name && object.ReferenceEquals(preCharacter, postCharacter))
            {
                Debug.Log("���� �ٸ� �����, ���� ��� ���� �ൿ");
                return true;
            }
        }

        return false;
    }

    protected override void Apply()
    {
        Debug.Log("E�� I �� Event �߻�");

        if (GameManager.Relation.IsI(preHero))
        {
            GameManager.Relation.SetChangeRelationship(preHero, postHero, -10);
            Debug.Log(preHero.Name + " : ����... ����ε�...");
        }
        else // I�� �ƴϸ� E�̱� ������...
        {
            GameManager.Relation.SetChangeRelationship(preHero, postHero, 10);
            Debug.Log(preHero.Name + " : �̰͵� õ������?");
        }

        if (GameManager.Relation.IsI(postHero))
        {
            GameManager.Relation.SetChangeRelationship(postHero, preHero, -10);
            Debug.Log(postHero.Name + " : ����... ����ε�...");
        }
        else // I�� �ƴϸ� E�̱� ������...
        {
            GameManager.Relation.SetChangeRelationship(postHero, preHero, 10);
            Debug.Log(postHero.Name + " : �̰͵� õ������?");
        }
    }
}
