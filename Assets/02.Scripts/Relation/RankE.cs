using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankE : CustomRank
{
    protected override bool Condition(int _teamScore)
    {
        if(_teamScore > 0)
        {
            return true;
        }

        return false;
    }
    protected override void Apply()
    {
        foreach(Hero hero in GameManager.Hero.GetHeroList())
        {
            if(!appliedHero.Contains(hero))
            {
                hero.AttackDamage += 10;
                appliedHero.Add(hero);
            }
        }
    }

    protected override void Clear()
    {
        foreach (Hero hero in appliedHero)
        {
            hero.AttackDamage -= 10;
            appliedHero.Remove(hero);
        }
    }

    
}
