using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireNight09 : Battle_Heros
{
    protected override void Start()
    {
        isCloseAttackUnit = true;
    }

    public override void ExecuteSkill()
    {
        base.ExecuteSkill();
        GameManager.Sound.PlayFireEffect();
        Vector2 center = new Vector2(transform.position.x, transform.position.y);

        Collider2D[] hitUnits = Physics2D.OverlapCircleAll(center, 10.5f);

        for (int i = 0; i < hitUnits.Length; i++)
        {
            if (!hitUnits[i].CompareTag("Enemy"))
                continue;

            hitUnits[i].GetComponent<Units>().Hit(charData, charData.AttackDamage * 1.5f);
            hitUnits[i].GetComponent<Units>().GetCC("burn", 5.0f, charData);
        }
    }


}
