using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeMan07 : Battle_Heros
{

    float skillValue = 0;

    protected override void Start()
    {
        isCloseAttackUnit = true;

    }


    public override void ExecuteSkill()
    {
        base.ExecuteSkill();

        skillValue = charData.AttackSpeed * 30 / 100;
        charData.AttackSpeed += skillValue;

    }


    IEnumerator SkillBuff()
    {
        yield return new WaitForSeconds(5f);
        charData.AttackSpeed -= skillValue;

    }
}
    