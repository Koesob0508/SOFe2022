using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankD : CustomRank
{
    protected override bool Condition(int _teamScore)
    {
        if(_teamScore > 5)
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
                hero.AttackSpeed += 0.5f;
                appliedHero.Add(hero);
            }
        }
    }

    protected override void Clear()
    {
        foreach (Hero hero in appliedHero)
        {
            hero.AttackSpeed -= 0.5f;
            appliedHero.Remove(hero);
        }
    }

    
}
