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
        if (state.CurrentMana >= state.MaxMana && bHasSkill)
        {
            btComp.TreeObject.bBoard.SetValueAsBool("CanSkill", true);
        }
    }

    public override void Hit(float damage)
    {
        // ������ ó�� = ( 100 / ���� + 100 ) * ������
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

    //���� ���ݽ� ȣ��. �ִϸ��̼��� ������ �������� ������ �Ի�.
    IEnumerator CouroutineCloseAttack()
    {
        yield return new WaitForSeconds(0.01f);

        float t = GetCurrentAnimationTime();

        yield return new WaitForSeconds(t);

        //������ ���
        attackTarget.GetComponent<Units>().Hit(state.AttackDamage);
    }
}
