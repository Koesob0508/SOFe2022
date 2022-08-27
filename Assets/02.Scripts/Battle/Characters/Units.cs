using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Units : MonoBehaviour
{
    bool isUpdating = false;
    protected bool isSkillPlaying = false;
    
    public Animator animator;
    public bool bHasSkillAnimation;
    float localScaleX;

    float tTimer = 1.0f;
    public float attackTimer = 0.0f;

    public GameObject UnitUIObject;
    GameObject UnitUI;
    protected Slider hpBar;
    protected Slider spBar;
    public Vector3 uiOffset;

    protected BehaviorTreeComponent btComp;

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
    
    public virtual void Initalize(Character charData)
    {
        animator = GetComponentInChildren<Animator>();
        UnitUI = Instantiate(UnitUIObject, transform.position, Quaternion.identity);
        UnitUI.transform.parent = transform;
        UnitUI.transform.position += uiOffset;
        hpBar = UnitUI.transform.GetChild(0).GetComponent<Slider>();
        spBar = UnitUI.transform.GetChild(1).GetComponent<Slider>();
        localScaleX = transform.localScale.x;

        //BT,BB 초기화
        btComp = GetComponent<BehaviorTreeComponent>();
        btComp.Initalize();
        btComp.TreeObject.bBoard.SetValueAsBool("IsDead", false);
        btComp.TreeObject.bBoard.SetValueAsBool("CanSkill", false);
        btComp.TreeObject.bBoard.SetValueAsFloat("AttackRange", charData.AttackRange);
        btComp.TreeObject.bBoard.SetValueAsFloat("Damage", charData.AttackDamage);
        //btComp.Initalize();

        GetComponent<Movement>().SetSpeed(charData.MoveSpeed);

    }
    public void StartBattle()
    {
        btComp.StartTree();
        isUpdating = true;
    }
    public void EndBattle()
    {
        btComp.StopTree();
        isUpdating = false;

    }
    public virtual void Attack()
    {
    }
    public virtual void Hit(float damage)
    {
    }
    public virtual void ExecuteSkill()
    {
        isSkillPlaying = true;
        PlaySkillAnimation();
        Debug.Log(GetCurrentAnimationTime());
        StartCoroutine("finishSkill", GetCurrentAnimationTime());
    }
    IEnumerator finishSkill(float t)
    {
        yield return new WaitForSeconds(t);
        skillFinished();
        isSkillPlaying = false;
        yield break;
    }
    float GetCurrentAnimationTime()
    {
        return animator.GetCurrentAnimatorStateInfo(0).speed;
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

    public virtual void UpdateUI()
    {
    }
}
