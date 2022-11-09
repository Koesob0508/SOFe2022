using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricWarrior17 : Battle_Heros
{
    public AnimationClip skill2AnimationClip;
    public AnimationClip skill3AnimationClip;


    protected override void Start()
    {
        isCloseAttackUnit = true;

    }

    public override void ExecuteSkill()
    {
        base.ExecuteSkill();

        StartCoroutine("CouroutineElectricAttack");
    }

    IEnumerator CouroutineElectricAttack()
    {
        yield return new WaitForSeconds(0.01f);

        float t = GetCurrentAnimationTime();

        yield return new WaitForSeconds(attackAnimationClip.length);

        attackTarget.GetComponent<Units>().Hit(charData, charData.AttackDamage * 0.8f);

        yield return new WaitForSeconds(skill2AnimationClip.length - 0.1f);

        attackTarget.GetComponent<Units>().Hit(charData, charData.AttackDamage * 0.8f);

        yield return new WaitForSeconds(skill3AnimationClip.length - 0.1f);

        attackTarget.GetComponent<Units>().Hit(charData, charData.AttackDamage * 0.8f);
        attackTarget.GetComponent<Units>().GetCC("faint", 3.0f, charData);

    }
}
