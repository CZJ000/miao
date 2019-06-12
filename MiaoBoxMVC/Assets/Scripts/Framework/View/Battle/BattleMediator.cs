using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using UnityEngine;



public    class BattleMediator : Mediator, IMediator
{

    public new const string NAME = "BattleMediator";
    public const string BATTLE_CAT_GROUP_INFO = "battlecatGroupInfo";
    public const string BATTLE_CAT_RANDOM_GROUP_INFO = "battlecatrandomInfo";
    public const string BATTLE_INIT_QUENE = "battleinitquene";
    public const string BATTLE_INIT_AI = "battleinitai";
    public const string SHOWREADTIME = "showreadtime";
    public const string NOTIFYFORPLAYER = "notifyfirplayer";
    public const string SHOWHUDTEXT = "showhudtext";
    public const string CHOOSEQUENEEND = "choosequeneend";
   
    public const string SHOWHUDSPRITE = "showhudsprite";
    public const string SHOWCHANGRBTN = "showchangebtn";

    public BattleView BattleView { get { return ViewComponent as BattleView; } }
    public  BattleMediator(): base(NAME)
     {
        
        
     }
    public override IEnumerable<string> ListNotificationInterests
    {

        get
        {
            List<string> list = new List<string>();
            list.Add(BATTLE_CAT_GROUP_INFO);
            list.Add(BATTLE_CAT_RANDOM_GROUP_INFO);
            list.Add(BATTLE_INIT_AI);
            list.Add(BATTLE_INIT_QUENE);
            list.Add(SHOWHUDTEXT);
            list.Add(SHOWREADTIME);
            list.Add(NOTIFYFORPLAYER);
            list.Add(CHOOSEQUENEEND);
          
            list.Add(SHOWHUDSPRITE);
            list.Add(SHOWCHANGRBTN);

            return list;
        }

    }
    public override void HandleNotification(INotification notification)
    {
        BattleView uview = ViewComponent as BattleView;
        BattleUIVO UIVO = new BattleUIVO();
        switch (notification.Name)
        {
            case BATTLE_CAT_GROUP_INFO:
                Debug.Log("init battle view ");
                uview.ShowCurrentCatGroup(notification.Body);
                break;
            case BATTLE_CAT_RANDOM_GROUP_INFO:
                uview.ShowRandomCatGroup(notification.Body);
                break;
            case BATTLE_INIT_QUENE:
                uview.BattleInitChooseQuene();
                break;
            case BATTLE_INIT_AI:
                uview.BattleInitCreatAi();
                break;
            case SHOWREADTIME:
                UIVO = notification.Body as BattleUIVO;
                uview.ShowReadTime(UIVO);
                break;
            case SHOWHUDTEXT:
                UIVO = notification.Body as BattleUIVO;
                uview.ShowHUDText(UIVO);

                break;
            case NOTIFYFORPLAYER:
                UIVO = notification.Body as BattleUIVO;
                uview.NotifyForPlayer(UIVO);
                break;
            case CHOOSEQUENEEND:
                uview.ChooseQueneEnd();
                break;
            
            case SHOWHUDSPRITE:
                UIVO = notification.Body as BattleUIVO;
                uview.ShowHUdSprite(UIVO);
                break;
            case SHOWCHANGRBTN:
                uview.ChangeQueneBtnUIHideOrshow((bool)notification.Body );
                break;

        }

    }
}


