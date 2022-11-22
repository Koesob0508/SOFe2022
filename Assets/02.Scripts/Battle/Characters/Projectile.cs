using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Character owner;
    float damage;
    bool isPenetration;

    public bool isEnemy;
    
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
    }
    /// <summary>
    /// 투사체, 관통 X
    /// </summary>
    /// <param name="target">유도 타겟</param>
    /// <param name="d">데미지</param>
    public void Initialize(Character Owner, Vector3 targetVector, float d, float speed)
    {
        damage = d;
        isPenetration = false;
        owner = Owner;

        Vector2 dir = (targetVector - transform.position).normalized;
        Debug.Log("dir:"+dir);
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
        if (!isEnemy)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<Units>().Hit(owner, damage);
                if (!isPenetration)
                    Destroy(gameObject);
            }
        }
        else
        {
            if (collision.CompareTag("Heros"))
            {
                collision.GetComponent<Units>().Hit(owner, damage);
                if (!isPenetration)
                    Destroy(gameObject);
            }
        }


    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
