using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBase : MonoBehaviour, IInventoryHero
{

    public string _Name;
    public string Name //constant
    {
        get
        {
            return _Name;
        }
    }

    public Sprite _Image; //constant
    public Sprite Image //constant
    {
        get
        {
            return _Image;
        }
    }

    public virtual void OnPickup()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnDrop()
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

        // if (hit.collider == null)
        {
            gameObject.SetActive(true);
            gameObject.transform.position = worldPoint;
        }

    }
}
