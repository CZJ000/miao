using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
public class TaskMediator : Mediator ,IMediator {

    public new const string NAME = "TaskMediator";

    //当更新AI对手时，更新model后再触发此事件，以更新Mono视图
  public const string REFRESH_AI_USER_INFO = "REFRESH_AI_USER_INFO";

    public const string FINISH_BATTBLE = "FINISH_BATTBLE";

    public TaskMediator() : base(NAME)
    {

    }



   // 需要监听的消息号
    public override IEnumerable<string> ListNotificationInterests
    {
        get
        {
            List<string> list = new List<string>();
            list.Add(TaskMediator.REFRESH_AI_USER_INFO);
            list.Add(TaskMediator.FINISH_BATTBLE);
            return list;
        }
    }


    //接收消息到消息之后处理
    public override void HandleNotification(INotification notification)
    {
        //MainMenuUIView uiView = ViewComponent as MainMenuUIView;
        TaskView uiView = ViewComponent as TaskView;
        switch (notification.Name)
        {
            //更新对手
            case REFRESH_AI_USER_INFO:
                

                break;

            //完成战斗后更新mono视图
            case FINISH_BATTBLE:
                uiView.FinishBattle(notification.Body);

                break;
        }
    }
}
