using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class DamagePopUp : MonoBehaviour
{
    IObjectPool<GameObject> pool;

    public void SetPool(IObjectPool<GameObject> pool)
    {
        this.pool = pool;
    }
    public void StartAnimation()
    {
        LeanTween.scale(gameObject, Vector3.one * 2, 1);
        LeanTweenExt.LeanAlphaText(GetComponent<TMPro.TextMeshProUGUI>(), 0, 1);
        LeanTween.moveLocalY(this.gameObject, 50, 1).setOnComplete(() => { pool.Release(gameObject); });
    }

    void Update()
    {
        
    }
}
