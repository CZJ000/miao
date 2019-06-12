using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using LitJson;
using Mono.Data.Sqlite;
using PureMVC.Interfaces;
using PureMVC.Patterns;
using Global;

public class CatGroupProxy : Proxy
{
    public class CatGroupInfo
    {
        public int id;
        public int catTypeId;
        public int groupId;
        public int userId;
        public int exceedpoint;
    }

    public new const string NAME = "CatGroupMenuProxy";

    public DbAccess dbAcces;
    private int mUserId = -1;
    private Dictionary<int, List<CatGroupInfo>> mCatGroupInfoDic;
    private Dictionary<int, stat_catRow> mCatStatDic;
    
    
    /// <summary>
    /// 从Info_aicat 里面提取战斗ai猫组信息放到里面
    /// </summary>
    private Dictionary<int, List<CatGroupInfo>> mAICatGroupInfoDic;
    private Dictionary<int, stat_catRow> mAICatStatDic;
    private int mAiUserId = -1;

    /// <summary>
    /// 
    /// </summary>
    private int mNeiborInfoProxy = -1;





    /// <summary>
    /// 根据UserAIInfoProxy 里面的UserID 到Info_aicat里面提取数据到mAICatGroupInfoDic里面
    /// </summary>
    /// <param name="aiUserId">Battle ai的userId</param>
    public void SetAIBattleGroupInfo(int aiUserId)
    {
        mAiUserId = (Facade.RetrieveProxy(UserAiInfoProxy.NAME) as UserAiInfoProxy).UsertData.Id;
        mAICatGroupInfoDic = new Dictionary<int, List<CatGroupProxy.CatGroupInfo>>();
        DbAccess dbAccess = new DbAccess();
        string query = string.Format("SELECT * FROM info_aicats WHERE userid={0}", mAiUserId);
        SqliteDataReader reader = dbAccess.ExecuteQuery(query);
        while (reader.Read())
        {
            CatGroupInfo groupInfo = new CatGroupProxy.CatGroupInfo()
            {
                id = Utils.GetInt(reader["id"]),
                catTypeId = Utils.GetInt(reader["cattypeid"]),
                groupId = Utils.GetInt(reader["groupId"]),
                userId = Utils.GetInt(reader["userid"]),
                exceedpoint = Utils.GetInt(reader["exceedpoint"])
            };
            int groupId = reader.GetInt32(reader.GetOrdinal("groupId"));
            if (!mAICatGroupInfoDic.ContainsKey(groupId))
            {
                mAICatGroupInfoDic.Add(groupId, new List<CatGroupInfo>() { groupInfo });
            }
            else
            {
                mAICatGroupInfoDic[groupId].Add(groupInfo);
            }
        }
        dbAccess.CloseSqlConnection();

        mAICatStatDic = new Dictionary<int, stat_catRow>();
        foreach (stat_catRow catStat in stat_cat.GetInstance().rowList)
        {
            mAICatStatDic.Add(catStat.id, catStat);
        }



    }


    public CatGroupProxy() : base(NAME)
    {
       
    }

    public override void OnRegister()
    {
       
    }
    /// <summary>
    /// 与服务器通讯  获得猫分组信息
    /// 信息内容   标签ID 标签名字
    /// </summary>
    public void SendCatGroupTitle(object data)
    {
        JsonData group = new JsonData();
        int count = 0;
        List<system_catgroupRow> catGroupRows = system_catgroup.GetInstance().rowList;
        for (int i = 0; i < catGroupRows.Count; i++)
        {
            JsonData child = new JsonData();
            child["id"] = catGroupRows[i].id;
            child["name"] = catGroupRows[i].name;
            group[count.ToString()] = child;
            count++;
        }
        group["count"] = count;

        JsonData type = (JsonData)data;
        Debug.Log(type["type"].ToString());
        switch (type["type"].ToString())
        {
            //case CatGroupMenuMediator.CAT_GROUP_TITLE:
            //    SendNotification(CatGroupMenuMediator.CAT_GROUP_TITLE, group);
            //    break;
            //case CatGroupMenuMediator.CAT_MOVE_GROUP_TITLE:
            //    SendNotification(CatGroupMenuMediator.CAT_MOVE_GROUP_TITLE, group);
            //    break;
            case CatGroupViewMediator.CAT_GROUP_TITLE:
                Debug.Log("1111");
                SendNotification(CatGroupViewMediator.CAT_GROUP_TITLE, group);
                break;
        }
    }



