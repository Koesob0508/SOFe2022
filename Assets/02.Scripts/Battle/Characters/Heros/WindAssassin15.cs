using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindAssassin15 : Battle_Heros
{
    Units target;

    protected override void Start()
    {
        isCloseAttackUnit = true;

    }

    public override void ExecuteSkill()
    {
        base.ExecuteSkill();

        StartCoroutine("SkillDamage");
    }

    IEnumerator SkillDamage()
    {
        Vector2 center = new Vector2(transform.position.x, transform.position.y);

        Collider2D[] hitUnits = Physics2D.OverlapCircleAll(center, 50f);

        for (int i = 0; i < hitUnits.Length; i++)
        {
            if (!hitUnits[i].CompareTag("Enemy"))
                continue;

            if (i == 0)
                target = hitUnits[i].GetComponent<Units>();

            else if (target.charData.CurrentHP > hitUnits[i].GetComponent<Units>().charData.CurrentHP)
                target = hitUnits[i].GetComponent<Units>();

        }
        yield return new WaitForSeconds(0.65f);
        target.Hit(charData, charData.AttackDamage * 1.5f);
        Vector2 targetPos = new Vector2(target.transform.position.x + 1f, target.transform.position.y);
        transform.Translate(targetPos);
    }
}

