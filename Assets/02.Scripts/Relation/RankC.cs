using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankC : CustomRank
{
    protected override bool Condition(int _teamScore)
    {
        if(_teamScore > 10)
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
                hero.MaxHP += 50f;
                hero.CurrentHP += 50f;
                appliedHero.Add(hero);
            }
        }
    }

    protected override void Clear()
    {
        foreach (Hero hero in appliedHero)
        {
            hero.MaxHP -= 50f;
            hero.CurrentHP -= 50f;
            appliedHero.Remove(hero);
        }
    }

    
}