    /// <summary>
    /// 初始化mCatGroupInfoDic mCatStatDic
    /// </summary>
    public void LoadCatGroupInfo()
    {
        Debug.Log("LoadCatGroupInfo()");
        mUserId = (Facade.RetrieveProxy(UserInfoProxy.NAME) as UserInfoProxy).UsertData.Id;
        mCatGroupInfoDic = new Dictionary<int, List<CatGroupProxy.CatGroupInfo>>();
        DbAccess dbAccess = new DbAccess();
        string query = string.Format("SELECT * FROM info_cats WHERE userid={0}", mUserId);
        SqliteDataReader reader = dbAccess.ExecuteQuery(query);
        while (reader.Read())
        {
            CatGroupInfo groupInfo = new CatGroupProxy.CatGroupInfo()
            {
                id = Utils.GetInt(reader["id"]),
                catTypeId = Utils.GetInt(reader["cattypeid"]),
                groupId = Utils.GetInt(reader["groupId"]),
                userId = Utils.GetInt(reader["userid"]),
                exceedpoint = Utils.GetInt(reader["exceedpoint"])
            };
            int groupId = reader.GetInt32(reader.GetOrdinal("groupId"));

            if (!mCatGroupInfoDic.ContainsKey(groupId))
            {
                mCatGroupInfoDic.Add(groupId, new List<CatGroupInfo>() { groupInfo });
            }
            else
            {
                mCatGroupInfoDic[groupId].Add(groupInfo);
            }
        }
        dbAccess.CloseSqlConnection();

        mCatStatDic = new Dictionary<int, stat_catRow>();
        foreach (stat_catRow catStat in stat_cat.GetInstance().rowList)
        {
            mCatStatDic.Add(catStat.id, catStat);
        }
        
        //之后刷新猫的分组信息；
      //  CatGroupContoler.Instance.RefreshCatGroup();

      //  SendNotification(CatGroupViewMediator.CAT_GROUP_DATA_LOAD);


    }

    /// <summary>
    /// 通过分组ID来获得每个组的猫信息；
    /// </summary>
    /// <param name="groupID"></param>
    void GetCatInfoByGroupID(int groupID)
    {
        // 请求分组数据
        JsonData data = new JsonData();
        data["id"] = groupID;
        AppFacade.GetInstance().SendNotification(NotiConst.GET_CAT_GROUP_INFO, data);
    }





    /// <summary>
    /// 与服务器通讯 获得猫分组内容信息（分组内 猫的 名字 等等）
    /// 
    /// data 组ID（测试用）
    /// </summary>
    public void SendCatGroupInfo(object data)
    {

        JsonData cat = catDateConvertGroup(data);
     //   SendNotification(CatGroupMenuMediator.CAT_GROUP_INFO, cat);
        SendNotification(CatGroupViewMediator.CAT_GROUP_INFO, cat);
    }


    /// <summary>
    /// 发送战斗场景 猫分组信息 
    /// </summary>
    /// <param name="data"></param>
    public void SendBattleGroupInfo(object data )
    {
        if(mCatGroupInfoDic==null)
        {

            LoadCatGroupInfo();

        }
        JsonData cat = catDateConvertGroup(data );
        SendNotification(BattleMediator.BATTLE_CAT_GROUP_INFO, cat);
    }

