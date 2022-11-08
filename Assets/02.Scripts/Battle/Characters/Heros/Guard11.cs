using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard11 : Battle_Heros
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
        charData.AttackDamage += 10f;
        charData.DefensePoint += 10f;
        charData.MoveSpeed += 15f;
        charData.AttackSpeed += 0.5f;   

        // effect & animation Ãß°¡
    }

    void UnbuffSkill()
    {
        charData.AttackDamage -= 10f;
        charData.DefensePoint -= 10f;
        charData.MoveSpeed -= 15f;
        charData.AttackSpeed -= 0.5f;
    }


}
