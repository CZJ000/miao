using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 测试类
/// </summary>
/// 
    
public class CreatPoint :Singleton<CreatPoint> {
   
    private const float  cosAngle = 18*PI;
    private const float  culAngle = 54*PI;
    private const float   PI =  Mathf.PI/180;
    private const float mathangle = 72f;


     
    private int rotatedir = 1;

    [Range(1,72)]
    public float speed = 10f;

    private bool isok = false;
    private const float angle = 72 * PI;
    private float timer = 0;
    public Transform CenterPoint;
    public Transform AICenterPoint;
    public float Cir_R=1;
    private List<Vector3> PointList = new List<Vector3>();                           //储存位置信息
    private List<GameObject> PrefabsList = new List<GameObject>();//储存猫对象
    private List<Vector3> AIPointList = new List<Vector3>();
    private List<GameObject> AIPrefabslist = new List<GameObject>();
    private Dictionary<int, Vector3> flagposdir = new Dictionary<int, Vector3>();    // 对应位置ID
                                          
   
   
	public enum PointId :int { One=0,Two=1,Three=2,Four=3,Five=4};
    private PointId CurrentPointEnum = PointId.One;
    
     
   // private MiaoData miaodata ;
     
   
    void Start()
    {


        PointInit();


       

    }

   public void  PointInit()
    {
        //初始化各项数据
        PointList.Clear();
        AIPointList.Clear();
        flagposdir.Clear();
        CurrentPointEnum= PointId.One;

        for (int i = 0; i < 5; i++)
        {
            CalculationPoint(CenterPoint.position, Cir_R, false);

          

        }

        for (int i = 0; i < 5; i++)
        {
            CalculationPoint(AICenterPoint.position, Cir_R, true);

           

        }
    }
    /// <summary>
    /// 接收外部转来的队伍 
    /// </summary>
    /// <param name="objList"></param>
    public    void   Creatprefabs( List<GameObject>  objList ,bool isAi)
    {
        if(isAi)
        {
            AIPrefabslist.Clear();
            if (objList.Count <= 0)
            {

                Debug.Log("队伍为空 ");
                return;

            }
            else
            {
                Debug.Log("队伍不   为空 ");
                for (int i=0;i<objList.Count; i++)
                {
                    AIPrefabslist.Add(objList[i]);

                }

            }
            for (int i = 0; i < AIPrefabslist.Count; i++)
            {

                AIPrefabslist[i].SetActive(true);
                AIPrefabslist[i].transform.position = AIPointList[i];
                AIPrefabslist[i].transform.rotation = AICenterPoint.transform.rotation;
                BattleCatInfo info = AIPrefabslist[i].GetComponent<BattleCatInfo>();
                info.currentposid = i;


            }
        }else
        {
            PrefabsList.Clear();
            if (objList.Count <= 0)
            {

                
                
                return;

            }
            else
            {
                for (int i = 0; i < objList.Count; i++)
                {
                    PrefabsList.Add(objList[i]);

                }
                Debug.Log("队伍为: "+PrefabsList.Count);


            }
            for (int i = 0; i < PrefabsList.Count; i++)
            {

                PrefabsList[i].SetActive(true);
                PrefabsList[i].transform.position = PointList[i];
                PrefabsList[i].transform.rotation = CenterPoint.transform.rotation;
                BattleCatInfo info = PrefabsList[i].GetComponent<BattleCatInfo>();
                info.currentposid = i;


            }
        }
      

    }
    void CalculationPoint(Vector3 Point,float  R,bool isAI)
    {
       
        if(isAI)
        {
            Vector3 tempPoint = Vector3.zero;

            tempPoint.y = Point.y;
            switch (CurrentPointEnum)
            {
                case PointId.One:

                    tempPoint.x = Point.x - R * Mathf.Cos(cosAngle);
                    tempPoint.z = Point.z - R * Mathf.Sin(cosAngle);
                    AIPointList.Add(tempPoint);
                    CurrentPointEnum = PointId.Two;

                    break;
                case PointId.Two:

                    tempPoint.x = Point.x;
                    tempPoint.z = Point.z - R;
                    AIPointList.Add(tempPoint);
                    CurrentPointEnum = PointId.Three;

                    break;
                case PointId.Three:
                    tempPoint.x = Point.x + R * Mathf.Cos(cosAngle);
                    tempPoint.z = Point.z - R * Mathf.Sin(cosAngle);
                    AIPointList.Add(tempPoint);  
                    CurrentPointEnum = PointId.Four;

                    break;
                case PointId.Four:

                    tempPoint.x = Point.x + R * Mathf.Cos(culAngle);
                    tempPoint.z = Point.z + R * Mathf.Sin(culAngle);
                    AIPointList.Add(tempPoint);
                    CurrentPointEnum = PointId.Five;

                    break;
                case PointId.Five:

                    tempPoint.x = Point.x - R * Mathf.Cos(culAngle);
                    tempPoint.z = Point.z + R * Mathf.Sin(culAngle);
                    AIPointList.Add(tempPoint);   
                    CurrentPointEnum = PointId.One;

                    break;
            }
        }else
        {
            Vector3 tempPoint = Vector3.zero;


            tempPoint.y = Point.y;
            switch (CurrentPointEnum)
            {
                case PointId.One:   //A
                    tempPoint.x = Point.x - R * Mathf.Cos(cosAngle);
                    tempPoint.z = Point.z + R * Mathf.Sin(cosAngle);
                    //tempPoint.x = Point.x - R;
                    // tempPoint.z = Point.z;
                    PointList.Add(tempPoint);
                    flagposdir.Add(0, tempPoint);
                    CurrentPointEnum = PointId.Two;

                    break;
                case PointId.Two:           //B    
                    tempPoint.x = Point.x;
                    tempPoint.z = Point.z + R;
                    // tempPoint.x = Point.x - R*Mathf.Cos(angle);
                    // tempPoint.z = Point.z+R* Mathf.Sin(angle); 
                    PointList.Add(tempPoint);
                    flagposdir.Add(1, tempPoint);
                    CurrentPointEnum = PointId.Three;

                    break;
                case PointId.Three:       //  C
                    tempPoint.x = Point.x + R * Mathf.Cos(cosAngle);
                    tempPoint.z = Point.z + R * Mathf.Sin(cosAngle);
                    // tempPoint.x = Point.x - R * Mathf.Cos(angle*2);
                    //tempPoint.z = Point.z + R * Mathf.Sin(angle*2);
                    PointList.Add(tempPoint);
                    flagposdir.Add(2, tempPoint);
                    CurrentPointEnum = PointId.Four;

                    break;
                case PointId.Four:          //D
                    tempPoint.x = Point.x + R * Mathf.Cos(culAngle);
                    tempPoint.z = Point.z - R * Mathf.Sin(culAngle);
                    // tempPoint.x = Point.x - R * Mathf.Cos(angle * 3);
                    //tempPoint.z = Point.z + R * Mathf.Sin(angle * 3);
                    PointList.Add(tempPoint);
                    flagposdir.Add(3, tempPoint);
                    CurrentPointEnum = PointId.Five;

                    break;
                case PointId.Five:          //E
                    tempPoint.x = Point.x - R * Mathf.Cos(culAngle);
                    tempPoint.z = Point.z - R * Mathf.Sin(culAngle);
                    // tempPoint.x = Point.x - R * Mathf.Cos(angle * 4);
                    // tempPoint.z = Point.z + R * Mathf.Sin(angle * 4);
                    PointList.Add(tempPoint);
                    flagposdir.Add(4, tempPoint);
                    CurrentPointEnum = PointId.One;

                    break;
            }
        }
       
    }
    void Update()
    { 

        if (Input.GetKeyDown(KeyCode.X))
        {
            isok = true;


        }
        if (isok)
        {
            timer += speed;
            if (timer <= mathangle)
            {
              
                    MoveGameobj(PrefabsList, rotatedir* speed);
            }
            else
            {

                isok = false;
                timer = 0f;
                reset();
             
            }
        }
           
        
    }
    /// <summary>
    /// 所有移动
    /// </summary>
    /// <param name="currentlist"></param>
    /// <param name="angledir"></param>
    void MoveGameobj(List<GameObject> currentlist,float angledir)
    {
         
        if(currentlist==null||currentlist.Count<=0)
        {

            return;
        }
        foreach(GameObject obj in currentlist)
        {

            obj.transform.RotateAround(CenterPoint.position, CenterPoint.up, angledir);
        }
     


    }
   
