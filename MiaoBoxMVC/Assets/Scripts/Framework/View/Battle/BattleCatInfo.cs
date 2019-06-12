using UnityEngine;
using System.Collections;
using Global;

public class BattleCatInfo : MonoBehaviour
{

    #region 猫信息
    public int id { get; set; }
    public int catCaptaintype { get; set; }
    public int cattypeid { get; set; }
    public string catname { get; set; }
    public int  lv { get; set; }
    public int power { get; set; }
    public int currentposid { get; set; }
    public string  about { get; set; }
    public string members { get; set; }
    public  int  skillaniid { get; set; }
    public int  skillid { get; set; }
    public int evo { get; set; }
    public int ownexp { get; set; }
    /// <summary>
    /// 猫的攻击类型；
    /// </summary>
    public int AttackType { get; set; }
    /// <summary>
    /// 猫的技能类型；
    /// </summary>
    public int SkillType { get; set; }
    #endregion

    #region 猫属性
    public string attribute { get; set; }
    public int cooldown { get { return _cooldown; } set { _cooldown = value; } }
    public int cachecooldown { get; set; }
    public RoleType roletype { get; set; } 
    public animastate animastate { get; set; }
    public bool ischaos { get; set; }
    #endregion

    private int _cooldown;



    #region 对属性操作的方法


    /// <summary>
    /// 是否准备好
    /// </summary>
    /// <returns></returns>
    public bool isperpare()
    {
        if (!isactiveforcat())
        {
            return false;
        }
        if (_cooldown == 0)
        {

            return true;
        }

        return false;

    }

    /// <summary>
    /// 是否有技能
    /// </summary>
    /// <returns></returns>
    public bool ishaveskill()
    {
        if (!isactiveforcat())
        {
            return false;
        }

        if (skillid!=0)
        {
            return true;

        }
        return false;
    }

    /// <summary>
    /// 是否是队长
    /// </summary>
    /// <returns></returns>
    public bool ishaveCaptain()
    {
        if (catCaptaintype == 1)
        {
            return true;
        }
        else return false;
    }
    /// <summary>
    /// 是否混乱状态
    /// </summary>
    /// <returns></returns>
    public bool ischaostate()
    {
        if (ischaos)
        {

            return true;
        }
        else
            return false;
    }
    /// <summary>
    /// 是否活跃状态
    /// </summary>
    /// <returns></returns>
    public bool isactiveforcat()
    {
        if (currentposid == 3 || currentposid == 4)
        {

            return false;
        }
        else return true;

    }

    /// <summary>
    /// 是否防御
    /// </summary>
    /// <returns></returns>
    public bool isdefense()
    {
        if (attribute.Equals("r") || attribute.Equals("g"))
        {
            return true;
        }
        else
            return false;
    }
    /// <summary>
    /// 减少cooldown
    /// </summary>
    public void DecreaseCooldown()
    {
        if (cooldown == 0)
        {
            cooldown = cachecooldown;
        }
        else
        {
            cooldown--;
        }
    }
    /// <summary>
    /// 是否攻击
    /// </summary>
    /// <returns></returns>
    public bool isattack()
    {
        if (attribute.Equals("w") || attribute.Equals("p"))
        {
            return true;
        }
        else
            return false;
    }

    
    #endregion



    /// <summary>
    /// 更新cooldown显示
    /// </summary>
    /// 
    public void UpdateCooldownInfo()
    {

        if (cooldown != 0)
        {
            BattleUIVO UIVO = new BattleUIVO("" + cooldown, Color.white, 2f);
            UIVO.hudparticipant = this.gameObject.GetComponent<Hudparticipant>();
            UIVO.yInterval = 1.5f;
            AppFacade.getInstance.SendNotification(BattleMediator.SHOWHUDTEXT, UIVO);

        }

    }

   


}
