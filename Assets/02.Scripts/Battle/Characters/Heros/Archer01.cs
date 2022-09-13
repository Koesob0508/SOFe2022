using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer01 : Battle_Heros
{

    protected override void Start()
    {
        isCloseAttackUnit = false;

    }


    public override void ExecuteSkill()
    {
        base.ExecuteSkill();


        Vector2 dir = attackTarget.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        GameObject projectile1 = Instantiate(projectileObject, projectileSpawnPoint.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        GameObject projectile2 = Instantiate(projectileObject, projectileSpawnPoint.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        GameObject projectile3 = Instantiate(projectileObject, projectileSpawnPoint.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));


        Vector3 targetPosition = attackTarget.transform.position;

        projectile1.GetComponent<Projectile>().Initialize(targetPosition, charData.AttackDamage);

        targetPosition.y += 10.0f;
        projectile1.GetComponent<Projectile>().Initialize(targetPosition, charData.AttackDamage);

        targetPosition.y -= 20.0f;
        projectile1.GetComponent<Projectile>().Initialize(targetPosition, charData.AttackDamage);

    }

}
