using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPriestess14 : Battle_Heros
{

    public GameObject AttackRange;
    public List<GameObject> attackList = new List<GameObject>();
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
        yield return new WaitForSeconds(0.65f);
        attackList = AttackRange.GetComponent<AttackRange>().attackList;

        for (int i = 0; i < attackList.Count; i++)
        {
            attackList[i].GetComponent<Units>().Hit(charData.AttackDamage * 0.7f);
        }

        charData.CurrentMana = 0;

    }
}
