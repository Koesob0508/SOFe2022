using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankB : CustomRank
{
    protected override bool Condition(int _teamScore)
    {
        if(_teamScore > 15)
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
                hero.MaxHP += 100f;
                hero.CurrentHP += 100f;
                appliedHero.Add(hero);
            }
        }
    }

    protected override void Clear()
    {
        foreach (Hero hero in appliedHero)
        {
            hero.MaxHP -= 100f;
            hero.CurrentHP -= 100f;
            appliedHero.Remove(hero);
        }
    }

    
}
