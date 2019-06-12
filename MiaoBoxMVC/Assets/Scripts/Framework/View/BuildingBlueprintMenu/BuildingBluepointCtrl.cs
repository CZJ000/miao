using UnityEngine;
using System.Collections.Generic;
using Global;
using UnityEngine.EventSystems;
using SUIFW;
public class BuildingBluepointCtrl : MonoBehaviour {

    /// <summary>
    /// 建筑类型ID
    /// </summary>
    public int foodBowID ;  
    /// <summary>
    /// 建筑是否已经建设
    /// </summary>
    public bool isBuild;
    /// <summary>
    /// 建筑位置ID
    /// </summary>
    public int modeltrsID;
    /// <summary>
    /// 建筑模型ID
    /// </summary>
    public int modelID;





	void Start () {
        //  UIEventListener.Get(gameObject).onClick += onBtnClick;


        //UIEventListener.Get(gameObject).onClick += TestOnButtonClick;
    }





    public void OnMouseUpAsButton()
    {
        /// 在执行点击事件之前，需要判定是否点击在UI上，如果在UI上则返回不执行下面的程序，防止穿透；
         if (CanvasUIMediator.Instance.IsInterceptFromUI) return;

        UIManager.GetInstance().ShowUIForms("LandedEstateMenuView");
      
        UIBaseBehaviour<LandedEstateMediator>.CreateUI<LandedEstateMenuView>();
        LandedEstateMediator mediator = AppFacade.GetInstance().RetrieveMediator(LandedEstateMediator.NAME) as LandedEstateMediator;
     
        mediator.landedEstateMenuView.BuildPoint = gameObject;
        mediator.landedEstateMenuView.isBulid = isBuild;
        mediator.landedEstateMenuView.modeltrsid = modeltrsID;
        mediator.landedEstateMenuView.tearDownBuilding = TearDownBuildingRefresh;
        //if (!mediator.landedEstateMenuView.IsInvoke)
        //{
            mediator.landedEstateMenuView.ShowLandedEstateView(foodBowID, modelID);
       // }
    }


    public void TearDownBuildingRefresh()
    {
        modelID = 0;
        
    }
 

    void TestOnButtonClick(GameObject go)
    {
      
      
      //  landedobj.GetComponent<LandedEstateMenuView>().ShowLandedEstateView(foodBowID, 6);
    }



    //private void onBtnClick(GameObject go)
    //{
    //    Debug.Log("onclick  onBtnClick BuildingBluepointCtrl");
    //    BuildingBlueprintMediator mediator = AppFacade.GetInstance().RetrieveMediator(BuildingBlueprintMediator.NAME) as BuildingBlueprintMediator;
    //    mediator.buldingBlueprint.isBulid = isBuild;
    //    mediator.buldingBlueprint.modeltrsid = modeltrsID;

    //    mediator.buldingBlueprint.buildingPos.transform.DestroyChildren();

    //    if (modelID != 0)
    //    {

    //        GameObject employee = BulitPool.GetInstance().GetBulitPool(modelID).CreateObject(mediator.buldingBlueprint.buildingPos.transform.position);
    //        employee.layer = 9;
    //        employee.transform.SetChildLayer(9);
    //        employee.transform.rotation = mediator.buldingBlueprint.buildingPos.transform.rotation;
    //        employee.transform.localScale = mediator.buldingBlueprint.buildingPos.transform.localScale;
    //        employee.transform.parent = mediator.buldingBlueprint.buildingPos.transform;
    //    }
       

    //    mediator.buldingBlueprint.showBuildingBlueprint(foodBowID, modelID);
    //    mediator.buldingBlueprint.menutype = MenuType.BuildingBlueprintMenu;

    //}  




    //void OnEnable()
    //{
    //    UIEventListener.Get(gameObject).onClick += TestOnButtonClick;
    //}

    //void OnDisable()
    //{
    //    // UIEventListener.Get(gameObject).onClick -= onBtnClick;
    //    UIEventListener.Get(gameObject).onClick -=TestOnButtonClick;
    //}


}
