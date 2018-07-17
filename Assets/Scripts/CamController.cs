using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class CamController : MonoBehaviour
{
    public braceletcontrol others; //获取手环控制对象
    public Vector3 target = new Vector3(-10.1f,4.5f,-1.36f);
    private Vector3 ratio;
    private Vector3 init;

    // Use this for initialization
    void Start()
    {
        init = transform.position; //初始化
        Camera.main.orthographic = true;
        Ratio_cal();
        //offset = transform.position - player.transform.position;  //获取玩家行走相对距离
    }

    // Update is called once per frame
    void Update()
    {
        if (others.pickupsuccess)//@如果拾取成功 切换相机为投射方式 加入对话后 使最后一句话说完切换投影模式 并且聚焦相机
        {
            Camera.main.orthographic = false;
            if(!(Math.Abs(target.x - transform.position.x) < 0.2))
            {

                transform.position = transform.position + new Vector3(ratio.x, ratio.y, ratio.z) * Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene(4);
            }
        }

        //transform.position = player.transform.position + offset + playerposvector; //相机随着玩家走动
    }

    void Ratio_cal() //计算系数 2秒速度聚焦
    {
        ratio.x = (target.x - init.x) / 4.0f;
        ratio.y = (target.y - init.y) / 4.0f;
        ratio.z = (target.z - init.z) / 4.0f;
    }
}