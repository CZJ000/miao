using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using Global;

public class BattleSceneManage : Singleton<BattleSceneManage> {
    public enum BattleState
    {
        Begin,
        ChooseQuene,
        ImproveCat,
        Battleing,
        Battleend
 
    }
   
    private BattleState CurrentBattlstate;
    private RoleType abilityattckstate ;
    private RoleType cacheabilityattckstate;
    private RoleType wintype;



  
    private List<GameObject> prefabslist = new List<GameObject>();
    private List<GameObject> Aiprefabslist = new List<GameObject>();

    public int EnemyCaptainCount { get; set; }
    public int PlayerCaptainCount { get; set; }
    public List<string> EnemyMembers = new List<string>();
    public List<string> PlayerMembers = new List<string>();

     
    //事件  

    public delegate void AllCooldown ();
    public event AllCooldown AllCooldownEvent ;
    

    private float PlayerHp  = 300;
    private float cachePlayerhp = 0;

    private float EnemyHp   = 300;
    private float cacheEnemyhp = 0;

    private float PlayerLimit = 0; 
    private float EnemyLimit = 0;

    private int  PlayerScore=0;
    private int  EnemyScore = 0;

    private Dictionary<int, BattleCatInfo> EmDefenselist = new Dictionary<int, BattleCatInfo>();
    private Dictionary<int, BattleCatInfo> PlDefenselist = new Dictionary<int, BattleCatInfo>();

    private bool iswin = false;
    private bool isEnemyGetScore = false;
    private bool isPlayerGetScore = false;
    void Start()
    {
        cacheEnemyhp = EnemyHp;
        cachePlayerhp = PlayerHp;
        CurrentBattlstate = BattleState.Begin;
        abilityattckstate =  RoleType.Player;
        cacheabilityattckstate = abilityattckstate;
        AppFacade.GetInstance().SendNotification(BattleInfoMediator.UPDATEPLAYERLIMIT, PlayerLimit%100);
        AppFacade.GetInstance().SendNotification(BattleInfoMediator.UPDATENEMYLIMIT, EnemyLimit % 100);
        StartCoroutine("UpdateBattle");
      
    } 
     IEnumerator  UpdateBattle()
    {   

        while(true )
        {
             
            switch (CurrentBattlstate)
            {
                case BattleState.Begin:
                   
                    Debug.Log(" 游戏 开始 2秒后开始选择队伍 ");
                    BattleUIVO UIVO = new BattleUIVO(" 游戏开始后开始选择队伍 ", Color.black, 2f);
                    //这一段要求取消
                    //AppFacade.getInstance.SendNotification(BattleMediator.NOTIFYFORPLAYER, UIVO);

                    yield return new WaitForSeconds(1f);

                    //变换摄像机 选择队伍状态
                    BattleCamera._instance.ChoosequeneChange();
                    AppFacade.getInstance.SendNotification(BattleMediator.BATTLE_INIT_QUENE);
                   
                    CurrentBattlstate = BattleState.ChooseQuene;
                    break;
                case BattleState.ChooseQuene:

                    Debug.Log("请在 5 秒内选择合适的队伍");
                     
                    //倒计时读秒
                    UIVO = new BattleUIVO(null, Color.black, 5f);
                    AppFacade.getInstance.SendNotification(BattleMediator.SHOWREADTIME, UIVO);
                    yield return new WaitForSeconds(5f); 
                    //加载选择队伍 AI队伍;
                    AppFacade.getInstance.SendNotification(BattleMediator.CHOOSEQUENEEND);
                    AppFacade.getInstance.SendNotification(BattleMediator.BATTLE_INIT_AI);
                    break;
                case BattleState.ImproveCat:
                   
                    yield return new WaitForSeconds(0.5f);                                               //缓冲 
                    AppFacade.getInstance.SendNotification(BattleMediator.SHOWCHANGRBTN, true );         //可以调整队伍 
                    InitConfig();                                                                        //得到队伍信息并排序 
                    CaptainConfig();
                    AllCooldownEvent();
                    UIVO = new BattleUIVO(null, Color.black, 3f);
                    AppFacade.getInstance.SendNotification(BattleMediator.SHOWREADTIME, UIVO);
                   
                    yield return new WaitForSeconds(3f); 
                    AppFacade.getInstance.SendNotification(BattleMediator.SHOWCHANGRBTN,false );         //关闭队伍调整
                    
                    CurrentBattlstate = BattleState.Battleing;
                    break;
                case BattleState.Battleing:
                   
                    UIVO = new BattleUIVO("开始战斗", Color.black, 1f);
                    //开始战斗也取消
                    //AppFacade.getInstance.SendNotification(BattleMediator.NOTIFYFORPLAYER, UIVO);
 
                                                                                                                   
                    BattleCamera._instance.BattleViewChange();                                           //变换摄像机 战斗状态 
                                                                                                         //   yield return new WaitForSeconds(1f);
                    StartCoroutine("battle");
                    StopCoroutine("UpdateBattle");
                   

                    break;
                case BattleState.Battleend:
                    ClearConfig();
                    yield return new WaitForSeconds(1f);
                    CurrentBattlstate = BattleState.ImproveCat;
                    break;
                default:
                    break;
            }
            yield return new WaitForFixedUpdate();
        }


    }

