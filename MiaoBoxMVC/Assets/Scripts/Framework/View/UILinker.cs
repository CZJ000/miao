/********************************************************************
	created:	2016/08/18
	created:	18:8:2016   22:24
	filename: 	F:\Users\Administrator\Projects\MiaoBox\miaobox\MiaoBoxMVC\Assets\Scripts\Framework\View\UILinker.cs
	file path:	F:\Users\Administrator\Projects\MiaoBox\miaobox\MiaoBoxMVC\Assets\Scripts\Framework\View
	file base:	UILinker
	file ext:	cs
	author:		Zhou Jingren
	
	purpose:	ui的配置文件，包含文件位置等
*********************************************************************/
using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// ui的配置文件，包含文件位置等
/// </summary>
public class UILinker
{
    public static GameObject CreateUIObject(string behaviorType)
    {
        GameObject gameobject = null;
        switch (behaviorType)
        {

            case "CatGroupView":
                gameobject = GameObject.FindGameObjectWithTag("CatGroupView");
                break;

            case "EmployeeView":
                gameobject = GameObject.FindGameObjectWithTag("EmployeeView");
                break;

            case "SpawnRandomCatView":

                gameobject = GameObject.FindGameObjectWithTag("SpawnRandomCatView");
                break;
            case "ShopView":

                gameobject = GameObject.FindGameObjectWithTag("ShopView");
                break;
            case "ShopMenuView":
                gameobject = GameObject.FindGameObjectWithTag("ShopMenuView");
                break;
            case "LandedEstateMenuView":

                gameobject= GameObject.FindGameObjectWithTag("LandedEstateMenuView");
                break;

            case "LoginView":
                //prefabPath = "UI/LoginMenu/LoginMenu";
                gameobject = LoginView.Instance.gameObject;
                break;

            case "ClerkAreaView":
                //prefabPath = "UI/LoginMenu/LoginMenu";
                gameobject = GameObject.FindGameObjectWithTag("ClerkAreaView");
                break;


            case "MainMenuView":
                //prefabPath = "UI/MainMenu/Prefab/MainMenuUI";
                gameobject = MainMenuView.Instance.gameObject;
                break;
            case "CatGroupContoler":
                gameobject = CatGroupContoler.Instance.gameObject;
                //prefabPath = "UI/CatGroupMenu/CatGroupMenu";
                break;
            case "MessageView":
                //prefabPath = "UI/MessageNotification/MessageNotification";
                break;
            
            case "BuildingChangeCtrl":
                gameobject = GameObject.FindGameObjectWithTag("BuildingChangeCtrl");
                Debug.Log("BuildingChangeCtrl!!~~~~~~~~~~~~~~~~");
                break;
            case "BattleView":
                gameobject = GameObject.FindGameObjectWithTag("BattleView");
                break;
            case "BattleInfoView":
                 gameobject = GameObject.FindGameObjectWithTag("BattleInfoView");
                break;
            default:
                Debug.LogError("Undifined UI Type: " + behaviorType);
                return null;
        }
        //prefab = Resources.Load(prefabPath);
        //return GameObject.Instantiate(prefab) as GameObject;
        return gameobject;
    }
}
