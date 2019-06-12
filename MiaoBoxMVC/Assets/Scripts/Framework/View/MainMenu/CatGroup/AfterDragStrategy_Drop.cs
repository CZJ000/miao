using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 拖动策略，移动到丢弃位置
/// </summary>
public class AfterDragStrategy_Drop : CatGroupAfterDragMethod
{

    protected override void AfterDragToThis()
    {
        //执行删除一只猫的方法；
        CatGroupContoler.Instance.DeleteOneCat(CatGroupDragImageContoler.Instance.StartDragCatInfo);
    }

    protected override void AfterStart()
    {

    }

}
