using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Xml;

public class smallgame1wordcontrol : MonoBehaviour{

    public Text countext; //测试按钮
    public TextAsset myText; //xml文件接口

    public GameObject gameobjectTodrag; //需要拖动的对象
    public List<GameObject> objectlist = new List<GameObject>(); //获取所有object的对象 用了检测对象之间的距离
    public List<GameObject> wordlist = new List<GameObject>();
    public bool iscollision = false; //是否碰撞标志位
    private GameObject self; //自己游戏对象
    private Vector3 Gocenter; //对象的中心
    private Vector3 touchposition;
    private Vector3 offset; //存放鼠标点击物体部位与现实物体中心的具体
    private Vector3 newGocenter; //需要拖动的目的地

    //拖拽逻辑的所有变量
    private List<Vector3> initialpos = new List<Vector3>(); //创建向量列表存储初始位置
    private bool inarea = false; //是否进入砖块放置区域标志位
    private float smalldistance = 10f; //存放最小的距离 先存放比较大的值
    private int smalldistanceindex; //选中方框的索引
    private GameObject nearest; //最接近砖块的位置的方框
    private int objectcount; //存储对象的长度
    private int currentindex;
    private int objectindex;
    private List<bool> issingle = new List<bool>(); //判断是否重复数组
    private List<wordliststruct> check = new List<wordliststruct>();
    private new Vector3 higher = new Vector3(0, 0, -1f); //用来提升砖块的层级

    //判断正确性的所有变量
    Dictionary<string, string> answer = new Dictionary<string, string>(); //存储正确答案的字典
    Dictionary<string, string> playeranswer = new Dictionary<string, string>(); //存储玩家答案的字典
    private List<bool> istrue = new List<bool>(); //存储每个面板的正确性 otngugo
    private bool fullmarks = true; //全部选中标志位,一开始设置为true

    RaycastHit hit; //存储射线位置

    private bool draggingMode = false; //拖拽状态标志位

    private struct wordliststruct
    {
        public bool isactivate; //是否激活标志位
        public int object_index; //连接的格子索引

        public wordliststruct(bool isactivate, int object_index) //构造函数
        {
            this.isactivate = isactivate;
            this.object_index = object_index;
        }

    }

    void Start()
    {
        answer = ReadSingleXml(myText); 
        objectcount = objectlist.Count;  //用来判断防止是否结束
        for(int i = 0;i< wordlist.Count; i++) //存储初始化位置
        {
            initialpos.Add(wordlist[i].transform.position);
        }
        for(int i = 0; i < objectlist.Count; i++) //初始化查重数组
        {
            issingle.Add(true);
        }
        //初始化连接数组
        for (int i = 0; i < wordlist.Count; i++)
        {
            check.Add(new wordliststruct(false, -1));
        }
    }

