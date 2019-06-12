using UnityEngine;
using System.Collections.Generic;
using PureMVC.Interfaces;
using PureMVC.Patterns;
public class ShopMenuViewMediator : Mediator, IMediator
{
    public new const string NAME = "ShopMenuViewMediator";


    /// <summary>
    /// 获取蓝图信息
    /// </summary>
    public const string BUILDING_BLUEPRINT_DATA = "BUILDING_BLUEPRINT_DATA";
    public const string BUILD_STAT_DATA = "BUILD_STAT_DATA";

    public ShopMenuView ShopMenuView
    {
        get
        {
            return m_viewComponent as ShopMenuView;
        }
    }


    public ShopMenuViewMediator():base(NAME)
    {

    }


    public override IEnumerable<string> ListNotificationInterests
    {
        get
        {
            List<string> list = new List<string>();
            list.Add(ShopMenuViewMediator.BUILDING_BLUEPRINT_DATA);
            //list.Add(LandedEstateMediator.BUILDING_DATA);
            list.Add(ShopMenuViewMediator.BUILD_STAT_DATA);

            return list;
        }
    }

    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {
            case ShopMenuViewMediator.BUILDING_BLUEPRINT_DATA:
                {
                    ShopMenuView.setBuildingBlueprintData(notification.Body);
                }
                break;

            case ShopMenuViewMediator.BUILD_STAT_DATA:
                {
                    ShopMenuView.setBuildStatData(notification.Body);
                }

                break;
        
                


        }
    }
}
