using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronNight13 : Battle_Heros
{

    public GameObject IronNightSkillEffect;
    protected override void Start()
    {
        isCloseAttackUnit = true;
        IronNightSkillEffect = Resources.Load<GameObject>("Prefabs/Effects/IronNightSkillEffect");

    }


    public override void ExecuteSkill()
    {
        base.ExecuteSkill();

        BuffSkill();
        StartCoroutine("BuffCoroutine");
    }


    IEnumerator BuffCoroutine()
    {
        yield return new WaitForSeconds(5f);
        UnbuffSkill();
    }

    void BuffSkill()
    {
        charData.DefensePoint += 50f;

        // effect & animation Ãß°¡
        Vector3 effectPosition = transform.position;
        effectPosition.z = transform.position.z - 1;
        effectPosition.y = transform.position.y + 0.7f;

        GameObject skillEffect = Instantiate(IronNightSkillEffect, effectPosition, Quaternion.identity);
        Destroy(skillEffect, 5.0f);
    }

    void UnbuffSkill()
    {
        charData.DefensePoint -= 50f;
    }
}

