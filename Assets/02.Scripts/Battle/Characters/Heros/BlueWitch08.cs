using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueWitch08 : Battle_Heros
{
    public GameObject AttackRange;
    float manaCheckTime = 0;
    public List<GameObject> attackList = new List<GameObject>();

    bool isMeditating;

    protected override void Start()
    {
        isCloseAttackUnit = false;

        isMeditating = true;
        charData.DefensePoint += 50f;
    }

    protected override void Update()
    {
        base.Update();

        if (isUpdating)
        {

            if (manaCheckTime > 1)
            {
                animator.SetTrigger("StartBattle");
                manaCheckTime = 0;
                charData.CurrentMana += 10;
                if (charData.CurrentMana >= charData.MaxMana)
                    SkillAttack();
            }
            manaCheckTime += Time.deltaTime;
        }
    }

    public override void Attack()
    {
        CheckForFlippingInAttacking();
    }

    void SkillAttack()
    {
        ExecuteSkill();

        isMeditating = false;
        charData.DefensePoint -= 50f;
        GameManager.Sound.PlayIceEffect();
        StartCoroutine(SkillEffect());

    }

    IEnumerator SkillEffect()
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


        GameObject effect = Instantiate(skillEffect, transform);
        Destroy(effect, 0.5f);

        yield return new WaitForSeconds(0.65f);
        attackList = AttackRange.GetComponent<AttackRange>().attackList;

        for(int i = 0; i<attackList.Count; i++)
        {
            attackList[i].GetComponent<Units>().Hit(charData, charData.AttackDamage * 1.5f);
        }

        charData.CurrentMana = 0;
        isMeditating = true;
        charData.DefensePoint += 50f;

    }
}
