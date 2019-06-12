using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BattleUIVO   {

   public  Hudparticipant hudparticipant { get; set; }
   public  string TextContext { get; set; }
   public  Color TextColor { get; set; }
   public  float IntervalTime { get; set; }
   public string spritename { get; set; }
    public float yInterval { get; set; }

   public BattleUIVO()
    {

    }
    public BattleUIVO(string textcontext,Color color,float intervaltime)
    {
        this.TextContext = textcontext;
        this.TextColor = color;
        this.IntervalTime = intervaltime;
    }


}
