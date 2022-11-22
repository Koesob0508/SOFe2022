using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizard116 : Battle_Enemys
{
    public GameObject AttackRange;
    public List<GameObject> attackList = new List<GameObject>();

    public override void Attack()
    {
        if (unitCC.faint)
            return;

        attackSpeed = state.AttackSpeed / attackAnimationClip.length;
        btComp.TreeObject.bBoard.SetValueAsFloat("AttackDelay", 1 / attackSpeed);
        animator.SetFloat("AttackSpeed", 1);

        CheckForFlippingInAttacking();
        PlayAttackAnimation();

        StartCoroutine("CouroutineAttack");
    }

    IEnumerator CouroutineAttack()
    {
        yield return new WaitForSeconds(animationDamageDelay / 2);

        attackList = AttackRange.GetComponent<AttackRange>().attackList;

        for (int j = 0; j < attackList.Count; j++)
        {
            attackList[j].GetComponent<Units>().Hit(charData, state.AttackDamage);

            attackList[j].GetComponent<Battle_Heros>().Buff("DefensePoint", -3f, 20f);
        }
    }
}
