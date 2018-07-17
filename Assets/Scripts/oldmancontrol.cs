using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oldmancontrol : MonoBehaviour {
    public Player others;
    public float speed; //速度插件
    private Animator animator;
    private Vector2 OlderWalkAreaLeft = new Vector2(3f, -4.12f);  //older npc的活动范围
    private Vector2 OlderWalkAreaRight = new Vector2(8f, -4.12f);
    private bool direct = true; //方向变换标志位
    private int flag = 0;


    public bool oldersay = false; //老人说第一句话的标志位
    public bool PlayerfaceOlder = false;

    // Use this for initialization
    void Start () {
        transform.position = new Vector2(11.5f, -4.12f); //初始化老人npc的位置
        animator = GetComponent<Animator>(); //获取动作对象
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Math.Abs(others.transform.position.x - transform.position.x) <= 2.0f)
        {
            PlayerfaceOlder = true;
            animator.SetTrigger("olderstand");
        }
        else
        {
            if (transform.position.x == OlderWalkAreaLeft.x)
            {
                direct = true;
                oldersay = true; //@@对话框替代品 等对话框加入 则flag变化更改位置
            }
            else if (transform.position.x == OlderWalkAreaRight.x)
            {
                flag = 1;
                direct = false;
            }

            if (!direct)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
                animator.SetTrigger("olderwalk");
                transform.position = Vector2.MoveTowards(transform.position, OlderWalkAreaLeft, speed * Time.deltaTime);
            }
            else if (direct && flag == 0)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
                animator.SetTrigger("olderwalk");
                transform.position = Vector2.MoveTowards(transform.position, OlderWalkAreaRight, speed * Time.deltaTime);
            }
            else
            {
                transform.localEulerAngles = new Vector3(0, 180, 0);
                animator.SetTrigger("olderwalk");
                transform.position = Vector2.MoveTowards(transform.position, OlderWalkAreaRight, speed * Time.deltaTime);
            }
        }
    }
}
