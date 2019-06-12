using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
using SUIFW;

public abstract class RigisterMethods : BaseUIForm
{
    /// <summary>
    /// 用于登录与注册时临时数据传输（代替Json）
    /// </summary>
    public TempLogRegDataVO tempData;

    /// <summary>
    /// 注册用户名；
    /// </summary>
    protected string RigistName;
    /// <summary>
    /// 注册密码；
    /// </summary>
    protected string RigistPassword;

    /// <summary>
    /// 开始注册的方法；
    /// </summary>
    public abstract void  OpenRegisterPanel();

    /// <summary>
    /// 关闭注册的方法；
    /// </summary>
    public abstract void CloseRegisterPaenl();

    /// <summary>
    /// 注册之后的回调函数；
    /// </summary>
    public abstract void AfterRegister(TempLogRegDataVO vo);

    /// <summary>
    /// 开始注册的方法；
    /// </summary>
    public void GoRegister()
    {
        //如果出现用户名或者密码空的情况；
        if (string.IsNullOrEmpty(RigistName) || string.IsNullOrEmpty(RigistPassword))
        {

        }
        else
        {
            tempData = new TempLogRegDataVO();
            tempData.UserName = RigistName;
            tempData.Password = RigistPassword;
            AppFacade.GetInstance().SendNotification(NotiConst.REGISTER_REQUEST, tempData);
            return;
        }
    }


}
