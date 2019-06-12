using LitJson;
using PureMVC.Interfaces;
using PureMVC.Patterns;

class LevelUpClerk : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        CatGroupProxy proxy = Facade.RetrieveProxy(CatGroupProxy.NAME) as CatGroupProxy;
        proxy.LevelUpClerk((JsonData)notification.Body);
    }
}