using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer01 : Battle_Heros
{

    protected override void Start()
    {
        isCloseAttackUnit = false;

    }
    public override void Attack()
    {
        base.Attack();
        GameManager.Sound.PlayArrowEffect();
    }

    public override void ExecuteSkill()
    {
        base.ExecuteSkill();

        StartCoroutine(CouroutineSkill());
    }

    IEnumerator CouroutineSkill()
    {

        yield return new WaitForSeconds(animationDamageDelay / 2);


        Vector3 dir = attackTarget.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        GameObject projectile1 = Instantiate(projectileObject, projectileSpawnPoint.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));


        Vector3 targetPosition = attackTarget.transform.position;

        projectile1.GetComponent<Projectile>().Initialize(charData, targetPosition, charData.AttackDamage, 500f);

        angle += 20f;
        GameObject projectile2 = Instantiate(projectileObject, projectileSpawnPoint.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        targetPosition.y += 1.0f;
        projectile2.GetComponent<Projectile>().Initialize(charData, targetPosition, charData.AttackDamage, 500f);

        angle -= 40f;
        targetPosition.y -= 2.0f;
        GameObject projectile3 = Instantiate(projectileObject, projectileSpawnPoint.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        projectile3.GetComponent<Projectile>().Initialize(charData, targetPosition, charData.AttackDamage, 500f);

    }
}
