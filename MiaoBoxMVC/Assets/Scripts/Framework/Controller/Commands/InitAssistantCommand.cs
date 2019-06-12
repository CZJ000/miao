using PureMVC.Patterns;
using PureMVC.Interfaces;
using UnityEngine;
using Global;

public class InitAssistantCommand : SimpleCommand {
    public override void Execute(INotification notification)
    {
        GameObject assistantViewObj = GameObject.FindGameObjectWithTag(TagName.ASSISTANT_VIEW);

        AssistantMediator assistantMediator = new AssistantMediator();
        AssistantView assistantView = assistantViewObj.GetComponent<AssistantView>();
        assistantMediator.ViewComponent = assistantView;
        Facade.RegisterMediator(assistantMediator);
        assistantView.enabled = true;
    }
}
