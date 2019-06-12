/*****************************************************
/** 类名：ExampleView.cs
/** 作者：Tearix
/** 日期：2018-03-07
/** 描述：示范View
*******************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MB.MVC;

// NewView使用NewModel的数据刷新界面，并监听NewModel的对应数据
// 一般在Awake时监听Model的数据，Destroy时取消监听（NewView层做了取消，子类无需理会）
public class ExampleView : NewView
{

    protected override void Awake()
    {
        base.Awake();
        Init(ExampleMgr.GetInstance().V_Model);
        BindModel(ExampleDefine.ExampleDataUpdate, OnExampleDataUpdate);
        BindModel(ExampleDefine.ExampleData1Update, OnExampleData1Update);
    }

    void OnExampleDataUpdate(params object[] args)
    {
        var data = ExampleMgr.GetInstance().V_Model.V_ExampleData;
        //使用data刷新界面
    }

    void OnExampleData1Update(params object[] args)
    {
        object data = args[0];
        //使用data刷新界面
    }
}
