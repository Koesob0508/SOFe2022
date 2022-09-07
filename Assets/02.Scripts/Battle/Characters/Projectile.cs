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
    /// ����ü, ���� X
    /// </summary>
    /// <param name="target">���� Ÿ��</param>
    /// <param name="d">������</param>
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
