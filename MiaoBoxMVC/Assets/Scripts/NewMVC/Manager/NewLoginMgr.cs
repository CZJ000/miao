/*****************************************************
/** 类名：NewLoginMgr.cs
/** 作者：Tearix
/** 日期：2018-03-07
/** 描述：
*******************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MB.MVC;
public class NewLoginMgr {
    public NewLoginModel V_Model = new NewLoginModel();
    public NewLoginLogic V_Logic = new NewLoginLogic();

    static NewLoginMgr m_Instance;
    public static NewLoginMgr GetInstance()
    {
        if (m_Instance == null)
            m_Instance = new NewLoginMgr();
        return m_Instance;
    }
}
