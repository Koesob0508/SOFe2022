using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneArcher06 : Battle_Heros
{

    protected override void Start()
    {
        isCloseAttackUnit = false;

    }

    public override void ExecuteSkill()
    {
        base.ExecuteSkill();


        StartCoroutine("SkillEffect");

    }

    IEnumerator SkillEffect()
    {
        yield return new WaitForSeconds(0.05f);

        if (spr.flipX)
        {
            spr.flipX = false;
            isFilped = false;
        }
        else
        {
            spr.flipX = true;
            isFilped = true;
        }

        for (int i =0;i<10; i++)
        {
            if (spr.flipX)
                transform.Translate(Vector3.left * 0.1f);
            else
                transform.Translate(Vector3.right * 0.1f);
            yield return new WaitForSeconds(0.03f);
        }
        if (spr.flipX)
        {
            spr.flipX = false;
            isFilped = false;
        }
        else
        {
            spr.flipX = true;
            isFilped = true;
        }
    }
}
