using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Global;
using UnityEngine.UI;
using SUIFW;
public enum CatGroup
{
    ear = 1,
    claw = 2,
    tail = 3,
    train = 4,
    emptyGroup = 5,
}

public enum CatType
{
   Captain=1,
   Member=2,
}

public class CatGroupView : BaseUIForm
{

 

   int groupPageId=1;

    int nonGroupPageId=0;

    List<CatInGroupInfoVO> ShowingCatGroupData;

    List<CatInGroupInfoVO> EmptyGroupCatData;

    List<GameObject> onGroupShowingObj ;

    List<GameObject> nonGroupShowingObj ;


    int catModelLayer;

    int count;
    int EmptyGroupPagePointer;

    /// <summary>
    /// 是否激活；
    /// </summary>
    public bool IsInvoke
    {
        get
        {

            //  Camera.main.GetComponent<UICamera>().eventType = UICamera.EventType.UI_2D;
            return gameObject.activeSelf;
            // return gameObject.activeInHierarchy;
        }
        set
        {
            if (value == false)
            {
                CloseUIForm();
            }
            else
            {
                if (!gameObject.activeSelf)
                    OpenUIForm("CatGroupView");
            }


        }
    }
    public void UnPackGroupInfo(object data)
    {
       

         JsonData cats = (JsonData)data;
        int count = (int)cats["count"];
        int groupId=(int)cats["groupid"];
        Debug.Log(count);
        List<CatInGroupInfoVO> changeGroup;
        if (groupId == (int)CatGroup.emptyGroup)
        {
            changeGroup = EmptyGroupCatData;
            changeGroup.Clear();
           
        }
        else
        {
            Debug.Log(111);
            changeGroup = ShowingCatGroupData;
            changeGroup.Clear();     
        }

        for (int i = 0; i < count; i++)
        {
            JsonData catInfo = cats[i];
            CatInGroupInfoVO catInGroupInfoVO = new CatInGroupInfoVO();
            catInGroupInfoVO.StoreId = (int)catInfo["id"];
            catInGroupInfoVO.CatTypeId = (int)catInfo["cattypeid"];
            catInGroupInfoVO.Name = (string)catInfo["catName"];
            catInGroupInfoVO.Type = (int)catInfo["captaintype"];
            catInGroupInfoVO.Level = (int)catInfo["lv"];
            catInGroupInfoVO.Power = (int)catInfo["power"];
            catInGroupInfoVO.GroupId = (int)catInfo["groupId"];
            catInGroupInfoVO.Attribute = (string)catInfo["attribute"];
            catInGroupInfoVO.MemberSlot = (string)catInfo["members"];
            catInGroupInfoVO.About = (string)catInfo["about"];
            changeGroup.Add(catInGroupInfoVO);

        }

      


    }

    public Transform centerGroupPanel;
    public Transform bottomGroupPanel;
    public GameObject catInGroupPrefab;

    public Transform selectCatShowingAnchor;
     GameObject selectCatShowingOb;


    public GameObject dismissField;

    public Vector3 createFromLeft;

    public Vector3 createFromLeftTop;

    public Vector3 createFromLeftBottom;


    float leftTopIntervalX = 160;

    float leftTopIntervalY = -100;

    float leftIntervalX = 100;

    float leftBottomIntervalX = 130;



    public Button onGroupLeftBtn;
    public Button onGroupRightBtn;
    public Button emptyGroupLeftBtn;
    public Button emptyGroupRightBtn;
    public Button closeBtn;

    public Text catName;
    public Text level;
    public Text energy;
    public Text introduce;
    public Text catGroupName;

    List<string> memberstrs;

    List<string> groupNameList;


    protected override void Awake()
    {
        base.Awake();
        //定义本窗体的性质(默认数值，可以不写)
        base.CurrentUIType.UIForms_Type = UIFormType.PopUp;
        base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.HideOther;
        base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;


         catModelLayer= LayerMask.NameToLayer("CatInGroupLayer");

        memberstrs = new List<string>();

        groupNameList = new List<string>();

        catName.enabled = false;

        level.enabled = false;
        energy.enabled = false;
        introduce.enabled = false;

       ShowingCatGroupData = new List<CatInGroupInfoVO>();

        EmptyGroupCatData = new List<CatInGroupInfoVO>();

       onGroupShowingObj = new List<GameObject>();

         nonGroupShowingObj = new List<GameObject>();


    }





