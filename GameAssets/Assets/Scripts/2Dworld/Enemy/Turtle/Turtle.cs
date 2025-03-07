using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : Enemy
{
    [Header("巡逻状态参数")]
    public float patrolSpeed;//巡逻速度
    public Vector3 startPos;//起始位置
    public Vector3 endPos;//末位置
    public Vector3 targetPos;//目的位置
    public int direction;//x轴移动方向
    public bool isHitWallReturn;//是否碰墙返回
    public Rigidbody2D rb2d;
    public bool isHitWall;//是否撞墙

    public float AttackAddFroce = 100f;

    private Vector2 BornTransform;// 诞生位置，重置游戏后会回到该位置

    [Header("缩头状态参数")]
    public bool isPlayClose;//玩家是否靠近

    [Header("状态机")]
    public StateMachine stateMachine;
    public State patrolState;
    public State sleepState;

    public State attackState;

    private int InfluTime;//玩家惊扰乌龟的次数

    [Header("玩家")]
    public PlayerMovement playerMovement;

    #region 动画机区域
    public Animator anim;

    #endregion

    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        targetPos = endPos;
        BornTransform = transform.position;//定义出生位置
        EventHandler.CheckPointReStartGame2DEvent += returnBornPosition;
        rb2d= GetComponent<Rigidbody2D>();
        transform.position = startPos;
        InitState();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        stateMachine.currentState.FrameUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public void InitState()//初始化状态机相关参数
    {
        stateMachine=new StateMachine();
        patrolState=new PatrolState(this);
        sleepState=new SleepState(this);
        attackState= new AttackState(this);
        stateMachine.Init(patrolState);
    }

    public void returnBornPosition()//重置原本位置
    {
        stateMachine.ChangeState(patrolState);//回到待机态
        
        targetPos = endPos;
        
        direction = 1;
        transform.position = startPos;
        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        isHitWallReturn = false;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&&stateMachine.currentState==sleepState&&!collision.gameObject.GetComponent<PlayerMovement>().IsSleepDown)
        {
            if(InfluTime<=3)
            {
                Debug.Log("Sleep Chuang!"+InfluTime);
                InfluTime+=1;
                InDeepRedColor();
                anim.SetBool("IsAngry",true);
            }
            else//跳四次红温进入攻击态
            {
                stateMachine.ChangeState(attackState);
            }

        }

        if (collision.gameObject.CompareTag("Player")&&stateMachine.currentState==attackState)
        {
            Vector2 objectPosition = transform.position;
            Vector2 playerPosition = collision.gameObject.transform.position;
            Vector2 objectPosition2D = new Vector2(objectPosition.x, objectPosition.y);
            Vector2 playerPosition2D = new Vector2(playerPosition.x, playerPosition.y);
            Vector2 ChongZhuangDir = (playerPosition2D - objectPosition2D).normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(ChongZhuangDir * AttackAddFroce, ForceMode2D.Impulse);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().addSpeed= rb2d.velocity.x;
            //Debug.Log("add speed to player! THE addspeed is "+ collision.gameObject.GetComponent<PlayerMovement>().addSpeed);
        }

            if (collision.gameObject.CompareTag("Player")&&stateMachine.currentState==attackState)
        {
            Vector2 objectPosition = transform.position;
            Vector2 playerPosition = collision.gameObject.transform.position;
            Vector2 objectPosition2D = new Vector2(objectPosition.x, objectPosition.y);
            Vector2 playerPosition2D = new Vector2(playerPosition.x, playerPosition.y);
            Vector2 ChongZhuangDir = (playerPosition2D - objectPosition2D).normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(ChongZhuangDir * AttackAddFroce, ForceMode2D.Impulse);
        }


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().addSpeed=0;
        }
    }


        void InDeepRedColor()
    {
        // 获取当前精灵的颜色
        Color currentColor = spriteRenderer.color;

        // 增加红色分量，这里我们增加0.1，你可以根据需要调整这个值
        currentColor.g -= 0.33f;
        currentColor.b -= 0.33f;

        // 确保颜色值在0到1之间
        //currentColor.r = Mathf.Clamp(currentColor.r, 0f, 1f);

        // 设置新的精灵颜色
        spriteRenderer.color = currentColor;
        Debug.Log(currentColor);
    }

    public void ResetColorAndInfluTime()
    {
        spriteRenderer.color = new Color (1, 1, 1, 1);
        InfluTime = 0;
        anim.SetBool("IsAngry",false);
        anim.Play("Move",0);
        isHitWall = false;

    }

    private void OnDisable()
    {
        EventHandler.CheckPointReStartGame2DEvent -= returnBornPosition;
    }

        public void AskAnimSound()
    {
        AudioManager.Instance.Play("Ask");
    }

    public void AngryAnimSound()
    {
        AudioManager.Instance.Play("Angry");
    }




}
