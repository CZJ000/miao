using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatInfoShow_InGroup :CatInfoShow
{ 

    public Image LeaderShow;

    public override void ShowMethod(CatInfo cat)
    {
        //是队长； 
        if (cat.CatCaptainTypeid == 1)
        {
            LeaderShow.transform.localScale = Vector3.one;
        }
        //不是队长
        else
        {
            LeaderShow.transform.localScale = Vector3.zero;
        }
    }


    protected override void AfterStart()
    {

    }
}
