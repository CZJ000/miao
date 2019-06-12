using PureMVC.Patterns;
using PureMVC.Interfaces;

public class SetAssistantCommand : SimpleCommand {
    public override void Execute(INotification notification)
    {
        AssistantMediator assistantMediator = AppFacade.getInstance.RetrieveMediator(AssistantMediator.NAME) as AssistantMediator;
        AssistantView assistantView = assistantMediator.ViewComponent as AssistantView;
        CatGroupProxy proxy = Facade.RetrieveProxy(CatGroupProxy.NAME) as CatGroupProxy;
        int assistantID = proxy.GetAssistantID();
        assistantView.SetAssistant(assistantID);
    }
}
