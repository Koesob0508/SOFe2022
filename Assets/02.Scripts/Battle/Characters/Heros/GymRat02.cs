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


        GameObject projectile1 = Instantiate(projectileObject, projectileSpawnPoint.transform.position, Quaternion.identity);

        projectile1.GetComponent<Projectile>().Initialize(charData.AttackDamage, 300f);

    }

}
