using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeMan07 : Battle_Heros
{


    public GameObject axemanSkillEffect;
    float skillValue = 0;

    protected override void Start()
    {
        isCloseAttackUnit = true;
        axemanSkillEffect = Resources.Load<GameObject>("Prefabs/Effects/AxeManSkillEffect");

    }


    public override void ExecuteSkill()
    {
        base.ExecuteSkill();

        skillValue = charData.AttackSpeed * 30 / 100;
        charData.AttackSpeed += skillValue;

        StartCoroutine(SkillBuff());
    }


    IEnumerator SkillBuff()
    {
        Vector3 effectPosition = transform.position;
        effectPosition.z = transform.position.z - 1;
        effectPosition.y = transform.position.y + 0.7f;

        GameObject skillEffect = Instantiate(axemanSkillEffect, effectPosition, Quaternion.identity);
        Destroy(skillEffect, 5.0f);

        yield return new WaitForSeconds(5f);
        charData.AttackSpeed -= skillValue;

    }
}
    