     IEnumerator  battle()
    {

        yield return new WaitForSeconds(1f);
        if (PlayerLimit>=500&& !isEnemyGetScore)
        {
            UserLimit(RoleType.Player);
            yield return new WaitForSeconds(5f);

        }
        if (EnemyLimit>=500&& !isPlayerGetScore)
        {
            UserLimit(RoleType.Enemy);
            yield return new WaitForSeconds(5f);
        }
        //缓冲时间

        //执行技能
        for (int i=0;i<prefabslist.Count;i++)
        {  
            if (prefabslist[i].GetComponent<BattelCat>().GetMs().GetCurrentState() != CatChaos._instance)
            {
                prefabslist[i].GetComponent<BattelCat>().GetMs().ChangeSate(CatSkill._instance);
            }
           
        }
        for (int i = 0; i < Aiprefabslist.Count; i++)
        {  
            if(Aiprefabslist[i].GetComponent<BattelCat>().GetMs().GetCurrentState() != CatChaos._instance)
            {
                Aiprefabslist[i].GetComponent<BattelCat>().GetMs().ChangeSate(CatSkill._instance);
            }
           
        }
        yield return new WaitForSeconds(1f);
        //执行防御
        for (int i = 0; i < prefabslist.Count; i++)
        {
            BattleCatInfo info = prefabslist[i].GetComponent<BattleCatInfo>();
            if (!info.ischaos&&info.isperpare()&&info.isactiveforcat()&&info.isdefense())
            {
                prefabslist[i].GetComponent<BattelCat>().GetMs().ChangeSate(CatDefense._instance);
               
               
            }

        }
        for (int i = 0; i < Aiprefabslist.Count; i++)
        {
            BattleCatInfo info = Aiprefabslist[i].GetComponent<BattleCatInfo>();
            if (!info.ischaos && info.isperpare()&&info.isactiveforcat()&&info.isdefense())
            {
                Aiprefabslist[i].GetComponent<BattelCat>().GetMs().ChangeSate(CatDefense._instance);
               
            }

        }
      

        // 还未设置GameOver


        int index = 0;
        int aiindex = 0;
           
            while(index<=prefabslist.Count-1||aiindex<=Aiprefabslist.Count-1)
            {
                switch (abilityattckstate)
                {
                    case   RoleType.Player:
                        
                        if (index>prefabslist.Count-1)
                        {
                            abilityattckstate = RoleType.Enemy;
                            break;
                        }
                       
                        BattleCatInfo info = prefabslist[index].GetComponent<BattleCatInfo>();
                        if(!info.ischaos&&info.isperpare()&&info.isactiveforcat( )&&info.isattack())
                        {

                      
                        prefabslist[index].GetComponent<BattelCat>().GetMs().ChangeSate(CatAttack._instance);
                            prefabslist[index].GetComponent<BattelCat>().GetMs().UpdateMs();

                        //处于攻击状态 让其一直做攻击状态的事；
                        yield return new WaitForSeconds(3f);

                       }
                        
                        index++;
                        abilityattckstate =  RoleType.Enemy;
                        yield return new  WaitForSeconds(0.02f);
                             if (isPlayerGetScore || isEnemyGetScore)
                              {
                                      StartCoroutine("RoundResult");
                              }
                    break;
                    case  RoleType.Enemy:
                        if (aiindex>Aiprefabslist.Count-1)
                        {
                            abilityattckstate =  RoleType.Player;
                            break;
                        }
                        BattleCatInfo ainfo= Aiprefabslist[aiindex].GetComponent<BattleCatInfo>();
                        if (!ainfo.ischaos && ainfo.isperpare() && ainfo.isactiveforcat()&&ainfo.isattack() )
                        {
                           Aiprefabslist[aiindex].GetComponent<BattelCat>().GetMs().ChangeSate(CatAttack._instance);
                       
                           Aiprefabslist[aiindex].GetComponent<BattelCat>().GetMs().UpdateMs();
                           yield return new WaitForSeconds(3f);
                    }
                       
                        aiindex++;
                        abilityattckstate = RoleType.Player;
                        yield return new WaitForSeconds(0.02f);
                    if (isPlayerGetScore || isEnemyGetScore)
                    {
                        StartCoroutine("RoundResult");
                    }
                    break;
                    default:
                        break;
                }
               
           
            yield return null;
           
        }
        yield return new WaitForSeconds(1f);

       
      
       BattleCamera._instance.batbackcq();
       yield return new WaitForSeconds(2f);
       setCurretnState(BattleState.Battleend);
       StartCoroutine("UpdateBattle");
       
       

    }
    public void  setCurretnState(BattleState state)
    {
        CurrentBattlstate = state;

    }


