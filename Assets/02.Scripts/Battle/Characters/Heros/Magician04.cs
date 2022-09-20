using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magician04 : Battle_Heros
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

        yield return new WaitForSeconds(t - 0.1f);

        Vector2 center = new Vector2(transform.position.x, transform.position.y);

        Collider2D[] hitUnits = Physics2D.OverlapCircleAll(center, 10.5f);

        for (int i = 0; i < hitUnits.Length; i++)
        {
            if (!hitUnits[i].CompareTag("Enemy"))
                continue;

            hitUnits[i].GetComponent<Units>().Hit(charData.AttackDamage * 1.5f);
        }

        GameObject effect = Instantiate(skillEffect, transform);
        Destroy(effect, 0.5f);

    }
}