    void Update()
    {
        set_text();
        iscollision = false;
        if(objectcount == 0) //如果玩家放置完成 则判断玩家的正确与否
        {
            check_validity();
            if (fullmarks)
            {
                countext.text = "success";
            }
            else
            {
                countext.text = "fail";
            }
            fullmarks = true;

        }
        if (Input.GetMouseButtonDown(0)) //鼠标左键按下
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) //如果射线碰撞到物体
            {
                gameobjectTodrag = hit.collider.gameObject; //获取射线射到的碰撞体
                Gocenter = gameobjectTodrag.transform.position;
                touchposition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //鼠标点击的位置
                offset = touchposition - Gocenter;
                
                draggingMode = true; //拖动模式开启
            }
            for (int i = 0; i < wordlist.Count; i++) //寻找当前对象索引
            {
                if (gameobjectTodrag.name == wordlist[i].name)
                {
                    objectindex = i;
                }
            }
            if(check[objectindex].isactivate == true) //玩家选择更改
            {
                playeranswer.Remove(objectlist[check[objectindex].object_index].name); //玩家选择更改 则移除此选择
                check[objectindex] = new wordliststruct(false, check[objectindex].object_index);
                issingle[check[objectindex].object_index] = true;
                objectcount++;
                  print(playeranswer);
            }

        }

        if (Input.GetMouseButton(0))
        {
            //每帧更新时 初始化最短距离和方框颜色
            inarea = false;
            smalldistance = 10f;
            for (int i = 0; i < wordlist.Count; i++) //存储初始化位置
            {
                objectlist[i].GetComponent<Renderer>().material.color = Color.white;
            }

            if (draggingMode && !iscollision)
            {
                touchposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                newGocenter = touchposition - offset; //获取目的地的中心的点
                gameobjectTodrag.transform.position = new Vector3(newGocenter.x, newGocenter.y, Gocenter.z);

            }
            for (int i = 0; i < objectlist.Count; i++) //判断砖块区域间的距离
            {
                float offset_x = 0; //x轴误差
                float offset_y = 0; //y轴误差
                offset_x = Math.Abs(gameobjectTodrag.transform.position.x - objectlist[i].transform.position.x); //x轴误差
                offset_y = Math.Abs(gameobjectTodrag.transform.position.y - objectlist[i].transform.position.y); //y轴误差
                if (offset_x < 1.2 && offset_y < 0.98f) //进入区域
                {
                    inarea = true; //标记已进入区域
                                   //进入区域后存储最短距离缩影
                    if (offset_x <= smalldistance) //如果距离比先前的小 则更新索引
                    {
                        smalldistanceindex = i;
                        smalldistance = offset_x;
                    }
                }
            } //检测碰撞是否发生模块 待定
            if (inarea)
            {
                objectlist[smalldistanceindex].GetComponent<Renderer>().material.color = Color.grey; //设置灰暗
            }

            //变暗 判断最近的
        }

        if (Input.GetMouseButtonUp(0))
        {
            if(inarea && issingle[smalldistanceindex]) //在区域内 加入格子内
            {
                inarea = false;
                playeranswer.Add(objectlist[smalldistanceindex].name,wordlist[objectindex].name); //保存玩家选择
                print(playeranswer);
                //将砖块放在指定位置
                objectlist[smalldistanceindex].GetComponent<Renderer>().material.color = Color.white;
                gameobjectTodrag.transform.position = objectlist[smalldistanceindex].transform.position;
                issingle[smalldistanceindex] = false;
                check[objectindex] = new wordliststruct(true, smalldistanceindex);
                objectcount--;
                gameobjectTodrag.transform.position = gameobjectTodrag.transform.position + higher;
            }
            else //如果不在范围内 则返回原点
            {
                gameobjectTodrag.transform.position = initialpos[objectindex];//返回初始位置
                objectlist[smalldistanceindex].GetComponent<Renderer>().material.color = Color.white;
                   print(playeranswer);
            }
            draggingMode = false;

        }
    }

    void set_text()
    {
        countext.text = "Count:" + objectcount.ToString();
    }


    private Dictionary<string, string> ReadSingleXml(TextAsset myText) //返回答案关联字典
    {
        Dictionary<string, string> temp = new Dictionary<string, string>();
        XmlDocument mDocument = new XmlDocument();
        // load xml script
        mDocument.LoadXml(myText.text);
        // read root point
        XmlElement mElement = mDocument.DocumentElement;
        // read node
        XmlNodeList mNodeList_panel = mElement.SelectNodes("/Poem/link/panel");
        XmlNodeList mNodeList_word = mElement.SelectNodes("/Poem/link/word");
        
        for (int i = 0; i < mNodeList_panel.Count; i++)   //创建键值对
        {
            temp.Add(mNodeList_panel[i].InnerText, mNodeList_word[i].InnerText);
        }

        return temp;
    }


    void check_validity() //测试玩家是否正确
    {
        //answer  playeranswer
        Dictionary<string, string>.KeyCollection keyCol = answer.Keys; 
        string[] mArray = new string[answer.Count]; //创建key键值存放数组
        int i = 0;
        foreach (string key in keyCol)//取出答案中所有的key值
        {
            mArray[i] = key;
            i++;
        }
        for(int j = 0;j < answer.Count; j++)
        {
            if(answer[mArray[j]] == playeranswer[mArray[j]])
            {
                istrue.Add(true);
            }
            else
            {
                istrue.Add(true);
                fullmarks = false; //全对标志位设为false
            }
        }
    }

    //测试函数
    /*
    void print(Dictionary<string, string> temp)
    {
        Dictionary<string, string>.KeyCollection keyCol = temp.Keys;
        Dictionary<string, string>.ValueCollection valCol = temp.Values;
        foreach (string key in keyCol)
        {
            Debug.Log(key);
        }
        foreach (string value in valCol)
        {
            Debug.Log(value);
        }
    }
    */


}
