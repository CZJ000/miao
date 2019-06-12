using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Global;
using UnityEngine.UI;
using SUIFW;

public class BattleView : BaseUIForm
{

    public GameObject Grid;
    public Text TitleByQueneName;
    public Button BtnCommit;
    public Button BtnLeft;
    public Button BtnRight;
    public GameObject BGPlane;
    public GameObject ChangeBtn;
    public GameObject HudTextPrefabs;
    public GameObject HudImagePrefabs;
    public GameObject HudSpriteAttribute;
    public GameObject TimeFlag;
    public Text TitleByInformation;

    private int CurrentGroupInfo = 0;

    public Text ReadTimeUI;
    public Text NotiFyToolTip;

 [HideInInspector]   public Text[] GridTypeText;
 [HideInInspector]  public Image[] GridAttributeImage;

    private List<string> TitleList = new List<string>();
    private List<GameObject> prefabslist = new List<GameObject>();
    private List<GameObject> prefabslistAI = new List<GameObject>();

    
    public bool iscommit { get; set; }

    protected override void Awake()
    {
        base.Awake();
        //定义本窗体的性质(默认数值，可以不写)
        base.CurrentUIType.UIForms_Type = UIFormType.PopUp;
        base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
        base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;

    }

    void Start()
    {
        BtnCommit.onClick.AddListener(delegate ()
        {
            ChooseQueneEnd();
            iscommit = true;
        });
        BtnLeft.onClick.AddListener(delegate ()
        {
            string currentext = TitleByQueneName.text;
            int index = TitleList.IndexOf(currentext) - 1;
            if (index == -1)
            {
                index = TitleList.Count - 1;
            }
            TitleByQueneName.text = TitleList[index];
            CurrentGroupInfo = index;
            GetCatCurrentGroupData();
        });
        BtnRight.onClick.AddListener(delegate ()
        {
            string currentext = TitleByQueneName.text;
            int index = TitleList.IndexOf(currentext) + 1;
            if (index == TitleList.Count)
            {
                index = 0;

            }
            TitleByQueneName.text = TitleList[index];
            CurrentGroupInfo = index;
            GetCatCurrentGroupData();
        });



        TitleList.Add("猫耳队");
        TitleList.Add("猫爪队");
        TitleList.Add("猫毛队");
        Init();
       



    }

    void Init()
    {
        iscommit = false;
        ChangeBtn.SetActive(false);
        ReadTimeUI.gameObject.SetActive(false);
        NotiFyToolTip.gameObject.SetActive(false);
        TitleByQueneName. text = TitleList[0];
    }

    


    /// <summary>
    /// 得到猫分组信息  
    /// </summary>
    void GetCatCurrentGroupData()
    {
        JsonData data = new JsonData();
        data["id"] = ++CurrentGroupInfo;                           //对应索引相差1；
        Debug.Log("当前选择组" + CurrentGroupInfo);
        AppFacade.GetInstance().SendNotification(NotiConst.GET_BATTLE_CAT_GROUP_INFO, data);
    }

    void GetCatRandomGroupData()
    {
        int index = 3;
        JsonData data = new JsonData();
        data["id"] = Random.Range(1, 4);                          // 随机索引；
        
        AppFacade.GetInstance().SendNotification(NotiConst.GET_BATTLE_RANDOM_CAT_INFO, data);
        while(prefabslistAI.Count<=0&&index>0)
        {
            index--;
            data["id"] = Random.Range(1, 4);                          // 随机索引；
            
            AppFacade.GetInstance().SendNotification(NotiConst.GET_BATTLE_RANDOM_CAT_INFO, data);
        }
    }
   

