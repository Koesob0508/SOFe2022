using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp114 : Battle_Enemys
{

    public GameObject AttackRange;
    public List<GameObject> attackList = new List<GameObject>();


    public override void ExecuteSkill()
    {
        base.ExecuteSkill();
        StartCoroutine("SkillDamage");
    }
    IEnumerator SkillDamage()
    {
        if (isFilped)
        {
            Vector3 rot = AttackRange.transform.rotation.eulerAngles;
            rot = new Vector3(rot.x, 180f, rot.z);
            AttackRange.transform.rotation = Quaternion.Euler(rot);
        }
        else
        {
            Vector3 rot = AttackRange.transform.rotation.eulerAngles;
            rot = new Vector3(rot.x, 0f, rot.z);
            AttackRange.transform.rotation = Quaternion.Euler(rot);
        }

        attackList = AttackRange.GetComponent<AttackRange>().attackList;
        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < attackList.Count; i++)
        {
            attackList[i].GetComponent<Units>().Hit(charData, charData.AttackDamage * 1.5f);
            attackList[i].GetComponent<Units>().GetCC("burn", 5.0f, charData);
        }

        charData.CurrentMana = 0;

    }

}
