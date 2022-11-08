using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame05 : Battle_Heros
{

    public GameObject AttackRange;
    public List<GameObject> attackList = new List<GameObject>();

    protected override void Start()
    {
        isCloseAttackUnit = true;

    }
    public override void Attack()
    {
        PlayAttackAnimation();

        StartCoroutine("CouroutineAttack");
    }

    IEnumerator CouroutineAttack()
    {
        for(int i = 0; i<3; i++)
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

            for (int j = 0; j < attackList.Count; j++)
            {
                attackList[j].GetComponent<Units>().Hit(charData, charData.AttackDamage * 0.5f);
            }

            yield return new WaitForSeconds(0.3f);
        }


    }
}
