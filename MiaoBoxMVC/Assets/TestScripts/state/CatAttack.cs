using UnityEngine;
using System.Collections;
using Global;

public class CatAttack : State<BattelCat> {

    public static CatAttack _instance;

   
    void Awake()
    {
        _instance = this;
    }
    public override void Enter(BattelCat Obj)
    {
 
    }
    public override void Execute(BattelCat Obj)
    {


        BattleCatInfo info = Obj.GetComponent<BattleCatInfo>();
        BattelCat TaregtObj=null;


        switch (Obj.GetAttackPatten())
        {

            case AttackPatten.Normal:
                TaregtObj = Obj.SetAttackObjIndex(info.currentposid, info.roletype);                      //获取攻击目标
                Obj.GetComponent<BattleDisplayAnimator>().AttackMove(TaregtObj.transform);                          
                break;
            case AttackPatten.Captain:
              //  TaregtObj = Obj.SetAttackObjIndex(info.currentposid, info.roletype);
                TaregtObj = Obj.SetAttackObjIndex(1, info.roletype); //选择B
                Obj.GetComponent<BattleDisplayAnimator>().AttackMove(TaregtObj.transform);
                Obj.transform.localScale = Vector3.one;
                BattleCamera._instance.SpecialViewChange(Obj.transform);                                            //特殊视角
                break;
        }
 
        BattleSceneManage.Instance.DecreaseHp(info.roletype, info, TaregtObj.GetComponent<BattleCatInfo>(), Obj.GetAttackPatten());                                                //伤害处理      
        //BattleUIVO UIVO = new BattleUIVO(info.name + "攻击 " + info.power.ToString(), Color.magenta,0.1f);         //攻击信息显示
        //UIVO.hudparticipant = Obj.GetComponent<Hudparticipant>();
        //UIVO.yInterval = 1.5f;
        //AppFacade.getInstance.SendNotification(BattleMediator.SHOWHUDTEXT,UIVO);                                   //hud 显示
        Obj.GetComponent<BattleDisplayAnimator>().ChangeAniamstate(animastate.attack01);                         //播放攻击动画

        Obj.SetAttackPatten(AttackPatten.Normal);                                                          //将攻击模式重置
       // Obj.GetMs().RevertToPreviourSate();
    }

    public override void Exit(BattelCat Obj)
    {
        base.Exit(Obj);
        
        Debug.Log("Exit cat attack");
    }

}

