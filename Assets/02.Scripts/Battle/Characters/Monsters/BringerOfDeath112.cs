using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringerOfDeath112 : Battle_Enemys
{
    GameObject axemanSkillEffect;

    protected override void Start()
    {
        base.Start();

        isCloseAttackUnit = true;
        isSpriteReverse = true;

        axemanSkillEffect = Resources.Load<GameObject>("Prefabs/Effects/BringerOfDeathEffect");

    }

    public override void ExecuteSkill()
    {
        base.ExecuteSkill();

        StartCoroutine(CoroutineSkill());
    }
    IEnumerator CoroutineSkill()
    {
        Vector2 center = new Vector2(transform.position.x, transform.position.y);

        Collider2D[] heroUnits = Physics2D.OverlapCircleAll(center, 150f, LayerMask.GetMask("Units"));

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < heroUnits.Length; i++)
        {
            if (heroUnits[i].CompareTag("Heros"))
            {
                heroUnits[i].GetComponent<Battle_Heros>().Buff("DefensePoint", -20f, 5f);

                Vector3 effectPosition = heroUnits[i].transform.position;
                effectPosition.z = heroUnits[i].transform.position.z - 1;
                effectPosition.y = heroUnits[i].transform.position.y + 0.7f;

                GameObject skillEffect = Instantiate(axemanSkillEffect, effectPosition, Quaternion.identity);
                skillEffect.transform.parent = heroUnits[i].transform;
                Destroy(skillEffect, 5.0f);

            }

        }
    }
}