    /// <summary>
    /// 队伍初始配置 
    /// </summary>
    public void InitConfig()
    {
         
         
        //设置对象
        prefabslist.Clear();
        for(int i=0;i< CreatPoint.Instance.getprefabs().Count;i++)
        {
            prefabslist.Add(CreatPoint.Instance.getprefabs()[i]);
        }
        Aiprefabslist.Clear();
        for (int i = 0; i < CreatPoint.Instance.getaiprefabs().Count; i++)
        {
            Aiprefabslist.Add(CreatPoint.Instance.getaiprefabs()[i]);
        }


        //排序 
        for (int i = 0; i < prefabslist.Count; i++)
        {

            for (int j = 0; j < prefabslist.Count - i - 1; j++)
            {

                if (prefabslist[j].GetComponent<BattleCatInfo>().currentposid > prefabslist[j + 1].GetComponent<BattleCatInfo>().currentposid)
                {
                    GameObject temp = prefabslist[j + 1];
                    prefabslist[j + 1] = prefabslist[j];
                    prefabslist[j] = temp;
                }
            }

        }
        for (int i = 0; i < Aiprefabslist.Count; i++)
        {

            for (int j = 0; j < Aiprefabslist.Count - i - 1; j++)
            {

                if (Aiprefabslist[j].GetComponent<BattleCatInfo>().currentposid > Aiprefabslist[j + 1].GetComponent<BattleCatInfo>().currentposid)
                {
                    GameObject temp = Aiprefabslist[j + 1];
                    Aiprefabslist[j + 1] = Aiprefabslist[j];
                    Aiprefabslist[j] = temp;
                }
            }

        }

        //事件注册 
        Debug.Log(prefabslist.Count);
        for (int i = 0; i < prefabslist.Count; i++)
        {
            //添加cooldown事件
            AllCooldownEvent += prefabslist[i].GetComponent<BattleCatInfo>().UpdateCooldownInfo;
            BattleCatInfo info = prefabslist[i].GetComponent<BattleCatInfo>();
           
            info.DecreaseCooldown();

            ///显示底部属性
            Hudparticipant ht = prefabslist[i].GetComponent<Hudparticipant>();
            Debug.Log(prefabslist[i]);
            Debug.Log(prefabslist[i].activeInHierarchy);
            ht.HudSpriteForWorld.CreatOrShowAttributSprite(prefabslist[i].transform, new Vector3(0, 0.1f, 0f), ht.AttributeSprite, ht.GetComponent<BattleCatInfo>().attribute);


        }
        for (int i = 0; i < Aiprefabslist.Count; i++)
        {
            //添加cooldown事件
            AllCooldownEvent += Aiprefabslist[i].GetComponent<BattleCatInfo>().UpdateCooldownInfo;
            BattleCatInfo info = Aiprefabslist[i].GetComponent<BattleCatInfo>();
            info.DecreaseCooldown();
            ///显示底部属性
            Hudparticipant ht = Aiprefabslist[i].GetComponent<Hudparticipant>();
            ht.HudSpriteForWorld.CreatOrShowAttributSprite(Aiprefabslist[i].transform, new Vector3(0, 0.1f, 0f), ht.AttributeSprite, ht.GetComponent<BattleCatInfo>().attribute);

        }
    }

