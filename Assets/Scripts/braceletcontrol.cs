using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class braceletcontrol : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public oldmancontrol others; //调入npc:older对象变量
    private Vector2 mousepos;
    private Image image;
    public GameObject go;
    public bool pickupsuccess = false; //设置拾取成功标志位
    public bool inpick = false;
    public int winkle = 10;
    //private int count = 0;
    private List<Vector3> offsets = new List<Vector3>();  //存放两次手镯掉落的位置

    // Use this for initialization
    void Start () {
        offsets.Add(new Vector3(-1.4f, -5.0f, 0f));
        offsets.Add(new Vector3(5f, -5.0f, 0f));
        transform.localScale = Vector3.zero; //将其隐藏
        transform.position = Camera.main.WorldToScreenPoint(offsets[0]); //设置初始位置
    }
	
	// Update is called once per frame
	void Update () {
        if (pickupsuccess)
        {
            //transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            transform.localScale = Vector3.zero;
        }
        else if (others.PlayerfaceOlder && !pickupsuccess && !inpick)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            transform.position = Camera.main.WorldToScreenPoint(offsets[0]); //设置初始位置
            /*//一闪一闪的效果
            if(count <= winkle)
            {
                count++;
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                transform.position = Camera.main.WorldToScreenPoint(offsets[0]); //设置初始位置
            }
            else if(count > winkle && count <= 2* winkle)
            {
                count++;
                transform.localScale = Vector3.zero;
                transform.position = Camera.main.WorldToScreenPoint(offsets[0]); //设置初始位置
            }
            else
            {
                count = 0;
            }
            */
        }
	}

    
    public void OnDrag(PointerEventData eventData)
    {
        inpick = true;
        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //鼠标拖拽的实时坐标

        if (others.PlayerfaceOlder && !pickupsuccess) //@@如果两人物静止，则手环出现(暂时的事件，之后更改为对话完成)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); //拖动时确保显示
            mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //鼠标拖拽的实时坐标
            transform.position = Camera.main.WorldToScreenPoint(mousepos); //实时跟踪鼠标的位置
        }
    }

    public void OnEndDrag(PointerEventData eventData) {  //鼠标拖拽完成松开触发的事件
        inpick = false;
        //Debug.Log("ok");
        if (Math.Abs(mousepos.x - others.transform.position.x) < 0.5f && Math.Abs(mousepos.y - others.transform.position.y) < 0.5f)
        {  
            pickupsuccess = true;
            //transform.localScale = Vector3.zero;
        }

    }
}
