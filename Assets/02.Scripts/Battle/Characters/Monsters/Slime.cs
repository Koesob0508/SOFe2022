using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Battle_Enemys
{
    public enum slimeType
    {
        red,
        blue
    }

    public slimeType type;

    bool hasSpawnSlime = false;

    [SerializeField] private GameObject blueSlimeObject;
    [SerializeField] private GameObject greenSlimeObject;

    public override void Hit(Character Causer, float damage)
    {
        if (hasSpawnSlime)
            return;

        state.CurrentHP -= (100 / (state.DefensePoint + 100)) * damage;

        if (state.CurrentHP <= 0 && !hasSpawnSlime)
        {
            hasSpawnSlime = true;
            Vector3 spawnPos = transform.position;

            GameObject slime = new GameObject();
            Enemy tmpEnmeyData = new Enemy();
            
            if (type == slimeType.red)
            {
                slime = Instantiate(blueSlimeObject, spawnPos, Quaternion.identity);
                tmpEnmeyData = GameManager.Data.ObjectCodex[109] as Enemy;
            }
            else if(type == slimeType.blue)
            {
                slime = Instantiate(greenSlimeObject, spawnPos, Quaternion.identity);
                tmpEnmeyData = GameManager.Data.ObjectCodex[108] as Enemy;
            }


            GameManager.Battle.AddEnemy(tmpEnmeyData, slime);

            slime.GetComponent<Battle_Enemys>().Initalize(tmpEnmeyData);
            slime.GetComponent<Units>().StartBattle();
        }

        if (state.CurrentHP <= 0 && !btComp.TreeObject.bBoard.GetValueAsBool("IsDead"))
        {
            btComp.TreeObject.bBoard.SetValueAsBool("IsDead", true);
            GameManager.Battle.DeadProcess(charData.Type, gameObject, Causer);
        }
        else
        {
            if (!isSkillPlaying)
                PlayGetHitAniamtion();
        }
    }

}
