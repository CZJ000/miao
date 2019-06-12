//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Global;
//using SUIFW;
//using UnityEngine.UI;
//public class RegisterUIForm : BaseUIForm {
//    /// <summary>
//    /// 用户名；
//    /// </summary>
//    public InputField InputFieldRigistName;
//    /// <summary>
//    /// 密码；
//    /// </summary>
//    public InputField InputFieldTextPassword;
//    /// <summary>
//    /// 确认密码；
//    /// </summary>
//    public InputField InputFieldTextConfrimPassword;

//    /// <summary>
//    /// 用于登录与注册时临时数据传输（代替Json）
//    /// </summary>
//    public TempLogRegDataVO tempData;

//    /// <summary>
//    /// 注册用户名；
//    /// </summary>
//    private string rigistName;
//    /// <summary>
//    /// 注册密码；
//    /// </summary>
//    private string rigistPassword;


//    private void Awake()
//    {
//        //定义本窗体的性质(默认数值，可以不写)
//        base.CurrentUIType.UIForms_Type = UIFormType.Normal;
//        base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.HideOther;
//        base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;
//    }



//    /// <summary>
//    /// 开始注册的方法；
//    /// </summary>
//    public void StartRigist()
//    {
//        //获取用户名；
//        rigistName = InputFieldRigistName.text;
//        //获取密码；
//        if (InputFieldTextPassword.text != InputFieldTextConfrimPassword.text)
//        {
//            Debug.Log("两次输入密码不正确~");
//            return;
//        }
//        if (InputFieldTextPassword.text.Length < 6)
//        {
//            Debug.Log("密码至少6位");
//            return;
//        }
//        //获取密码；
//        rigistPassword = InputFieldTextPassword.text;
//        //开始注册；
//        GoRegister();
//    }


//    /// <summary>
//    /// 开始注册的方法；
//    /// </summary>
//    public void GoRegister()
//    {
//        //如果出现用户名或者密码空的情况；
//        if (string.IsNullOrEmpty(rigistName) || string.IsNullOrEmpty(rigistPassword))
//        {

//        }
//        else
//        {
//            tempData = new TempLogRegDataVO();
//            tempData.UserName = rigistName;
//            tempData.Password = rigistPassword;
//            AppFacade.GetInstance().SendNotification(NotiConst.REGISTER_REQUEST, tempData);
//            return;
//        }
//    }


//    /// <summary>
//    /// 注册之后的回调方法；
//    /// </summary>
//    /// <param name="vo"></param>
//    public  void AfterRegister(TempLogRegDataVO vo)
//    {
//        //如果注册成功；
//        if (vo.status == ErrorCode.SUCCESS)
//        {
//            Debug.Log("注册成功");

//        }
//        //如果注册失败；
//        else
//        {
//            Debug.Log("注册失败");
//        }
//    }


//    //// Use this for initialization
//    //void Start () {

//    //}

//    //// Update is called once per frame
//    //void Update () {

//    //}
//}
