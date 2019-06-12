using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterDragStrategy_BottomBackGround : CatGroupAfterDragMethod
{


    protected override void AfterDragToThis()
    {
        CatInfo StartCatInfo = CatGroupDragImageContoler.Instance.StartDragCatInfo;
        CatGroupContoler.Instance.MoveCatsToBottomGroup(new CatInfo[] { StartCatInfo });
        //进行一次刷新；
        CatGroupContoler.Instance.RefreshCatGroup(5);
        CatGroupContoler.Instance.RefreshCatGroup(StartCatInfo.GroupId);
    }

    protected override void AfterStart()
    {
        
    }
}
