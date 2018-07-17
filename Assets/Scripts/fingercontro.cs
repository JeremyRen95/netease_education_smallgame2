using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class fingercontro : MonoBehaviour {
    public oldmancontrol others; //创建older对象获取npc位置
    public braceletcontrol bracelet; //创建手环对象
    public float speed; //设置物体移动的速度
    private bool updateflag = false; //更新层级flag
    private Vector3 target; //箭头

    // Use this for initialization
    void Start () {
        transform.localScale = new Vector3(0,0,0);
        transform.position = new Vector3(-1.4f,-5.69f,0f); //确定手指初始化位置并隐藏
	}
	
	// Update is called once per frame
	void Update () {
        //获取目标坐标
        target = transform.position;
        target.x = others.transform.position.x;
        
        if (others.PlayerfaceOlder && !updateflag)   //手镯出现的同时出现提示且只更新一次
        { 
            updateflag = true;
            transform.localScale = new Vector3(0.2f, 0.2f, 1);
            transform.position = new Vector3(-1.4f, -5.69f, 0); 
        }
        
        if (Math.Abs(target.x - transform.position.x) < float.Epsilon)
        {
            transform.localScale = new Vector3(0.2f, 0.2f, 1);
            transform.position = new Vector3(-1.4f, -5.69f, 0);
        }
        if(updateflag && !bracelet.inpick && !bracelet.pickupsuccess)
        {
            transform.localScale = new Vector3(0.2f, 0.2f, 1);
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        else if (bracelet.inpick)
        {
            transform.localScale = new Vector3(0, 0, 0);
            transform.position = new Vector3(-1.4f, -5.69f, -1f); //玩家拖动手环之后 消失隐藏
        }
        
    }
}
