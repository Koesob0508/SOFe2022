using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samurai16 : Battle_Heros
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
        GameManager.Sound.PlayWindEffect();
        StartCoroutine("SkillDamage");
    }

    IEnumerator SkillDamage()
    {
        if(isFilped)
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
        yield return new WaitForSeconds(0.65f);
                
        if (spr.flipX)
        {
            if(transform.position.x + (Vector3.left * 3f).x > - Path.PathManager.Width/2)
                transform.Translate(Vector3.left * 3f);
        }
        else
        {
            if (transform.position.x + (Vector3.right * 3f).x < Path.PathManager.Width / 2)
                transform.Translate(Vector3.right * 3f);
        }

        for (int i = 0; i < attackList.Count; i++)
        {
            attackList[i].GetComponent<Units>().Hit(charData, charData.AttackDamage * 1.5f);
        }

        charData.CurrentMana = 0;

    }
}
