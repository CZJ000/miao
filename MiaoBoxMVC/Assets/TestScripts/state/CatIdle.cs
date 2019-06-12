using UnityEngine;
using System.Collections;

public class CatIdle :  State<BattelCat> {
    public static CatIdle _instance;
   

    void Awake()
    {
        _instance = this;
    }
    public override void Enter(BattelCat Obj)
    {
      
       
        
    }
    public override void Execute(BattelCat Obj)
    { 
    //{
    //    BattleCatInfo info = Obj .gameObject.GetComponent<BattleCatInfo>();
    //    //  Debug.Log(info.roletype.ToString()+info.name+"Execute Cat ....");

    //    //GUIManage._instance.showhudtext(Obj.gameObject.GetComponent<Hudparticipant>(),info.currentposid+ info.roletype.ToString()+ "cooldown 不足 继续闲置" + info.cooldown,Color.cyan, 2f);
    //    // Debug.Log("cooldown 不足 继续闲置"+info.cooldown);

      

       
    //    {
    //        BattleUIVO UIVO = new BattleUIVO(info.currentposid + info.roletype.ToString() + "cooldown为 " + info.cooldown + " 继续闲置", Color.cyan, 0.1f);
    //        UIVO.hudparticipant = Obj.GetComponent<Hudparticipant>();
    //        AppFacade.getInstance.SendNotification(BattleMediator.SHOWHUDTEXT, UIVO);
    //    }
       
        // Obj.ChangeState()
    }

    public override void Exit(BattelCat Obj)
    { 
       
    }

}
