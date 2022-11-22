using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldGolem118 : Battle_Enemys
{

    protected override void Start()
    {
        base.Start();

        isSpriteReverse = true;
    }

    public override void ExecuteSkill()
    {
        base.ExecuteSkill();


        if (isFilped)

        {
            Vector3 rot = projectileSpawnPoint.transform.rotation.eulerAngles;

            rot = new Vector3(rot.x, 180f, rot.z);

            projectileSpawnPoint.transform.rotation = Quaternion.Euler(rot);

        }

        else

        {

            Vector3 rot = projectileSpawnPoint.transform.rotation.eulerAngles;

            rot = new Vector3(rot.x, 0f, rot.z);

            projectileSpawnPoint.transform.rotation = Quaternion.Euler(rot);

        }

        StartCoroutine(SkillCoroutine());
    }

    IEnumerator SkillCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        Vector2 dir = attackTarget.transform.position - transform.position;

        GameObject projectile = Instantiate(projectileObject, projectileSpawnPoint.transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile_118>().Initialize(charData, dir, state.AttackDamage);
    }



}