   /// <summary>
   /// 队长设置
   /// </summary>
    public void CaptainConfig()
    {
        
        int playerindex = 0;
        int enemyindex = 0;
        EnemyMembers.Clear();
        PlayerMembers.Clear();
        if(prefabslist==null)
        {
            prefabslist = CreatPoint.Instance.getprefabs();
        }
        if (Aiprefabslist==null)
        {
            Aiprefabslist = CreatPoint.Instance.getaiprefabs();
        }

        //设置队长技能 和队长数量 
        for (int i=0;i<prefabslist.Count;i++)
        {
            
            BattleCatInfo info = prefabslist[i].GetComponent<BattleCatInfo>();
            if (info.ishaveCaptain())
            {
                playerindex++;
                string[] str = info.members.Split(',');
                foreach(var temp in str)
                {
                    PlayerMembers.Add(temp);
                }
               

            }
          

        }
        for (int i=0;i<Aiprefabslist.Count;i++)
        { 
           
            BattleCatInfo info = Aiprefabslist[i].GetComponent<BattleCatInfo>();
            if (info.ishaveCaptain())
            {
                enemyindex++;
                string[] str = info.members.Split(',');
                foreach (var temp in str)
                {
                    EnemyMembers.Add(temp);
                }
            }
        }
        PlayerCaptainCount = playerindex;
        Debug.Log("PlayerCaptainCount" + PlayerCaptainCount);
        EnemyCaptainCount = enemyindex;

       
        // 设置队列状态
        for (int i = 0; i < prefabslist.Count; i++)
        {
            BattleCatInfo info = prefabslist[i].GetComponent<BattleCatInfo>();

            if(info.isactiveforcat())
            { 
                if (PlayerMembers==null)
                {
                   
                    prefabslist[i].GetComponent<BattelCat>().GetMs().SetCurrentState(CatChaos._instance);
                    prefabslist[i].GetComponent<BattelCat>().GetMs().UpdateMs();

                    return;
                }
               if (info.ishaveCaptain())
                {
                    
                    prefabslist[i].GetComponent<BattelCat>().GetMs().ChangeSate(CatIdle._instance);
                }
                else
                {
                    if (PlayerMembers.Contains(info.attribute))
                    {
                        
                        PlayerMembers.Remove(info.attribute);
                        prefabslist[i].GetComponent<BattelCat>().GetMs().ChangeSate(CatIdle._instance);
                    }
                    else
                    {
                        
                        
                        prefabslist[i].GetComponent<BattelCat>().GetMs().SetCurrentState(CatChaos._instance);
                        prefabslist[i].GetComponent<BattelCat>().GetMs().UpdateMs();
                    }
                }
              
            }else
            {
                
                prefabslist[i].GetComponent<BattelCat>().GetMs().ChangeSate(CatIdle._instance);
            }

        }
        for (int i = 0; i < Aiprefabslist.Count; i++)
        {
            BattleCatInfo info = Aiprefabslist[i].GetComponent<BattleCatInfo>();

            if (info.isactiveforcat())
            {
                if (EnemyMembers == null)
                {
                    
                    Aiprefabslist[i].GetComponent<BattelCat>().GetMs().SetCurrentState(CatChaos._instance);
                    Aiprefabslist[i].GetComponent<BattelCat>().GetMs().UpdateMs();

                    return;
                }
                if (info.ishaveCaptain())
                { 
                    Aiprefabslist[i].GetComponent<BattelCat>().GetMs().ChangeSate(CatIdle._instance);
                }
                else
                {
                    if (EnemyMembers.Contains(info.attribute))
                    {
                         
                        EnemyMembers.Remove(info.attribute);
                        Aiprefabslist[i].GetComponent<BattelCat>().GetMs().ChangeSate(CatIdle._instance);
                    }
                    else
                    {


                        
                        Aiprefabslist[i].GetComponent<BattelCat>().GetMs().SetCurrentState(CatChaos._instance);
                        Aiprefabslist[i].GetComponent<BattelCat>().GetMs().UpdateMs();

                    }
                }
                
            }else
            {
                
                Aiprefabslist[i].GetComponent<BattelCat>().GetMs().ChangeSate(CatIdle._instance);
            }

        }
        //检测队列信息  设置队长限制 
       
        if (PlayerCaptainCount >= 3)
        {  
            for (int i = 0; i < prefabslist.Count; i++)
            {  
                BattleCatInfo info = prefabslist[i].GetComponent<BattleCatInfo>();
                if(info.isactiveforcat())
                {
                    info.ischaos = true;
                    prefabslist[i].GetComponent<BattelCat>().GetMs().SetCurrentState(CatChaos._instance);
                    prefabslist[i].GetComponent<BattelCat>().GetMs().UpdateMs();
                }else
                {
                    info.ischaos = false;
                    prefabslist[i].GetComponent<BattelCat>().GetMs().ChangeSate(CatIdle._instance);
                }
               
                

            }
        }
        if (EnemyCaptainCount >= 3)
        {
            for (int i = 0; i < Aiprefabslist.Count; i++)
            {
                BattleCatInfo info = Aiprefabslist[i].GetComponent<BattleCatInfo>();
                if(info.isactiveforcat())
                {
                    info.ischaos = true;
                    Aiprefabslist[i].GetComponent<BattelCat>().GetMs().SetCurrentState(CatChaos._instance);
                    Aiprefabslist[i].GetComponent<BattelCat>().GetMs().UpdateMs();
                }else
                {
                    info.ischaos = false ;
                    Aiprefabslist[i].GetComponent<BattelCat>().GetMs().ChangeSate(CatIdle._instance);
                }
                
            }

        }
    }