    /// <summary>
    /// 显示某一组猫内容 
    /// </summary>
    /// <param name="data"></param>
    public void ShowCatGroupInfo(object data)
    {
        JsonData cats = (JsonData)data;
        int groupId = (int)cats["groupid"];
        Debug.Log(groupId);
        UnPackGroupInfo(data);  
     
    }


    public void OpenCatGroupView()
    {
       
        IsInvoke = true;
        catName.enabled = false;

        level.enabled = false;
        energy.enabled = false;
        introduce.enabled = false;


        JsonData type = new JsonData();

        type["type"] = CatGroupViewMediator.CAT_GROUP_TITLE;
        AppFacade.Instance.SendNotification(NotiConst.GET_CAT_GROUP_TITLE, type);

        InitCatGroupView();
        RefreshShowingGroupCat();
        RefreshEmptyGroupCat();
    }
        
    public void InitCatGroupView()
    {
      
        groupPageId =1;
        nonGroupPageId = 0;
        JsonData groupId = new JsonData();
        groupId["id"] = groupPageId;
        //显示某一猫组信息；可供刷新用
        AppFacade.GetInstance().SendNotification(NotiConst.GET_CAT_GROUP_INFO, groupId);

        JsonData emptyId = new JsonData();

        emptyId["id"] =(int)CatGroup.emptyGroup;
        //显示某一猫组信息；可供刷新用
        AppFacade.GetInstance().SendNotification(NotiConst.GET_CAT_GROUP_INFO, emptyId);    
    }

    public void RefreshCatGroupViewAfterSwitch(object data)
    {
        JsonData groupId = new JsonData();
        groupId["id"] = groupPageId;
        //显示某一猫组信息；可供刷新用
        AppFacade.GetInstance().SendNotification(NotiConst.GET_CAT_GROUP_INFO, groupId);

        JsonData emptyId = new JsonData();

        emptyId["id"] = (int)CatGroup.emptyGroup;
        //显示某一猫组信息；可供刷新用
        AppFacade.GetInstance().SendNotification(NotiConst.GET_CAT_GROUP_INFO, emptyId);


        RefreshShowingGroupCat();
        RefreshEmptyGroupCat();

    }

    public void RefreshCatGroupViewAfterDelete(object data)
    {
        JsonData showGroup = new JsonData();
        showGroup["id"] = 5;
        //显示某一猫组信息；可供刷新用
        AppFacade.Instance.SendNotification(NotiConst.GET_CAT_GROUP_INFO, showGroup);
        RefreshEmptyGroupCat();
    }


    // Use this for initialization
    void Start()
    {


        onGroupLeftBtn.onClick.AddListener(OnGroupLeftBtnClick);
        onGroupRightBtn.onClick.AddListener(OnGroupRightBtnClick);

        emptyGroupLeftBtn.onClick.AddListener(OnNonGroupLeftBtnClick);
        emptyGroupRightBtn.onClick.AddListener(OnNonGroupRightBtnClick);
        closeBtn.onClick.AddListener(OnCloseBtnOn);
       

    }

    // Update is called once per frame
    void Update()
    {

    }


