using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class data2save
{
    public bool isinitial = false; //是否载入初始化剧情标志位，初始化为false
}

public class GameData
{
    //密钥,用于防止拷贝存档// 
    public string key;

    //下面是添加需要储存的内容// 
    public bool isDu;//点击读档为True，加载场景后为False

    //场景存储内容
    public data2save save;

    public GameData()  //构造函数
    {
        save = new data2save();//存储内容初始化，只能放构造函数
        isDu = false;
    }
}

public class GameDataManager : MonoBehaviour {
    private string dataFileName = "initial.dat";//设置存档文件的名称
    private string dataFolderName = "data"; //数据文件夹名称
    private Xmlsave xs = new Xmlsave();

    public GameData gamedata; //需要存储的对象
    // Use this for initialization
    void Start () {
        gamedata = new GameData(); //创建一个待存储的数据
        gamedata.key = SystemInfo.deviceUniqueIdentifier; //获取设备的唯一ID

    }
    
    // Update is called once per frame
    void Update () {
		
	}

    public void Save()  //数据存储函数
    {
        string gameDataFile = GetDataPath() + "/" + dataFolderName + "/" + dataFileName;
        string dataString = xs.SerializeObject(gamedata, typeof(GameData));
        xs.CreateXML(gameDataFile, dataString);
    }

    private static string GetDataPath()
    {
        // Your game has read+write access to /var/mobile/Applications/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX/Documents 
        // Application.dataPath returns ar/mobile/Applications/XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX/myappname.app/Data              
        // Strip "/Data" from path 
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
            // Strip application name 
            path = path.Substring(0, path.LastIndexOf('/'));
            return path + "/Documents";
        }
        else
            //    return Application.dataPath + "/Resources"; 
            return Application.dataPath;
    }
}
