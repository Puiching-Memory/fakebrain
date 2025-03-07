using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : State
{
    public Enemy enemy;
    public EnemyState(Enemy enemy)
    {
        this.enemy = enemy;
    }
}
