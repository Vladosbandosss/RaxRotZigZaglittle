using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    Rigidbody rb;
    private bool moveLeft;
    [SerializeField] float speed = 4f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveLeft = true;
    }

    private void Update()
    {
        CheckInput();

        CheckFalling();
    }

    private void CheckFalling()
    {
        if (rb.velocity.y < -0.5f)
        {
            Debug.Log("педорахнулся");
            //Конец игры!!!!!
            //звук педорахания и тд
        }
    }

    private void FixedUpdate()
    {
        if (GamePlayController.instance.gamePlaying)
        {
            if (moveLeft)
            {
                rb.velocity = new Vector3(-speed, Physics.gravity.y, 0f);
              //  Debug.Log("лево");
            }
            else if (!moveLeft)
            {
                rb.velocity = new Vector3(0f, Physics.gravity.y, speed);
                //Debug.Log("ПРЯМО");
            }
        }
       
    }

    void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!GamePlayController.instance.gamePlaying)
            {
                GamePlayController.instance.gamePlaying = true;
                GamePlayController.instance.ActivateSpawn();
            }

        }

        if (GamePlayController.instance.gamePlaying)
        {
            if (Input.GetMouseButtonDown(0))
            {
                moveLeft = !moveLeft;
            }
        }
        
    }




}