    /// <summary>
    /// 发送战斗场景 随机猫分组信息
    /// </summary>
    /// <param name="data"></param>
    public void SendBattleRandomGroupInfo(object data)
    {
        if (mCatGroupInfoDic == null)
        {

            LoadCatGroupInfo();

        }
        JsonData cat = catDateConvertGroup(data);
        SendNotification(BattleMediator.BATTLE_CAT_RANDOM_GROUP_INFO, cat);

    }
    /// <summary>
    /// 将猫的数据打包成组
    /// </summary>
    private JsonData catDateConvertGroup(object data)
    {
        JsonData group = (JsonData)data;
        int groupId = (int)group["id"];

        JsonData cat = new JsonData();
        int count = 0;
       
        if (mCatGroupInfoDic.ContainsKey(groupId))
        {

            foreach (CatGroupInfo groupInfo in mCatGroupInfoDic[groupId])
            {
                int cattype = groupInfo.catTypeId;   //猫类型的ID号
                if (!mCatStatDic.ContainsKey(cattype)) Debug.LogError("stat_cat不包含ID为" + cattype + "的猫， 请检查并修改数据库！");
                int id = groupInfo.id;                 //猫存贮的ID号
                stat_catRow catStat = mCatStatDic[cattype];
                JsonData child = new JsonData();
                child["id"] = id;
                child["cattypeid"] = cattype;
                child["captaintype"] = catStat.type;
                child["catName"] = catStat.name;
                child["lv"] = catStat.lv;
                child["power"] = catStat.power;
                child["groupId"] = groupId;
                child["about"] = catStat.about;
                child["cooldown"] = catStat.cooldown;
                child["skillaniid"]  =catStat.skill_aniid;
                child["skill_id"]= catStat.skill_id;
                child["attack_aniid"] = catStat.attack_aniid;
                child["attribute"]= catStat.attribute;
                child["evo"]=catStat.evo;
                child["members"] = catStat.members_slot;
                child["ownexp"] = catStat.ownexp;
                cat[count.ToString()] = child;
                count++;
            }
        }

        cat["count"] = count;
        cat["groupid"] = groupId;
        return cat;
    }
    /// <summary>
    /// 这里是更换猫分组的方法：
    /// </summary>
    /// <param name="data"></param>
    public void SwitchCatGroup(object data)
    {
        JsonData cat = (JsonData)data;
        Debug.Log(cat.ToJson());
        int newId = int.Parse(cat["newId"].ToString());
        int oldId = int.Parse(cat["oldId"].ToString());
        int count = int.Parse(cat["count"].ToString());
        //检查是否交换受限；
        bool islimit = limitCatGroupNum(newId,count);
        if (islimit)
        {
            //交换受限传递错误信息：
            SendNotification(CatGroupMenuMediator.CAT_SWITCH_GROUP_FAIL);

        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                int id = (int)cat[i.ToString()];
                foreach (CatGroupInfo groupInfo in mCatGroupInfoDic[oldId])
                {
                    //比对的分组信息；
                    if (groupInfo.id == id)
                    {
                        Debug.Log("开始更换分组：" + groupInfo.id);
                        mCatGroupInfoDic[oldId].Remove(groupInfo);
                        if (mCatGroupInfoDic.ContainsKey(newId))
                        {
                            mCatGroupInfoDic[newId].Add(groupInfo);
                        }
                        else
                        {
                            mCatGroupInfoDic.Add(newId, new List<CatGroupInfo>() { groupInfo });
                        }
                        groupInfo.groupId = newId;
                        Debug.Log("更换分组成功");
                        break;
                    }
                }
            }


        }

