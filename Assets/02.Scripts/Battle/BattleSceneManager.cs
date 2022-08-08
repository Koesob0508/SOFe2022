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
    public void Init(GameManager.MapType mapType)
    {
        Debug.Log("BattleManager Initalized");
        switch (mapType)
        {
            case GameManager.MapType.Boss:
                {

                    break;
                }
            case GameManager.MapType.Dessert:
                {

                    break;
                }
            case GameManager.MapType.Jungle:
                {

                    break;
                }
            default:
                throw new System.Exception("Undefined Map Type!");
        }
    }
    public void Init(List<Hero> Heros, List<Enemy> Enemies, GameManager.MapType mapType)
    {
        Debug.Log("BattleManager Initalized");
        HeroList = Heros;
        EnemyList = Enemies;
    }
    void Update()
    {
        
    }
}
