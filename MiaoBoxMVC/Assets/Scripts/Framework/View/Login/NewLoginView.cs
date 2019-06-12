/*****************************************************
/** 类名：NewLoginView.cs
/** 作者：Tearix
/** 日期：2018-03-07
/** 描述：
*******************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MB.MVC;
using UnityEngine.UI;
using Global;
using SUIFW;

public class NewLoginView : BaseUIForm {

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

    #endregion

    protected override void Awake()
    {
        base.Awake();
        Init(NewLoginMgr.GetInstance().V_Model);
        BindModel(EM_Login.Login, OnLogin);
        BindModel(EM_Login.RegisterToLogin, OnRegisterToLogin);

        // 初始化UI，绑定UI点击事件等
        //定义本窗体的性质(默认数值，可以不写)
        base.CurrentUIType.UIForms_Type = UIFormType.Normal;
        base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
        base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;
        //设置密码舒服框为非明文；
        InputFieldPassword.contentType = InputField.ContentType.Password;
        //登录按钮的点击事件；
        ButtonLogin.onClick.AddListener(StartLogin);
        //设置注册按钮的回调事件：
        ButtonRegister.onClick.AddListener(StartRegister);
    }

    void OnRegisterToLogin(params object[] args)
    {
        string name = (string)args[0];
        string password = (string)args[1];
        InputFieldUserName.text = name;
        InputFieldPassword.text = password;
    }

    void OnLogin(params object[] args)
    {
        bool succ = (bool)args[0];
        //如果登录成功；
        if (succ)
        {
            Debug.Log("登录成功");
            (AppFacade.getInstance.RetrieveMediator(SceneMediator.NAME) as SceneMediator).LoadScene(EnumScene.SceneMain);
        }
        //如果登录失败；
        else
        {
            Debug.Log("登录失败");
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        CloseUIForm();
        Destroy(this.gameObject);
    }

    /// <summary>
    /// 开始登录的方法；
    /// </summary>
    void StartLogin()
    {
        string name = InputFieldUserName.text;
        string password = InputFieldPassword.text;
        if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(password))
        {
            NewLoginMgr.GetInstance().V_Logic.F_Login(name, password);
        }
        else
        {
            Debug.Log("请输入用户名或密码");
        }
    }

    void StartRegister()
    {
        OpenUIForm(NotiConst.REGISTER_VIEW);
    }
}