    void RefreshShowingGroupCat()
    {

        if (groupPageId < 5)
        {
            dismissField.SetActive(false);
            catGroupName.text = groupNameList[groupPageId - 1];

            foreach (GameObject o in onGroupShowingObj)
            {
                Destroy(o);
            }

            onGroupShowingObj.Clear();
            if (groupPageId == 4)
            {
                for (int i = 0; i < 8; i++)
                {
                    GameObject o = (GameObject)Instantiate(catInGroupPrefab, Vector3.zero, Quaternion.identity);
                    o.transform.parent = centerGroupPanel;
                    o.transform.localPosition = Vector3.zero;
                    o.transform.GetChild(0).localPosition = createFromLeftTop + new Vector3(i % 4 * leftTopIntervalX, i / 4 * leftTopIntervalY, -20);
                    o.transform.GetChild(1).localPosition = createFromLeftTop + new Vector3(i % 4 * leftTopIntervalX, i / 4 * leftTopIntervalY, 0);

                    o.transform.GetChild(1).tag = TagName.CATGROUPCATBG;

                    o.transform.localScale = Vector3.one;
                    CatInGroupItem item = o.GetComponent<CatInGroupItem>();
                    if (i < ShowingCatGroupData.Count)
                    {
                        int id = ShowingCatGroupData[i].CatTypeId;
                        GameObject cat = CatPool.GetInstance().GetCatPool(id).CreateObject(Vector3.zero);
                        cat.layer = catModelLayer;
                        cat.transform.SetChildLayer(catModelLayer);
                        cat.AddComponent<EmployeeRandomAnimation>();
                        item.catInGroupInfo = ShowingCatGroupData[i];

                        item.Init();
                        cat.AddComponent<Cat3DModelInGroup>().infoVO = ShowingCatGroupData[i];
                      
                        cat.transform.tag = TagName.CATINGROUP;
                        cat.transform.parent = item.catTransform;
                        cat.transform.localPosition = new Vector3(0, -0.5f, 0);
                        cat.transform.rotation = item.catTransform.transform.rotation;
                        cat.transform.localScale = Vector3.one;
                        o.transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
                    }
                    else
                    {
                        o.transform.GetChild(1).GetComponent<BoxCollider>().enabled = true;
                        item.attributeLogo.enabled = false;
                        item.groupId = groupPageId;
                        item.Init();
                        // item.GetComponent<BoxCollider>().enabled = false;
                    }
                    onGroupShowingObj.Add(o);
                }
            }
            else

            { 
               // Debug.Log(ShowingCatGroupData[0].GroupId);
            Debug.Log(ShowingCatGroupData.Count);
                if (ShowingCatGroupData.Count > 0)
                {
                    List<CatInGroupInfoVO> LeftCatGroupData = new List<CatInGroupInfoVO>();

                    for (int i = 0; i < ShowingCatGroupData.Count; i++)
                    {
                        LeftCatGroupData.Add(ShowingCatGroupData[i]);
                    }


                        CatInGroupInfoVO captain = null;
                    int num = 0;

                    for (int i = 0; i < LeftCatGroupData.Count; i++)
                    {
                        if (LeftCatGroupData[i].Type == 1)
                        {
                            captain = ShowingCatGroupData[i];
                            num = CheckMemberNumFromSlot(captain.MemberSlot);
                            LeftCatGroupData.Remove(captain);
                            break;
                        }
                    }


                    Debug.Log(num);
                    GameObject o = (GameObject)Instantiate(catInGroupPrefab, Vector3.zero, Quaternion.identity);

                    o.transform.parent = centerGroupPanel;
                    o.transform.localPosition = Vector3.zero;

                    o.transform.GetChild(0).localPosition = createFromLeft + new Vector3(0, 0, -20);
                    o.transform.GetChild(1).localPosition = createFromLeft;

                    o.transform.GetChild(1).tag = TagName.CATGROUPCATBG;

                    o.transform.localScale = Vector3.one;

                    CatInGroupItem item = o.GetComponent<CatInGroupItem>();
                    int id = captain.CatTypeId;
                    GameObject cat = CatPool.GetInstance().GetCatPool(id).CreateObject(Vector3.zero);
                    cat.layer = catModelLayer;
                    cat.transform.SetChildLayer(catModelLayer);
                    cat.transform.tag = TagName.CATINGROUP;
                    cat.AddComponent<EmployeeRandomAnimation>();
                  
                    cat.AddComponent<Cat3DModelInGroup>().infoVO = captain;

                    cat.transform.parent = item.catTransform;
                    cat.transform.localPosition = new Vector3(0, -0.5f, 0);
                    cat.transform.rotation = item.catTransform.transform.rotation;
                    cat.transform.localScale = Vector3.one;
                    o.transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;

                    item.catInGroupInfo = captain;

                    item.Init();
                    onGroupShowingObj.Add(o);

                  

                    int offset = 0;
                    for (int i = 0; i < LeftCatGroupData.Count; i++)
                    {

                        for (int j = 0; j < memberstrs.Count; j++)
                        {

                            if (memberstrs[j] == LeftCatGroupData[i].Attribute)
                            {
                                o = (GameObject)Instantiate(catInGroupPrefab, Vector3.zero, Quaternion.identity);
                                o.transform.parent = centerGroupPanel;
                                o.transform.localPosition = Vector3.zero;
                                o.transform.GetChild(0).localPosition = createFromLeft + new Vector3((i + 1) * leftIntervalX, 0, -20);
                                o.transform.GetChild(1).localPosition = createFromLeft + new Vector3((i + 1) * leftIntervalX, 0, 0);
                                o.transform.GetChild(1).tag = TagName.CATGROUPCATBG;
                                o.transform.localScale = Vector3.one;
                                item = o.GetComponent<CatInGroupItem>();
                                id = LeftCatGroupData[i].CatTypeId;
                                cat = CatPool.GetInstance().GetCatPool(id).CreateObject(Vector3.zero);

                                cat.layer = catModelLayer;
                                cat.transform.SetChildLayer(catModelLayer);
                                cat.transform.tag = TagName.CATINGROUP;
                                cat.AddComponent<EmployeeRandomAnimation>();

                                cat.AddComponent<Cat3DModelInGroup>().infoVO = LeftCatGroupData[i];
                                
                                cat.transform.parent = item.catTransform;
                                cat.transform.localPosition = new Vector3(0, -0.5f, 0);
                                cat.transform.rotation = item.catTransform.transform.rotation;
                                cat.transform.localScale = Vector3.one;
                                o.transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;

                                item.catInGroupInfo = LeftCatGroupData[i];

                                item.Init();
                                onGroupShowingObj.Add(o);
                                memberstrs.Remove(memberstrs[j]);
                                break;
                            }

                        }
                        if (i == LeftCatGroupData.Count - 1)
                        {
                            offset = i;
                        }
                    }


                    if (LeftCatGroupData.Count == 0)
                    {
                        offset = 0;
                    }
                    else
                    {
                        offset++;
                    }


                    Debug.Log(offset);
                    for (int i = 0; i < memberstrs.Count; i++)
                    {
                        o = (GameObject)Instantiate(catInGroupPrefab, Vector3.zero, Quaternion.identity);
                        o.transform.parent = centerGroupPanel;
                        o.transform.localPosition = Vector3.zero;
                        o.transform.GetChild(0).localPosition = createFromLeft + new Vector3((i + offset + 1) * leftIntervalX, 0, -20);
                        o.transform.GetChild(1).localPosition = createFromLeft + new Vector3((i + offset + 1) * leftIntervalX, 0, 0);
                        o.transform.GetChild(1).tag = TagName.CATGROUPCATBG;
                        o.transform.localScale = Vector3.one;
                        item = o.GetComponent<CatInGroupItem>();

                        o.transform.GetChild(1).GetComponent<BoxCollider>().enabled = true;

                        CatInGroupInfoVO catInGroupInfoVO = new CatInGroupInfoVO();

                        catInGroupInfoVO.GroupId = groupPageId;
                        catInGroupInfoVO.Attribute = memberstrs[i];
                        catInGroupInfoVO.Type = 2;


                        item.catInGroupInfo = catInGroupInfoVO;

                        item.Init();
                        onGroupShowingObj.Add(o);

                    }




                }
                else
                {
                    GameObject o = (GameObject)Instantiate(catInGroupPrefab, Vector3.zero, Quaternion.identity);

                    o.transform.parent = centerGroupPanel;
                    o.transform.localPosition = Vector3.zero;

                    o.transform.GetChild(0).localPosition = createFromLeft + new Vector3(0, 0, -20);
                    o.transform.GetChild(1).localPosition = createFromLeft;

                    o.transform.GetChild(1).tag = TagName.CATGROUPCATBG;

                    o.transform.localScale = Vector3.one;

                    CatInGroupItem item = o.GetComponent<CatInGroupItem>();

                    o.transform.GetChild(1).GetComponent<BoxCollider>().enabled = true;


                    CatInGroupInfoVO catInGroupInfoVO = new CatInGroupInfoVO();
                    catInGroupInfoVO.GroupId = groupPageId;

                    catInGroupInfoVO.Type = 1;


                    item.empty = true;

                    item.catInGroupInfo = catInGroupInfoVO;

                    item.Init();
                    onGroupShowingObj.Add(o);
                }
            }

        }
        else
        {
            catGroupName.text = "解雇";
            foreach (GameObject o in onGroupShowingObj)
            {
                Destroy(o);
            }
            dismissField.SetActive(true);


        }

       


    }