        JsonData group = new JsonData();
        group["id"] = newId;
        SendNotification(CatGroupMenuMediator.CAT_SWITCH_GROUP, group);
        //AppFacade.getInstance.SendNotification(NotiConst.GET_CAT_GROUP_DATA);
        AppFacade.getInstance.SendNotification(NotiConst.SET_CLERK);
    }




    /// <summary>
    /// 这里是更换猫分组的方法：
    /// </summary>
    /// <param name="data"></param>
    public void ChangeCatGroup(object data)
    {

        ChangeCatInfoVO changeCatInfoVO=(ChangeCatInfoVO)data;

            int oneCatGroupID= changeCatInfoVO.OneCatGroupID;
            int oneCatID=changeCatInfoVO.OneCatID;
            int otherCatGroupID= changeCatInfoVO.OtherCatGroupID;
            int otherCatID = changeCatInfoVO.OtherCatID;



        int otherType = changeCatInfoVO.OtherType;
        int oneType = changeCatInfoVO.OneType;

        if (otherCatID == -1)
        {          
            if (oneType == 1&& oneCatGroupID != 5&&oneCatGroupID!=4)
            {

                Debug.Log(mCatGroupInfoDic[oneCatGroupID].Count);
                for (int i= mCatGroupInfoDic[oneCatGroupID].Count-1; i >=0;i--)
                {


                    CatGroupInfo catGroupInfo = mCatGroupInfoDic[oneCatGroupID][i];            
                    mCatGroupInfoDic[oneCatGroupID].Remove(catGroupInfo);

                    catGroupInfo.groupId = otherCatGroupID;
                    mCatGroupInfoDic[otherCatGroupID].Add(catGroupInfo);
                    Debug.Log("count");
                }

                Debug.Log(mCatGroupInfoDic[oneCatGroupID].Count);

            }
            else
            {
                foreach (CatGroupInfo catGroupInfo in mCatGroupInfoDic[oneCatGroupID])
                {
                    if (catGroupInfo.id == oneCatID)
                    {
                       
                        mCatGroupInfoDic[oneCatGroupID].Remove(catGroupInfo);
                        if (!mCatGroupInfoDic.ContainsKey(otherCatGroupID))
                        {
                          
                            mCatGroupInfoDic[otherCatGroupID] = new List<CatGroupInfo>();
                        }
                        catGroupInfo.groupId = otherCatGroupID;
                        mCatGroupInfoDic[otherCatGroupID].Add(catGroupInfo);
                        Debug.Log("1111");
                        Debug.Log(otherCatGroupID);
                        break;
                    }
                }

            }

                   
        }
        else
        {
            CatGroupInfo oneCatGroupInfo=null;
            CatGroupInfo otherCatGroupInfo=null;
            if (oneCatGroupID == 5&&oneType==1&&otherCatGroupID!=4)
            {
                foreach (CatGroupInfo catGroupInfo in mCatGroupInfoDic[oneCatGroupID])
                {
                    if (catGroupInfo.id == oneCatID)
                    {
                        oneCatGroupInfo = catGroupInfo;
                        mCatGroupInfoDic[oneCatGroupID].Remove(catGroupInfo);
                        break;
                    }
                }                      
                    for (int i = mCatGroupInfoDic[otherCatGroupID].Count - 1; i >= 0; i--)
                    {
                        CatGroupInfo catGroupInfo = mCatGroupInfoDic[otherCatGroupID][i];
                        mCatGroupInfoDic[otherCatGroupID].Remove(catGroupInfo);
                         catGroupInfo.groupId = oneCatGroupID;
                        mCatGroupInfoDic[oneCatGroupID].Add(catGroupInfo);
                    }
                oneCatGroupInfo.groupId = otherCatGroupID;
                mCatGroupInfoDic[otherCatGroupID].Add(oneCatGroupInfo);                            
            }
            else if (otherCatGroupID == 5&&oneType==1)
            {
                foreach (CatGroupInfo catGroupInfo in mCatGroupInfoDic[otherCatGroupID])
                {
                    if (catGroupInfo.id == otherCatID)
                    {
                        otherCatGroupInfo = catGroupInfo;
                        mCatGroupInfoDic[otherCatGroupID].Remove(catGroupInfo);
                        break;
                    }
                }                           
                    for (int i = mCatGroupInfoDic[oneCatGroupID].Count - 1; i >= 0; i--)
                    {

                        CatGroupInfo catGroupInfo = mCatGroupInfoDic[oneCatGroupID][i];
                        mCatGroupInfoDic[oneCatGroupID].Remove(catGroupInfo);
                    catGroupInfo.groupId = otherCatGroupID;
                        mCatGroupInfoDic[otherCatGroupID].Add(catGroupInfo);
                    }
                    otherCatGroupInfo.groupId = oneCatGroupID;
                    mCatGroupInfoDic[oneCatGroupID].Add(otherCatGroupInfo);
                
               
            }
            else
            {
                foreach (CatGroupInfo catGroupInfo in mCatGroupInfoDic[oneCatGroupID])
                {
                    if (catGroupInfo.id == oneCatID)
                    {
                        oneCatGroupInfo = catGroupInfo;
                        mCatGroupInfoDic[oneCatGroupID].Remove(catGroupInfo);
                        break;
                    }
                }

                foreach (CatGroupInfo catGroupInfo in mCatGroupInfoDic[otherCatGroupID])
                {
                    if (catGroupInfo.id == otherCatID)
                    {
                        otherCatGroupInfo = catGroupInfo;
                        mCatGroupInfoDic[otherCatGroupID].Remove(catGroupInfo);
                        break;
                    }
                }
                if (oneCatGroupInfo != null && otherCatGroupInfo != null)
                {
                    otherCatGroupInfo.groupId = oneCatGroupID;
                    mCatGroupInfoDic[oneCatGroupID].Add(otherCatGroupInfo);
                    oneCatGroupInfo.groupId = otherCatGroupID;
                    mCatGroupInfoDic[otherCatGroupID].Add(oneCatGroupInfo);

                }



            }
        }



         
        SendNotification(CatGroupViewMediator.CAT_SWITCH_GROUP);
        //  SendNotification();
        // SendNotification(CatGroupMenuMediator.CAT_SWITCH_GROUP, group);
        //AppFacade.getInstance.SendNotification(NotiConst.GET_CAT_GROUP_DATA);
        AppFacade.getInstance.SendNotification(NotiConst.SET_CLERK);
    }





    /// <summary>
    /// 限制猫分组人数
    /// </summary>
    public  bool limitCatGroupNum(int groupid,int addnum)
    {

        if (mCatGroupInfoDic == null)
        {
            LoadCatGroupInfo();
        }
     //  LoadCatGroupInfo();/
        switch (groupid)
        {
            case 1:
                if (!mCatGroupInfoDic.ContainsKey(groupid))
                {
                    return limitCondition(5, 0, addnum);

                }
                return limitCondition(5, mCatGroupInfoDic[groupid].Count, addnum);
               
            case 2:
                if (!mCatGroupInfoDic.ContainsKey(groupid))
                {
                    return limitCondition(5, 0, addnum);

                }
                return  limitCondition(5, mCatGroupInfoDic[groupid].Count, addnum);
                
            case 3:
                if (!mCatGroupInfoDic.ContainsKey(groupid))
                {
                    return limitCondition(5, 0, addnum);

                }
                return limitCondition(5, mCatGroupInfoDic[groupid].Count, addnum);
            //4是毛队列，改成无限多的猫，永远不受限制；
            case 4:

                //if (!mCatGroupInfoDic.ContainsKey(groupid))
                //{
                //    return limitCondition(8, 0, addnum);

                //}
                //return  limitCondition(8, mCatGroupInfoDic[groupid].Count, addnum);
                return false;
            case 5:
                if (!mCatGroupInfoDic.ContainsKey(groupid))
                {
                    return limitCondition(8, 0, addnum);

                }
                return  limitCondition(40, mCatGroupInfoDic[groupid].Count, addnum);
                
            default:
                return false;
                



        }
        
        
    }


    private bool  limitCondition(int maxNum,int currentNum,int addNum)
    {
        int end = currentNum + addNum;
        if (end > maxNum)
        {
            return true;

        }
        return false;


    }
    /// <summary>
    /// 添加雇佣的猫，存入缓存字典
    /// </summary>
    /// <param name="id"></param>
    /// <param name="groupid"></param>
    /// <param name="typeid"></param>
    /// <param name="userid"></param>
    public void  addCatByEmloyee(int id ,int groupid,int typeid,int userid)
    {
        CatGroupInfo info = new CatGroupInfo();
        info.id = id;
        info.groupId = groupid;
        info.catTypeId = typeid;
        info.userId = userid;
        if (!mCatGroupInfoDic.ContainsKey(info.groupId))
        {
            mCatGroupInfoDic.Add(info.groupId, new List<CatGroupInfo>() {info });
        }
        else
        {
            mCatGroupInfoDic[info.groupId].Add(info);
        }

    }
    public void DeleteCat(object data)
    {
        JsonData catarray = (JsonData)data;

        

        int groupId = (int)catarray["groupId"];
        int catStoreId = (int)catarray["id"];

        if (mCatGroupInfoDic.ContainsKey(groupId))
        {
            foreach (CatGroupInfo info in mCatGroupInfoDic[groupId])
            {
                if (info.id == catStoreId)
                {
                    mCatGroupInfoDic[groupId].Remove(info);
                    break;
                }
            }
        }
        else
        {
            Debug.LogError("no such groupID");
        }



        SendNotification(CatGroupViewMediator.CAT_DELETE);
       


    }

   

    /// <summary>
    /// 缓存写回数据库 不必每次修改缓存都写回
    /// </summary>
    public void SaveToDb()
    {
        if (mUserId != -1)
        {
            Debug.Log("save");
            dbAcces = new DbAccess();
            dbAcces.BeginTransaction();
            dbAcces.SqlRequest(string.Format("delete from info_cats where userid={0} ", mUserId));
            foreach (KeyValuePair<int, List<CatGroupInfo>> kvp in mCatGroupInfoDic)
            {
                foreach (CatGroupInfo groupInfo in kvp.Value)
                {
                    string[] values = new string[]
                    {
                        groupInfo.id.ToString(),
                        groupInfo.catTypeId.ToString(),
                        groupInfo.groupId.ToString(),
                        groupInfo.userId.ToString(),
                        "0",
                        string.Format("'{0}'",System.DateTime.Now.ToString()),
                        "0",
                        groupInfo.exceedpoint.ToString(),
                        "0"
                    };


                    dbAcces.InsertInto("info_cats", values);
                }
            }
            dbAcces.Commit();
            dbAcces.CloseSqlConnection();
        }
    }

    public int GetAssistantID()
    {
        const int assistantGroup = 4;
        if (mCatGroupInfoDic.ContainsKey(assistantGroup))
        {
            List<CatGroupInfo> catList = mCatGroupInfoDic[assistantGroup];
            if(catList.Count == 0)
            {
                return -1;
            }
            else
            {
                int randomCat = Random.Range(0, catList.Count);
                CatGroupInfo catInfo = catList[randomCat];
                return catInfo.catTypeId;
            }
        }
        else
        {
            return -1;
        }
    }

    public List<JsonData> GetClerkIDs()
    {
        const int clerkGroup = 4;
        List<JsonData> clerkIDs = new List<JsonData>();
        if (mCatGroupInfoDic.ContainsKey(clerkGroup))
        {
            List<CatGroupInfo> catList = mCatGroupInfoDic[clerkGroup];

            foreach(var catInfo in catList)
            {
                JsonData child = new JsonData();
                child["id"] = catInfo.id;
                child["catTypeId"] = catInfo.catTypeId;
                clerkIDs.Add(child);
            }
        }
        return clerkIDs;
    }

    public void LevelUpClerk(JsonData clerk)
    {
        int id = (int)clerk["id"];
        const int clerkGroup = 4;
        List<CatGroupInfo> catList = mCatGroupInfoDic[clerkGroup];

        foreach (var catInfo in catList)
        {
            if (catInfo.id == id)
            {
                UserInfoProxy user = AppFacade.GetInstance().RetrieveProxy(UserInfoProxy.NAME) as UserInfoProxy;
                int exp = user.getEXP();
                if (!mCatStatDic.ContainsKey(catInfo.catTypeId)) Debug.LogError("数据库缺少猫类型ID: " + catInfo.catTypeId);
                stat_catRow catStat = mCatStatDic[catInfo.catTypeId];

                if (catInfo.exceedpoint == catStat.exceedlimit && catStat.evo == -1)
                {
                    Debug.Log ("经验已经满了.");
                    MessageView.GetInstance().ShowMessage("经验已经满了.");
                }
                else if(exp < catStat.ownexp)
                {
                    Debug.Log("经验不足无法升级.");
                    MessageView.GetInstance().ShowMessage("经验不足无法升级.");
                }
                else
                {
                    catInfo.exceedpoint = catInfo.exceedpoint + 1;
                    exp -= catStat.ownexp;
                    user.setEXP(exp);
                    if (catInfo.exceedpoint > catStat.exceedlimit)
                    {
                        stat_catRow newCatStat = mCatStatDic[catStat.evo];
                        JsonData content = new JsonData();
                        content["oldCatId"] = catInfo.id;
                        content["oldCatTypeId"] = catInfo.catTypeId;
                        content["newCatTypeId"] = catStat.evo;
                        content["oldCatName"] = catStat.name;
                        content["newCatName"] = newCatStat.name;
                        catInfo.exceedpoint = 0;
                        catInfo.catTypeId = catStat.evo;
                        Facade.SendNotification(ClerkAreaMediator.CHANGED_CAT, content);
                        Facade.SendNotification(ClerkAreaMediator.SHOW_LEVEL_UP_CAT_INFO, content);
                        AppFacade.GetInstance().SendNotification(NotiConst.CAT_GROUP_CLOSE);
                    }
                    else
                    {
                        JsonData content = new JsonData();
                        content["exceedpoint"] = catInfo.exceedpoint;
                        content["exceedlimit"] = catStat.exceedlimit;
                        content["id"] = id;
                        Facade.SendNotification(ClerkAreaMediator.LEVLE_UP, content);
                    }
                }               
                break;
            }
        }        
    }

    //     public override void OnRemove()
    //     {
    //         dbAcces.CloseSqlConnection();
    //     }
}
