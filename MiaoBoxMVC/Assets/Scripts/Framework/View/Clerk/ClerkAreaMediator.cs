using PureMVC.Patterns;
using PureMVC.Interfaces;
using System.Collections.Generic;
using LitJson;

public class ClerkAreaMediator : Mediator {
    public new const string NAME = "ClerkAreaMediator";

    public const string LEVLE_UP = "LEVLE_UP";
    public const string CHANGED_CAT = "CHANGED_CAT";
    public const string SHOW_LEVEL_UP_CAT_INFO = "SHOW_LEVEL_UP_CAT_INFO";

    public ClerkAreaMediator() : base(NAME) {
    }

    public ClerkAreaView clerkView
    {
        get
        {
            return ViewComponent as ClerkAreaView;
        }
    }

    public override IEnumerable<string> ListNotificationInterests
    {
        get
        {
            List<string> list = new List<string>();
            list.Add(ClerkAreaMediator.LEVLE_UP);
            list.Add(ClerkAreaMediator.CHANGED_CAT);
            list.Add(ClerkAreaMediator.SHOW_LEVEL_UP_CAT_INFO);
            return list;
        }
    }
    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {
            case ClerkAreaMediator.LEVLE_UP:
                clerkView.LevelUp((JsonData)notification.Body);
                break;
            case ClerkAreaMediator.CHANGED_CAT:
                clerkView.ChangedCat((JsonData)notification.Body);
                break;
            case ClerkAreaMediator.SHOW_LEVEL_UP_CAT_INFO:
                clerkView.ShowLevelUpCatInfo((JsonData)notification.Body);
                break;
            default:
                break;
        }
    } 
}
