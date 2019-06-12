using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureMVC.Interfaces;
using PureMVC.Patterns;

public  class ChangeBattleAIUserCommand:SimpleCommand
{
    public override void Execute(INotification notification)
    {
        UserAiInfoProxy userAiInfoProxy=AppFacade.getInstance.RetrieveProxy(UserAiInfoProxy.NAME) as UserAiInfoProxy;
        userAiInfoProxy.GetRandomAIUserFromServer();
    }
}