    //清除配置
    public void ClearConfig()
    {
        for (int i = 0; i < prefabslist.Count; i++)
        {
            AllCooldownEvent -= prefabslist[i].GetComponent<BattleCatInfo>().UpdateCooldownInfo;
          
            BattleCatInfo info = prefabslist[i].GetComponent<BattleCatInfo>();
            prefabslist[i].GetComponent<BattelCat>().GetMs().ChangeSate(CatIdle._instance);                          //重置猫的状态
            prefabslist[i].GetComponent<Hudparticipant>().HudSpriteForWorld.FadeAttribute(prefabslist[i].transform); //消除底部属性显示
            
           
        }
        for (int i = 0; i < Aiprefabslist.Count; i++)
        {

            AllCooldownEvent -= Aiprefabslist[i].GetComponent<BattleCatInfo>().UpdateCooldownInfo;
            
            BattleCatInfo info = Aiprefabslist[i].GetComponent<BattleCatInfo>();
            Aiprefabslist[i].GetComponent<BattelCat>().GetMs().ChangeSate(CatIdle._instance);                           //重置猫的状态
            Aiprefabslist[i].GetComponent<Hudparticipant>().HudSpriteForWorld.FadeAttribute(Aiprefabslist[i].transform);//消除底部属性显示
            
        }
        PlDefenselist.Clear();
        EmDefenselist.Clear();
    }
    

