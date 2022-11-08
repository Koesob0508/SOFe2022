using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King19 : Battle_Heros
{

    protected override void Start()
    {
        isCloseAttackUnit = true;

    }


    public override void ExecuteSkill()
    {
        base.ExecuteSkill();
    }

    public void GetBuff()
    {
        Vector2 center = new Vector2(transform.position.x, transform.position.y);

        Collider2D[] ourUnits = Physics2D.OverlapCircleAll(center, 150f);

        for (int i = 0; i < ourUnits.Length; i++)
        {
            if (!ourUnits[i].CompareTag("Heros"))
                continue;

            ourUnits[i].GetComponent<Battle_Heros>().Buff("Attack", 0.1f * charData.AttackDamage);
            ourUnits[i].GetComponent<Battle_Heros>().Buff("AttackSpeed", 0.01f * charData.AttackDamage);

        }
    }


}
