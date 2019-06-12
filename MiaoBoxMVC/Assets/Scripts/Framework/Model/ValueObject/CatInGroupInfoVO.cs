using UnityEngine;
using System.Collections;

/// <summary>
/// info_cats 和 stat_cats 结合
/// </summary>
public class CatInGroupInfoVO 
{
 
    public int StoreId { set; get; }
    public int CatTypeId { set; get; }
    public int Type { set; get; }
    public int GroupId { get; set; }
    public int UserId { set; get; }
    public int Exceedpoint { set; get; }
    public string Name { set; get; }
    public int Level { set; get; }
    public int Power { set; get; }


    public string About { get; set; }

    public int CoolDown { get; set; }

    public int SkillID { get; set; }

    public string Attribute { get; set; }

    public int Evo { get; set; }

    public string  MemberSlot { get; set; }

    public int  OwnExp { get; set; }

}
