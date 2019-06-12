using Global;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 猫分组的控制器，此处还要用来存储数据；
/// </summary>
public class CatGroupContoler : MonoBehaviour
{
    /// <summary>
    /// 静态单例；
    /// </summary>
    public static CatGroupContoler Instance;
    /// <summary>
    /// UI摄像机；
    /// </summary>
    public Camera UICamera;
    /// <summary>
    /// 是否获得过所有组件；
    /// </summary>
    private bool IsGetAllCompoenets = false;
    /// <summary>
    /// 目录类型；
    /// </summary>
    public MenuType menutype;
    /// <summary>
    /// 是否激活；通过设置Scale来显示
    /// </summary>
    public bool IsInvoke
    {
        set
        {
            transform.localScale = value ? Vector3.one : Vector3.zero;
        }
        get
        {
            return transform.localScale != Vector3.zero;
        }
    }
    /// <summary>
    /// 所有的猫信息；
    /// </summary>
    public List<CatInfoCopy> ListAllCatInfo = new List<CatInfoCopy>();
    /// <summary>
    /// 所有的猫图片；
    /// </summary>
    public List<Sprite> ListAllCatImageSprite = new List<Sprite>();
    /// <summary>
    /// 所有的猫图片，通过ID来取值
    /// </summary>
    public Dictionary<int, Sprite> DicAllCatImageByID = new Dictionary<int, Sprite>();

    private void Awake()
    {
        Instance = this;
    }

    #region 控制组件；

    //取消按钮；
    public Button CancelButton;
    /// <summary>
    /// 默认猫图片；
    /// </summary>
    public Sprite DefaultCatSprite;

    #endregion


    // Use this for initialization
    void Start()
    {
        GetAllCopoenets();
    }

    // Update is called once per frame
    void Update()
    {

    }


    #region 猫分组的一些控制命令；

    /// <summary>
    /// 显示猫分组，需要请求猫标题，并做准备处理
    /// </summary>
    public void showCatGroup()
    {

    }

    /// <summary>
    /// 交换两只猫分组的方法；
    /// </summary>
    /// <param name="CatA"></param>
    /// <param name="CatB"></param>
    public void SwitchTwoCatGroup(CatInfo CatA,CatInfo CatB)
    {
        //取出两个分组ID；
        int GroupIDA = CatA.GroupId;
        int GroupIDB = CatB.GroupId;
        //如果同组则不交换；
        if (GroupIDA == GroupIDB) return;
        //如果两个分组都是0也不交换（此时代表两个空的猫信息）；
        if (GroupIDA == 0 || GroupIDB == 0) return;
        //进行分组交换；
        OnCatSwitchGroup(CatB, GroupIDA, GroupIDB);
        OnCatSwitchGroup(CatA, GroupIDB, GroupIDA);
    }

    /// <summary>
    /// 更换猫分组的方法；
    /// </summary>
    /// <param name="a"></param>
    /// <param name="_groupID"></param>
    public void ChangeCatGroup(CatInfo a, int _groupID)
    {
        OnCatSwitchGroup(a, _groupID, a.GroupId);
    }

    /// <summary>
    /// 猫换分组的方法：一只猫
    /// </summary>
    /// <param name="cat">需要交换的猫</param>
    /// <param name="NewGroupID">新组ID</param>
    /// <param name="OldGroupID">旧组ID</param>
    public void OnCatSwitchGroup(CatInfo cat, int NewGroupID,int OldGroupID)
    {
        Debug.Log("进行猫分组变更：一只猫");
        JsonData catMoveData = new JsonData();
        catMoveData["newId"] = NewGroupID;
        catMoveData["oldId"] = OldGroupID;
        catMoveData["0"] = cat.Id;
        //此处应该是涉及到多个交换时执行的方法，本次值交换一个则直接输入1即可；
        catMoveData["count"] = "1";
        AppFacade.GetInstance().SendNotification(NotiConst.CAT_SWITCH_GROUP, catMoveData);
    }

    /// <summary>
    /// 将多只猫移动到分组5（最下面的那个组）当中；
    /// </summary>
    /// <param name="cats"></param>
    public void MoveCatsToBottomGroup(CatInfo[] cats)
    {
        for (int i = 0; i < cats.Length; i++)
        {
            JsonData catMoveData = new JsonData();
            catMoveData["newId"] = 5;
            catMoveData["oldId"] = cats[i].GroupId;
            catMoveData["0"] = cats[i].Id;
            catMoveData["count"] = "1";
            AppFacade.GetInstance().SendNotification(NotiConst.CAT_SWITCH_GROUP, catMoveData);
        }
    }


