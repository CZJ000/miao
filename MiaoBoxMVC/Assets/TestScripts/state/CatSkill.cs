using UnityEngine;
using System.Collections;

public class CatSkill : State<BattelCat>
{
    public static CatSkill _instance;
    void Awake()
    {
        _instance = this;
    }
    public override void Enter(BattelCat Obj)
    {
        base.Enter(Obj);
        Obj.GetMs().UpdateMs();

    }
    public override void Execute(BattelCat Obj)
    {
        base.Execute(Obj);
        BattleCatInfo info = Obj.GetComponent<BattleCatInfo>();
        BattleUIVO UIVO = new BattleUIVO(  "使用技能 " + info.skillid, Color.green, 0.5f);
        UIVO.hudparticipant = Obj.GetComponent<Hudparticipant>();
        UIVO.yInterval = 1.5f;
        if (info.ishaveskill()&&!info.ischaos)
        {
           
            AppFacade.getInstance.SendNotification(BattleMediator.SHOWHUDTEXT, UIVO);
            
        }
        
        Obj.GetMs().RevertToPreviourSate();

    }

    public override void Exit(BattelCat Obj)
    {
        base.Exit(Obj);
    }
}