    /// <summary>
    /// 展示当前的猫分组
    /// </summary>
    public void ShowCurrentCatGroup(object data)
    {
        if (prefabslist.Count > 0)
        {
           
            foreach (GameObject temp in prefabslist)
            {
                int destroyid = temp.GetComponent<BattleCatInfo>().cattypeid;
                CatPool.GetInstance().DestructSinglePool(destroyid);
            }
        }

     
        prefabslist.Clear();                              //每次展示队伍前 ，清理上次的展示队伍
        JsonData cats = (JsonData)data;
        int count = (int)cats["count"];
       
        for (int i = 0; i < count; i++)
        {
            
            JsonData catinfo = cats[i];
            int cattype= (int)catinfo["cattypeid"];
           
            prefabslist.Add(CatPool.GetInstance().GetCatPool(cattype).CreateObject(Grid.transform.GetChild(i).transform.position));
            BattleCatInfo battlecatinfo = prefabslist[i].GetComponent<BattleCatInfo>();
           
            if (battlecatinfo==null)
            {
                battlecatinfo= prefabslist[i].AddComponent<BattleCatInfo>();
            }
            battlecatinfo.about = (string)catinfo["about"];
            battlecatinfo.catCaptaintype = (int )catinfo["captaintype"] ;
         
            battlecatinfo.name = (string)catinfo["catName"];
            battlecatinfo.catname= (string)catinfo["catName"];
            battlecatinfo.cattypeid = cattype;
            battlecatinfo.id = (int)catinfo["id"];
            battlecatinfo.cooldown = (int)catinfo["cooldown"];
            battlecatinfo.cachecooldown = (int)catinfo["cooldown"];
            battlecatinfo.attribute = (string)catinfo["attribute"];
            battlecatinfo.evo = (int)catinfo["evo"];
            battlecatinfo.power = (int)catinfo["power"];
            battlecatinfo.members = (string)catinfo["members"];
            battlecatinfo.ownexp = (int )catinfo["ownexp"];

            battlecatinfo.skillaniid = (int)catinfo["skillaniid"];

            battlecatinfo.AttackType = (int)catinfo["attack_aniid"];
            battlecatinfo.skillid = (int)catinfo["skill_id"];

            battlecatinfo.roletype = RoleType.Player;
            battlecatinfo.ischaos = false;
            // prefabslist[i].transform.parent.gameObject.layer = 5;



         
                prefabslist[i].AddComponent<BattleDisplayAnimator>();
            
           

            prefabslist[i].layer = 5;
            prefabslist[i].transform.SetChildLayer(5);



            GridTypeText[i].text = battlecatinfo.catCaptaintype.ToString();
            GridAttributeImage[i].color = MiaoBoxTool.SwitchColor(battlecatinfo.attribute);

            prefabslist[i].transform.position = new Vector3(prefabslist[i].transform.position.x, -10f, -5f);
            prefabslist[i].transform.eulerAngles = new Vector3(0, 180, 0);
            prefabslist[i].transform.localScale = new  Vector3 (10,10,10);
           
          

        }
        

    }



