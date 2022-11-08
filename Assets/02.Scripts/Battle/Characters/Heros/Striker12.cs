using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Striker12 : Battle_Heros
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

        StartCoroutine("CouroutineStrikerAttack");
    }

    IEnumerator CouroutineStrikerAttack()
    {
        yield return new WaitForSeconds(animationDamageDelay/2);

        attackTarget.GetComponent<Units>().Hit(charData, charData.AttackDamage * 1.3f);

        yield return new WaitForSeconds(animationDamageDelay/2);

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
            attackList[j].GetComponent<Units>().Hit(charData, charData.AttackDamage * 0.7f);
        }

        yield return new WaitForSeconds(0.3f);
    }
}