    /// <summary>
    /// 将多只猫移动到分组5（最下面的那个组）当中；
    /// </summary>
    /// <param name="cats"></param>
    public void MoveCatsToBottomGroup(CatInfoCopy[] cats)
    {
        for (int i = 0; i < cats.Length; i++)
        {
            if (cats[i] == null || string.IsNullOrEmpty(cats[i].Name)) continue;
            JsonData catMoveData = new JsonData();
            catMoveData["newId"] = 5;
            catMoveData["oldId"] = cats[i].GroupId;
            catMoveData["0"] = cats[i].Id;
            catMoveData["count"] = "1";
            AppFacade.GetInstance().SendNotification(NotiConst.CAT_SWITCH_GROUP, catMoveData);
        }
    }

    /// <summary>
    /// 猫换分组的方法：多只猫
    /// </summary>
    /// <param name="cats">多只猫的数组</param>
    /// <param name="NewGroupID">新组ID</param>
    /// <param name="OldGroupID">旧组ID</param>
    public void OnCatSwitchGroup(CatInfo[] cats, string NewGroupID, string OldGroupID)
    {
        Debug.Log("进行猫分组变更：多只猫");
        JsonData catMoveData = new JsonData();
        catMoveData["newId"] = NewGroupID;
        catMoveData["oldId"] = OldGroupID;
        catMoveData["count"] = cats.Length;
        for (int i = 0; i < cats.Length; i++)
        {
            catMoveData[i.ToString()] = cats[i].Id;
        }
        AppFacade.GetInstance().SendNotification(NotiConst.CAT_SWITCH_GROUP, catMoveData);
    }

    #endregion

    /// <summary>
    /// 清空所有猫列表的方法，在使用加值前调用；
    /// </summary>
    public void ClearList()
    {
        ListAllCatInfo.Clear();
    }

    /// <summary>
    /// 显示猫内容，接收到的回调函数；
    /// </summary>
    /// <param name="data"></param>
    public void ShowCatGroupInfo(object data)
    {
        //首先清空原来的List；
        ListAllCatInfo.Clear();
        JsonData cats = (JsonData)data;
        int CatCount = (int)cats["count"];
        int GroupID = (int)cats["groupid"];
        //存储信息：
        for (int i = 0; i < CatCount; i++)
        {
            JsonData catInfo = cats[i];
            CatInfoCopy cat = new CatInfoCopy();
            //设置猫的信息存进List；
            cat.Id = (int)catInfo["id"];
            cat.CatTypeid = (int)catInfo["cattypeid"];
            cat.CatCaptainTypeid = (int)catInfo["captaintype"];
            cat.Name = (string)catInfo["catName"];
            cat.Level = (int)catInfo["lv"];
            cat.Power = (int)catInfo["power"];
            cat.GroupId = (int)catInfo["groupId"];
            cat.Attribute = (string)catInfo["attribute"];
            cat.MembersSlot = (string)catInfo["members"];
            cat.AttackType = (int)catInfo["attack_aniid"];
            cat.SkillType = (int)catInfo["skill_id"];
            //存进List；
            ListAllCatInfo.Add(cat);
        }
        //先删除所有猫；
        //CatModelContorl.NeedRefreshAll(GroupID);
        //根据不同的猫分组对数据进行存储；
        //1、 2、 3组存在最上方的位置，4组在最下方，作为空闲的猫；
        switch (GroupID)
        {
            case 1:
                CatGroupCenterControler.Instance.AddCatInfoToPanel(0, ListAllCatInfo);
                CatGroupCenterControler.Instance.Refresh();
                break;
            case 2:
                CatGroupCenterControler.Instance.AddCatInfoToPanel(1, ListAllCatInfo);
                CatGroupCenterControler.Instance.Refresh();
                break;
            case 3:
                CatGroupCenterControler.Instance.AddCatInfoToPanel(2, ListAllCatInfo);
                CatGroupCenterControler.Instance.Refresh();
                break;
            case 4:
                CatGroupCenterControler.Instance.AddCatInfoToCamp(ListAllCatInfo);
                CatGroupCenterControler.Instance.RefreshCampMessage();
                break;
            case 5:
                CatGroupBottomControler.Instance.ClearCatInfoList();
                for (int i = 0; i < ListAllCatInfo.Count; i++)
                {
                    CatGroupBottomControler.Instance.AddCatInfoCopy(ListAllCatInfo[i]);
                }
                //刷新页面；
                CatGroupBottomControler.Instance.Refresh();
                break;
        }
    }


