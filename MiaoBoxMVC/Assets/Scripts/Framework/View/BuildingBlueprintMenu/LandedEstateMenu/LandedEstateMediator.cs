using UnityEngine;
using UnityEditor;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections.Generic;

public class LandedEstateMediator : Mediator, IMediator
{

    public new const string NAME = "LandedEstateMediator";


    /// <summary>
    /// 获取蓝图信息
    /// </summary>
    public const string BUILDING_BLUEPRINT_DATA = "BUILDING_BLUEPRINT_DATA";
    public const string BUILD_STAT_DATA = "BUILD_STAT_DATA";
   
    
    /// <summary>
    /// 获取建筑信息
    /// </summary>
    public const string BUILDING_DATA = "BUILDING_DATA";


    public LandedEstateMenuView landedEstateMenuView
    {
        get
        {
            return m_viewComponent as LandedEstateMenuView;
        }
    }

    public LandedEstateMediator():base(NAME)
    {

    }

    public override IEnumerable<string> ListNotificationInterests
    {
        get
        {
            List<string> list = new List<string>();
            list.Add(LandedEstateMediator.BUILDING_BLUEPRINT_DATA);
            list.Add(LandedEstateMediator.BUILDING_DATA);
            list.Add(LandedEstateMediator.BUILD_STAT_DATA);
          

            return list;
        }
    }

    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {
            case LandedEstateMediator.BUILDING_BLUEPRINT_DATA:
                {
                    landedEstateMenuView.setBuildingBlueprintData(notification.Body);
                }
                break;
            case LandedEstateMediator.BUILD_STAT_DATA:
                {
                    landedEstateMenuView.setBuildStatData(notification.Body);

                }
                break;
            case LandedEstateMediator.BUILDING_DATA:
                {
                    Debug.Log("mediator");
                    landedEstateMenuView.setBuildingData((string)notification.Body);
                }
                break;
           

        }
    }


}