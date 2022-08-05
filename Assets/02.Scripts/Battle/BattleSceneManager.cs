using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    private List<Hero> HeroList = new List<Hero>();
    private List<Enemy> EnemyList = new List<Enemy>();
    /// <summary>
    /// Call When BattleSelectScene Loaded to Initalize BattleSceneManager
    /// </summary>
    /// <param name="Heros"> Heros List that player owns</param>
    /// <param name="Enemies">Enemy List of this stage</param>
    public void Init()
    {
        Debug.Log("BattleManager Initalized");
    }
    public void Init(List<Hero> Heros, List<Enemy> Enemies)
    {
        Debug.Log("BattleManager Initalized");
        HeroList = Heros;
        EnemyList = Enemies;
    }
    void Update()
    {
        
    }
}
