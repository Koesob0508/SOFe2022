using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter03 : Battle_Heros
{

    protected override void Start()
    {
        isCloseAttackUnit = false;

    }


    public override void ExecuteSkill()
    {
        base.ExecuteSkill();

        Debug.Log("Use Hunter Skill");

        isCloseAttackUnit = true;
        animator.SetBool("Skill", true);
        bHasSkill = false;

        charData.AttackSpeed *= 2;
        charData.AttackDamage *= 1.2f;
        charData.AttackRange = 1;

    }
}