    public void GetGroupTile(object data)
    {


        JsonData groupInfo = (JsonData)data;
        int count =(int) groupInfo["count"];


        for (int i = 0; i < count; i++)
        {
            groupNameList.Add(groupInfo[i.ToString()]["name"].ToString());
        }

        groupNameList.Add("解雇");

        for (int i = 0; i < groupNameList.Count; i++)
        {

        }

    }


     int CheckMemberNumFromSlot(string memberslot)
    {
        memberstrs.Clear();
        
        string[] strs = memberslot.Split(',');
        for (int i = 0; i < strs.Length; i++)
        {
            memberstrs.Add(strs[i]);
        }

    


        return memberstrs.Count;
    }


    void  RefreshEmptyGroupCat()
    {
        foreach (GameObject o in nonGroupShowingObj)
        {
            Destroy(o);
        }

        nonGroupShowingObj.Clear();
       
        for (int i = 0; i < 4; i++)
        {                       
                GameObject o = (GameObject)Instantiate(catInGroupPrefab, Vector3.zero, Quaternion.identity);
                o.transform.parent = bottomGroupPanel;
                o.transform.localPosition = Vector3.zero;      
            o.transform.GetChild(0).localPosition = createFromLeftBottom + new Vector3(i * leftBottomIntervalX, 0, -20);
            o.transform.GetChild(1).localPosition = createFromLeftBottom + new Vector3(i * leftBottomIntervalX, 0, 0);

            o.transform.GetChild(1).tag = TagName.CATGROUPCATBG;

            o.transform.localScale = Vector3.one;
            CatInGroupItem item = o.GetComponent<CatInGroupItem>();          
           
            if (nonGroupPageId + i < EmptyGroupCatData.Count)
            {
                CatInGroupInfoVO catInGroupInfoVO = EmptyGroupCatData[nonGroupPageId + i];
             
                item.catInGroupInfo = catInGroupInfoVO;
                item.Init();
                int id = EmptyGroupCatData[nonGroupPageId + i].CatTypeId;
                GameObject cat = CatPool.GetInstance().GetCatPool(id).CreateObject(Vector3.zero);
                cat.layer = catModelLayer;
                cat.transform.SetChildLayer(catModelLayer);
                cat.transform.tag = TagName.CATINGROUP;

                cat.AddComponent<EmployeeRandomAnimation>();
                cat.AddComponent<Cat3DModelInGroup>().infoVO = catInGroupInfoVO;
                cat.transform.parent = item.catTransform;
                cat.transform.localPosition = new Vector3(0, -0.5f, 0);
                cat.transform.rotation = item.catTransform.transform.rotation;
                cat.transform.localScale = Vector3.one;
                o.transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                o.transform.GetChild(1).GetComponent<BoxCollider>().enabled = true;
                item.attributeLogo.enabled = false;
                item.groupId = 5;
                item.Init();
            }
          
            nonGroupShowingObj.Add(o);
        }

    }

