using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_118 : MonoBehaviour
{
    Character owner;
    float damage;

    Transform startPos, endPos;

    [SerializeField] private RuntimeAnimatorController FireAnimator;
    [SerializeField] private RuntimeAnimatorController GroundeAnimator;

    float angle = 90f;
    float gravity = 9.8f;

    public List<GameObject> attackList = new List<GameObject>();

    private void Start()
    {

        GetComponent<Animator>().runtimeAnimatorController = FireAnimator;
    }

    public void Initialize(Character Owner, Vector3 targetVector, float d)
    {
        damage = d/3;
        owner = Owner;

        Vector2 dir = (targetVector - transform.position).normalized;

        GetComponent<Rigidbody2D>().AddForce(dir * 15f);

        StartCoroutine(SimulateProjectile());
    }

    IEnumerator SimulateProjectile()
    {
        yield return new WaitForSeconds(0.5f);

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody2D>().gravityScale = 0;

        GetComponent<Animator>().runtimeAnimatorController = GroundeAnimator;

        for (int i = 0; i < 10; i++)
        {

            for (int j = 0; j < attackList.Count; j++)
            {
                attackList[j].GetComponent<Units>().Hit(owner, damage);
            }

            yield return new WaitForSeconds(0.3f);
        }

        Destroy(this.gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Heros"))
        {
            attackList.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Heros"))
        {
            attackList.Remove(collision.gameObject);
        }
    }
}
