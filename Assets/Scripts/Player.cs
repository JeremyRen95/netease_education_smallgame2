using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int speed;
    public oldmancontrol others;
    private Vector2 mousepos; //鼠标当前点击位置坐标
    private Vector2 playerPos; //玩家的位置坐标
    private Vector2 target; //初始化目标
    private bool move_flag = false; //鼠标触发移动标志位
    private Animator animator;
    private bool firstcartoon = false; //第一段过场动画完成标志位

    public GameObject go;
    public bool PlayerfaceOlder = false;

    // Use this for initialization
    void Start()
    {
        transform.position = new Vector2(-9.7f, -4.12f);
        playerPos = transform.position;
        target.y = playerPos.y; // initialise the y
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (others.oldersay) //当老人说完第一句话时女孩进场
        {
            if (Math.Abs(others.transform.position.x - transform.position.x) <= 2.0f) //如果older npc与player在一定距离则older面对小女孩 两个人停止运动
            {
                PlayerfaceOlder = true;
                animator.SetTrigger("playerStand");
                //others.speed = 0; //使older静止
            }
            else //否则继续走动
            {
                transform.Translate(Vector3.right * 2 * Time.deltaTime);
                animator.SetTrigger("playerWalk");
            }
        }
        if (firstcartoon) //当第一段对话结束后恢复点触逻辑
        {
            if (Math.Abs(target.x - transform.position.x) < float.Epsilon)
            {
                animator.SetTrigger("playerStand");
                move_flag = false;
            }

            if (move_flag)
            {
                transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime); //从position走到target 并设置速度
                animator.SetTrigger("playerWalk");
            }

            if (Input.GetMouseButton(0)) //判断鼠标是否按下,并获取目标点位置
            {
                float delta_x;//世界坐标的转换 用来判断player方向
                move_flag = true;
                mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //鼠标的世界坐标
                playerPos = transform.position;  //player的世界坐标
                target.x = mousepos.x;      //玩家点触鼠标位置的方向      
                delta_x = mousepos.x - playerPos.x;   //相对位置距离
                if (delta_x >= 0)//如果点击鼠标在player前方向前走，否则向后走
                {
                    transform.localEulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    transform.localEulerAngles = new Vector3(0, 180, 0);
                }//点触一步步走
            }
            //Debug.Log("target" + target);

            //transform.Translate(-Vector3.left * Time.deltaTime, Space.World); //移动任务向前走
            //transform.position = Input.mousePosition;
        }

    }

    void OnMouseDown() //鼠标点击事件
    {
        //Debug.Log("OnMouseDown response");
        //transform.Translate(-Vector3.left * Time.deltaTime, Space.World);//(Input.mousePosition);//获取鼠标的位置
        //判断鼠标位置是在前方还是后方 需要鼠标点击位置减去player位置
        //前方前进 后方后退
    }

    void OnMouseDrag()
    {
        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//再点击button的时候去读取鼠标的坐标
        Debug.Log(mousepos);//显示
    }

    public void Braceletclick() //手环拖动函数
    {
        //mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//再点击button的时候去读取鼠标的坐标
        //Debug.Log(mousepos);//显示
    }
}
