using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
using UnityEngine.EventSystems;
using SUIFW;
using PureMVC;
public class ClerkInfo
{
    public int id;
    public int catTypeId;
}

public class ClerkCtl : MonoBehaviour,IPointerClickHandler {

    public enum EInnerState
    {
        FALL_OFF,           //掉落
        MOVE_AROUND,        //游走
    }

    public GameObject hudtextPrefab;
    public ClerkInfo clerkInfo = new ClerkInfo();   

    private Timer mMove_timer;      //定时器 
    private CharacterController mCc;
    private Animation mAni;
    private EInnerState mInnerState;            //当前状态
    private Vector3 mLastPos;       //用于判断撞墙，也就是如果一直移动不到目标点 那就是撞墙了
    private Vector3 mDeltaDesPos;   //方向
    private Vector3 mDesPos;        //目标点
    private float mFallspeed = 0;
    private const float mMoveLength = 1f;   //步长
    private const float mMoveSpeed = 1.2f;
    private const float g = 0.2f;
    private bool mIdleState = false;

    private GameObject hudTextPosiTarget;

    void Start()
    {
        hudTextPosiTarget = (GameObject)Instantiate(new GameObject(),transform.position+new Vector3(0,0.5f,0),Quaternion.identity);
        hudTextPosiTarget.transform.parent = transform;
       mCc = this.GetComponent<CharacterController>();
        mAni = this.GetComponent<Animation>();
       
        
        
       
       
        
    }

    

    void OnEnable()
    {
        Init();
    }

    void OnDisable()
    {
        Destroy(this);
        

    }
    public void Init()
    {

       
         mDeltaDesPos = Vector3.zero;
        mDesPos = Vector3.zero;
        mLastPos = transform.position;
        mInnerState = EInnerState.FALL_OFF;
        mFallspeed = 0;
    }

    void Gravity()
    {
        mFallspeed -= Time.deltaTime / 10;
        mCc.Move(new Vector3(0, mFallspeed - g * Time.deltaTime * Time.deltaTime, 0));
    }

    void Update()
    {
        switch (mInnerState)
        {
            case EInnerState.FALL_OFF:
                {
                    if (transform.position.y > -0.1f)
                    {
                        Gravity();
                    }
                    else {
                        mInnerState = EInnerState.MOVE_AROUND;
                        mMove_timer = new Timer(3f, true);
                    }
                    break;
                }
            case EInnerState.MOVE_AROUND:
                {
                    if (transform.position.y > -0.1f) //地面高度约为-0.1f
                    {
                        mInnerState = EInnerState.FALL_OFF;
                        return;
                    }
                    if (mMove_timer.Ready())
                    {
                        mIdleState = false;
                        mDeltaDesPos = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
                        mDesPos = transform.position + mDeltaDesPos * mMoveLength;
                        mMove_timer.Do();
                        //mAni.CrossFade("run", 0.1f);
                    }
                    Move();
                    break;
                }
        }
    }

    public void Move()
    {
        bool isNearDes = (transform.position - mDesPos).magnitude <= Time.deltaTime * mMoveSpeed;
        if (!isNearDes)
        {
            mCc.Move(mDeltaDesPos * Time.deltaTime * mMoveSpeed);
            Vector3 actualVelocity = mCc.velocity;
            actualVelocity.y = 0;
            if (actualVelocity != Vector3.zero)
            {

                Vector3 lookAtDir = Vector3.RotateTowards(transform.forward, actualVelocity, 5f * Time.deltaTime, 10000); //慢慢转身 防止抖动
                transform.LookAt(transform.position + lookAtDir);
            }
        }
        if ((isNearDes) || transform.position == mLastPos)
        {
            if (mIdleState == false)
            {
                mIdleState = true;

                int randomAnim = Random.Range(0, 7);
                switch (randomAnim)
                {
                    case 0:
                        mAni.CrossFade("idle", 0.1f);
                        break;
                    case 1:
                        mAni.CrossFade("lucky", 0.1f);
                        break;
                    case 2:
                        mAni.CrossFade("dig", 0.1f);
                        break;
                    case 3:
                        mAni.CrossFade("dance", 0.1f);
                        break;
                    case 4:
                        mAni.CrossFade("licking", 0.1f);
                        break;
                    case 5:
                        mAni.CrossFade("catch", 0.1f);
                        break;
                    case 6:
                        mAni.CrossFade("clap", 0.1f);
                        break;
                }
            }
        }
        else {
            mIdleState = false;
            mAni.CrossFade("walk", 0.1f);
        }
        mLastPos = transform.position;
    }

    //void ClickCat(GameObject go)
    //{
    //    JsonData child = new JsonData();
    //    child["id"] = clerkInfo.id;
    //    child["catTypeId"] = clerkInfo.catTypeId;
    //    AppFacade.Instance.SendNotification(NotiConst.LEVEL_UP_CLERK, child);
    //}

    public void ShowString(string content)
    {

      
        GameObject gameObject=  HudTextPool.GetInstance().GetHudTextPool().CreateObject(hudTextPosiTarget.transform.position,true);

        gameObject.GetComponent<HudTextAction>().OnBorn(content, hudTextPosiTarget);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        
        JsonData child = new JsonData();
        child["id"] = clerkInfo.id;
        child["catTypeId"] = clerkInfo.catTypeId;
        AppFacade.Instance.SendNotification(NotiConst.LEVEL_UP_CLERK, child);
    }
}
