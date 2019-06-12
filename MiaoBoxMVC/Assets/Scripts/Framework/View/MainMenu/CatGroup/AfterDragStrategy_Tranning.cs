using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 拖动策略，移动到训练中心
/// </summary>
public class AfterDragStrategy_Tranning : CatGroupAfterDragMethod
{
    /// <summary>
    /// 拖动到此处的回调方法；
    /// </summary>
    protected override void AfterDragToThis()
    {
        CatInfo StartCatInfo = CatGroupDragImageContoler.Instance.StartDragCatInfo;
        CatGroupContoler.Instance.SwitchCatGroupFromCampToGroup(catInfo, StartCatInfo);
    }

    protected override void AfterStart()
    {

    }

}
