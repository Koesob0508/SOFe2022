using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSeeker104 : Battle_Enemys
{
    bool useResurrection = false;
    bool isResurrecting = false;

    public override void Hit(Character Causer, float damage)
    {
        if (isResurrecting)
            return;
        state.CurrentHP -= (100 / (state.DefensePoint + 100)) * damage;

        if (state.CurrentHP <= 0 && !btComp.TreeObject.bBoard.GetValueAsBool("IsDead"))
        {
            if (!useResurrection)
            {
                PlayDeadAnimation();
                useResurrection = true;
                isResurrecting = true;
                StartCoroutine(CouroutineResurrection());
                Debug.Log("Resurrection");
            }
            else
            {
                btComp.TreeObject.bBoard.SetValueAsBool("IsDead", true);
                GameManager.Battle.DeadProcess(charData.Type, gameObject, Causer);
            }
        }
        else
        {
            if (!isSkillPlaying)
                PlayGetHitAniamtion();
        }
    }

    IEnumerator CouroutineResurrection()
    {
        yield return new WaitForSeconds(3f);
        animator.SetBool("canResurrection", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("canResurrection", false);
        state.CurrentHP = state.MaxHP;
        isResurrecting = false;
    }
}