    public void ShowSelectCat3DModel(int catId)
    {
        Destroy(selectCatShowingOb);
        selectCatShowingOb = CatPool.GetInstance().GetCatPool(catId).CreateObject(Vector3.zero);
        selectCatShowingOb.AddComponent<EmployeeRandomAnimation>();
        selectCatShowingOb.layer = catModelLayer;
        selectCatShowingOb.transform.SetChildLayer(catModelLayer);
        selectCatShowingOb.transform.parent = selectCatShowingAnchor;
        selectCatShowingOb.transform.localPosition = new Vector3(0, -80f, 0);
        selectCatShowingOb.transform.localScale = new Vector3(120, 120, 120);
        selectCatShowingOb.transform.localRotation = Quaternion.identity;

    }


    #region 事件回调
    public void OnGroupLeftBtnClick()
    {
        if (groupPageId == 1)
        {
            groupPageId = 5;
        }
        else
        {
            groupPageId--;
        }
        if (groupPageId < 5)
        {
            JsonData groupId = new JsonData();

            groupId["id"] = groupPageId;

            //显示某一猫组信息；可供刷新用
            AppFacade.GetInstance().SendNotification(NotiConst.GET_CAT_GROUP_INFO, groupId);
            
        }
       
        RefreshShowingGroupCat();
    }


    


    public void OnGroupRightBtnClick()
    {

        if (groupPageId == 5)
        {
            groupPageId = 1;
        }
        else
        {
            groupPageId++;
        }
        if (groupPageId < 5)
        {
            JsonData groupId = new JsonData();

            groupId["id"] = groupPageId;

            //显示某一猫组信息；可供刷新用
            AppFacade.GetInstance().SendNotification(NotiConst.GET_CAT_GROUP_INFO, groupId);
          
        }
        
        RefreshShowingGroupCat();
    }



    public void OnNonGroupLeftBtnClick()
    {
        count = EmptyGroupCatData.Count;
        if (nonGroupPageId == 0)
        {
            return;
        }
        else
        {
            nonGroupPageId -= 4;
        }
        RefreshEmptyGroupCat();

    }


    public void OnNonGroupRightBtnClick()
    {
        count = EmptyGroupCatData.Count;
        if (nonGroupPageId+4 >= count)
        {
            return;
        }
        else
        {
            nonGroupPageId += 4;
        }
        RefreshEmptyGroupCat();
    }

    public void OnCloseBtnOn()
    {
        IsInvoke = false;
        if (selectCatShowingOb != null)
        {
            Destroy(selectCatShowingOb);
        }
        AppFacade.GetInstance().SendNotification(NotiConst.CAT_GROUP_CLOSE);
    }


    #endregion
}
