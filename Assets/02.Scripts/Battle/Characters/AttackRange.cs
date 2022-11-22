using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    public List<GameObject> attackList = new List<GameObject>();
    public bool isEnemy;
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isEnemy && collision.CompareTag("Heros"))
        {
            attackList.Add(collision.gameObject);
        }

        if (!isEnemy && collision.CompareTag("Enemy"))
        {
            attackList.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isEnemy && collision.CompareTag("Heros"))
        {
            attackList.Remove(collision.gameObject);
        }

        if (!isEnemy && collision.CompareTag("Enemy"))
        {
            attackList.Remove(collision.gameObject);
        }
    }
}
