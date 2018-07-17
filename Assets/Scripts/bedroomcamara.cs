using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class bedroomcamara : MonoBehaviour {
    
    public Vector3 target = new Vector3(0, 0, -10.51f); //目标位置
    private Vector3 init = new Vector3(3.42f,2.43f,-1.43f); //初始向量
    private Vector3 ratio; //系数向量
    public bool finished = false;

    // Use this for initialization
    void Start()
    {
        transform.position = init; //初始化
        Camera.main.orthographic = false; //投影模式
        Ratio_cal();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(Math.Abs(target.z - transform.position.z) < 0.1) && !finished)
        {
            transform.position = transform.position + new Vector3(ratio.x, ratio.y, ratio.z) * Time.deltaTime;
        }
        else
        {
            finished = true;
            Camera.main.orthographic = true; //投影模式
            transform.position = new Vector3(0, 0, -10f);
        }
    }

    void Ratio_cal() //计算系数 2秒速度聚焦
    {
        ratio.x = (target.x - init.x) / 2.0f;
        ratio.y = (target.y - init.y) / 2.0f;
        ratio.z = (target.z - init.z) / 2.0f;
    }
}
