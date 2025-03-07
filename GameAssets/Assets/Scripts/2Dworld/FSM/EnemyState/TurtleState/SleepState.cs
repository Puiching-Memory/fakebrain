using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepState : EnemyState
{
    public Turtle turtle;
    [Header("乌龟缩出来的观望时间")]
    [SerializeField]public float SuoOutCheckTime = 1f;

    private float CheckTimer;

    public SleepState(Enemy enemy) : base(enemy)
    {
        turtle=enemy as Turtle;
    }

    public override void EnterState()
    {
        //turtle.rb2d.velocity = new Vector2(0, turtle.rb2d.velocity.y);
        turtle.rb2d.velocity = Vector2.zero;
        turtle.rb2d.isKinematic=true;
        turtle.rb2d.gravityScale=999;
        turtle.anim.SetBool("IsStartSuo", true);
        turtle.ResetColorAndInfluTime();
    }

    public override void ExitState()
    {
        turtle.rb2d.isKinematic = false;
        turtle.anim.SetBool("IsStartSuo", false);

        turtle.anim.SetInteger("PassCheckInt", 0);
        turtle.anim.SetBool("IsStartCheck", false);

        turtle.rb2d.gravityScale=1;
        
    }

    public override void FrameUpdate()
    {
        CheckChangeState();
    }

    public override void PhysicsUpdate()
    {
        
    }

    public void CheckChangeState()
    {
        if(!turtle.isPlayClose||turtle.playerMovement.IsSleepDown)
        {
            turtle.anim.SetBool("IsStartCheck", true);
            turtle.anim.SetTrigger("AskTrigger");
            CheckTimer+=Time.deltaTime;
            if (CheckTimer >= SuoOutCheckTime)
            {
                turtle.anim.SetTrigger("SuoOutTrigger");
                if (CheckTimer >= (SuoOutCheckTime+0.2f))
                {
                    TurtleChangeState();
                }
            }
        }
        else
        {
            turtle.anim.SetBool("IsStartCheck", false);
            turtle.anim.SetInteger("PassCheckInt", -1);
            CheckTimer = 0;
        }
    }

    void TurtleChangeState()
    {
        turtle.stateMachine.ChangeState(turtle.patrolState);
        

    }
}