    public void ShowRandomCatGroup(object data)
    {
       
        prefabslistAI.Clear();                              //每次展示队伍前 ，清理上次的展示队伍
        JsonData cats = (JsonData)data;
        int count = (int)cats["count"];
        for (int i = 0; i < count; i++)
        {

            JsonData catinfo = cats[i];
            int cattype = (int)catinfo["cattypeid"];

            prefabslistAI.Add(CatPool.GetInstance().GetCatPool(cattype).CreateObject(Vector3.zero));
            BattleCatInfo battlecatinfo = prefabslistAI[i].GetComponent<BattleCatInfo>();
            if (battlecatinfo == null)
            {
                battlecatinfo = prefabslistAI[i].AddComponent<BattleCatInfo>();
            }
            battlecatinfo.about = (string)catinfo["about"];
            battlecatinfo.catCaptaintype = (int)catinfo["captaintype"];
            battlecatinfo.name = (string)catinfo["catName"];
            battlecatinfo.catname = (string)catinfo["catName"];
            battlecatinfo.cattypeid = cattype;
            battlecatinfo.id = (int)catinfo["id"];
            battlecatinfo.cooldown = (int)catinfo["cooldown"];
            battlecatinfo.cachecooldown = (int)catinfo["cooldown"];
            battlecatinfo.skillaniid = (int)catinfo["skillaniid"];

            battlecatinfo.members = (string)catinfo["members"];
            battlecatinfo.attribute = (string)catinfo["attribute"];
            battlecatinfo.evo = (int)catinfo["evo"];
            battlecatinfo.power = (int)catinfo["power"];
            battlecatinfo.ownexp = (int)catinfo["ownexp"];

            battlecatinfo.skillid = (int)catinfo["skill_id"];
            battlecatinfo.AttackType = (int)catinfo["attack_aniid"];
            battlecatinfo.roletype = RoleType.Enemy;
            battlecatinfo.ischaos = false;
            
            BattelCat battelCat = prefabslistAI[i].GetComponent<BattelCat>();
            if (battelCat == null)
            {
                prefabslistAI[i].AddComponent<BattelCat>();
            }
            prefabslistAI[i].layer = 0;
            prefabslistAI[i].transform.SetChildLayer(0);  

            //  prefabslistAI[i].SetActive(false);
            prefabslistAI[i].transform.eulerAngles = Vector3.zero;
            prefabslistAI[i].transform.localScale = Vector3.one;
            BattleDisplayAnimator battledisplayaniam = prefabslistAI[i].GetComponent<BattleDisplayAnimator>();
            if (battledisplayaniam==null)
            {
                prefabslistAI[i].AddComponent<BattleDisplayAnimator>();
            }
            Hudparticipant ht = prefabslistAI[i].GetComponent<Hudparticipant>();
            if (ht==null)
            {
                ht=  prefabslistAI[i].AddComponent<Hudparticipant>();
                ht.Textprefabs = HudTextPrefabs;
                ht.ImagePrefbas = HudImagePrefabs;
                ht.AttributeSprite = HudSpriteAttribute;
               
              
                
               
            }

          
        }
    }

   
    /// <summary>
    /// 重复利用显示面板的模型 将其重置 
    /// </summary>
    private void ResetPrefabs()
    {
        
        if (prefabslist != null && prefabslist.Count > 0)
        {
            foreach (GameObject temp in prefabslist)
            {

                temp.layer = 0;
                temp.transform.SetChildLayer(0);
                 
                temp.transform.eulerAngles = Vector3.zero;
                temp.transform.localScale = Vector3.one;
                Hudparticipant ht = temp.GetComponent<Hudparticipant>();
                if (ht == null)
                {
                    Debug.Log(temp.activeInHierarchy);
                    Debug.Log("ResetPrefabs");
                    ht = temp.AddComponent<Hudparticipant>();
                    ht.Textprefabs = HudTextPrefabs;
                    ht.ImagePrefbas = HudImagePrefabs;
                    ht.AttributeSprite = HudSpriteAttribute;
                 //   ht.HudSpriteForWorld.CreatOrShowAttributSprite( temp.transform, new Vector3(0, 0.1f, 0f), ht.AttributeSprite, ht.GetComponent<BattleCatInfo>().attribute);

                }

                Debug.Log(temp.activeInHierarchy);

                BattelCat battelCat = temp.GetComponent<BattelCat>();
                if (battelCat == null)
                {
                    temp.AddComponent<BattelCat>();
                }



            }
        }
        else
        {
            Debug.Log("玩家队伍是空的");
        }

    }

    
    /// <summary>
    /// 隐藏 Quene UI面板或者展示
    /// </summary>
    /// <param name="state"></param>
    public void QueneUIHideOrShow(bool state)
    {
        BGPlane.SetActive(state);

    }

    /// <summary>
    /// 隐藏  (调整Quene按钮)或者展示
    /// </summary>
    /// <param name="state"></param>
    public void ChangeQueneBtnUIHideOrshow(bool state)
    {
        ChangeBtn.SetActive(state);
        if (state)
        {
            //显示所有猫；
            CreatPoint.Instance.ShowAllCats();
        }
        else
        {
            //隐藏不在最前的猫；
            CreatPoint.Instance.HideCat();
        }
    }
    /// <summary>
    ///初始化选择队伍
    /// </summary>
    public void  BattleInitChooseQuene()
    {
        CurrentGroupInfo = 0;
        TitleByQueneName.text = TitleList[0];
        GetCatCurrentGroupData();
        QueneUIHideOrShow(true);
      
       
    }

    /// <summary>
    /// 初始化AI
    /// </summary>
    public void BattleInitCreatAi()
    {

       GetCatRandomGroupData();
        if (prefabslistAI.Count <= 0|| prefabslistAI == null)
        {
            Debug.LogError("队伍为空");
        }else
        {
            
            CreatPoint.Instance.Creatprefabs(prefabslistAI, true);
        }
       
    }


