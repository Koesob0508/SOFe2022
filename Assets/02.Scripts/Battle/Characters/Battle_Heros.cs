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
    }

    public override void Attack()
    {
        //if (charData.CurrentMana >= charData.MaxMana && bHasSkill)
        //{
        //    ExecuteSkill();
        //}
        //else
        //{
        //    base.Attack();
        //    StartCoroutine("CouroutineAttack");
        //    charData.CurrentMana += 100;
        //}

        base.Attack();
        StartCoroutine("CouroutineAttack");
        charData.CurrentMana += 100;

        if (charData.CurrentMana >= charData.MaxMana && bHasSkill)
        {
            btComp.TreeObject.bBoard.SetValueAsBool("CanSkill", true);
        }
    }
    
    public override void Hit(float damage)
    {
        // ������ ó�� = ( 100 / ���� + 100 ) * ������
        charData.CurrentHP -= (100 / (charData.DefensePoint + 100)) * damage;
        // charData.CurrentHP -= 10f;

        if (charData.CurrentHP <= 0 && !btComp.TreeObject.bBoard.GetValueAsBool("IsDead"))
        {
            btComp.TreeObject.bBoard.SetValueAsBool("IsDead", true);
            GameManager.Battle.DeadProcess(charData.Type, gameObject);
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
        h.IsActive = false;
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
        yield return new WaitForSeconds(0.01f);

        float t = GetCurrentAnimationTime();

        yield return new WaitForSeconds(t);

        if (isCloseAttackUnit)
        {
            attackTarget.GetComponent<Units>().Hit(charData.AttackDamage);
        }
        else
        {
            Vector2 dir = attackTarget.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            GameObject projectile = Instantiate(projectileObject, projectileSpawnPoint.transform.position, Quaternion.AngleAxis(angle, Vector3.forward) );
            projectile.GetComponent<Projectile>().Initialize(attackTarget.transform.position, charData.AttackDamage, 500f);

        }
    }
}