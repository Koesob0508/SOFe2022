using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Units : MonoBehaviour
{
    bool isInitalized = false;

    
    public Animator animator;
    public bool bHasSkillAnimation;
    float localScaleX;

    float tTimer = 1.0f;
    public float attackTimer = 0.0f;

    public GameObject UnitUIObject;
    GameObject UnitUI;
    Slider hpBar;
    public Vector3 uiOffset;

    BehaviorTreeComponent btComp;

    public Character charData;

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

        if (isInitalized)
        {
            UpdateTimers();
            UpdateUI();
        }
            
    }
    
    public void Initalize(Character charData)
    {
        this.charData = charData;
        this.charData.CurrentHP = this.charData.MaxHP;
        
        animator = GetComponent<Animator>();
        UnitUI = Instantiate(UnitUIObject, transform.position, Quaternion.identity);
        UnitUI.transform.parent = transform;
        UnitUI.transform.position += uiOffset;
        hpBar = UnitUI.transform.GetChild(0).GetComponent<Slider>();

        localScaleX = transform.localScale.x;
        //bb 초기화
        btComp = GetComponent<BehaviorTreeComponent>();
        btComp.TreeObject.bBoard.SetValueAsBool("IsDead", false);
        btComp.TreeObject.bBoard.SetValueAsFloat("AttackRange", charData.AttackRange);
        btComp.TreeObject.bBoard.SetValueAsFloat("Damage", charData.AttackDamage);
        btComp.Initalize();

        GetComponent<Movement>().SetSpeed(charData.MoveSpeed);

        isInitalized = true;

    }

    public void StopBattle()
    {
        isInitalized=false;
        btComp.Terminate();
    }
    public void GetDamage(float damage)
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
    }
    public void PlayAttackAnimation()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            animator.SetTrigger("Attack");
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
    private void UpdateTimers()
    {
        tTimer += Time.deltaTime;
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            attackTimer += Time.deltaTime;
    }

    private void UpdateUI()
    {
        Debug.Log("Update UI");
        hpBar.value = charData.CurrentHP / charData.MaxHP;
    }
}
