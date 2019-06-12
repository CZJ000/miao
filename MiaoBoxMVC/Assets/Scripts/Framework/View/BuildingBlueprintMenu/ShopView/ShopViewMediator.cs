using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
public class ShopViewMediator : Mediator, IMediator
{

    public new const string NAME = "ShopViewMediator";


    /// <summary>
    /// 获取蓝图信息
    /// </summary>
    public const string BUILDING_BLUEPRINT_DATA = "BUILDING_BLUEPRINT_DATA";
    public const string BUILD_STAT_DATA = "BUILD_STAT_DATA";

    public ShopView ShopView
    {
        get
        {
            return m_viewComponent as ShopView;
        }
    }


    public ShopViewMediator():base(NAME)
    {

    }


    public override IEnumerable<string> ListNotificationInterests
    {
        get
        {
            List<string> list = new List<string>();
            //list.Add(ShopMenuViewMediator.BUILDING_BLUEPRINT_DATA);
            ////list.Add(LandedEstateMediator.BUILDING_DATA);
            //list.Add(LandedEstateMediator.BUILD_STAT_DATA);

            return list;
        }
    }

    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {
            //case ShopMenuViewMediator.BUILDING_BLUEPRINT_DATA:
            //    {
            //        ShopMenuView.setBuildingBlueprintData(notification.Body);
            //    }
            //    break;

            //case ShopMenuViewMediator.BUILD_STAT_DATA:
            //    {
            //        ShopMenuView.setBuildStatData(notification.Body);
            //    }

            //    break;




        }
    }
}
