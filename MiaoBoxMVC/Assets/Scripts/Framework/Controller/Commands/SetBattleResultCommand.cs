using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using UnityEngine;
using LitJson;
using Global;

public class SetBattleResultCommand : SimpleCommand
{

    public override void Execute(INotification notification)
    {
        UserInfoProxy proxy = Facade.RetrieveProxy(UserInfoProxy.NAME) as UserInfoProxy;
       
        switch (notification.Name)
        {
            case NotiConst.SET_BATTLE_RESULT_EXP: 
               
                BattleResultVO BRVO = notification.Body as BattleResultVO;
                if (BRVO.type==RoleType.Player)
                {
                    int currentExp = proxy.getEXP();
                    int endExp = currentExp + BRVO.Exp;
                    proxy.setExpNotRefrsh(endExp);
                    AppFacade.getInstance.SendNotification(BattleInfoMediator.UPDATEBATTLERESULT, BRVO);                        //视图层暂时写此处
                    //todo 减少敌方经验
                }
                else if (BRVO.type == RoleType.Enemy)
                {
                    int currentExp = proxy.getEXP();
                    int endExp = currentExp - BRVO.Exp;               //减少经验
                    if (endExp <= 0)
                    {
                        endExp = 0;
                    }
                    proxy.setExpNotRefrsh(endExp);
                    AppFacade.getInstance.SendNotification(BattleInfoMediator.UPDATEBATTLERESULT, BRVO);                        //视图层暂时写此处
                    //todo 增加敌方经验
                }
                
               
                break;
        }


    }
}
