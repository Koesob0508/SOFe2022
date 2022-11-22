using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard11 : Battle_Heros
{
    public GameObject GuardSkillEffect;
    
    protected override void Start()
    {
        isCloseAttackUnit = true;

        GuardSkillEffect = Resources.Load<GameObject>("Prefabs/Effects/GuardSkillEffect");
    }


    public override void ExecuteSkill()
    {
        base.ExecuteSkill();

        Buff("AttackDamage", 10f, 3f);
        Buff("DefensePoint", 10f, 3f);
        Buff("MoveSpeed", 15f, 3f);
        Buff("AttackSpeed", 1.5f, 3f);

        Vector3 effectPosition = transform.position;
        effectPosition.z = transform.position.z - 1;

        GameObject skillEffect = Instantiate(GuardSkillEffect, effectPosition, Quaternion.identity);
        skillEffect.transform.parent = transform;

        Destroy(skillEffect, 3.0f);
        
    }
}
