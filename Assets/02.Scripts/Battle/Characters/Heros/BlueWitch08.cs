using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueWitch08 : Battle_Heros
{
    public GameObject AttackRange;
    float manaCheckTime = 0;
    public List<GameObject> attackList = new List<GameObject>();

    protected override void Start()
    {
        isCloseAttackUnit = false;
    }

    protected override void Update()
    {
        base.Update();
        if (manaCheckTime > 1)
        {
            manaCheckTime = 0;
            charData.CurrentMana += 30;
            if (charData.CurrentMana >= charData.MaxMana)
                SkillAttack();
        }
        manaCheckTime += Time.deltaTime;
    }

    public override void Attack()
    {
        
    }

    void SkillAttack()
    {
        ExecuteSkill();
        StartCoroutine("SkillEffect");

    }

    IEnumerator SkillEffect()
    {
        GameObject effect = Instantiate(skillEffect, transform);
        Destroy(effect, 0.5f);

        yield return new WaitForSeconds(0.65f);
        attackList = AttackRange.GetComponent<AttackRange>().attackList;

        for(int i = 0; i<attackList.Count; i++)
        {
            attackList[i].GetComponent<Units>().Hit(charData.AttackDamage * 1.5f);
        }

        charData.CurrentMana = 0;

    }
}
