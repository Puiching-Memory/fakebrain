using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entangled : MonoBehaviour
{
    private Vector3 connectPos;
    public float MaxDistance;

    public float CurrentDistance;

    public float MinDistance;

    public float LaCheSpeed;

    private Rigidbody2D rb2d;

    public bool IsHanding;
    SpringJoint2D PlayerspringJoint2D;
    private Animator anim;

    private LineRenderer lineRenderer;

    private GameObject Player;
    public PlayerMovement playerMovement;

    private void Start()
    {
        connectPos = transform.position;
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lineRenderer = GetComponent<LineRenderer>();
        Player = GameObject.FindWithTag("Player");
        playerMovement = Player.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(IsHanding)
        {
            anim.SetBool("ShootOut",true);
        }
        else
        {
            anim.SetBool("ShootOut",false);
        }

        if(IsHanding)
        {
            if (CurrentDistance> MinDistance)
            {
                PlayerspringJoint2D.distance = CurrentDistance;
                CurrentDistance -= Time.deltaTime*LaCheSpeed;

            }
        }

        if (playerMovement.isBeEntangled && IsHanding)
        {
            lineRenderer.positionCount = 2;
            Vector2 PLposition = new Vector2 (Player.transform.position.x, Player.transform.position.y+1);
            lineRenderer.SetPosition(0, PLposition);
            lineRenderer.SetPosition(1, transform.position);
        }
        else
        {
            lineRenderer.positionCount = 0; // 不绘制线条
        }

        if(!playerMovement.isBeEntangled&&IsHanding)
        {
            anim.SetTrigger("Ask");
            IsHanding = false;
        }


    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (!collision.GetComponent<PlayerMovement>().IsSleepDown)
            {
                anim.SetTrigger("Shock");
                IsHanding = true;
                collision.gameObject.GetComponent<PlayerMovement>().isBeEntangled = true;
                PlayerspringJoint2D= collision.gameObject.GetComponent<SpringJoint2D>();
            
                PlayerspringJoint2D.connectedBody = rb2d;
                PlayerspringJoint2D.enabled = true;
                //springJoint2D.connectedAnchor = connectPos;
                PlayerspringJoint2D.distance = MaxDistance;
                CurrentDistance = MaxDistance;
            }

        }

    }


    public void SlidAnimSound()
    {
        AudioManager.Instance.Play("Slid");
    }

    // private void OnTriggerStay2D(Collider2D collision)
    // {
    //     if(collision.CompareTag("Player")&&!IsHanding)
    //     {
    //         if(!collision.gameObject.GetComponent<PlayerMovement>().isBeEntangled)
    //         {
    //             IsHanding = false;
    //         }


    //     }
    // }

    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Player"))
    //     {
            
    //         Invoke("DestoryHanding",3f);
            

    //     }
    // }

    // void DestoryHanding()
    // {
    //     IsHanding = false;

    // }
}
