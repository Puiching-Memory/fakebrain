using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyState
{
    /*龟龟红温状态
    玩家在乌龟壳上跳跃，每一次都会让精灵贴图变红
    到达第三次之后，乌龟进入红温乌龟状态，他会冒出头来，向玩家方向发起冲撞
    如果碰到墙壁即冲撞结束，乌龟会直接进入沉寂，不会有下一步动作
    To Do list：撞碎墙壁*/

    public float AttackTime = 8f;
    public float AttackSpeed = 6.5f;

    private float timer;

    

    private float ChongZhuangDir;

    private Transform Player;

    public Turtle turtle;
    public AttackState(Enemy enemy) : base(enemy)
    {
        turtle=enemy as Turtle;
    }

    public override void EnterState()
    {
        //Debug.Log("I AM attack！！！");
        turtle.AngryAnimSound();
        timer = 0;
        
        turtle.anim.SetTrigger("AngryChongFeng");
        Player = GameObject.FindWithTag("Player").transform;
        Vector2 objectPosition = turtle.transform.position;
        Vector2 playerPosition = Player.position;
        Vector2 objectPosition2D = new Vector2(objectPosition.x, 0);
        Vector2 playerPosition2D = new Vector2(playerPosition.x, 0);
        ChongZhuangDir = (playerPosition2D - objectPosition2D).normalized.x;
        if(ChongZhuangDir>0)
        {
            turtle.transform.localScale = new Vector3(-Mathf.Abs(turtle.transform.localScale.x), turtle.transform.localScale.y, turtle.transform.localScale.z);

        }
        else
        {
            turtle.transform.localScale = new Vector3(Mathf.Abs(turtle.transform.localScale.x), turtle.transform.localScale.y, turtle.transform.localScale.z);
        }
    }

    public override void ExitState()
    {
        turtle.anim.SetBool("IsPatrol", true);
        turtle.ResetColorAndInfluTime();
        turtle.isHitWallReturn = true;
        
    }

    public override void FrameUpdate()
    {
        //CheckChangeState();
        timer+=Time.deltaTime;
        if (timer>=AttackTime)
        {
            turtle.stateMachine.ChangeState(turtle.patrolState);
        }
    }

    public override void PhysicsUpdate()
    {
        if(turtle.isHitWallReturn)
        {
            WallPatrol();
        }
        else
        {
            ChongZhuangPlayer();
        }
    }


    public void WallPatrol()//撞墙巡逻方法
    {
        turtle.rb2d.velocity =new Vector2(AttackSpeed * -ChongZhuangDir, turtle.rb2d.velocity.y);
        if(turtle.isHitWall)
        {
            turtle.transform.localScale = new Vector3(-turtle.transform.localScale.x, turtle.transform.localScale.y, turtle.transform.localScale.z);
            turtle.direction = -turtle.direction;
        }
    }

    public void ChongZhuangPlayer()//两点移动巡逻方法
    {
        turtle.rb2d.velocity = new Vector2(AttackSpeed * ChongZhuangDir, turtle.rb2d.velocity.y);
        //turtle.direction=-turtle.direction;
        //turtle.transform.localScale = new Vector3(-turtle.transform.localScale.x, turtle.transform.localScale.y, turtle.transform.localScale.z);
        
    }
}