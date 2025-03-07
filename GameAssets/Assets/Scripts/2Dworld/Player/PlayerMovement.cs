using UnityEngine;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    #region 可更改参数
    public float moveSpeed = 5.0f; // 移动速度
    public float jumpForce = 10f; // 跳跃力

    public float upGravity;//跳跃时重力大小
    public float downGravity;//下落时重力大小
    public float addSpeed;//额外的速度，作用于移动平台

    private GameObject MainCamera;


    
    #endregion

    #region 装睡状态参数

    public bool IsSleepDown;
    public float SleepDownMoveSpeed = 1;

    #endregion

    #region 被捆住状态参数
    public bool isBeEntangled;//是否被捆住
    public SpringJoint2D springJoint2D;//2d弹簧关节

    #endregion

    #region 私有参数与组件变量
    private Rigidbody2D rb; // 获取2d刚体组件
    private SpriteRenderer spriteRenderer;//获取精灵图像
    private Animator anim;

    public AnimationCurve movementCurve;
    private float CurrentSpeed;
    
    private string state_type;//状态机类别，分为Could_moving，On_Space跟Is_Sleeping
    private float moveInput;
    private float AddTime;

    public bool IsDie = false;

    public GameObject animCamera;


    #endregion






    #region 射线地面检测部分

    [Header("射线地面检测部分")]    
    private bool isGrounded; // 判断是否在地面
    public float groundCheckDistance;//离地面的距离
    [SerializeField]private Transform groundCheck;//
    [SerializeField]private LayerMask WhatisGround;
    

    #endregion






    #region 行为函数

    void Awake()
    {
        MainCamera = transform.Find("Main Camera").gameObject;
        //MainCamera.SetActive(false);

    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // PlayerParameter Selfparam = GetComponent<PlayerParameter>();//获取玩家参数储存组件
        //if(animCamera!=null)animCamera.SetActive(false);
        //MainCamera.SetActive(true);
        
    }

    void Update()
    {
        JudgementIsGround();//地面射线检测

        moveInput = Input.GetAxis("Horizontal");

        if(isBeEntangled)
            {
                state_type = "BeEntangled";
                //springJoint2D.enabled = true;
            }

        if (isGrounded && !isBeEntangled)
        {
            if(Input.GetMouseButton(1))
            {
                IsSleepDown = true;
                state_type="SleepDown";
            }
            else
            {
                IsSleepDown = false;
                state_type="Could_moving";
            }
            
        }
        else if(!isGrounded && !isBeEntangled)
        {
            if(isBeEntangled)
            {
                state_type = "BeEntangled";
                //springJoint2D.enabled = true;
                return;
            }
            state_type="On_Space";
        }


        switch(state_type)
        {
            case "Could_moving":
                GroundMoving();
                break;

            case "On_Space":
                On_space_Moving();
                break;

            case "SleepDown":
                SleepDownMove();
                break;
            case"BeEntangled":
                BeEntangled();
                break;
        }
        // 获取水平输入
        ChangeGravityScale();//改变重力
        
    }

    void OnDisable()
    {
        SetBoolAnimate("IsWalk" , false);
    }

    #endregion

    #region 功能函数

    float Judement_CurveAddUpSpeed()//用曲线设定玩家移动速度
    {
        if(moveInput != 0)
        {
            CurrentSpeed = moveSpeed*movementCurve.Evaluate(AddTime);
            AddTime +=Time.deltaTime;
            return CurrentSpeed;

        }
        else
        {
            AddTime = 0;
            return 0;

        }
    }

        float Judement_CurveAddUpSpeed_SleepState()//用曲线设定玩家移动速度，但是装睡模式
    {
        if(moveInput != 0)
        {
            CurrentSpeed = SleepDownMoveSpeed*movementCurve.Evaluate(AddTime);
            AddTime +=Time.deltaTime;
            return CurrentSpeed;

        }
        else
        {
            AddTime = 0;
            return 0;

        }
    }



    private void JudgementIsGround()//
    {
        isGrounded = Physics2D.Raycast(groundCheck.position,Vector2.down,groundCheckDistance,WhatisGround);

        if (isGrounded)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("OnSpace"))
            {
                anim.Play("ToGround",0);
            }
            SetBoolAnimate("ToGround" , true);
            SetBoolAnimate("OnSpace" , false);
           
        }
        else
        {
            isGrounded = false;
            SetBoolAnimate("ToGround" , false);
            SetBoolAnimate("OnSpace" , true);
        }
    }
    #endregion






    #region 跳跃力与改变滑翔重力

    void giveJumpForce(float force)//赋予跳跃力
    {
        JumpAnimSound();
        rb.velocity = new Vector2(rb.velocity.x, 0);//将刚体y轴速度清0
        rb.AddForce(new Vector2(0f, force));
        StartCoroutine(PlayerMovement.DelayToInvoke(()=>SetBoolAnimate("IsJump" , false),0.1f));
    }

    void ChangeGravityScale()//改变重力
    {
        if(rb.velocity.y>0.1f)
        {
            rb.gravityScale = upGravity;
        }
        else if(rb.velocity.y < -0.1f)//&&!isfloating)
        {
            rb.gravityScale = downGravity;
        }
    }
    #endregion






    #region 动画器函数

    private void SetBoolAnimate(string name , bool booltype)
    {
        anim.SetBool(name, booltype);
    }

    private void SetFloatAnimate(string name , float floatnum)
    {
        anim.SetFloat(name, floatnum);
    }
    #endregion

    public static IEnumerator DelayToInvoke(Action action, float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
       action();
    }

    public void WalkAnimSound()
    {
        AudioManager.Instance.Play("Walk");
    }

    public void PaAnimSound()
    {
        AudioManager.Instance.Play("Pa");
    }

    public void JumpAnimSound()
    {
        AudioManager.Instance.Play("Jump");
    }

    public void ToGroundAnimSound()
    {
        AudioManager.Instance.Play("ToGround");
    }





    #region 地面状态机
    void GroundMoving()//地面移动脚本
    {
        SetBoolAnimate("IsSleepWalk" , false);
        SetBoolAnimate("IsSleepDown" , false);//站起来了，故退出躺下的动画参数
        float moveInput = Input.GetAxis("Horizontal");//获取水平输入
        float HORSpeed = Mathf.Abs(rb.velocity.x);//获取水平速度
        //Debug.Log(HORSpeed);

        if (moveInput != 0)
        {
            SetBoolAnimate("IsWalk" , true);
            
            
        }
        else
        {
            SetBoolAnimate("IsWalk" , false);
            AudioManager.Instance.Stop("Walk");
        }
        


        //CurrentSpeed = Judement_ShiftSpeed(); //判断是否疾跑
        CurrentSpeed = Judement_CurveAddUpSpeed();
        //Debug.Log(CurrentSpeed);

        rb.velocity = new Vector2(moveInput * CurrentSpeed+addSpeed, rb.velocity.y);//
        //Debug.Log(moveInput);

        if (moveInput < 0) //
        {
           spriteRenderer.flipX = true;
        }
        else if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }


        
        if (Input.GetButtonDown("Jump") && isGrounded)//第一段跳
        {
            SetBoolAnimate("IsJump" , true);//设置动画
            StartCoroutine(PlayerMovement.DelayToInvoke(()=>giveJumpForce(jumpForce),0.15f));//播放完动画后起跳
            //Invoke("SetDoubleJump", 0.1f);//设二段跳判断
            //Debug.Log("I CAN jump AGAIN");
            
            
            
        }
        if (anim.GetBool("IsJump") == true)
        {
            StartCoroutine(PlayerMovement.DelayToInvoke(()=>SetBoolAnimate("IsJump" , false),0.1f));
        }

    }
    #endregion


    
    #region 空中状态机

    void On_space_Moving()//空中移动脚本
    {
        AudioManager.Instance.Stop("Walk");

        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * CurrentSpeed, rb.velocity.y);

        if (moveInput < 0) //
        {
           spriteRenderer.flipX = true;
        }
        else if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }

    }
    #endregion


    #region 装睡态状态机

        void SleepDownMove()//装睡移动脚本
    {
        SetBoolAnimate("IsSleepDown" , true);
        float moveInput = Input.GetAxis("Horizontal");//获取水平输入
        float HORSpeed = Mathf.Abs(rb.velocity.x);//获取水平速度
        //Debug.Log(HORSpeed);
        


        if (moveInput != 0)
        {
            SetBoolAnimate("IsSleepWalk" , true);
        }
        else
        {
            SetBoolAnimate("IsSleepWalk" , false);
        }


        
        CurrentSpeed = Judement_CurveAddUpSpeed_SleepState();
        //Debug.Log(CurrentSpeed);

        rb.velocity = new Vector2(moveInput * CurrentSpeed+addSpeed, rb.velocity.y);//
        //Debug.Log(moveInput);

        if (moveInput < 0) //
        {
           spriteRenderer.flipX = true;
        }
        else if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    #endregion


    #region 被捆住状态机

    void BeEntangled()
    {
        anim.Play("ONHangUP",0);
        // float moveInput = Input.GetAxis("Horizontal");//获取水平输入
        // float HORSpeed = Mathf.Abs(rb.velocity.x);//获取水平速度
        // CurrentSpeed = Judement_CurveAddUpSpeed();
        // rb.velocity = new Vector2(moveInput * CurrentSpeed+addSpeed, rb.velocity.y);


        if(Input.GetMouseButton(1))
        {
            anim.SetTrigger("BreakSleepLai");
            springJoint2D.enabled = false;
            isBeEntangled = false;
            state_type = "On_Space";
        }
    }

    #endregion


    #region 判断是否死亡

    public void Die()
    {
        IsDie = true;
        anim.Play("Die",0);
        anim.Play("DieAnim",1);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("DeadZone"))
        {
            IsDie = true;
            Die();
            rb.velocity = new Vector2(rb.velocity.x, 0);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("DeadZone"))
        {
            Invoke("IsDie",3f);

        }
    }
    #endregion

    void InvokeDie()
    {
        IsDie = false;
        
    }

}
