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
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
    }
    /// <summary>
    /// ����ü, ���� X
    /// </summary>
    /// <param name="target">���� Ÿ��</param>
    /// <param name="d">������</param>
    public void Initialize(Vector3 targetVector, float d, float speed)
    {
        damage = d;
        isPenetration = false;

        Vector2 dir = (targetVector - transform.position).normalized;
        GetComponent<Rigidbody2D>().AddForce(dir * speed);
    }

    public void Initialize(float d, float speed)
    {
        damage = d;
        isPenetration = true;

        GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Units>().Hit(damage);
            if(!isPenetration)
                Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
