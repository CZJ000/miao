/*****************************************************
/** 类名：NewLoginModel.cs
/** 作者：Tearix
/** 日期：2018-03-07
/** 描述：
*******************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MB.MVC;

public class NewLoginModel : NewModel {

    string m_LoginName;
    string m_LoginPassW;

    public string V_LoginName
    {
        get { return m_LoginName; }
        set { m_LoginName = value; }
    }

    public string V_LoginPassW
    {
        get { return m_LoginPassW; }
        set { m_LoginPassW = value; }
    }

    public void F_ReturnLoginRet(bool succ)
    {
        Refresh(EM_Login.Login, succ);
    }

    public void F_ReturnRegisterRet(bool succ, string name, string password)
    {
        Refresh(EM_Login.Register, succ, name, password);
    }

    public void F_RegisterToLogin(string name, string password)
    {
        Refresh(EM_Login.RegisterToLogin, name, password);
    }
}
