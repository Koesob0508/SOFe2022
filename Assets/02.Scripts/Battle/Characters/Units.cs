using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Units : MonoBehaviour, IDragHandler, IEndDragHandler
{
    bool isUpdating = false;
    protected bool isSkillPlaying = false;
    protected bool isCloseAttackUnit; // 근접 유닛
    
    public Animator animator;
    public bool bHasSkill;
    float localScaleX;

    float tTimer = 1.0f;
    public float attackTimer = 0.0f;

    public GameObject UnitUIObject;
    
    GameObject UnitUI;
    protected Slider hpBar;
    protected Slider spBar;

    public Vector3 uiOffset;

    protected BehaviorTreeComponent btComp;

    public GameObject attackTarget;

    public Character charData;

    public System.Action skillFinished;

    HeroInvenItem invenItemUI;

    public GameObject projectileObject;
    public GameObject projectileSpawnPoint;
    public GameObject skillEffect;

    protected virtual void Start()
    {
        isCloseAttackUnit = true;
    }

    protected virtual void Update()
    {
        UpdateUI();
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
        btComp.TreeObject.bBoard.SetValueAsFloat("AttackDelay", 1 / charData.AttackSpeed);

        GetComponent<Movement>().SetSpeed(charData.MoveSpeed);
    }

    public void SetItemUI(HeroInvenItem itemUI)
    {
        this.invenItemUI = itemUI;
    }
    public void StartBattle()
    {
        btComp.StartTree();
        isUpdating = true;
    }
    public virtual void Attack()
    {
        PlayAttackAnimation();
    }
    public virtual void Hit(float damage)
    {
    }
    public virtual void ExecuteSkill()
    {

        isSkillPlaying = true;

        PlaySkillAnimation();

        StartCoroutine("CouroutineSkill");

    }

    IEnumerator CouroutineSkill()
    {
        yield return new WaitForSeconds(0.05f);

        float t = GetCurrentAnimationTime();

        yield return new WaitForSeconds(t);
        skillFinished();
        isSkillPlaying = false;
    }

    

    protected float GetCurrentAnimationTime()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length;

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
    public virtual void Dead()
    {
        isUpdating = false;
        UnitUI.gameObject.SetActive(false);
        PlayDeadAnimation();
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
        hpBar.value = charData.CurrentHP / charData.MaxHP;
        spBar.value = charData.CurrentMana / charData.MaxMana;
    }



    public void OnDrag(PointerEventData eventData)
    {
        UpdateUI();
        Vector2 screenPos = eventData.position;
        if (screenPos.x > Screen.width / 2)
        {
            screenPos.x = Screen.width / 2;
        }

        Vector3 WorldPos = Camera.main.ScreenToWorldPoint(screenPos);
        WorldPos.z = 0;
        transform.position = WorldPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        int layerMask = ~(1 << LayerMask.NameToLayer("Units"));  // Unit 레이어만 충돌 체크함
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero,Mathf.Infinity,layerMask);

        if (hit.collider != null)
        {
            HeroInvenItem item = hit.collider.gameObject.GetComponent<HeroInvenItem>();
            if (item == invenItemUI)
            {
                GameManager.Battle.DeleteHeroOnBattle(gameObject);
                invenItemUI.ReturnToInven();
            }
        }
    }

}