    /// <summary>
    /// 移动按钮 数据响应
    /// </summary>
    /// <param name="data"></param>
    public void CatMoveTitle(object data)
    {
        JsonData group = (JsonData)data;
        int count = (int)group["count"];
        for (int i = 0; i <group.Count; i++)
        {
            JsonData json = group[i.ToString()];
            int id = (int)json["id"];
            string name = (string)json["name"];
        }
        
    }


    /// <summary>
    /// 切换分组
    /// </summary>
    /// <param name="data"></param>
    public void SwitchCatGroup(object data)
    {
        //获得分组ID；
        JsonData group = (JsonData)data;
        int groupId = (int)group["id"];
    }

    /// <summary>
    /// 在猫分组和最下面的猫中间进行交换
    /// </summary>
    /// <param name="CatInTeam">猫分组中的信息</param>
    /// <param name="CatInGroup">最下面的猫信息</param>
    /// <param name="CatGroupID">猫分组编号</param>
    public void SwitchCatGroupFromGroupToTeam(CatInfo CatInTeam, CatInfo CatInGroup,int CatGroupID)
    {
        //先进行猫检测1：如果是队长位置
        if (CatInTeam.gameObject.name.Contains("Leader"))
        {
            Debug.Log(CatInGroup.CatCaptainTypeid);
            //判定原来的猫是不是队长猫；
            if (CatInGroup.CatCaptainTypeid == 1)
            {
                //将原来的猫移动到底部；
                CatGroupCenterControler.Instance.RemoveAllMembersByGroupID(CatGroupID);
                ChangeCatGroup(CatInGroup, CatGroupID);
            }
            //如果底部是个空位，则将队长移动到此；
            else if (CatInGroup.Attribute == "null")
            {
                //将原来的猫移动到底部；
                CatGroupCenterControler.Instance.RemoveAllMembersByGroupID(CatGroupID);
            }
            else
            {
                //不是队长猫则不能交换；
                Debug.Log("不是队长猫，不能交换！");
                MessageView.GetInstance().ShowMessage("不是队长猫，不能交换！");
                return;
            }
        }
        else
        {
            //是队长猫 或者两者属性不一且猫分组的属性不是空的
            if (CatInGroup.CatCaptainTypeid == 1 || ((CatInGroup.Attribute != CatInTeam.Attribute) && CatInGroup.Attribute != "null") )
            {
                string Mes = "猫属性不符，不能交换！";
                if (CatInGroup.CatCaptainTypeid == 1)
                {
                    //此时是有队长猫的情况；
                    Mes = CatInGroup.Name + " 是队长猫，不能作为队员！";
                }
                //不是同样的属性也不能交换；
                Debug.Log(Mes);
                MessageView.GetInstance().ShowMessage(Mes);
                Debug.Log("猫属性分别为：" + CatInGroup.Attribute + "和" + CatInTeam.Attribute);
                return;
            }
            else
            {
                //属性符合，检查是否是空对象；
                if (CatInTeam.CatTypeid <= 0)
                {
                    //空对象，则直接将此猫进行移动；分组中有空位；
                    ChangeCatGroup(CatInGroup, CatGroupID);
                }
                else if (CatInGroup.CatTypeid <= 0)
                {
                    //空对象，这里是下面分组有空位；
                    ChangeCatGroup(CatInTeam, 5);
                }
                //非空对象，交换猫；
                else
                {
                    //猫交换；
                    SwitchTwoCatGroup(CatInGroup, CatInTeam);
                }
            }
        }
        //刷新一遍猫分组；
        RefreshCatGroup(CatGroupID);
        RefreshCatGroup(5);
    }

    /// <summary>
    /// 最下面的猫和训练营的猫进行交换；
    /// </summary>
    /// <param name="CatInCamp">训练营的猫信息</param>
    /// <param name="CatInGroup">最底部的猫信息</param>
    public void SwitchCatGroupFromCampToGroup(CatInfo CatInCamp, CatInfo CatInGroup)
    {
        if (CatInCamp.Id <= 0 && CatInGroup.Id>0)
        {
            //直接将最开始的猫交换到第4组；
            ChangeCatGroup(CatInGroup, 4);
        }
        else if (CatInGroup.Id <= 0 && CatInCamp.Id>0)
        {
            //直接将最开始的猫交换到第5组；
            ChangeCatGroup(CatInCamp, 5);
        }
        else
        {
            //此时存在猫，则进行猫交换；
            SwitchTwoCatGroup(CatInGroup, CatInCamp);
        }
        //进行刷新；
        RefreshCatGroup(5);
        RefreshCatGroup(4);
    }

