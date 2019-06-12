using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatInfoShow_InTeam : CatInfoShow
{

    public Text HintText;

    public Image LeaderShow;

    public override void ShowMethod(CatInfo cat)
    {
        //是队长；
        if (cat.CatCaptainTypeid == 1)
        {
            HintText.transform.localScale = Vector3.zero;
            LeaderShow.transform.localScale = Vector3.one;
        }
        //不是队长..
        else
        {
            HintText.transform.localScale = Vector3.one; 
            LeaderShow.transform.localScale = Vector3.zero;
        }
    }

    protected override void AfterStart()
    {
        
    }
    
}
