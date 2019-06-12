using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 拖动策略：移动到某一分组中；
/// </summary>
public class AfterDragStrategy_InGroup : CatGroupAfterDragMethod
{
    /// <summary>
    /// 猫的分组ID；
    /// </summary>
    public int CatGroupID = 1;
    
    protected override void AfterDragToThis()
    {
        CatGroupContoler.Instance.SwitchCatGroupFromGroupToTeam(catInfo, CatGroupDragImageContoler.Instance.StartDragCatInfo,CatGroupID);
    }

    protected override void AfterStart()
    {

    }

}
