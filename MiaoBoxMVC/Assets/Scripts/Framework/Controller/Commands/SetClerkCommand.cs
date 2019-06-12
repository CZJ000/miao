using PureMVC.Patterns;
using PureMVC.Interfaces;

public class SetClerkCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        ClerkAreaMediator clerkMediator = AppFacade.getInstance.RetrieveMediator(ClerkAreaMediator.NAME) as ClerkAreaMediator;
        ClerkAreaView clerkView = clerkMediator.ViewComponent as ClerkAreaView;
        CatGroupProxy proxy = Facade.RetrieveProxy(CatGroupProxy.NAME) as CatGroupProxy;
        clerkView.SetClerks(proxy.GetClerkIDs());
    }
}
