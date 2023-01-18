using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CC
{
    public bool burn = false;
    public bool faint = false;
    public bool hide = false;
}

public class Units : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler
{

    protected bool isUpdating = false;
    protected bool isSkillPlaying = false;
    protected bool isCloseAttackUnit; // 근접 유닛

    public Animator animator;
    public bool bHasSkill;

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

    //HeroInvenItem invenItemUI;

    public GameObject projectileObject;
    public GameObject projectileSpawnPoint;
    public GameObject skillEffect;


    [SerializeField] private float speed = 1;

    protected bool bCanMove = false;

    int curIdx = 0;
    int maxIdx = 0;

    protected bool isFilped = false; // 범위 스킬 사용하는 유닛 용

    Vector2 moveDir = new Vector2();
    ArrayList path;

    protected SpriteRenderer spr;
    protected bool isSpriteReverse = false;

    public AnimationClip attackAnimationClip;

    protected float animationDamageDelay;
    protected float attackSpeed;

    public CC unitCC;
    private Character burnCauser; // 화상을 건 적

    GameObject burnEffectObject;
    GameObject faintEffectObject;

    protected virtual void Start()
    {
        isCloseAttackUnit = true;

        burnEffectObject = Resources.Load<GameObject>("Prefabs/Effects/burnEffect");
        faintEffectObject = Resources.Load<GameObject>("Prefabs/Effects/faintEffect");

    }

