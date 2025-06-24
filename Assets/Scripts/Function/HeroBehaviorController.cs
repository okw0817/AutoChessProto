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
            if (hero.TargetHero == null || hero.TargetHero.IsDie() || hero.FindCount > 2)
            {
                hero.FindCount = 0;
                hero.TargetHero = FindTarget(hero, enemies);
            }
            else
            {
                if (!hero.isArrive() || hero.IsMoving)
                {
                    hero.Move();
                }
                else if (hero.isArrive() && !hero.IsMoving)
                    hero.Attack(hero.TargetHero, hero.cur_HeroState.Damage);
            }
        }

        foreach (var enemy in enemies)
        {
            if (enemy.TargetHero == null || enemy.TargetHero.IsDie())
                enemy.TargetHero = FindTarget(enemy, heroes);
            else
            {
                if (!enemy.isArrive())
                {
                    enemy.Move();
                }
                else
                    enemy.Attack(enemy.TargetHero, enemy.cur_HeroState.Damage);
            }
        }

        
    }
    #endregion

    #region Methods : Private

    private Hero FindTarget(Hero hero, List<Hero> targetList)
    {
        Hero minDistanceHero = null;
        float minDistance = float.MaxValue;

        foreach (var target in targetList)
        {
            float distance = Vector3.Distance(hero.transform.position, target.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                minDistanceHero = target;
            }
        }

        return minDistanceHero;
    }
    #endregion
}
