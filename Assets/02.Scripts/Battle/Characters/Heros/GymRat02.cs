using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GymRat02 : Battle_Heros
{

    protected override void Start()
    {
        isCloseAttackUnit = true;

    }


    public override void ExecuteSkill()
    {
        base.ExecuteSkill();


        StartCoroutine("SkillEffect");
    }

    IEnumerator SkillEffect()
    {
        yield return new WaitForSeconds(0.05f);

        float t = GetCurrentAnimationTime();

        yield return new WaitForSeconds(t-0.05f);

        GameObject projectile1 = Instantiate(projectileObject, projectileSpawnPoint.transform.position, Quaternion.identity);

        projectile1.GetComponent<Projectile>().Initialize(charData.AttackDamage, 300f);

    }
}
