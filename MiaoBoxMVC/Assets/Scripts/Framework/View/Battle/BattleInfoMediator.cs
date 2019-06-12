using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using UnityEngine;
using Global;

public class BattleInfoMediator : Mediator, IMediator
{
    public new const string NAME = "BattleInfoMediator";
    public  const string UPDATEPLAYERHP = "Updateplayerhp";
    public  const string UPDATENEMYHP = "Updatenemyhp";
    public const string UPDATENEMYSCORE = "updatenemyscore";
    public const string UPDATPLAYERSCORE = "updateplayerscore";
    public const string UPDATESCOREINFORESULT = "updatescoreresult";
    public const string UPDATEBATTLERESULT = "updatebattleresult";
    public const string UPDATENEMYLIMIT = "updatenemylimit";
    public const string UPDATEPLAYERLIMIT = "updateplayerlimit";
    public BattleInfoMediator(): base(NAME)
     {


    }
    public override IEnumerable<string> ListNotificationInterests
    {

        get
        {
            List<string> list = new List<string>();
            list.Add(UPDATEPLAYERHP);
            list.Add(UPDATENEMYHP);
            list.Add(UPDATPLAYERSCORE);
            list.Add(UPDATENEMYSCORE);
            list.Add(UPDATESCOREINFORESULT);
            list.Add(UPDATEBATTLERESULT);
            list.Add(UPDATENEMYLIMIT);
            list.Add(UPDATEPLAYERLIMIT);
            return list;
        }

    }
    public override void HandleNotification(INotification notification)
    {
        BattleInfoView uview = ViewComponent as BattleInfoView;
        switch(notification.Name)
        {


            case UPDATENEMYHP:
                uview.UpdateEnemyhp((float)notification.Body);
                break;
            case UPDATEPLAYERHP:
                uview.UpdatePlayerHp((float)notification.Body);
                break;
            case UPDATENEMYSCORE:
                uview.UpdateEnemyScore((int )notification.Body);
                break;
            case UPDATPLAYERSCORE:
                uview.UpdatePlayerScore((int)notification.Body);
                break;   
              case UPDATESCOREINFORESULT:
                uview.UpdateScoreInfoResult(( RoleType) notification.Body );
                break;
            case UPDATEBATTLERESULT:
                uview.ResultBattle(notification.Body as BattleResultVO);
                break;
            case UPDATENEMYLIMIT:
                uview.UpdateEnemyLimit((float)notification.Body);
                break;
            case UPDATEPLAYERLIMIT:
                uview.UpdatePlayerLimit((float)notification.Body);
                break;
        }

    }
}
