/*****************************************************
/** 类名：ExampleMgr.cs
/** 作者：Tearix
/** 日期：2018-03-07
/** 描述：示例Mgr，一般一个系统都有一个单例来访问，并且数据在model变量
*******************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleMgr {

    public ExampleModel V_Model = new ExampleModel();
    public ExampleLogic V_Logic = new ExampleLogic();

    static ExampleMgr m_Instance;
    public static ExampleMgr GetInstance()
    {
        if (m_Instance == null)
            m_Instance = new ExampleMgr();
        return m_Instance;
    }
	
}