    protected virtual void Update()
    {
        UpdateUI();

        if (bCanMove && !unitCC.faint)
            Move();


        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y); // 앞 유닛이 앞에 보이게
    }

    public virtual void Initalize(Character charData)
    {
        animator = GetComponentInChildren<Animator>();
        UnitUI = Instantiate(UnitUIObject, transform.position, Quaternion.identity);
        UnitUI.GetComponent<Canvas>().sortingLayerID = SortingLayer.NameToID("Char");
        UnitUI.GetComponent<Canvas>().sortingOrder = -99;
        UnitUI.transform.parent = transform;
        hpBar = UnitUI.transform.GetChild(0).GetComponent<Slider>();
        spBar = UnitUI.transform.GetChild(1).GetComponent<Slider>();

        //BT,BB 초기화
        btComp = GetComponent<BehaviorTreeComponent>();
        btComp.Initalize();
        btComp.TreeObject.bBoard.SetValueAsBool("IsDead", false);
        btComp.TreeObject.bBoard.SetValueAsBool("CanSkill", false);
        btComp.TreeObject.bBoard.SetValueAsFloat("AttackRange", charData.AttackRange);
        btComp.TreeObject.bBoard.SetValueAsFloat("Damage", charData.AttackDamage);
        btComp.TreeObject.bBoard.SetValueAsFloat("AttackDelay", 1 / charData.AttackSpeed);

        GetComponent<Units>().SetSpeed(charData.MoveSpeed);
        spr = GetComponentInChildren<SpriteRenderer>();

        unitCC = new CC();
    }

    public void SetItemUI(HeroInvenItem itemUI)
    {
        //this.invenItemUI = itemUI;
    }
    public void StartBattle()
    {
        btComp.StartTree();
        isUpdating = true;
    }
    public virtual void Attack()
    {
        CheckForFlippingInAttacking();
        PlayAttackAnimation();
    }
    public virtual void Hit(Character Causer, float damage)
    {
    }
    public virtual void ExecuteSkill()
    {

        isSkillPlaying = true;
        BattleLogPanel.Log log = new BattleLogPanel.Log();
        log.Causer = charData;
        log.Target = attackTarget.GetComponent<Units>().charData;
        log.Type = BattleLogPanel.LogType.Skill;
        log.CauserObject = this.gameObject;
        log.TargetObject = attackTarget;
        GameManager.Battle.LogDelegate(log);
        PlaySkillAnimation();

        StartCoroutine(CouroutineSkill());

    }

    IEnumerator CouroutineSkill()
    {
        yield return new WaitForSeconds(0.05f);

        float t = GetCurrentAnimationTime();
        yield return new WaitForSeconds(t + attackSpeed);
        if(gameObject.activeSelf)
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

    #region Event Interface ( Click, Drag )
    public void OnPointerClick(PointerEventData eventData)
    {
        if (charData.Type == GameManager.ObjectType.Enemy)
            return;
        else
            GameManager.Battle.HeroObjectClicked(charData as Hero, eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (charData.Type == GameManager.ObjectType.Enemy)
            return;
        GameManager.Battle.CloseInfoPopUp();
        UpdateUI();
        Vector2 screenPos = eventData.position;
        
        Vector3 WorldPos = Camera.main.ScreenToWorldPoint(screenPos);
        WorldPos.z = 0;
        transform.position = WorldPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (charData.Type == GameManager.ObjectType.Enemy)
            return;
        int layerMask = ~(1 << LayerMask.NameToLayer("Units"));  // Unit 레이어만 충돌 체크함
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero,Mathf.Infinity,layerMask);
        Vector2 screenPos = eventData.position;
        if (hit.collider != null)
        {
            HeroInvenItem item = hit.collider.gameObject.GetComponent<HeroInvenItem>();
            if(item != null && !item.GetIsHeroInInven())
                item.ReturnToInven(gameObject);
            else
            {
                if (screenPos.x > Screen.width / 2)
                {
                    screenPos.x = Screen.width / 2;
                }
                if (screenPos.x < Screen.width * 0.1)
                {
                    screenPos.x = Screen.width * 0.1f;
                }
                if (screenPos.y > Screen.height / 2)
                {
                    screenPos.y = Screen.height / 2;
                }
                if (screenPos.y < 0)
                {
                    screenPos.y = 0;
                }
            }
        }
        else
        {
            if (screenPos.x > Screen.width / 2)
            {
                screenPos.x = Screen.width / 2;
            }
            if (screenPos.x < Screen.width * 0.1)
            {
                screenPos.x = Screen.width * 0.1f;
            }
            if (screenPos.y > Screen.height / 2)
            {
                screenPos.y = Screen.height / 2;
            }
            if (screenPos.y < 0)
            {
                screenPos.y = 0;
            }
        }
        Vector3 WorldPos = Camera.main.ScreenToWorldPoint(screenPos);
        WorldPos.z = 0;
        transform.position = WorldPos;

        GameManager.Battle.CloseInfoPopUp();
    }
    #endregion
    public void SetSpeed(float Speed)
    {
        this.speed = Speed / 10;
    }
    private void Move()
    {
        Vector2 curPos = gameObject.transform.position;
        if (curIdx != maxIdx - 1)
        {
            moveDir = (path[curIdx + 1] as Path.Node).pos - (path[curIdx] as Path.Node).pos;
            gameObject.transform.Translate(moveDir * speed * Time.deltaTime);
            float tempDist = Vector2.Distance((path[curIdx + 1] as Path.Node).pos, gameObject.transform.position);
            if (tempDist < 0.1f)
            {
                curIdx++;
            }
        }
        else
        {
            moveDir = (path[curIdx + 1] as Path.Node).pos - (path[curIdx] as Path.Node).pos;
            gameObject.transform.Translate(moveDir.normalized * speed * Time.deltaTime);
            float tempDist = Vector2.Distance((path[curIdx + 1] as Path.Node).pos, gameObject.transform.position);
            if (tempDist <= GetComponent<Units>().charData.AttackRange)
            {
                bCanMove = false;
                animator.SetBool("Run", false);
            }
            return;
        }
        animator.SetBool("Run", true);


        CheckForFlippingInMoving();
    }
    public void StopMovement()
    {
        bCanMove = false;
    }
    public void SetPath(ArrayList path)
    {
        this.path = path;
        curIdx = 0;
        maxIdx = path.Count - 1;
        bCanMove = true;
    }
    private void CheckForFlippingInMoving()
    {
        bool movingLeft = moveDir.x < 0;
        bool movingRight = moveDir.x > 0;

        if (movingLeft)
        {
            spr.flipX = !isSpriteReverse;
            isFilped = !isSpriteReverse;

        }

        if (movingRight)
        {
            spr.flipX = isSpriteReverse;
            isFilped = isSpriteReverse;

        }
    }

    protected void CheckForFlippingInAttacking()
    {
        Vector2 curPos = gameObject.transform.position;
        Vector2 tarPos = attackTarget.transform.position;
        if (tarPos.x - curPos.x < 0)
        {
            spr.flipX = !isSpriteReverse;
            isFilped = !isSpriteReverse;
        }
        else
        {
            spr.flipX = isSpriteReverse;
            isFilped = isSpriteReverse;
        }
    }

    public void GetCC(string type, float time, Character Causer = null)
    {
        switch (type)
        {
            case "burn":
                unitCC.burn = true;
                burnCauser = Causer;
                StartCoroutine(BurnDamaged());
                break;
            case "faint":
                unitCC.faint = true;

                Vector3 effectPosition = transform.position;
                effectPosition.z = transform.position.z - 1;
                effectPosition.y = transform.position.y + 1;

                GameObject burnEffect = Instantiate(faintEffectObject, effectPosition, Quaternion.identity);
                burnEffect.transform.parent = transform;
                Destroy(burnEffect, time);
                break;
            case "hide":
                unitCC.hide = true; break;
        }
        StartCoroutine(CCBurnCouroutine(type, time));

    }

    IEnumerator CCBurnCouroutine(string type, float time)
    {
        yield return new WaitForSeconds(time);
        switch (type)
        {
            case "burn":
                unitCC.burn = false; break;
            case "faint":
                unitCC.faint = false; break;
            case "hide":
                unitCC.hide = false; break;
        }
    }
    IEnumerator BurnDamaged()
    {
        while(true)
        {
            if (unitCC.burn == false)
                break;

            yield return new WaitForSeconds(1.0f);
            Hit(burnCauser, 5f);

            Vector3 effectPosition = transform.position;
            effectPosition.z = transform.position.z - 1;

            GameObject burnEffect = Instantiate(burnEffectObject, effectPosition, Quaternion.identity);
            burnEffect.transform.parent = transform;
            Destroy(burnEffect, 1.0f);
        }
    }

   
}