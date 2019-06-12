using PureMVC.Patterns;
using PureMVC.Interfaces;
using UnityEngine;
using Global;
using SUIFW;
public class InitClerkCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {

     
        UIBaseBehaviour<ClerkAreaMediator>.CreateUI<ClerkAreaView>();
      
    }
}
