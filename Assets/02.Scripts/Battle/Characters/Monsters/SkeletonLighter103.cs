using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonLighter103 : Battle_Enemys
{
    public GameObject AttackRange;
    public List<GameObject> attackList = new List<GameObject>();

    public override void ExecuteSkill()
    {
        base.ExecuteSkill();

        StartCoroutine(CoroutineSkill());
    }
    IEnumerator CoroutineSkill()
    {
        yield return new WaitForSeconds(0.5f);

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

        for (int j = 0; j < attackList.Count; j++)
        {
            attackList[j].GetComponent<Units>().Hit(charData, state.AttackDamage * 1.5f);
        }

    }
}
