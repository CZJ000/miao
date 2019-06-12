using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCamera : MonoBehaviour {

    public  enum Camerastate
    {
        NormalState,
        SpecialView,
        ChooseQueneView,
        BattlingView,
        Convertstate
    }


    public Camerastate mCamerastate = Camerastate.NormalState;

    //贝塞尔曲线算法类
    public Bezier CQBezier;          //选择队伍的曲线
    public Bezier BatBezier;         //战斗的曲线
    public Bezier SpeBezier;         //攻击的特殊曲线
    public Bezier BackBatToCQ;       //返回选择队伍的曲线

    public Vector3 CQStartPos= new Vector3(0f, 0f, 0f);
    public Vector3 CQEndPos= new Vector3(0f, 12f, -19f);
    public Vector3 BatEndPos=new Vector3(8f,5f,-20f);
    public Vector3 SpeEndPos=Vector3.zero ;


    // The distance in the x-z plane to the target
    float distance = 6.0f;

    float height = 8.0f;

    public float stopdistance = 3f;
    public float stopheight = 4f;

    float heightDamping = 4.0f;
    float rotationDamping = 0.0f;

    private bool isneedback = false;
    private Transform target;
    //曲线的对象
   
    private List<Vector3> translist = new List<Vector3>();

    private Timer time;

    //拖动条用来控制贝塞尔曲线的两个点 --调试 
    public float hSliderValue0;
    public float hSliderValue1;

    public static BattleCamera _instance;

    private bool Cameraanimation = false  ;

    public GameObject lookatobj;


    public float speed = 0.1f;

    void Awake()
    {
        _instance = this;

    }
    void Start()
    {

         // -5.0 - 5.0之间的一个数值
        hSliderValue0 = 5f;//GUI.HorizontalSlider(new Rect(25, 25, 100, 30), hSliderValue0, -5.0F, 5.0F);
        hSliderValue1 = 0f;//GUI.HorizontalSlider(new Rect(25, 70, 100, 30), hSliderValue1, -5.0F, 5.0F);
         
        CQBezier = new Bezier(CQStartPos, new Vector3(hSliderValue1, hSliderValue0, 0f), new Vector3(hSliderValue1, hSliderValue0, 0f),CQEndPos );
        BatBezier = new Bezier(CQEndPos, new Vector3(hSliderValue1, hSliderValue0, 0f), new Vector3(hSliderValue1, hSliderValue0, 0f), BatEndPos);
        BackBatToCQ= new Bezier(BatEndPos, new Vector3(hSliderValue1, hSliderValue0, 0f), new Vector3(hSliderValue1, hSliderValue0, 0f), CQEndPos);
    }

     

    void InitCalculate(ref  Bezier currentBezier)
    {
         
        if(translist.Count>0)
        translist.Clear();

        for (float i = 1; i <= 20; i++)
        {
            //参数的取值范围 0 - 1 返回曲线没一点的位置
            //为了精确这里使用i * 0.01 得到当前点的坐标
            if (currentBezier != null)
            {
                
                Vector3 vec = currentBezier.GetPointAtTime((float)(i * 0.05f));
               
                translist.Add(vec);
            }
            
            
           
        }

    }
    void FixedUpdate()
    {
         switch (mCamerastate)
        {
            case Camerastate.NormalState: break;
            case Camerastate.BattlingView:
                mCamerastate = Camerastate.Convertstate;
                if (isneedback)
                {
                    //Debug.Log("切换回最高视角");
                    //InitCalculate(ref BackBatToCQ);
                    transform.position = CQEndPos;
                    transform.LookAt(lookatobj.transform);
                    isneedback = false;
                }
                else
                {
                    //Debug.Log("切换到战斗");
                    //InitCalculate(ref BatBezier);
                    transform.position = BatEndPos;
                    transform.LookAt(lookatobj.transform);
                }

                break;
            case Camerastate.ChooseQueneView :
                mCamerastate = Camerastate.Convertstate;
                

                InitCalculate(ref CQBezier);




                break;
            case Camerastate.SpecialView :
                if (isneedback)
                {
                    mCamerastate = Camerastate.Convertstate;
                    InitCalculate(ref SpeBezier);
                    isneedback = false;
                }
                else
                {
                    Cameraanimation = false;
                    FollowTarget(target);
                }
               
                break;
            case Camerastate.Convertstate:
                mCamerastate = Camerastate.NormalState;
                Cameraanimation = true;
                break;
            default:break;

        }     
        if (Cameraanimation)
        {
            if (translist.Count <= 0)
                return;
                if (isRecheValue(transform.position, translist[0]))
                {

                    translist.Remove(translist[0]);
                    if (translist.Count <= 0)
                    {
                        Cameraanimation = false;
                        return;
                    }
                }else
                {
                    Vector3 temppos = Vector3.Lerp(transform.position, translist[0], Time.deltaTime * speed);
                    transform.position = temppos;
                }
            transform.LookAt(lookatobj.transform);
           

           
        }

    }

    private bool isRecheValue(Vector3 obj,Vector3 target)
    {
         
        float distance = Vector3.Distance(obj, target);
        if (Mathf.Abs( distance) < 0.01f)
        {
           // Debug.Log("scessufor");
            transform.position = translist[0];
             
            return true;

        }

        else
        {
           // Debug.Log("false + ");
            return false;
        }
         
    }

    //选择队伍的视角
    public void ChoosequeneChange( )
    {
        mCamerastate = Camerastate.ChooseQueneView;

    }
    //战斗时的视角
    public void BattleViewChange()
    {
        mCamerastate = Camerastate.BattlingView;
    }

    //返回选择队伍视角
    public void batbackcq()
    {
        isneedback = true;
        BattleViewChange();
    }

    public void currentbackbat()
    {
        SpeBezier = new Bezier(transform.position, new Vector3(hSliderValue1, hSliderValue0 , 0f), new Vector3(hSliderValue1, hSliderValue0, 0f), BatEndPos);
        isneedback = true;
        
    }

    //特殊视角
    public void SpecialViewChange(Transform trans)
    {
        mCamerastate = Camerastate.SpecialView;
        target = trans;
      
    }

    private   void  FollowTarget(Transform  target)
    {
        if (height>stopheight)
        {
            height -= Time.deltaTime*heightDamping;
        }
        if(distance>stopdistance)
        {
            distance -= Time.deltaTime;
        }
       
       
        // Calculate the current rotation angles
        var wantedRotationAngle = target.eulerAngles.y;
        var wantedHeight = target.position.y + height;
        var currentRotationAngle = transform.eulerAngles.y;
        var currentHeight = transform.position.y;
        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;
        // Set the height of the camera
        //transform.position.y = currentHeight;       
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
        // Always look at the target
        transform.LookAt(target);
        

    }
} 
 
