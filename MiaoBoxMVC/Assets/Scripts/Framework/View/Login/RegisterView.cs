using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global;

public class RegisterView : RigisterMethods
{
    private bool IsGetAllCompoenets = false;

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



    private void Awake()
    {
        //定义本窗体的性质(默认数值，可以不写)
        base.CurrentUIType.UIForms_Type = UIFormType.PopUp;
        base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
        base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;
    }



    private void OnLevelWasLoaded(int level)
    {
        CloseUIForm();
        Destroy(this.gameObject);
    }

    private void Start()
    {
        //设置点击事件；
        GetAllCopoenets();
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// 获得所有组件的方法；
    /// </summary>
    public void GetAllCopoenets()
    {
        if (IsGetAllCompoenets) return;
        ButtonRigist.onClick.AddListener(delegate()
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
        IsGetAllCompoenets = true;
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


    #region 复写方法

    public override void CloseRegisterPaenl()
    {
        // transform.localScale = Vector3.zero;
        CloseUIForm();
    }

    public override void OpenRegisterPanel()
    {
        // transform.localScale = Vector3.one;
        OpenUIForm(NotiConst.REGISTER_VIEW);
    }

    /// <summary>
    /// 注册之后的回调方法；
    /// </summary>
    /// <param name="vo"></param>
    public override void AfterRegister(TempLogRegDataVO vo)
    {
        //如果注册成功；
        if (vo.status == ErrorCode.SUCCESS)
        {
            Debug.Log("注册成功");

        }
        //如果注册失败；
        else
        {
            Debug.Log("注册失败");
        }
    }

    #endregion
}
