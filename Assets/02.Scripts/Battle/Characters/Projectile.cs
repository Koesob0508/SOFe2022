using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
      
    Rigidbody2D rigid;

    float damage;
    bool isPenetration;
    
    private void Update()
    {
    }
    /// <summary>
    /// 투사체, 관통 X
    /// </summary>
    /// <param name="target">유도 타겟</param>
    /// <param name="d">데미지</param>
    public void Initialize(Vector3 targetVector, float d)
    {
        damage = d;
        isPenetration = false;

        Vector2 dir = (targetVector - transform.position).normalized;
        GetComponent<Rigidbody2D>().AddForce(dir * 500.0f);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Units>().Hit(damage);
            Destroy(gameObject);
        }
    }
}
