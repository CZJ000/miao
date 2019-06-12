using UnityEngine;
using System.Collections;


/*
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 作用：传输数据的模型类
 * 
 * 
 * 时间 2017/1/6
 * 
 * */
public class BuildModelVo   {
     
	 public int foodbowid { get; set; }
    //在地图上自己建筑对应的ID，有四个建筑分别对应1，2，3，4
     public int ModeltrsId { get; set; }
    //对应Resource里面的prefabsID
     public int Modelid { get; set; }
   
}