    //处理数值  
    public void DecreaseHp(  RoleType roletype, BattleCatInfo attackobj, BattleCatInfo beattackedobj, AttackPatten attacktype)
    {
        float value = 0;

        BattleCatInfo defendObj=null;
        float defendValue=0;

        if (roletype== RoleType.Player)
        {
            bool isfind = false;
            if (EmDefenselist!=null &&EmDefenselist.Count>0)
            {
                if (attackobj.attribute.Equals("w"))
                {
                    int keyid = 0;
                     isfind = false;
                    foreach (KeyValuePair<int ,BattleCatInfo> key in EmDefenselist)
                    {
                       
                        if (key.Value.attribute.Equals("r"))
                        {
                            defendValue = key.Value.power;
                            value =attackobj.power- key.Value.power;
                            keyid = key.Value.id;
                            defendObj = key.Value;
                            isfind = true;
                            break;
                        }

                    }
                    if (isfind)
                    {
                        EmDefenselist.Remove(keyid);
                    }else
                    {
                        defendObj = null;
                        value = attackobj.power;
                    }
                   
                }else if (attackobj.attribute.Equals("p"))
                {
                    int keyid = 0;
                     isfind = false;
                    foreach(KeyValuePair < int, BattleCatInfo > key in EmDefenselist)
                    {
                        if (key.Value.attribute.Equals("g"))
                        {
                            defendValue= key.Value.power;
                            value = attackobj.power- key.Value.power;
                            keyid = key.Value.id;
                            isfind = true;
                            defendObj = key.Value;
                            break;
                        }
                    }
                    if (isfind)
                    {
                        EmDefenselist.Remove(keyid);
                    }
                    else
                    {
                        defendObj = null;
                        value = attackobj.power;
                    }
                  
                  
                }
                
            }
            else
            {
                 
                value = attackobj.power;
            }
         
            EnemyHp -= value;
            float t = EnemyHp / cacheEnemyhp;
            if (attacktype==AttackPatten.Normal)
            {
                AddLimitValue(RoleType.Enemy,80);                                                 //limit的增加值
                AddLimitValue(RoleType.Player, 50);                                                        //limit的增加值
            }
                                                          
          

            Color attribute=Color.white;
            if (attackobj.attribute.Equals("w"))
            {
                attribute = Color.white;
            }
            else if(attackobj.attribute.Equals("p"))
            {
                attribute = new Color(1, 0, 216 / 255f);
            }

            BattleUIVO UIVO;
            if (isfind)
            {
               
                UIVO = new BattleUIVO(defendValue + "\n防御"  , Color.white, 1f);
                UIVO.hudparticipant = defendObj.GetComponent<Hudparticipant>();
                UIVO.yInterval = 1.5f;
                AppFacade.getInstance.SendNotification(BattleMediator.SHOWHUDTEXT, UIVO);

                UIVO = new BattleUIVO(value+"", Color.white, 1f);
                UIVO.hudparticipant = beattackedobj.GetComponent<Hudparticipant>();
                UIVO.yInterval = 1.5f;
                AppFacade.getInstance.SendNotification(BattleMediator.SHOWHUDTEXT, UIVO);

            }
            else
            {
                UIVO = new BattleUIVO(value + "", attribute, 1f);
                UIVO.yInterval = 1.5f;
                UIVO.hudparticipant = beattackedobj.GetComponent<Hudparticipant>();
                AppFacade.getInstance.SendNotification(BattleMediator.SHOWHUDTEXT, UIVO);

            }





            AppFacade.getInstance.SendNotification(BattleInfoMediator.UPDATENEMYHP, t);

            if (EnemyHp <= 0)
            {
                EnemyHp = 0;
                PlayerScore++;
                isPlayerGetScore = true;
                //重新开局
                if (PlayerScore >= 2)
                {
                    iswin = true;
                    wintype = RoleType.Player;
                }
            }
           
            Debug.LogWarning("enemy hp + " + t);


        }
        else if (roletype == RoleType.Enemy)
        {
            bool isfind=false;
            if (PlDefenselist != null&&PlDefenselist.Count>0)
            {
                if (attackobj.attribute.Equals("w"))
                {
                    int keyid = 0;
                    isfind = false;
                    foreach (KeyValuePair<int, BattleCatInfo> key in PlDefenselist)
                    {
                        if (key.Value.attribute.Equals("r"))
                        {
                            defendValue = key.Value.power;
                            value = attackobj.power - key.Value.power;
                            keyid = key.Value.id;
                            isfind = true;
                            defendObj = key.Value;
                            break;
                        }
                    }
                    if(isfind)
                    {
                        PlDefenselist.Remove(keyid);
                    }else
                    {
                        defendObj = null;
                        value = attackobj.power;
                    }
                   
                }
                else if (attackobj.attribute.Equals("p"))
                {
                    int keyid = 0;
                     isfind = false;
                    foreach (KeyValuePair<int, BattleCatInfo> key in PlDefenselist)
                    {
                        if (key.Value.attribute.Equals("g"))
                        {
                            defendValue = key.Value.power;
                            value = attackobj.power - key.Value.power;
                            keyid = key.Value.id;
                            isfind = true;
                            defendObj = key.Value;
                            break;
                        }
                    }
                    if (isfind)
                    {
                        PlDefenselist.Remove(keyid);
                    }else
                    {
                        defendObj = null;
                        value = attackobj.power;
                    }
                    

                }
               
            }
            else
            {
                value = attackobj.power;
            }
            PlayerHp -= value;

            //减去最终伤害值
            if (attacktype == AttackPatten.Normal)
            {
                AddLimitValue(RoleType.Enemy, 50);                                                 //limit的增加值
                AddLimitValue(RoleType.Player, 80);                                                        //limit的增加值
            }
            float t = PlayerHp / cachePlayerhp;


            Color attribute = Color.white;
            if (attackobj.attribute.Equals("w"))
            {
                attribute = Color.white;
            }
            else if (attackobj.attribute.Equals("p"))
            {
                attribute = new Color(1, 0, 216 / 255f);
            }
            BattleUIVO UIVO;
            if (isfind)
            {


          
                UIVO = new BattleUIVO(defendValue + "防御", Color.white, 1f);
                UIVO.hudparticipant = defendObj.GetComponent<Hudparticipant>();
                UIVO.yInterval = 1.5f;
                AppFacade.getInstance.SendNotification(BattleMediator.SHOWHUDTEXT, UIVO);


                UIVO = new BattleUIVO(value + "", Color.white, 1f);
                UIVO.hudparticipant = beattackedobj.GetComponent<Hudparticipant>();
                UIVO.yInterval = 1.5f;
                AppFacade.getInstance.SendNotification(BattleMediator.SHOWHUDTEXT, UIVO);
            }
            else
            {
                UIVO = new BattleUIVO(value + "", attribute, 1f);
                UIVO.yInterval = 1.5f;
                UIVO.hudparticipant = beattackedobj.GetComponent<Hudparticipant>();
                AppFacade.getInstance.SendNotification(BattleMediator.SHOWHUDTEXT, UIVO);

            }

            AppFacade.getInstance.SendNotification(BattleInfoMediator.UPDATEPLAYERHP, t);


          
            if (PlayerHp <= 0)
            {
                PlayerHp = 0;
                EnemyScore++;
                isEnemyGetScore = true;
                 //重新开局
                if (EnemyScore>=2)
                {
                    Debug.Log("enemy win");
                    iswin = false;
                    wintype = RoleType.Enemy;

                }
            }

            Debug.Log(iswin);

        }
          
    }

