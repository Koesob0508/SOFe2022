using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    GameObject targetUnit;
    Vector3 targetPos;
    float damage;
    bool isPenetration;

    private void Start()
    {
        targetUnit = null;
        damage = 0;
        isPenetration = false;
    }

    private void Update()
    {
        transform.Translate(new Vector2(0.1f, 0.0f));
        //if (targetUnit)
        //{
        //    targetPos = targetUnit.transform.position;
        //    transform.Translate(new Vector2(targetPos.x - transform.position.x, targetPos.y - transform.position.y));
        //}
    }

    /// <summary>
    /// 직선 투사체, 관통 여부 입력
    /// </summary>
    /// <param name="p">관통 여부</param>
    /// <param name="d">데미지</param>
    public void Initialize(bool p, float d)
    {
        isPenetration = p;
        damage = d;
    }
    /// <summary>
    /// 유도 투사체, 관통 X
    /// </summary>
    /// <param name="target">유도 타겟</param>
    /// <param name="d">데미지</param>
    public void Initialize(GameObject target, float d)
    {
        targetUnit = target;
        damage = d;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == targetUnit)
        {
            targetUnit.GetComponent<Units>().Hit(damage);
            Destroy(gameObject);
        }
    }
}
