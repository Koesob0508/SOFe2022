using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonSlime111 : Battle_Enemys
{

    [SerializeField] private GameObject redSlimeObject;

    protected override void Start()
    {
        base.Start();

        isSpriteReverse = true;
    }

    public override void ExecuteSkill()
    {
        base.ExecuteSkill();

        StartCoroutine(SkillCoroutine());

    }

    IEnumerator SkillCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        Vector3 spawnPos = transform.position;

        spawnPos.y += 2f;
        GameObject slime1 = Instantiate(redSlimeObject, spawnPos, Quaternion.identity);

        Enemy tmpEnmeyData = GameManager.Data.ObjectCodex[110] as Enemy;

        GameManager.Battle.AddEnemy(tmpEnmeyData, slime1);

        slime1.GetComponent<Battle_Enemys>().Initalize(tmpEnmeyData);
        slime1.GetComponent<Units>().StartBattle();


        spawnPos.y -= 4f;
        GameObject slime2 = Instantiate(redSlimeObject, spawnPos, Quaternion.identity);

        GameManager.Battle.AddEnemy(tmpEnmeyData, slime2);

        slime2.GetComponent<Battle_Enemys>().Initalize(tmpEnmeyData);
        slime2.GetComponent<Units>().StartBattle();

    }
}