    /// <summary>
    /// 增加litmit
    /// </summary>
    /// <param name="roletype"></param>
    /// <param name="value"></param>
    public void AddLimitValue(RoleType roletype ,float value)
    {
        
        if (roletype==RoleType.Player)
        {
            PlayerLimit += value;
            if(PlayerLimit>=500)
            {

                PlayerLimit = 500;
            }
            float Realvalue = PlayerLimit / 500;
            AppFacade.GetInstance().SendNotification(BattleInfoMediator.UPDATEPLAYERLIMIT, Realvalue);
        }
        else if (roletype==RoleType.Enemy)
        {
            EnemyLimit += value;
            if (EnemyLimit>= 500)
            {

                EnemyLimit = 500;
            }
            float Realvalue = EnemyLimit / 500;
            AppFacade.GetInstance().SendNotification(BattleInfoMediator.UPDATENEMYLIMIT, Realvalue);
        }
             
    

    }

    /// <summary>
    /// 使用limit
    /// </summary>
    /// <param name="roletype"></param>
    public void UserLimit(RoleType roletype)
    {
        if (roletype==RoleType.Player)
        {
            PlayerLimit = 0;
            float Realvalue = PlayerLimit / 500;
            AppFacade.GetInstance().SendNotification(BattleInfoMediator.UPDATEPLAYERLIMIT, Realvalue);
            for (int i = 0; i < prefabslist.Count; i++)
            {
                BattleCatInfo info = prefabslist[i].GetComponent<BattleCatInfo>();
                if (info.ishaveCaptain())
                {
                    prefabslist[i].GetComponent<BattelCat>().SetAttackPatten(AttackPatten.Captain);               //设置攻击模式
                    prefabslist[i].GetComponent<BattelCat>().GetMs().ChangeSate(CatAttack._instance);
                    prefabslist[i].GetComponent<BattelCat>().GetMs().UpdateMs();
                    break;
                };

            }

        }
        else if (roletype==RoleType.Enemy)
        {

            EnemyLimit = 0;
            float Realvalue = EnemyLimit / 500;
            AppFacade.GetInstance().SendNotification(BattleInfoMediator.UPDATENEMYLIMIT, Realvalue);
            for (int i = 0; i < Aiprefabslist.Count; i++)
            {
                BattleCatInfo info = Aiprefabslist[i].GetComponent<BattleCatInfo>();
                if (info.ishaveCaptain())
                {
                    Aiprefabslist[i].GetComponent<BattelCat>().SetAttackPatten(AttackPatten.Captain);               //设置攻击模式
                    Aiprefabslist[i].GetComponent<BattelCat>().GetMs().ChangeSate(CatAttack._instance);
                    Aiprefabslist[i].GetComponent<BattelCat>().GetMs().UpdateMs();
                    break;
                };

            }
        }
             

    }

    //处理防御 
    public void Defense( RoleType roletype,BattleCatInfo  defenseobj)
    {

        if (roletype == RoleType.Player)
        {
            PlDefenselist.Add(defenseobj.id, defenseobj);
        }
        else if (roletype == RoleType.Enemy)
        {
            EmDefenselist.Add(defenseobj.id, defenseobj);
        }
    }

