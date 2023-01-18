using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Heros : Units
{
    public override void Initalize(Character charData)
    {
        this.charData = charData;
        this.charData.CurrentMana = 0;
        base.Initalize(charData);

        // animationDamageDelay = attackAnimationClip.length;
        animationDamageDelay = this.charData.AttackSpeed;

    }

    public override void Attack()
    {
        // if (charData.CurrentMana >= charData.MaxMana && bHasSkill)
        // {
        //     ExecuteSkill();
        // }
        // else
        // {
        //     base.Attack();
        //     StartCoroutine("CouroutineAttack");
        //     charData.CurrentMana += 100;
        // }

        if (isSkillPlaying || unitCC.faint)
            return;

        base.Attack();


        attackSpeed = attackAnimationClip.length * charData.AttackSpeed;
        btComp.TreeObject.bBoard.SetValueAsFloat("AttackDelay", 1 / attackSpeed);
        animator.SetFloat("AttackSpeed", attackSpeed);

        StartCoroutine(CouroutineAttack());
        
        if(bHasSkill)
            charData.CurrentMana += 10;

        if (charData.CurrentMana >= charData.MaxMana && bHasSkill)
        {
            btComp.TreeObject.bBoard.SetValueAsBool("CanSkill", true);
        }

    }
    
    public override void Hit(Character Causer, float damage)
    {
        charData.CurrentHP -= (100 / (charData.DefensePoint + 100)) * damage;
        // charData.CurrentHP -= 10f;

        if (charData.CurrentHP <= 0 && !btComp.TreeObject.bBoard.GetValueAsBool("IsDead"))
        {
            btComp.TreeObject.bBoard.SetValueAsBool("IsDead", true);
            GameManager.Battle.DeadProcess(charData.Type, gameObject, Causer);
        }
        else
        {
            if (!isSkillPlaying)
                PlayGetHitAniamtion();
        }
    }
    public override void Dead()
    {
        base.Dead();
        var h = charData as Hero;
        h.isDead = true;
    }

    public void ReduceHunger(int amount)
    {
        charData.CurHunger -= amount;
        if(charData.CurHunger < 0)
        {
            charData.CurHunger = 0;
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
    

    IEnumerator CouroutineAttack()
    {

        yield return new WaitForSeconds(animationDamageDelay / 2);

        if (isCloseAttackUnit)
        {
            attackTarget.GetComponent<Units>().Hit(charData, charData.AttackDamage);
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
            GameObject projectile = Instantiate(projectileObject, projectileSpawnPoint.transform.position, Quaternion.AngleAxis(angle, Vector3.forward) );
            projectile.GetComponent<Projectile>().Initialize(charData, attackTarget.transform.position, charData.AttackDamage, 500f);

        }
    }
    public void Buff(string type, float value, float time)
    {
        switch (type)
        {
            case "AttackDamage":
                charData.AttackDamage += value;
                break;
            case "AttackSpeed":
                charData.AttackSpeed += value;
                break;
            case "MoveSpeed":
                charData.MoveSpeed += value;
                break;
            case "DefensePoint":
                charData.DefensePoint += value;
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
                charData.AttackDamage -= value;
                break;
            case "AttackSpeed":
                charData.AttackSpeed -= value;
                break;
            case "MoveSpeed":
                charData.MoveSpeed -= value;
                break;
            case "DefensePoint":
                charData.DefensePoint -= value;
                break;
        }

    }
}