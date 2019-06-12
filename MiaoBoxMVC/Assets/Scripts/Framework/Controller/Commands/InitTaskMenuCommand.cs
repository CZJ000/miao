using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureMVC.Patterns;
using PureMVC.Interfaces;
public  class InitTaskMenuCommand:SimpleCommand
{
    public override void Execute(INotification notification)
    {
        UIBaseBehaviour<TaskMediator>.CreateUI<TaskView>();
    }
}

