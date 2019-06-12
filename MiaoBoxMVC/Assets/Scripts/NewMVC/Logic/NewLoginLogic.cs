/*****************************************************
/** 类名：NewLoginLogic.cs
/** 作者：Tearix
/** 日期：2018-03-07
/** 描述：
*******************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;

public class NewLoginLogic {

    public void SendRegister(string name, string password)
    {
        /**********
         * 需要改成Json与服务器通讯
         **********/

        DbAccess dbAccess = new DbAccess();
        string userName = name;//string.Format("'{0}'",data.UserName);
        string passWord = password;//string.Format("'{0}'", data.Password);
        dbAccess.InsertIntoSpecific("info_users",
            new string[] { "type", "playername", "passwd", "lv", "vip", "gold", "diamond", "exp" },
            new string[] { "1", userName, passWord, "0", "0", "100", "5", "0" });
        //dbAccess.CloseSqlConnection();

        int id = -1;
        SqliteDataReader reader = dbAccess.SelectWhere("info_users",
                                                        new string[] { "id" },
                                                        new string[] { "playername", "passwd" },
                                                        new string[] { " = ", " = " },
                                                        new string[] { userName, passWord });

        if (reader.Read())
        {
            id = Utils.GetInt(reader["id"]);
        }

        dbAccess.InsertIntoSpecific("info_spheres",
                                    new string[] { "userid", "customer_current" },
                                    new string[] { id.ToString(), "0" });
        dbAccess.CloseSqlConnection();


        NewLoginMgr.GetInstance().V_Model.F_ReturnRegisterRet(true, name, password);
    }

    public void F_Login(string name, string password)
    {
        //请求登陆
        //不论成功失败都会添加用户Id数据
        UserInfoProxy userInfoProxy = AppFacade.getInstance.RetrieveProxy(UserInfoProxy.NAME) as UserInfoProxy;
        NeighborInfoProxy neighborUserInfoProxy = AppFacade.getInstance.RetrieveProxy(NeighborInfoProxy.NAME) as NeighborInfoProxy;

        bool ret = false;
        DbAccess dbAccess = new DbAccess();
        string query = string.Format("SELECT id,lv FROM info_users WHERE playername='{0}' AND passwd='{1}'", name, password);
        Debug.Log("login");
        SqliteDataReader reader = dbAccess.ExecuteQuery(query);
        if (reader.Read())
        {
            int id = Utils.GetInt(reader["id"]);
            userInfoProxy.UsertData.Id = id;
            neighborUserInfoProxy.userID = id;
            neighborUserInfoProxy.userLV = Utils.GetInt(reader["lv"]);
            ret = true;

            LocalSaveData.LoginUserName = name;
            LocalSaveData.LoginPassword = password;
            NewLoginMgr.GetInstance().V_Model.V_LoginName = name;
            NewLoginMgr.GetInstance().V_Model.V_LoginPassW = password;
        }
        dbAccess.CloseSqlConnection();

        NewLoginMgr.GetInstance().V_Model.F_ReturnLoginRet(ret);
    }
}