    /// <summary>
    /// 删除一只猫的方法；
    /// </summary>
    /// <param name="cat"></param>
    public void DeleteOneCat(CatInfo cat)
    {
        //需要进行确认；确认执行之后方才执行确认方法；
        string CatMes = cat.Name + "（等级" + cat.Level + "）";
        string Mes = "你确定要解雇吗？\n" + CatMes + "将会被删除！";
        MessageView.GetInstance().ShowMessage(Mes, delegate ()
         {
            //此时执行删除猫的方法；
            DeleteCat(cat);
            //执行完成之后刷新底部的猫列表；
            RefreshCatGroup(5);
         }, null, MessageView.ShowType.WithYesAndNoBtn);
    }

    /// <summary>
    /// 删除猫的方法（真删除）；
    /// </summary>
    /// <param name="cat"></param>
    private void DeleteCat(CatInfo cat)
    {
        //以下是真正删除猫的方法；
        JsonData deleteCat = new JsonData();
        deleteCat["count"] = 1;
        deleteCat["group"] = cat.GroupId;
        deleteCat["0"] = cat.Id;
        AppFacade.GetInstance().SendNotification(NotiConst.CAT_DELETE, deleteCat);
    }

    public void limitInfo()
    {
        Debug.Log("移动将达到组员上限，移动失败");
        string s = "移动将达到组员上限，移动失败";
        MessageView.GetInstance().ShowMessage(s);
    }


    /// <summary>
    /// 猫删除完成 重新请求 猫分组数据
    /// </summary>
    /// <param name="data"></param>
    public void CatDeleteFinsh(object data)
    {
        JsonData group = (JsonData)data;
        JsonData groupData = new JsonData();
        groupData["id"] = group["group"];
        AppFacade.GetInstance().SendNotification(NotiConst.GET_CAT_GROUP_INFO, groupData);
    }


    /// <summary>
    /// 刷新猫分组的方法；
    /// </summary>
    public void RefreshCatGroup()
    {
        //分别获得4个分组的猫信息；
        GetCatInfoByGroupID(1);
        GetCatInfoByGroupID(2);
        GetCatInfoByGroupID(3);
        GetCatInfoByGroupID(4);
        GetCatInfoByGroupID(5);
    }

    /// <summary>
    /// 输入一个参数，仅刷新当前组；
    /// </summary>
    /// <param name="_groupID"></param>
    public void RefreshCatGroup(int _groupID)
    {
        GetCatInfoByGroupID(_groupID);
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
    /// 赋值猫的信息的方法；
    /// </summary>
    /// <param name="a">后台信息</param>
    /// <param name="b">挂载到游戏对象上的UI保存信息</param>
    public void CopyCatInfo(CatInfoCopy a, CatInfo b)
    {
        //赋值一遍信息；
        b.Id = a.Id;
        b.CatTypeid = a.CatTypeid;
        b.CatCaptainTypeid = a.CatCaptainTypeid;
        b.GroupId = a.GroupId;
        b.Keep = a.Keep;
        b.AcqDate = a.AcqDate;
        b.Grow = a.Grow;

        b.Evo = a.Evo;
        b.Name = a.Name;
        b.Level = a.Level;
        b.Iq = a.Iq;
        b.Power = a.Power;
        b.React = a.React;
        b.Skill = a.Skill;
        b.Attribute = a.Attribute;

        b.MembersSlot = a.MembersSlot;
        b.About = a.About;

        b.AttackType = a.AttackType;
        b.SkillType = a.SkillType;
        
    }

    /// <summary>
    /// 根据ID从字典中取图片；
    /// </summary>
    /// <param name="catID"></param>
    public Sprite GetCatSpriteFromDic(int catID)
    {
        if (DicAllCatImageByID.ContainsKey(catID))
        {
            return DicAllCatImageByID[catID];
        }
        Debug.Log("不存在此猫图片：" + catID);
        return default(Sprite);
    }

    public void GetAllCopoenets()
    {
        if (IsGetAllCompoenets) return;
        IsGetAllCompoenets = true;
        CancelButton.onClick.AddListener(delegate ()
        {
            IsInvoke = false;
            //保存数据；  
            AppFacade.GetInstance().SendNotification(NotiConst.CAT_GROUP_CLOSE);
        });
        //获取所有的猫图片；
        for (int i = 0; i < ListAllCatImageSprite.Count; i++)
        {
            //将名字从第3个截取并转换为ID；
            //Debug.Log(ListAllCatImageSprite[i].name.Substring(3));
            int catID = int.Parse(ListAllCatImageSprite[i].name.Substring(3));
            DicAllCatImageByID.Add(catID, ListAllCatImageSprite[i]);
        }
    }
}


