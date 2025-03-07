using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterMovement : MonoBehaviour
{
    public Transform player; // 玩家的Transform组件
    public SpriteRenderer playerSpriteRenderer; // 玩家的SpriteRenderer组件
    public Vector3 offsetRight = new Vector3(2.3f, 1, 0); // 玩家面对右边时灯笼的相对位置
    public Vector3 offsetLeft = new Vector3(-2.3f, 1, 0); // 玩家面对左边时灯笼的相对位置
    public float movementTime = 10f;

    public bool moveable = true; //能否移动，当触碰到物体时，设为不可移动

    public float floatStrength = 5f; // 灯笼浮动的强度
    public float floatSpeed = 2f; // 灯笼浮动的速度
    private float timer; // 计时器，用于控制浮动的周期

    private Vector3 floatposition;//需要计算的浮动高度
    private Rigidbody2D rb;

    private SpriteRenderer spriteRenderer;


    void Start()
    {
        timer = 0f; // 初始化计时器
        rb = GetComponent<Rigidbody2D>(); // 获取Rigidbody组件
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());//忽略与玩家的碰撞
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        // 更新计时器
        timer += Time.deltaTime * floatSpeed;

        // 计算物体的Y坐标，使其根据正弦函数上下浮动
        float yPosition = Mathf.Sin(timer) * floatStrength;

        floatposition =  new Vector3(0, yPosition,0); //计算出的浮动高度


        if (Vector3.Distance(player.position, transform.position)>10)//如果距离太远，瞬移回玩家身边
        {
            transform.position = player.position + offsetLeft + floatposition;
        }


    }
    void FixedUpdate()
    {
        // 根据玩家的SpriteRenderer组件的翻转状态，设置灯笼的目标位置
        if (playerSpriteRenderer.flipX == false)
        {
            // 玩家面对右边，灯笼移动到玩家的右侧
            rb.MovePosition(Vector3.Lerp(transform.position, player.position + offsetRight + floatposition, Time.deltaTime * movementTime));
            spriteRenderer.flipX = false;
        }
        else
        {
            // 玩家面对左边，灯笼移动到玩家的左侧
            rb.MovePosition(Vector3.Lerp(transform.position, player.position + offsetLeft + floatposition, Time.deltaTime * movementTime));
            spriteRenderer.flipX = true;
        }
    }

    //     private void OnCollisionEnter2D(Collision2D collision)//
    // {
    //     if (collision.gameObject.CompareTag("Ground"))//
    //     {
    //         moveable = false;
    //     }
    // }

    //     private void OnCollisionExit2D(Collision2D collision)//
    // {
    //     if (collision.gameObject.CompareTag("Ground"))
    //     {
    //         moveable = true;
    //     }
    // }
}
