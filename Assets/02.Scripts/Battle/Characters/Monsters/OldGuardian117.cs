using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldGuardian117 : Battle_Enemys
{
    public GameObject SkillEffect;

    protected override void Start()
    {
        isCloseAttackUnit = true;

        SkillEffect = Resources.Load<GameObject>("Prefabs/Effects/GuardSkillEffect");
    }

    public override void ExecuteSkill()
    {
        base.ExecuteSkill();

        Buff("AttackDamage", state.AttackDamage * 0.5f, 3f);
        Buff("DefensePoint", state.DefensePoint * 0.5f, 3f);
        Buff("MoveSpeed", state.MoveSpeed * 0.5f, 3f);
        Buff("AttackSpeed", state.AttackSpeed * 0.5f, 3f);

        Vector3 effectPosition = transform.position;
        effectPosition.z = transform.position.z - 1;

        GameObject skillEffect = Instantiate(SkillEffect, effectPosition, Quaternion.identity);
        skillEffect.transform.parent = transform;

        Destroy(skillEffect, 3.0f);

    }

}
