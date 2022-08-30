using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Heros : Units
{
    public override void Initalize(Character charData)
    {
        this.charData = charData;
        this.charData.CurrentHP = this.charData.MaxHP;
        this.charData.CurrentMana = 0;

        base.Initalize(charData);
    }

    public override void Attack()
    {
        charData.CurrentMana += 10;
        if (charData.CurrentMana >= charData.MaxMana && bHasSkillAnimation)
        {
            btComp.TreeObject.bBoard.SetValueAsBool("CanSkill", true);
        }
    }

    public override void Hit(float damage)
    {
        // 데미지 처리 = ( 100 / 방어력 + 100 ) * 데미지
        charData.CurrentHP -= (100 / (charData.DefensePoint + 100)) * damage;
        // charData.CurrentHP -= 10f;

        if (charData.CurrentHP <= 0)
        {
            btComp.TreeObject.bBoard.SetValueAsBool("IsDead", true);
            GameManager.Battle.DeadProcess(charData.Type);
        }
        else
        {
            if (!isSkillPlaying)
                PlayGetHitAniamtion();
        }
    }

    public override void ExecuteSkill()
    {
        base.ExecuteSkill();
        charData.CurrentMana = 0;
    }

    public override void UpdateUI()
    {
        hpBar.value = charData.CurrentHP / charData.MaxHP;
        spBar.value = charData.CurrentMana / charData.MaxMana;
    }

}