using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samuri2_18 : Battle_Heros
{

    public GameObject alterGambeObject;

    protected override void Start()
    {
        isCloseAttackUnit = true;
    }


    public override void ExecuteSkill()
    {
        base.ExecuteSkill();
        CreateAlterEgo();
    }

    void CreateAlterEgo()
    {
        Vector3 alterPosition = transform.position;
        float randomX = Random.RandomRange(-0.1f, 0.1f);
        float randomY = Random.RandomRange(-0.5f, 0.5f);
        alterPosition.x += randomX;
        alterPosition.y += randomY;

        GameObject alter = Instantiate(alterGambeObject, alterPosition, transform.rotation);

        Character tmpCharData = new Character();

        tmpCharData.MaxHP = charData.MaxHP;
        tmpCharData.CurrentHP = 1;
        tmpCharData.CurHunger = charData.CurHunger;
        tmpCharData.MaxMana = 0;
        tmpCharData.CurrentMana = 0;
        tmpCharData.MoveSpeed = charData.MoveSpeed;
        tmpCharData.AttackDamage = charData.AttackDamage/2;
        tmpCharData.AttackRange = charData.AttackRange;
        tmpCharData.AttackSpeed = charData.AttackSpeed/2;
        tmpCharData.DefensePoint = charData.DefensePoint/2;

        alter.GetComponent<Battle_Heros>().Initalize(tmpCharData);
        alter.GetComponent<Battle_Heros>().bHasSkill = false;
        alter.GetComponent<Units>().StartBattle();

        
    }

}

