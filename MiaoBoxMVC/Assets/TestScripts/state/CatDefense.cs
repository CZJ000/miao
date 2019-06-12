using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatDefense : State<BattelCat>
{

    public static CatDefense _instance;

    void Awake()
    {
        _instance = this;
    }
    public override void Enter(BattelCat Obj)
    {
        

        base.Enter(Obj);
        Debug.Log("Enter Defense");//TO DO 
        Obj.GetMs().UpdateMs();


    }
    public override void Execute(BattelCat Obj)
    {
        base.Execute(Obj);
        BattleCatInfo info = Obj.gameObject.GetComponent<BattleCatInfo>();
        BattleSceneManage.Instance.Defense(info.roletype, info );
        BattleUIVO UIVO = new BattleUIVO(info.currentposid +  "防御", Color.grey, 1f);
        UIVO.hudparticipant = Obj.GetComponent<Hudparticipant>();
        UIVO.yInterval = 1.5f;
        AppFacade.getInstance.SendNotification(BattleMediator.SHOWHUDTEXT, UIVO);
        Obj.GetMs().RevertToPreviourSate();
    }

    public override void Exit(BattelCat Obj)
    {
        base.Exit(Obj);
        
    }

}
