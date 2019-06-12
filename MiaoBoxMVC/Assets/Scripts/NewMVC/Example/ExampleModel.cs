/*****************************************************
/** 类名：ExampleModel.cs
/** 作者：Tearix
/** 日期：2018-03-07
/** 描述：示范Model
*******************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MB.MVC;

// model层存数据并抛出对应事件
public class ExampleModel : NewModel {

    int m_ExampleData;
    object m_ExampleData1;

	public int V_ExampleData
    {
        get { return m_ExampleData; }
        set {
            m_ExampleData = value;
            Refresh(ExampleDefine.ExampleDataUpdate);
        }
    }

    public void F_RefreshExampleData1(object obj)
    {
        m_ExampleData1 = obj;
        Refresh(ExampleDefine.ExampleDataUpdate, m_ExampleData1);
    }
}
