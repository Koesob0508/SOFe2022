using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Enemys : Units
{
    struct EnemyState
    {
        public float MaxHP;
        public float CurrentHP;
        public float MaxMana;
        public float CurrentMana;
        public float MoveSpeed;
        public float AttackDamage;
        public float AttackRange;
        public float AttackSpeed;
        public float DefensePoint;
    }
    EnemyState state;


    public override void Initalize(Character charData)
    {
        this.charData = charData;

        state.MaxHP = charData.MaxHP;
        state.CurrentHP = charData.MaxHP;
        state.MaxMana = charData.MaxMana;
        state.CurrentMana = 0;
        state.MoveSpeed = charData.MoveSpeed;
        state.AttackDamage = charData.AttackDamage;
        state.AttackRange = charData.AttackRange;
        state.AttackSpeed = charData.AttackSpeed;
        state.DefensePoint = charData.DefensePoint;

        base.Initalize(charData);
    }

    public override void Attack()
    {
        base.Attack();
        if (isCloseAttackUnit)
            StartCoroutine("CouroutineCloseAttack");

        state.CurrentMana += 10;
        if (state.CurrentMana >= state.MaxMana && bHasSkillAnimation)
        {
            btComp.TreeObject.bBoard.SetValueAsBool("CanSkill", true);
        }
    }

    public override void Hit(float damage)
    {
        // 데미지 처리 = ( 100 / 방어력 + 100 ) * 데미지
        state.CurrentHP -= (100 / (state.DefensePoint + 100)) * damage;

        if (state.CurrentHP <= 0)
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
        state.CurrentMana = 0;
    }

    public override void UpdateUI()
    {
        hpBar.value = state.CurrentHP / state.MaxHP;
        spBar.value = state.CurrentMana / state.MaxMana;
    }

    //근접 공격시 호출. 애니메이션이 끝나는 시점에서 데미지 게산.
    IEnumerator CouroutineCloseAttack()
    {
        yield return new WaitForSeconds(0.01f);

        float t = GetCurrentAnimationTime();

        yield return new WaitForSeconds(t);

        //데미지 계산
        attackTarget.GetComponent<Units>().Hit(state.AttackDamage);
    }
}
