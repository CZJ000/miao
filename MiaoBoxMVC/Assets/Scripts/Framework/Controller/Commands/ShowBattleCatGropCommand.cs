using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using UnityEngine;
using LitJson;
using Global;


class ShowBattleCatGropCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        CatGroupProxy proxy = Facade.RetrieveProxy(CatGroupProxy.NAME) as CatGroupProxy;
        switch (notification.Name)
        {
            case NotiConst.GET_BATTLE_CAT_GROUP_INFO:
               
                proxy.SendBattleGroupInfo(notification.Body);

                break;
            case NotiConst.GET_BATTLE_RANDOM_CAT_INFO:
                proxy.SendBattleRandomGroupInfo(notification.Body);
                break;
        }
        

    }
    }
 
