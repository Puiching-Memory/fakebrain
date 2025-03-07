using System;
using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PatrolState : EnemyState
{
    public Turtle turtle;
    public PatrolState(Enemy enemy) : base(enemy)
    {
        turtle=enemy as Turtle;
    }

    public override void EnterState()
    {
        turtle.anim.SetBool("IsPatrol", true);
        turtle.ResetColorAndInfluTime();
    }

    public override void ExitState()
    {
        turtle.anim.SetBool("IsPatrol", false);
        
    }

    public override void FrameUpdate()
    {
        CheckChangeState();
    }

    public override void PhysicsUpdate()
    {
        if(turtle.isHitWallReturn)
        {
            WallPatrol();
        }
        else
        {
            NormalPatrol();
        }
    }

    public void WallPatrol()//撞墙巡逻方法
    {
        turtle.rb2d.velocity =new Vector2(turtle.patrolSpeed * turtle.direction,turtle.rb2d.velocity.y);
        if(turtle.isHitWall)
        {
            turtle.transform.localScale = new Vector3(-turtle.transform.localScale.x, turtle.transform.localScale.y, turtle.transform.localScale.z);
            turtle.direction = -turtle.direction;
        }
        if(turtle.direction>0)
        {
            turtle.transform.localScale = new Vector3(-Mathf.Abs(turtle.transform.localScale.x), turtle.transform.localScale.y, turtle.transform.localScale.z);

        }
        else
        {
            turtle.transform.localScale = new Vector3(Mathf.Abs(turtle.transform.localScale.x), turtle.transform.localScale.y, turtle.transform.localScale.z);
        }
    }

    public void NormalPatrol()//两点移动巡逻方法
    {
        turtle.rb2d.velocity = new Vector2(turtle.patrolSpeed * turtle.direction, turtle.rb2d.velocity.y);
        if(Mathf.Abs(turtle.transform.position.x-turtle.targetPos.x)<0.1f)
        {
            if(turtle.targetPos ==turtle.startPos)
            {
                turtle.targetPos = turtle.endPos;
            }
            else if(turtle.targetPos ==turtle.endPos)
            {
                turtle.targetPos = turtle.startPos;
            }
            turtle.direction=-turtle.direction;
            turtle.transform.localScale = new Vector3(-turtle.transform.localScale.x, turtle.transform.localScale.y, turtle.transform.localScale.z);
        }
    }

    public void CheckChangeState()
    {
        if (turtle.playerMovement.IsSleepDown) return;
        if (turtle.isPlayClose)
        {
            turtle.anim.SetTrigger("ShockTrigger");
            turtle.stateMachine.ChangeState(turtle.sleepState);
        }
    }
}