    void reset( )
    {
        foreach (GameObject obj in PrefabsList)
        {
            for (int i = 0; i < PointList.Count; i++)
            {
                if (Vector3.Distance(obj.transform.position, PointList[i]) <= 0.5f)
                {
                    obj.transform.position = PointList[i];
                    obj.transform.rotation = CenterPoint.rotation;
                    foreach (KeyValuePair<int, Vector3> dir in flagposdir)
                    {
                        if (dir.Value == obj.transform.position)
                        {
                            obj.GetComponent<BattleCatInfo>().currentposid = dir.Key;
                        }
                    }
                }

            }
        }
       

        BattleSceneManage.Instance.CaptainConfig();

        

    }


    /// <summary>
    /// 隐藏不在最前的猫；
    /// </summary>
    /// <returns></returns>
    IEnumerator ForHideCats()
    {
        //等待0.5s之后；
        yield return new WaitForSeconds(0.5f);
        //执行隐藏；
        for (int i = 0; i < PrefabsList.Count; i++)
        {
            if (Vector3.Distance(PrefabsList[i].transform.position, PointList[1]) < 0.5f)
            {
                PrefabsList[i].transform.localScale = Vector3.one;
            }
            else if (Vector3.Distance(PrefabsList[i].transform.position, PointList[0]) < 0.5f)
            {
                PrefabsList[i].transform.localScale = Vector3.one;
            }
            else if (Vector3.Distance(PrefabsList[i].transform.position, PointList[2]) < 0.5f)
            {
                PrefabsList[i].transform.localScale = Vector3.one;
            }
            else
            {
                PrefabsList[i].transform.localScale = Vector3.zero;
            }
        }
        //隐藏敌方不在最前的3只猫。
        for (int i = 0; i < AIPrefabslist.Count; i++)
        {
            if (i >= 3) AIPrefabslist[i].transform.localScale = Vector3.zero;

        }
    }

