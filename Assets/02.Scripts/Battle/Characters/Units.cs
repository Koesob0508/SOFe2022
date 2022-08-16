using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Units : MonoBehaviour
{
    bool isUpdating = false;
    bool isSkillPlaying = false;
    
    public Animator animator;
    public bool bHasSkillAnimation;
    float localScaleX;

    float tTimer = 1.0f;
    public float attackTimer = 0.0f;

    public GameObject UnitUIObject;
    GameObject UnitUI;
    Slider hpBar;
    Slider spBar;
    public Vector3 uiOffset;

    BehaviorTreeComponent btComp;

    public Character charData;

    public System.Action skillFinished;


    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

        if (isUpdating)
        {
            UpdateUI();
        }
            
    }
    
    public void Initalize(Character charData)
    {
        this.charData = charData;
        this.charData.CurrentHP = this.charData.MaxHP;
        this.charData.CurrentMana = 0;

        animator = GetComponentInChildren<Animator>();
        UnitUI = Instantiate(UnitUIObject, transform.position, Quaternion.identity);
        UnitUI.transform.parent = transform;
        UnitUI.transform.position += uiOffset;
        hpBar = UnitUI.transform.GetChild(0).GetComponent<Slider>();
        spBar = UnitUI.transform.GetChild(1).GetComponent<Slider>();
        localScaleX = transform.localScale.x;

        //BT,BB 초기화
        btComp = GetComponent<BehaviorTreeComponent>();
        btComp.TreeObject.bBoard.SetValueAsBool("IsDead", false);
        btComp.TreeObject.bBoard.SetValueAsBool("CanSkill", false);
        btComp.TreeObject.bBoard.SetValueAsFloat("AttackRange", charData.AttackRange);
        btComp.TreeObject.bBoard.SetValueAsFloat("Damage", charData.AttackDamage);
        //btComp.Initalize();

        GetComponent<Movement>().SetSpeed(charData.MoveSpeed);
    }
    public void StartBattle()
    {
        btComp.Initalize();
        isUpdating = true;
    }
    public void EndBattle()
    {
        btComp.Terminate();
        isUpdating = false;

    }
    public void Attack()
    {
        charData.CurrentMana += 10;
        if(charData.CurrentMana >= charData.MaxMana)
        {
            btComp.TreeObject.bBoard.SetValueAsBool("CanSkill", true);
        }
    }
    public void Hit(float damage)
    {
        if (damage > charData.DefensePoint)
        {
            charData.CurrentHP -= damage - charData.DefensePoint;
        }
        else // 방어력이 데미지 보다 높으면 1데미지만 
        {
            charData.CurrentHP -= 1;
        }

        if(charData.CurrentHP <= 0)
        {
            btComp.TreeObject.bBoard.SetValueAsBool("IsDead", true);
            GameManager.Battle.DeadProcess(charData);
        }
        else
        {
            if(!isSkillPlaying)
                PlayGetHitAniamtion();
        }
    }
    public virtual void ExecuteSkill()
    {
        isSkillPlaying = true;
        PlaySkillAnimation();
        charData.CurrentMana = 0;
        StartCoroutine("finishSkill", 3);
    }
    IEnumerator finishSkill(float t)
    {
        yield return new WaitForSeconds(t);
        skillFinished();
        isSkillPlaying=false;
        yield break;
    }
    float GetCurrentAnimationTime()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
    public void PlaySkillAnimation()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Skill"))
        {
            animator.SetTrigger("Skill");
        }
    }
    public void PlayAttackAnimation()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            animator.SetTrigger("Attack");
        }
    }
    public void PlayGetHitAniamtion()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("GetHit"))
        {
            animator.SetTrigger("GetHit");

        }
    }
    public void PlayDeadAnimation()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            animator.SetTrigger("Death");
        }
    }
    public void PlayIdleAnimation()
    {
        animator.SetBool("Run", false);
    }

    private void UpdateUI()
    {
        hpBar.value = charData.CurrentHP / charData.MaxHP;
        spBar.value = charData.CurrentMana / charData.MaxMana;
    }
}
