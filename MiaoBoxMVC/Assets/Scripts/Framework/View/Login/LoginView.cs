using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global;
using SUIFW;
/// <summary>
/// 登录面板控制器；
/// </summary>
public class LoginView : BaseUIForm
{

    #region 属性

    /// <summary>
    /// 静态单例；
    /// </summary>
    public static LoginView Instance;
    /// <summary>
    /// 是否获得过所有的组件；
    /// </summary>
    private bool IsGetAllCompoenets = false;
    /// <summary>
    /// 用户名；
    /// </summary>
    private string _UserName = "";
    /// <summary>
    /// 密码；
    /// </summary>
    private string _Password = "";
    /// <summary>
    /// 用于登录与注册时临时数据传输（代替Json）
    /// </summary>
    private TempLogRegDataVO _tempData;

    #endregion


    #region 组件：UGUI；

    /// <summary>
    /// 登录的主界面；
    /// </summary>
    public Transform PanelLogin;
    /// <summary>
    /// 登录按钮；
    /// </summary>
    public Button ButtonLogin;
    /// <summary>
    /// 注册按钮；
    /// </summary>
    public Button ButtonRegister;
    /// <summary>
    /// 输入框：用户名；
    /// </summary>
    public InputField InputFieldUserName;
    /// <summary>
    /// 输入框：密码；
    /// </summary>
    public InputField InputFieldPassword;
    /// <summary>
    /// 注册的方法；
    /// </summary>
    public RigisterMethods NowRigitMehod;
    
    #endregion

    #region 封装
    /// <summary>
    /// 封装：用户名；
    /// </summary>
    public string UserName
    {
        get
        {
            return _UserName;
        }

        set
        {
            _UserName = value;
        }
    }
    /// <summary>
    /// 封装：密码；
    /// </summary>
    public string Password
    {
        get
        {
            return _Password;
        }

        set
        {
            _Password = value;
        }
    }
    /// <summary>
    /// 用于登录与注册时临时数据传输（代替Json）
    /// </summary>
    public TempLogRegDataVO TempData
    {
        get
        {
            return _tempData;
        }

        set
        {
            _tempData = value;
        }
    }
    
    #endregion

    private void Awake()
    {
        Instance = this;
        //定义本窗体的性质(默认数值，可以不写)
        base.CurrentUIType.UIForms_Type = UIFormType.Normal;
        base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
        base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;
    }

    // Use this for initialization
    void Start()
    {
        //获取所有组件的方法；
        GetAllCompoenets();
        //尝试一次自动登录；
        TryLoginMethod();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 尝试登录的方法，也就是在运行时自动登录；
    /// </summary>
    public void TryLoginMethod()
    {
        //首先获取用户名和密码；
        UserName = LocalSaveData.LoginUserName;
        Password = LocalSaveData.LoginPassword;
        //如果用户名和密码均不为空；
        if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
        {
            //将用户名密码填到输入框中；
            InputFieldUserName.text = UserName;
            InputFieldPassword.text = Password;
        }
    }


    /// <summary>
    /// 开始登录的方法；
    /// </summary>
    void StartLogin()
    {
        TempData = new TempLogRegDataVO();
        TempData.UserName = UserName;
        TempData.Password = Password;
        AppFacade.GetInstance().SendNotification(NotiConst.LOGIN_REQUEST, TempData);
    }

    /// <summary>
    /// 登陆之后返回的方法；
    /// </summary>
    /// <param name="vo"></param>
    public void ReceiveLoginMessage(TempLogRegDataVO vo)
    {
        //如果登录成功；
        if (vo.status == ErrorCode.SUCCESS)
        {
            Debug.Log("登录成功");
            (AppFacade.getInstance.RetrieveMediator(SceneMediator.NAME) as SceneMediator).LoadScene( EnumScene.SceneMain);
        }
        //如果登录失败；
        else
        {
            Debug.Log("登录失败");
        }
    }

    /// <summary>
    /// 接受到注册的回调；
    /// </summary>
    /// <param name="vo"></param>
    public void ReceiveRegisterMessage(TempLogRegDataVO vo)
    {
        NowRigitMehod.AfterRegister(vo);
    }



    private void OnLevelWasLoaded(int level)
    {
        CloseUIForm();
        Destroy(this.gameObject);
    }


    /// <summary>
    /// 获得所有组件的方法；
    /// </summary>
    public void GetAllCompoenets()
    {
        //此方法只执行一次；
        if (IsGetAllCompoenets) return;
        //设置密码舒服框为非明文；
        InputFieldPassword.contentType = InputField.ContentType.Password;
        //登录按钮的点击事件；
        ButtonLogin.onClick.AddListener(delegate ()
        {
        UserName = InputFieldUserName.text;
        Password = InputFieldPassword.text;
        //如果用户名和密码均不为空；
        if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
        {
            //进行一次登录
            StartLogin();
        }
        else
        {
                //此时用户名或者密码有其中之一为空；
                Debug.Log("请输入用户名或密码");
            }
        });
        //设置注册按钮的回调事件：
        ButtonRegister.onClick.AddListener(delegate()
        {
            NowRigitMehod.OpenRegisterPanel();
        });
        IsGetAllCompoenets = true;
    }


}
