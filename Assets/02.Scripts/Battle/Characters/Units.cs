using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Units : MonoBehaviour
{
    bool isInitalized = false;

    public float unitMaxHP = 100.0f; // Max Health Point
    public float unitCurHP = 100.0f; // Current Health Point
    public float unitAD = 10.0f; // Attack Damage
    public float unitAS = 0.5f; // Attack Speed
    public float unitDP = 1.0f; // Defense Point
    public float unitMM = 1.0f; // Max Mana
    public float unitCM = 1.0f; // Current Mana
    public float unitMS = 1.0f; // Movement Speed
    public float unitAR = 1.0f; // Attack Range

    
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
        unitMaxHP = charData.MaxHP;
        unitCurHP = charData.MaxHP;
        unitCM = charData.CurrentMana;
        unitMM = charData.MaxMana;
        unitAD = charData.AttackDamage;
        unitAS = charData.AttackSpeed;
        unitAR = charData.AttackRange;
        unitDP = charData.DefensePoint;
        unitMS = charData.MoveSpeed;

        animator = GetComponent<Animator>();
        UnitUI = Instantiate(UnitUIObject, transform.position, Quaternion.identity);
        UnitUI.transform.parent = transform;
        UnitUI.transform.position += uiOffset;
        hpBar = UnitUI.transform.GetChild(0).GetComponent<Slider>();

        localScaleX = transform.localScale.x;
        //bb 초기화
        btComp = GetComponent<BehaviorTreeComponent>();
        btComp.TreeObject.bBoard.SetValueAsBool("IsDead", false);
        btComp.TreeObject.bBoard.SetValueAsFloat("AttackRange", unitAR);
        btComp.TreeObject.bBoard.SetValueAsFloat("Damage", unitAD);
        btComp.Initalize();

        isInitalized = true;

    }


    public void GetDamage(float damage)
    {
        if (damage > unitDP)
            unitCurHP -= damage - unitDP;
        else // 방어력이 데미지 보다 높으면 1데미지만 
            unitCurHP -= 1;

        
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
        hpBar.value = unitCurHP / unitMaxHP;
    }
}
