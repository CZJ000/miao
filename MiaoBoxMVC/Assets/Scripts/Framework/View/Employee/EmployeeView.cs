using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using Global;
using LitJson;
using SUIFW;
public class EmployeeView : BaseUIForm{

   // public GameObject employPanel;
    public Button yesBtn;
    public Button cancelBtn;
    public Transform catPosition;
   
    public Text eName;
    public Text eLevel;
    public Text eAtk;

    public Text eSkill;
    public Text eHireprice; 

    //public GameObject[] bornPoints;
    //public GameObject[] disappearPoints;

    public EmployeeInfo currentClickCatInfo;

    public bool employeesussce = false;

    ////雇员刷新时间数据 暂时不写到Proxy中
    //private float refreshTime = 0f;
    //private float currentTime = 0f;
    //private float lastTime = 0f;





    /// <summary>
    /// 是否激活；
    /// </summary>
    public bool IsInvoke
    {
        get
        {

            //  Camera.main.GetComponent<UICamera>().eventType = UICamera.EventType.UI_2D;
            // return transform.localScale != Vector3.zero;
            return gameObject.activeInHierarchy;
            // return gameObject.activeInHierarchy;
        }
        set
        {
            if (value == false)
            {
                CloseUIForm();
            }
            else
            {
              
                OpenUIForm("EmployeeView");
            }
          
            
            //transform.localScale = value ? Vector3.one : Vector3.zero;
            //if (transform.localScale == Vector3.one)
            //{
            //    MainMenuView.Instance.blockPanel.SetActive(true);
            //}
            //else
            //{
            //    MainMenuView.Instance.blockPanel.SetActive(false);
            //}

        }
    }




    protected override void Awake()
    {
        base.Awake();
        //定义本窗体的性质(默认数值，可以不写)
        base.CurrentUIType.UIForms_Type = UIFormType.PopUp;
        base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
        base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;

    }




    // Use this for initialization
    void Start () {


        //refreshTime = Random.Range(10f, 15f);

        //currentTime = Time.realtimeSinceStartup;
        //lastTime = Time.realtimeSinceStartup;


        yesBtn.onClick.AddListener(OnBuyBtnOn);
        cancelBtn.onClick.AddListener(OnCancelBtnOn);
    }

    ////10~15秒 生出来一只小雇员
    //void FixedUpdate()
    //{
    //    currentTime = Time.realtimeSinceStartup;
    //    if (currentTime - lastTime > refreshTime)
    //    {
    //        SendAddEmployeeCommand();

    //        refreshTime = Random.Range(10f, 15f);
    //        lastTime = Time.realtimeSinceStartup;
    //    }
    //}

  

    //public void GenerateEmployee(EmployeeInfoVO employeeVO)
    //{
    //    //设置出生点和消失点
    //    int bornPointIndex = Random.Range(0, 4);
    //    Vector3 bornPoint = bornPoints[bornPointIndex].transform.position;
    //    Vector3 disappearPoint = disappearPoints[bornPointIndex].transform.position;

    //    GameObject employee = CatPool.GetInstance().GetCatPool(employeeVO.Id).CreateObject(bornPoint);

    //    EmployeeCtl employeeCtl = employee.AddComponent<EmployeeCtl>();
    //    employeeCtl.bornPosition = bornPoint;
    //    employeeCtl.disappearPosition = disappearPoint;
    //    employee.GetComponent<CharacterController>().enabled = true;

    //         EmployeeInfo employeeInfo = employee.AddComponent<EmployeeInfo>();
    //    employeeInfo.Id = employeeVO.Id;
    //    employeeInfo.Name = employeeVO.Name;
    //    employeeInfo.Level = employeeVO.Level;
    //    employeeInfo.Evo = employeeVO.Evo;
    //    employeeInfo.Iq = employeeVO.Iq;
    //    employeeInfo.Power = employeeVO.Power;
    //    employeeInfo.React = employeeVO.React;
    //    employeeInfo.Skill = employeeVO.Skill;
    //    employeeInfo.About = employeeVO.About;
    //    employeeInfo.Hireprice = employeeVO.HirePrice;
    //}

    public void   EmploySuccess()
    {
        Debug.Log("雇佣成功!");
        string s = "雇佣成功!";
        MessageView.GetInstance().ShowMessage("雇佣成功!");


        employeesussce = true;
        GameObject employeeCat = catPosition.transform.GetChild(0).gameObject;
        employeeCat.SetActive(false);
        IsInvoke = false;
       
    }

    public void  EmployFailure(object data)
    {

        FailType fialtype = ( FailType)data;
        if (fialtype ==  FailType.Money)
        {
            Debug.Log("金钱不够!雇佣失败!");
            string s = "金钱不够!雇佣失败!";
            //NotifactionFunc._instance.showNotifaction(s);
            MessageView.GetInstance().ShowMessage(s);

        } else if (fialtype == FailType.LimitNum)
        {
            string s = "储备人员已满，请处理储备人员的数量。";
            Debug.Log("储备人员已满，请处理储备人员的数量。");
            //   NotifactionFunc._instance.showNotifaction(s);
            MessageView.GetInstance().ShowMessage("储备人员已满，请处理储备人员的数量。");
        }
       


        GameObject employeeCat = catPosition.transform.GetChild(0).gameObject;
        employeeCat.SetActive(false);
        IsInvoke = false;
        
    }

    //private void SendAddEmployeeCommand()
    //{
    //    AppFacade.getInstance.SendNotification(NotiConst.ADD_EMPLOYEE_MODEL);
    //}

 


    #region 事件回调

    public void OnBuyBtnOn()
    {
        GameObject employeeCat = catPosition.transform.GetChild(0).gameObject;
        Destroy(employeeCat.GetComponent<EmployeeRandomAnimation>());
        int layer = LayerMask.NameToLayer(NotiConst.Layer_Cats);
        employeeCat.layer = layer;
       // GameObjectUtils.SetLayerRecursively(employeeCat, layer);
        employeeCat.transform.SetChildLayer(layer);

        //发送消息 存入info_cats数据库(如果不够钱直接返回付不起)
        AppFacade.Instance.SendNotification(NotiConst.EMPLOY_EMPLOYEE, currentClickCatInfo.Id);

      int  groupPageId = 5;
      
        JsonData groupId = new JsonData();
        groupId["id"] = groupPageId;
        AppFacade.Instance.SendNotification(NotiConst.GET_CAT_GROUP_INFO, groupId);
    }

    public void OnCancelBtnOn()
    {
        GameObject employeeCat = catPosition.transform.GetChild(0).gameObject;
        Destroy(employeeCat.GetComponent<EmployeeRandomAnimation>());
        int layer = LayerMask.NameToLayer(NotiConst.Layer_Cats);
        employeeCat.layer = layer;
        employeeCat.transform.SetChildLayer(layer);
        //GameObjectUtils.SetLayerRecursively(employeeCat, layer);
        employeeCat.SetActive(false);
        IsInvoke = false;
    }



    #endregion

}
