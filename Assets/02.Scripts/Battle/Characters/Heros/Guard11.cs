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

        BuffSkill();
        StartCoroutine("BuffCoroutine");
    }


    IEnumerator BuffCoroutine()
    {
        yield return new WaitForSeconds(5f);
        UnbuffSkill();
    }

    void BuffSkill()
    {
        charData.AttackDamage += 10f;
        charData.DefensePoint += 10f;
        charData.MoveSpeed += 15f;
        charData.AttackSpeed += 1.5f;

        // effect & animation Ãß°¡
        Vector3 effectPosition = transform.position;
        effectPosition.z = transform.position.z - 1;

        GameObject skillEffect = Instantiate(GuardSkillEffect, effectPosition, Quaternion.identity);
        Destroy(skillEffect, 5.0f);
    }

    void UnbuffSkill()
    {
        charData.AttackDamage -= 10f;
        charData.DefensePoint -= 10f;
        charData.MoveSpeed -= 15f;
        charData.AttackSpeed -= 1.5f;
    }


}
