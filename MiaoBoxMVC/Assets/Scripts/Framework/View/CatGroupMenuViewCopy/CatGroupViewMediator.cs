using UnityEngine;
using System.Collections;


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns;
using PureMVC.Interfaces;



public class CatGroupViewMediator : Mediator, IMediator
{

    public new const string NAME = "CatGroupViewMediator";

    // 猫分组信息
    public const string CAT_GROUP_TITLE = "catGroupTitle";     //不需要请求了
    // 猫移动分组信息
    public const string CAT_MOVE_GROUP_TITLE = "catMoveGroupTitle";
    // 猫分组内容信息

    public const string CAT_GROUP_DATA_LOAD = "catGroupDataLoad";

    public const string CAT_GROUP_INFO = "catGroupInfo";

    public const string CAT_SWITCH_GROUP = "catSwitchGroup";

    public const string CAT_SWITCH_GROUP_FAIL = "catswitchgroupfail";

    public const string CAT_DELETE = "catDelete";

    public CatGroupView CatGroupView
    {
        get
        {
            return m_viewComponent as CatGroupView;
        }
    }

    public CatGroupViewMediator() : base(NAME)
    {

    }
    //需要监听的消息号
    public override IEnumerable<string> ListNotificationInterests
    {
        get
        {
            List<string> list = new List<string>();
            list.Add(CAT_GROUP_TITLE);
            list.Add(CAT_GROUP_INFO);
            list.Add(CAT_MOVE_GROUP_TITLE);
            list.Add(CAT_SWITCH_GROUP);
            list.Add(CAT_SWITCH_GROUP_FAIL);
            list.Add(CAT_DELETE);
            list.Add(CAT_GROUP_DATA_LOAD);
            return list;
        }
    }
    //接收消息到消息之后处理
    public override void HandleNotification(INotification notification)
    {
        
        Debug.Log("猫分组接受到指令名：" + notification.Name);
        switch (notification.Name)
        {

            case CAT_GROUP_DATA_LOAD:
                CatGroupView.InitCatGroupView();
                break;
            case CAT_GROUP_TITLE:

                CatGroupView.GetGroupTile(notification.Body);
                //       uiView.ShowCatGroupTitle(notification.Body);
                break;
            case CAT_GROUP_INFO:
                CatGroupView.ShowCatGroupInfo(notification.Body);
                break;
            //case CAT_MOVE_GROUP_TITLE:
             
            //    break;
            case CAT_SWITCH_GROUP:
                CatGroupView.RefreshCatGroupViewAfterSwitch(notification.Body);
                break;
            //case CAT_SWITCH_GROUP_FAIL:
            //    m_viewComponent.limitInfo();
            //    break;
            case CAT_DELETE:
                CatGroupView.RefreshCatGroupViewAfterDelete(notification.Body);
                break;
            default: break;
        }
    }
}
