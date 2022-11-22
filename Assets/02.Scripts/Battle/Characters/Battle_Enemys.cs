using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Enemys : Units
{
    protected struct EnemyState
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
    protected EnemyState state;


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

        animationDamageDelay = this.charData.AttackSpeed;

        base.Initalize(charData);
    }

    public override void Attack()
    {
        if (isSkillPlaying || unitCC.faint)
            return;

        base.Attack();

        attackSpeed = state.AttackSpeed * attackAnimationClip.length;
        btComp.TreeObject.bBoard.SetValueAsFloat("AttackDelay", 1 / attackSpeed);
        animator.SetFloat("AttackSpeed", attackSpeed);


        StartCoroutine(CouroutineAttack());

        if (bHasSkill)
            state.CurrentMana += 10;

        if (state.CurrentMana >= state.MaxMana && bHasSkill)
        {
            btComp.TreeObject.bBoard.SetValueAsBool("CanSkill", true);
        }
    }

    public override void Hit(Character Causer, float damage)
    {
        state.CurrentHP -= (100 / (state.DefensePoint + 100)) * damage;

        if (state.CurrentHP <= 0 && !btComp.TreeObject.bBoard.GetValueAsBool("IsDead"))
        {
            btComp.TreeObject.bBoard.SetValueAsBool("IsDead", true);
            GameManager.Battle.DeadProcess(charData.Type,gameObject,Causer);
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

    IEnumerator CouroutineAttack()
    {
        yield return new WaitForSeconds(animationDamageDelay / 2);

        if (isCloseAttackUnit)
        {
            attackTarget.GetComponent<Units>().Hit(charData, state.AttackDamage);
        }
        else
        {
            if (isFilped)

            {
                Vector3 rot = projectileSpawnPoint.transform.rotation.eulerAngles;

                rot = new Vector3(rot.x, 180f, rot.z);

                projectileSpawnPoint.transform.rotation = Quaternion.Euler(rot);

            }

            else

            {

                Vector3 rot = projectileSpawnPoint.transform.rotation.eulerAngles;

                rot = new Vector3(rot.x, 0f, rot.z);

                projectileSpawnPoint.transform.rotation = Quaternion.Euler(rot);

            }

            Vector2 dir = attackTarget.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            GameObject projectile = Instantiate(projectileObject, projectileSpawnPoint.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
            projectile.GetComponent<Projectile>().Initialize(charData, attackTarget.transform.position, state.AttackDamage, 500f);

        }
    }
    public void Buff(string type, float value, float time)
    {
        switch (type)
        {
            case "AttackDamage":
                state.AttackDamage += value;
                break;
            case "AttackSpeed":
                state.AttackSpeed += value;
                break;
            case "MoveSpeed":
                state.MoveSpeed += value;
                break;
            case "DefensePoint":
                state.DefensePoint += value;
                break;
        }
        StartCoroutine(CoroutineBuff(type, value, time));
    }

    IEnumerator CoroutineBuff(string type, float value, float time)
    {
        yield return new WaitForSeconds(time);
        switch (type)
        {
            case "AttackDamage":
                state.AttackDamage -= value;
                break;
            case "AttackSpeed":
                state.AttackSpeed -= value;
                break;
            case "MoveSpeed":
                state.MoveSpeed -= value;
                break;
            case "DefensePoint":
                state.DefensePoint -= value;
                break;
        }

    }
}
