using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 拖动策略：移动到底部
/// </summary>
public class AfterDragStrategy_Bottom : CatGroupAfterDragMethod
{

    protected override void AfterDragToThis()
    {
        CatInfo StartCatInfo = CatGroupDragImageContoler.Instance.StartDragCatInfo;
        if (StartCatInfo.GroupId < 4)
        {
            //此时是从1~3个猫队伍里分出来的猫分组；
            CatGroupContoler.Instance.SwitchCatGroupFromGroupToTeam(StartCatInfo, catInfo, StartCatInfo.GroupId);
        }
        else
        {
            //此时是从训练营里分出来的猫分组；
            CatGroupContoler.Instance.SwitchCatGroupFromCampToGroup(StartCatInfo, catInfo);
        }
    }

    protected override void AfterStart()
    {
       
    }

}
