﻿/********************************************************************
	created:	2016/08/18
	created:	18:8:2016   22:57
	filename: 	F:\Users\Administrator\Projects\MiaoBox\miaobox\MiaoBoxMVC\Assets\Scripts\Framework\Controller\Commands\ShowMainMenuUICommand.cs
	file path:	F:\Users\Administrator\Projects\MiaoBox\miaobox\MiaoBoxMVC\Assets\Scripts\Framework\Controller\Commands
	file base:	ShowMainMenuUICommand
	file ext:	cs
	author:		Zhou Jingren
	
	purpose:	显示MainMenu,初始化UserInfo
*********************************************************************/
using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using SUIFW;
public class InitMainMenuUICommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        UIManager.GetInstance().ShowUIForms("MainMenuView");
      
        UIBaseBehaviour<MainMenuUIMediator>.CreateUI<MainMenuView>();
        AppFacade.getInstance.RegisterProxy(new ManorInfoProxy());
        AppFacade.getInstance.RegisterProxy(new CatGroupProxy());
        AppFacade.getInstance.RegisterProxy(new EmployeeProxy());
        AppFacade.getInstance.RegisterProxy(new BuildBlueprintProxy());
        AppFacade.getInstance.RegisterProxy(new UserAiInfoProxy());
        
    }
}
