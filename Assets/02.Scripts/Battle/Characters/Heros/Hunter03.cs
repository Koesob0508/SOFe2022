using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter03 : Battle_Heros
{


    public GameObject hunterSkillEffect;

    protected override void Start()
    {
        isCloseAttackUnit = false;
        hunterSkillEffect = Resources.Load<GameObject>("Prefabs/Effects/hunterSkillEffect");

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

        Vector3 effectPosition = transform.position;
        effectPosition.z = transform.position.z - 1;
        effectPosition.y = transform.position.y - 0.7f;

        GameObject burnEffect = Instantiate(hunterSkillEffect, effectPosition, Quaternion.identity);
        burnEffect.transform.parent = transform;
        Destroy(burnEffect, 2.0f);

    }
}