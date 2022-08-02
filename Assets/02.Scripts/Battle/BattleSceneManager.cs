using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    /// <summary>
    /// Call When BattleSelectScene Loaded to Initalize BattleSceneManager
    /// </summary>
    /// <param name="Heros"> Heros List that player owns</param>
    /// <param name="Enemies">Enemy List of this stage</param>
    public void Initalize(List<Hero> Heros, List<Enemy> Enemies)
    {
        Debug.Log("BattleManager Initalized");
    }
    void Update()
    {
        
    }
}
