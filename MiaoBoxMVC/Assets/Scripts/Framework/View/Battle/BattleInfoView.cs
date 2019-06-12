using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using SUIFW;
public class BattleInfoView : BaseUIForm
{

    //public GameObject BattleReslutBg;
  
    //public Text ScoreInfoPlayerResult;
    //public Text ScoreInfoEnemyResult;

    public GameObject EnemyHp;
    public GameObject PlayerHp;
    public Image PlayerLimit;
    public Text PlayerLimitText;
    public Image EnemyLimit;
    public Text EnemyLimitText;
   // public Text WinerTitleText;
  //  public Text ReawardText;
    public Text PlayerScore;
    public Text EnemyScore;
    /// <summary>
    /// 自己的名字；
    /// </summary>
    public Text TextUserName;
    /// <summary>
    /// 敌人的名字；
    /// </summary>
    public Text TextEnermyName;


   protected override void Awake()
    {
        base.Awake();
        //定义本窗体的性质(默认数值，可以不写)
        base.CurrentUIType.UIForms_Type = UIFormType.Fixed;
        base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
        base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;
    }

    private void Start()
    {
        //获得敌人名字；
        UserInfoProxy userInfoProxy = AppFacade.getInstance.RetrieveProxy(UserInfoProxy.NAME) as UserInfoProxy;
        TextUserName.text = userInfoProxy.UsertData.PlayerName;
    }

    public void UpdatePlayerLimit(float value)
    {
        //PlayerLimit.material.SetFloat("_Fill", value);
        //StartCoroutine(MiaoBoxTool.FillShader(PlayerLimit, "_Fill", value, 1f));
        PlayerLimitText.text = (int)(value * 100) + "%";

    }
    public void UpdateEnemyLimit(float value)
    {
        //EnemyLimit.material.SetFloat("_Fill", value);
        //StartCoroutine(MiaoBoxTool.FillShader(EnemyLimit, "_Fill", value, 1f));
        EnemyLimitText.text = (int)(value * 100) + "%";
    }

    public void UpdateEnemyhp(float value)
    {
        Image im = EnemyHp.gameObject.GetComponent<Image>();
        StartCoroutine(MiaoBoxTool.FillImage(im, value, 1f));
     
       /// float textvalue =  value * 100f;
        Debug.Log("textvalue" + value);
      //  EnemyHp.transform.GetComponentInChildren<Text>().text =( (int )textvalue).ToString() + "%";
    }

    public void UpdatePlayerHp(float value)
    {
        
        Image im = PlayerHp.gameObject.GetComponent<Image>();
        StartCoroutine(MiaoBoxTool.FillImage(im, value, 1f));
        //  float textvalue = value * 100f;
        // PlayerHp.transform.GetComponentInChildren<Text>().text = ((int)textvalue).ToString() + "%";

    }
    public void ResultBattle( BattleResultVO BRVO)
    {

        OpenUIForm("BattleResultView");

        SendMessage("GetBattleResult", "BattleResultVO", BRVO);

        //BattleReslutBg.gameObject.SetActive(!BattleReslutBg.activeSelf);
        //if (BattleReslutBg.activeSelf)
        //{
        //    if (BRVO.type == RoleType.Player)
        //    {
        //        WinerTitleText.text = "玩家取得胜利";
        //        ReawardText.text = "战斗获得 " + "<color=blue>exp</color>" + "x" + BRVO.Exp;
        //    }
        //    else if (BRVO.type == RoleType.Enemy)
        //    {
        //        WinerTitleText.text = "玩家取得胜利";
        //        ReawardText.text = "战斗失去 " + "<color=blue>exp</color>" + "x" + BRVO.Exp;

        //    }
        //}


    }
    
     

    public void UpdateScoreInfoResult( RoleType winnertype)
    {
        //ScoreInfoBg.gameObject.SetActive(!ScoreInfoBg.activeSelf);
        //if (ScoreInfoBg.activeSelf)
        //{
        //    ScoreInfoEnemyResult.gameObject.GetComponentInChildren<TweenLetters>().ResetToBeginning();
        //    ScoreInfoPlayerResult.gameObject.GetComponentInChildren<TweenLetters>().ResetToBeginning();
        //    ScoreInfoEnemyResult.gameObject.GetComponentInChildren<TweenLetters>().PlayForward();
        //    ScoreInfoPlayerResult.gameObject.GetComponentInChildren<TweenLetters>().PlayForward();


        //    if (winnertype == RoleType.Enemy)
        //    {
        //        ScoreInfoEnemyResult.text = "敌人得1分";
        //        ScoreInfoPlayerResult.text = "玩家失败0分";

        //    }
        //    else if (winnertype == RoleType.Player)
        //    {
        //        ScoreInfoEnemyResult.text = "敌人失败0分";
        //        ScoreInfoPlayerResult.text = "玩家得1分";
        //    }

        //    else if (winnertype == RoleType.NUll)
        //    {
        //        ScoreInfoEnemyResult.text = "敌人得1分";
        //        ScoreInfoPlayerResult.text = "玩家得1分";
        //    }
        //}

    }

    public void UpdatePlayerScore(int Score)
    {
        PlayerScore .text = Score.ToString();
    }
    public void UpdateEnemyScore(int Score)
    {
        EnemyScore .text = Score.ToString();
    }
    public void OnDisable()
    {
        Destroy(this);
    }
}
