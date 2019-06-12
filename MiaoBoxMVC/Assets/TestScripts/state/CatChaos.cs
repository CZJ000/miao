using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatChaos : State<BattelCat>
{

    public static CatChaos _instance;
    void Awake()
    {
        _instance = this;
    }
    public override void Enter(BattelCat Obj)
    {
        base.Enter(Obj);
        

    }
    public override void Execute(BattelCat Obj)
    {
      
         
        BattleCatInfo info = Obj.gameObject. GetComponent<BattleCatInfo>();
        info.ischaos = true;
        if(info.ischaostate())  
        {
            BattleUIVO UIVO = new BattleUIVO(info.name + "混乱 ", Color.yellow, 0.1f);
            UIVO.hudparticipant = Obj.GetComponent<Hudparticipant>();
            UIVO.spritename = "emoji_wenti";
            AppFacade.getInstance.SendNotification(BattleMediator.SHOWHUDSPRITE, UIVO);
        }
      

    }

    public override void Exit(BattelCat Obj)
    {
        base.Exit(Obj);
        //删除 图标 
        BattleCatInfo info = Obj.gameObject.GetComponent<BattleCatInfo>();
        info.ischaos = false;
        Hudparticipant ht = Obj.GetComponent<Hudparticipant>();
        ht.HudMiaoImage.fade();
    }
}
