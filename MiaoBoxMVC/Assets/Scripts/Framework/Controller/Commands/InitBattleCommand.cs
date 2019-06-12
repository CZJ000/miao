
using PureMVC.Patterns;
using PureMVC.Interfaces;

using SUIFW;

class InitBattleCommand: SimpleCommand
{
    public override void Execute(INotification notification)
    {
        UIManager.GetInstance().ShowUIForms("BattleView");
        UIManager.GetInstance().ShowUIForms("BattleInfoView");
        UIBaseBehaviour<BattleMediator>.CreateUI<BattleView>();
        UIBaseBehaviour<BattleInfoMediator>.CreateUI<BattleInfoView>();
    }


    }
 
