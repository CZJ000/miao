using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;

public class HandleBattleInfo : MonoBehaviour {

    public int EnemyCaptainCount { get; set; }
    public int PlayerCaptainCount { get; set; }

    private void   CalculatCaptainCount(int CaptainCount ,RoleType roletype)
    {
        if (roletype== RoleType.Enemy)
        {
            EnemyCaptainCount++;

        }else if (roletype == RoleType.Player)
        {
            PlayerCaptainCount++;
        }
    }

}
