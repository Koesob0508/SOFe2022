using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronNight13 : Battle_Heros
{

    protected override void Start()
    {
        isCloseAttackUnit = true;

    }


    public override void ExecuteSkill()
    {
        base.ExecuteSkill();

        BuffSkill();
        StartCoroutine("BuffCoroutine");
    }


    IEnumerator BuffCoroutine()
    {
        yield return new WaitForSeconds(5f);
        UnbuffSkill();
    }

    void BuffSkill()
    {
        charData.DefensePoint += 50f;

        // effect & animation Ãß°¡
    }

    void UnbuffSkill()
    {
        charData.DefensePoint -= 50f;
    }
}

