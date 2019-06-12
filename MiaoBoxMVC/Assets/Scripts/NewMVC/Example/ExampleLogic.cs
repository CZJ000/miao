/*****************************************************
/** 类名：ExampleLogic.cs
/** 作者：Tearix
/** 日期：2018-03-07
/** 描述：示范Logic
*******************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Logic类一般处理逻辑，和更新NewModel的数据
public class ExampleLogic {

	public void ReceiveMsgFromNet(object obj)
    {
        ExampleMgr.GetInstance().V_Model.F_RefreshExampleData1(obj);
    }
}
