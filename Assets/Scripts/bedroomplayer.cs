using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class bedroomplayer : MonoBehaviour
{
    public bedroomcamara others; //获取相机的对象
    public float speed; //速度参数
    private Animator animator;
    //private bool istand = false;
    private bool destoryBub = false;
    private Vector2 target = new Vector2(7.6f, -3.4f);

    public GameObject dlgBubble;

    // Use this for initialization
    void Start()
    {
        transform.position = new Vector3(-11f, -3.4f, 0); //初始化位置
        animator = GetComponent<Animator>(); //创建动画对象
    }

    // Update is called once per frame
    void Update()
    {
        if (others.finished) //转场动画结束
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime); //场景行走至目的地
            if ((Math.Abs(transform.position.x - target.x) < float.Epsilon))
            {
                animator.SetTrigger("bedstand");
                //istand = true;
                if (!destoryBub)
                {
                    dlgBubble.GetComponent<Animator>().SetBool("BubOut", true);
                }
            }
        }
    }

    public void my_BubSmall()
    {
        dlgBubble.GetComponent<Animator>().SetBool("BubOut", false);
        destoryBub = true;
        SceneManager.LoadScene(1);
    }
}