    //显示上个回合结果
    IEnumerator RoundResult()
    {
        StopCoroutine("battle");
        //处理攻击顺序   
        if (iswin)
        {
            UpdateenemyScore();                                                                     //更新回合分数
            UpdateplayerScore();
            UpdateResultBattle(wintype);
            yield return new WaitForSeconds(2f);
            BattleEnd();                                                                        //战斗结束                                                                                   
        }
        else
        {

            if (cacheabilityattckstate == RoleType.Player)
            {
                abilityattckstate = RoleType.Enemy;
                cacheabilityattckstate = abilityattckstate;
            }
            else
            {
                abilityattckstate = RoleType.Player;
                cacheabilityattckstate = abilityattckstate;
            }


            CatPool.GetInstance().DestructAll();
            CreatPoint.Instance.PointInit();   //初始化点数据
            ClearConfig(); ;//清除配置

            prefabslist.Clear();
            Aiprefabslist.Clear();
            if (isPlayerGetScore && !isEnemyGetScore)
            {
                AppFacade.GetInstance().SendNotification(BattleInfoMediator.UPDATESCOREINFORESULT, RoleType.Player);
            }
            else if (!isPlayerGetScore && isEnemyGetScore)
            {

                AppFacade.GetInstance().SendNotification(BattleInfoMediator.UPDATESCOREINFORESULT, RoleType.Enemy);
            }
            else if (isEnemyGetScore && isEnemyGetScore)
            {
                AppFacade.GetInstance().SendNotification(BattleInfoMediator.UPDATESCOREINFORESULT, RoleType.NUll);
            }

            yield return new WaitForSeconds(5f);
            AppFacade.GetInstance().SendNotification(BattleInfoMediator.UPDATESCOREINFORESULT, RoleType.NUll);            //关闭窗口
            NewBattle();
        }
       
    }
    //新的回合 
    public void NewBattle( )
    {
        
        isEnemyGetScore = false;
        isPlayerGetScore = false;
        PlayerHp = cachePlayerhp;
        EnemyHp = cacheEnemyhp;
        PlayerLimit = 0;
        EnemyLimit = 0;
        //AddLimitValue(RoleType.Player, 100);
        //AddLimitValue(RoleType.Enemy, 100);
        UpdateenemyScore();                                                                     //更新回合分数
        UpdateplayerScore();
        AppFacade.getInstance.SendNotification(BattleInfoMediator.UPDATENEMYHP, 1f);            //更新hp
        AppFacade.getInstance.SendNotification(BattleInfoMediator.UPDATEPLAYERHP,1f);
        setCurretnState(BattleState.Begin);
        StartCoroutine("UpdateBattle");
    }

    public void UpdateenemyScore()
    {
        AppFacade.getInstance.SendNotification(BattleInfoMediator.UPDATENEMYSCORE, EnemyScore);
    }
    public void UpdateplayerScore()
    {
        AppFacade.getInstance.SendNotification(BattleInfoMediator.UPDATPLAYERSCORE, PlayerScore);
    }

    public void UpdateResultBattle(RoleType type)
    {
        int Exp = 0;
        if (type==RoleType.Player)
        {
            for (int i=0;i<prefabslist.Count;i++)
            {
                Exp += prefabslist[i].GetComponent<BattleCatInfo>().ownexp;

            }
            BattleResultVO BRVO = new BattleResultVO();
            BRVO.type = type;
            BRVO.Exp = Exp;
          
            AppFacade.getInstance.SendNotification(NotiConst.SET_BATTLE_RESULT_EXP, BRVO);
        }else if (type == RoleType.Enemy)
        {
            for (int i = 0; i < Aiprefabslist.Count; i++)
            {
                Exp += Aiprefabslist[i].GetComponent<BattleCatInfo>().ownexp;
               
            }
            BattleResultVO BRVO = new BattleResultVO();
            BRVO.type = type;
            BRVO.Exp = Exp;
            AppFacade.getInstance.SendNotification(BattleInfoMediator.UPDATEBATTLERESULT, Exp);
            //todo 与服务器相连转送给敌方经验
        }

    }

    public void BattleEnd()
    {
        iswin = false;                                                                                         //置空战斗胜利
        wintype =  RoleType.NUll;                                                                              //置空战斗胜利状态
        AppFacade.GetInstance().SendNotification(BattleInfoMediator.UPDATEBATTLERESULT, RoleType.NUll);        //关闭窗口
        CatPool.GetInstance().DestructAll();                                                                   // 重置猫池
        StopAllCoroutines();                                                                                   //关闭所有协程
        (AppFacade.getInstance.RetrieveMediator(SceneMediator.NAME) as SceneMediator).LoadScene(EnumScene.SceneMain);
    }
}
