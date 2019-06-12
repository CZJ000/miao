using System.Collections;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using LitJson;
using Mono.Data.Sqlite;
using System.Collections.Generic;
using UnityEngine;
public class UserAiInfoProxy : Proxy, IProxy {

    public new const string NAME = "UserAiInfoProxy";

    private new UserInfoVO Data;

    public UserInfoVO UsertData
    {
        get
        {
            return Data;
        }
    }


    public UserAiInfoProxy() : base(NAME)
    {
        Data = new UserInfoVO();
    }
    public void Init()
    {
        DbAccess dbAccess = new DbAccess();
        string query = string.Format("SELECT * FROM info_users WHERE id={0}", Data.Id);
        Debug.Log(Data.Id);
        SqliteDataReader reader = dbAccess.ExecuteQuery(query);
        if (reader.Read())
        {
            Data.PlayerName = Utils.GetString(reader["playername"]);
            Data.Level = Utils.GetInt(reader["lv"]);
            Data.Gold = Utils.GetInt(reader["gold"]);
            Data.Diamond = Utils.GetInt(reader["diamond"]);
            Data.Exp = Utils.GetInt(reader["exp"]);
            Data.Blueprint = Utils.GetString(reader["blueprints_budin"]);
            Data.facility1 = Utils.GetInt(reader["facility1"]);
            Data.facility2 = Utils.GetInt(reader["facility2"]);
            Data.facility3 = Utils.GetInt(reader["facility3"]);
            Data.facility4 = Utils.GetInt(reader["facility4"]);
        }
        dbAccess.CloseSqlConnection();

    }

    /// <summary>
    /// 用于任务系统，按下左键或者右键时从Info_aiuser 里面随机取出一个等级相同的作为对手
    /// </summary>
    public void GetRandomAIUserFromServer()
    {
        UserInfoProxy userInfoProxy = AppFacade.getInstance.RetrieveProxy(UserInfoProxy.NAME) as UserInfoProxy;
       int mlevel= userInfoProxy.UsertData.Level;
        DbAccess dbAccess = new DbAccess();


        string query = string.Format("SELECT *  FROM info_aiuser WHERE lv={0} ORDER BY RANDOM() limit 1", mlevel);
        SqliteDataReader reader = dbAccess.ExecuteQuery(query);
        while (reader.Read())
        {
            Data.PlayerName = Utils.GetString(reader["playername"]);
            Data.Level = Utils.GetInt(reader["lv"]);
            Data.Gold = Utils.GetInt(reader["gold"]);
            Data.Diamond = Utils.GetInt(reader["diamond"]);
            Data.Exp = Utils.GetInt(reader["exp"]);
            Data.Blueprint = Utils.GetString(reader["blueprints_budin"]);
            Data.facility1 = Utils.GetInt(reader["facility1"]);
            Data.facility2 = Utils.GetInt(reader["facility2"]);
            Data.facility3 = Utils.GetInt(reader["facility3"]);
            Data.facility4 = Utils.GetInt(reader["facility4"]);
        }
        dbAccess.CloseSqlConnection();


        //数据改变时手动更新View（手动保持先更新数据再更新视图）
        //参数待定
        SendNotification(TaskMediator.REFRESH_AI_USER_INFO,null);
    }
}
