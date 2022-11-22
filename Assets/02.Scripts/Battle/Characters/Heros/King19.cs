using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King19 : Battle_Heros
{

    public GameObject KingSkillEffect;

    protected override void Start()
    {
        isCloseAttackUnit = true;

        KingSkillEffect = Resources.Load<GameObject>("Prefabs/Effects/KingSkillEffect");
    }


    public override void ExecuteSkill()
    {
        base.ExecuteSkill();


        StartCoroutine(SkillCoroutine());
    }

    IEnumerator SkillCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        Vector2 center = new Vector2(transform.position.x, transform.position.y);

        Collider2D[] ourUnits = Physics2D.OverlapCircleAll(center, 150f);

        for (int i = 0; i < ourUnits.Length; i++)
        {
            if (!ourUnits[i].CompareTag("Heros"))
                continue;

            ourUnits[i].GetComponent<Battle_Heros>().Buff("Attack", 0.1f * charData.AttackDamage, 5f);
            ourUnits[i].GetComponent<Battle_Heros>().Buff("AttackSpeed", 0.01f * charData.AttackDamage, 5f);

            GameObject skillEffect = Instantiate(KingSkillEffect, ourUnits[i].transform.position, Quaternion.identity);
            skillEffect.transform.parent = ourUnits[i].transform;

            Destroy(skillEffect, 5.0f);

        }
    }


}
