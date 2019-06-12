/*****************************************************
/** 类名：NewRegisterView.cs
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

public class NewRegisterView : BaseUIForm
{

    /// <summary>
    /// 用户名；
    /// </summary>
    public InputField InputFieldRigistName;
    /// <summary>
    /// 密码；
    /// </summary>
    public InputField InputFieldTextPassword;
    /// <summary>
    /// 确认密码；
    /// </summary>
    public InputField InputFieldTextConfrimPassword;
    /// <summary>
    /// 注册按钮；
    /// </summary>
    public Button ButtonRigist;
    /// <summary>
    /// 返回按钮；
    /// </summary>
    public Button ButtonBack;

    /// <summary>
    /// 注册用户名；
    /// </summary>
    protected string RigistName;
    /// <summary>
    /// 注册密码；
    /// </summary>
    protected string RigistPassword;


    protected override void Awake()
    {
        base.Awake();
        Init(NewLoginMgr.GetInstance().V_Model);
        BindModel(EM_Login.Register, OnRegister);

        //定义本窗体的性质(默认数值，可以不写)
        base.CurrentUIType.UIForms_Type = UIFormType.PopUp;
        base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
        base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;
        ButtonRigist.onClick.AddListener(delegate ()
        {
            StartRigist();
        });
        ButtonBack.onClick.AddListener(delegate ()
        {
            CloseRegisterPaenl();
        });
        //设置密码为不显示；
        InputFieldTextPassword.contentType = InputField.ContentType.Password;
        InputFieldTextConfrimPassword.contentType = InputField.ContentType.Password;
    }

    void OnRegister(params object[] args)
    {
        bool succ = (bool)args[0];
        string name = (string)args[1];
        string password = (string)args[2];
        //如果注册成功；
        if (succ)
        {
            Debug.Log("注册成功");
            CloseRegisterPaenl();
            NewLoginMgr.GetInstance().V_Model.F_RegisterToLogin(name, password);
        }
        //如果注册失败；
        else
        {
            Debug.Log("注册失败");
        }
    }

    /// <summary>
    /// 开始注册的方法；
    /// </summary>
    public void StartRigist()
    {
        //获取用户名；
        RigistName = InputFieldRigistName.text;
        //获取密码；
        if (InputFieldTextPassword.text != InputFieldTextConfrimPassword.text)
        {
            Debug.Log("两次输入密码不正确~");
            return;
        }
        if (InputFieldTextPassword.text.Length < 6)
        {
            Debug.Log("密码至少6位");
            return;
        }
        //获取密码；
        RigistPassword = InputFieldTextPassword.text;
        //开始注册；
        GoRegister();
    }

    private void OnLevelWasLoaded(int level)
    {
        CloseUIForm();
        Destroy(this.gameObject);
    }

    public void CloseRegisterPaenl()
    {
        // transform.localScale = Vector3.zero;
        CloseUIForm();
    }

    public void GoRegister()
    {
        //如果出现用户名或者密码空的情况；
        if (string.IsNullOrEmpty(RigistName) || string.IsNullOrEmpty(RigistPassword))
        {

        }
        else
        {
            NewLoginMgr.GetInstance().V_Logic.SendRegister(RigistName, RigistPassword);
            return;
        }
    }
}
