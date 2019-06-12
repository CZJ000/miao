using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SUIFW;
using Global;
using UnityEngine.UI;
public class BattleResultView : BaseUIForm {


    public Text ReawardText;
    public Text WinerTitleText;
    protected override void Awake()
    {
        base.Awake();
        //定义本窗体的性质(默认数值，可以不写)
        base.CurrentUIType.UIForms_Type = UIFormType.PopUp;
        base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
        base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;
        ReceiveMessage("GetBattleResult", ResultBattle);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void ResultBattle(KeyValuesUpdate keyValues)
    {

        BattleResultVO BRVO = keyValues.Values as BattleResultVO;
        gameObject.SetActive(!gameObject.activeSelf);
        
        if (gameObject.activeSelf)
        {
            if (BRVO.type == RoleType.Player)
            {
                WinerTitleText.text = "玩家取得胜利";
                ReawardText.text = "战斗获得 " + "<color=blue>exp</color>" + "x" + BRVO.Exp;
            }
            else if (BRVO.type == RoleType.Enemy)
            {
                WinerTitleText.text = "玩家取得胜利";
                ReawardText.text = "战斗失去 " + "<color=blue>exp</color>" + "x" + BRVO.Exp;

            }
        }


    }

}