    public void HideCat()
    {
        Debug.Log("0.5s后开始隐藏猫");
        StartCoroutine(ForHideCats());
    }

    /// <summary>
    /// 展示所有猫
    /// </summary>
    public void ShowAllCats()
    {
        Debug.Log("显示所有的猫！");
        for (int i = 0; i < PrefabsList.Count; i++)
        {
            PrefabsList[i].transform.localScale = Vector3.one;
        };
        //显示所有猫；
        for (int i = 0; i < AIPrefabslist.Count; i++)
        {
            AIPrefabslist[i].transform.localScale = Vector3.one;
        }
    }

    /// <summary>
    /// 展示最前面的那只猫；
    /// </summary>
    public void OnlyShowForawadCat()
    {
        for (int i = 0; i < PrefabsList.Count; i++)
        {
            if (Vector3.Distance( PrefabsList[i].transform.position,PointList[0])<0.5f)
            {
                PrefabsList[i].SetActive(true);
            }
            else
            {
              
                PrefabsList[i].SetActive(false);
            }
        }
    }

    public void changepos(int dir )
    { 
        isok = true;
        rotatedir = dir;

    }
    
    public List<GameObject> getprefabs()
    {
        return PrefabsList;
    }
    public List<GameObject> getaiprefabs()
    {
        return AIPrefabslist;
    }

}
