//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using SUIFW;
//using Global;
//using UnityEngine.UI;
//public class LoginUIForm : BaseUIForm {


//    /// <summary>
//    /// 注册按钮；
//    /// </summary>
//    public Button ButtonRegister;
//    /// <summary>
//    /// 输入框：用户名；
//    /// </summary>
//    public InputField InputFieldUserName;
//    /// <summary>
//    /// 输入框：密码；
//    /// </summary>
//    public InputField InputFieldPassword;


//    /// <summary>
//    /// String密码；
//    /// </summary>
//    string password;

//    /// <summary>
//    /// String用户名；
//    /// </summary>
//    string userName;

 
//    /// <summary>
//    /// 用于登录与注册时临时数据传输（代替Json）
//    /// </summary>
//    private TempLogRegDataVO tempData;

//    private void Awake()
//    {
//        //定义本窗体的性质(默认数值，可以不写)
//        base.CurrentUIType.UIForms_Type = UIFormType.Normal;
//        base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
//        base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;
//        /* 给按钮注册事件 */
//        //RigisterButtonObjectEvent("Btn_OK", LogonSys);
//        //Lamda表达式写法
//        RigisterButtonObjectEvent("LoginBtn",
//            p =>
//            {

//                userName = InputFieldUserName.text;
//                password = InputFieldPassword.text;
//                //如果用户名和密码均不为空；
//                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
//                {
//                    //进行一次登录
//                    StartLogin();
//                }
//                else
//                {
//                    //此时用户名或者密码有其中之一为空；
//                    Debug.Log("请输入用户名或密码");
//                }

//               // OpenUIForm(NotiConst.SELECT_HERO_FORM);
//            }            
//            );

//        RigisterButtonObjectEvent("RegisterBtn",
//         p =>
//         {
//             OpenUIForm(NotiConst.REGISTER_UI_FORM);
//            // NowRigitMehod.OpenRegisterPanel();
//         }



//         );

      
//    }



//    /// <summary>
//    /// 开始登录的方法；
//    /// </summary>
//    void StartLogin()
//    {
//        tempData = new TempLogRegDataVO();
//        tempData.UserName = userName;
//        tempData.Password = password;
//        AppFacade.GetInstance().SendNotification(NotiConst.LOGIN_REQUEST, tempData);
//    }


//    /// <summary>
//    /// 登陆之后返回的方法；
//    /// </summary>
//    /// <param name="vo"></param>
//    public void ReceiveLoginMessage(TempLogRegDataVO vo)
//    {
//        //如果登录成功；
//        if (vo.status == ErrorCode.SUCCESS)
//        {
//            Debug.Log("登录成功");
//            (AppFacade.getInstance.RetrieveMediator(SceneMediator.NAME) as SceneMediator).LoadScene(EnumScene.SceneMain);
//        }
//        //如果登录失败；
//        else
//        {
//            Debug.Log("登录失败");
//        }
//    }

//    /// <summary>
//    /// 接受到注册的回调；
//    /// </summary>
//    /// <param name="vo"></param>
//    public void ReceiveRegisterMessage(TempLogRegDataVO vo)
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


//    //   // Use this for initialization
//    //   void Start () {

//    //}

//    //// Update is called once per frame
//    //void Update () {

//    //}
//}