    /// <summary>
    /// 结束队伍选择
    /// </summary>
    public void ChooseQueneEnd()
    {
        if (iscommit)
        {
            iscommit = false;
            return;
        }
        ResetPrefabs();
        ReadTimeUI.gameObject.SetActive(false);    //重置记时
        QueneUIHideOrShow(false);
        CreatPoint.Instance.Creatprefabs(  prefabslist, false);
        Debug.Log(CurrentGroupInfo);
      //  TitleList.Remove(TitleList[--CurrentGroupInfo]);
        BattleSceneManage.Instance.setCurretnState(BattleSceneManage.BattleState.ImproveCat);
    }


     

    /// <summary>
    /// 玩家提示消息
    /// </summary>
    /// <param name="text"></param>
    /// <param name="inteval"></param>
    public void NotifyForPlayer( BattleUIVO UIVO)
    {
        NotiFyToolTip.gameObject.SetActive(true);
        NotiFyToolTip.text = UIVO.TextContext;
        NotiFyToolTip.color = Color.black;
        StartCoroutine(MiaoBoxTool.FadeText(NotiFyToolTip, UIVO.IntervalTime, new Color(UIVO.TextColor.r, UIVO.TextColor.g, UIVO.TextColor.b,0) ));
       
    }


    public void  ShowReadTime(BattleUIVO UIVO)
    {
        StartCoroutine(ShowReadTimeIE(UIVO));

    }
    /// <summary>
    /// 时间倒计时
    /// </summary>
    /// <param name="inteval"></param>
    /// <returns></returns>
    public IEnumerator ShowReadTimeIE(BattleUIVO UIVO)
    {
         
        float intervaltime = UIVO.IntervalTime;
        TimeFlag.SetActive(true);
        ReadTimeUI.gameObject.SetActive(true );
      
        TimeFlag.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        yield return new WaitForFixedUpdate();
        StartCoroutine(MiaoBoxTool.ScaleChange(TimeFlag  , 5f, 1f));
        while (intervaltime > 0)
        {
            if (!ReadTimeUI.gameObject.activeSelf)
            {
                break;
            }

            ReadTimeUI.color = new Color(0,0,0,1);
            ReadTimeUI.text = intervaltime.ToString();
            StartCoroutine(MiaoBoxTool.FadeText(ReadTimeUI, 1f, new Color(0, 0, 0, 0) ));
            --intervaltime;
            yield return new WaitForSeconds(1f);
            yield return new WaitForFixedUpdate();
        }
      
       
        
        StartCoroutine(MiaoBoxTool.ScaleChange(TimeFlag, 5f, 0.001f));
        yield return new WaitForSeconds(1f);
        TimeFlag.SetActive(false );



    }
    /// <summary>
    /// 对象浮体信息的展示
    /// </summary>
    /// <param name="hudpartcipant"></param>
    /// <param name="text"></param>
    /// <param name="color"></param>
    /// <param name="delaytime"></param>
    public void ShowHUDText( BattleUIVO UIVO)
    {
        
        HUDMiaoText ht = UIVO.hudparticipant.HudMiaoText;
        
        if (ht != null)
        {

         
          float y = UIVO.hudparticipant.transform.localScale.y* UIVO.yInterval;                 //位置偏移量
          ht.offset = new Vector3(0f, y, 0f);
          ht.  AddText(UIVO.TextContext  , UIVO.TextColor, UIVO.hudparticipant.transform ,UIVO.IntervalTime);

        }



      
    }


    /// <summary>
    /// 对浮体图标的展示
    /// </summary>
    /// <param name="UIVO"></param>
    public void ShowHUdSprite(BattleUIVO UIVO )
    {
        HUDMiaoImage ht = UIVO.hudparticipant.HudMiaoImage;

        if (ht != null)
        {
            
            float y = UIVO.hudparticipant.transform.localScale.y*2f;               //位置偏移量
            ht.offset = new Vector3(0.5f, y, 0f);
            ht.AddImage("ui_emoji/emoji_jingtan", UIVO.hudparticipant.transform, 2f);

        }

    }

    
}