using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeroBehaviorController
{
    #region Members : Private
    private TileController TileController;
    private List<Hero> heroes;
    private List<Hero> enemies;
    #endregion

    #region Methods : Constructor
    public HeroBehaviorController(TileController tileController, List<Hero> heroes, List<Hero> enemies)
    {
        this.TileController = tileController;
        this.heroes = heroes;
        this.enemies = enemies;
    }
    #endregion

    #region Methods : Public
    public void UpdateMove()
    {
        if (enemies.Count == 0)
            return;

        foreach (var hero in heroes)
        {
            if (hero.TargetHero == null || hero.TargetHero.IsDie())
                hero.TargetHero = FindTarget(hero);
            else
            {
                if (!hero.isArrive())
                {
                    hero.Move();
                }
                else
                    hero.Attack(hero.TargetHero, hero.cur_HeroState.Damage);
            }
        }

    }
    #endregion

    #region Methods : Private

    private Hero FindTarget(Hero hero)
    {
        Hero minDistanceHero = null;
        float minDistance = 9999;

        foreach (var enmey in enemies)
        {
            float distance = Vector3.Distance(hero.transform.position, enmey.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                minDistanceHero = enmey;
            }
        }

        return minDistanceHero;
    }
    #endregion
}
