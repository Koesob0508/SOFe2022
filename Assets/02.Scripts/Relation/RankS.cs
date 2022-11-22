using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankS : CustomRank
{
    protected override bool Condition(int _teamScore)
    {
        if(_teamScore > 25)
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
                hero.MaxMana -= 10;
                hero.MaxHP += 150f;
                hero.CurrentHP += 150f;
                hero.AttackSpeed += 0.5f;
                hero.AttackDamage += 10;
                appliedHero.Add(hero);
            }
        }
    }

    protected override void Clear()
    {
        foreach (Hero hero in appliedHero)
        {
            hero.MaxMana += 10;
            hero.MaxHP -= 150f;
            hero.CurrentHP -= 150f;
            hero.AttackSpeed -= 0.5f;
            hero.AttackDamage += 10;
            appliedHero.Remove(hero);
        }
    }

    
}
