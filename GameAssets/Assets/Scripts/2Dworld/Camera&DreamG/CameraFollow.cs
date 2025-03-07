using Fungus;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    /*两个形态：
    1.常规型，跟着玩家运动，镜头大小较小
    2.关卡型，移动到关卡中心，镜头大小较大*/
    public Transform player; // 玩家的Transform组件
    private GameObject Player;
    private PlayerMovement PlayerState;
    public SpriteRenderer playerSpriteRenderer; // 玩家的SpriteRenderer组件
    public Vector3 offsetRight = new Vector3(2.3f, 1, -10); // 玩家面对右边时相机的相对位置
    public Vector3 offsetLeft = new Vector3(-2.3f, 1, -10); // 玩家面对左边时相机的相对位置
    public float movementTime = 10f;

    public bool LevelCarmeraMode = false;
    public bool FocusMode = false;
    public Transform FocusTarget;
    public float moveSpeed = 1;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        PlayerState = Player.GetComponent<PlayerMovement>();

    }

    void Update()
    {
        if(!FocusMode)
        {
            if(!LevelCarmeraMode)
            {
                if (playerSpriteRenderer.flipX == false)
                {
                // 玩家面对右边，相机移动到玩家的右侧
                transform.position = Vector3.Lerp(transform.position, player.position + offsetRight, Time.deltaTime * movementTime);
                }
                else
                {
                // 玩家面对左边，相机移动到玩家的左侧
                transform.position = Vector3.Lerp(transform.position, player.position + offsetLeft, Time.deltaTime * movementTime);
                }
            }
        }
        else
        {
            FocusOnObject();
        }

        if (PlayerState.IsSleepDown || PlayerState.IsDie)
        {
            FocusMode = true;
        }
        else
        {
            FocusMode = false;
        }

    }

    public void FocusOnObject()
    {
        Vector3 targetPosition = new Vector3(FocusTarget.position.x, FocusTarget.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }


}