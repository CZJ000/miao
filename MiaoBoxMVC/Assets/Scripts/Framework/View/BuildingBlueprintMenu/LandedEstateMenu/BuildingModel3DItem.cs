using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Global;
using UnityEngine.EventSystems;
public class BuildingModel3DItem : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler,IPointerClickHandler{

    Vector3 posi;



    bool endDrag;
    bool triggerEnter;

    float onMouseDragPosiZ;
    float screenRate;
    float realHeight;
    float screenHRate;
    float screenWRate;
    /// <summary>
    /// 建筑蓝图数据
    /// </summary>
    /// <value>The blueprint data.</value>
    public stat_blueprintRow blueprintData
    {
        get;
        set;
    }
    /// <summary>
    /// 建筑数据
    /// </summary>
    public stat_buildingRow bulitData
    {
        get;
        set;
        
    }


    /// <summary>
    /// 存储建筑ID， 显示建筑的时候使用
    /// </summary>
    /// <value>The building I.</value>
    public int buildingID;






    int selectBuildingIDCache;



    LandedEstateMenuView view;



    private void Start()
    {
     //   UIEventListener.Get(this.gameObject).onDrag+= Drag;
        posi = gameObject.transform.parent.localPosition;
        onMouseDragPosiZ = gameObject.transform.parent.localPosition.z -140;
       
         screenRate = (float)Screen.height / Screen.width;
        realHeight = 1280* screenRate;
        screenHRate = (float)realHeight / Screen.height;
        screenWRate= (float)1280 / Screen.width;
        view = AppFacade.Instance.RetrieveMediator(LandedEstateMediator.NAME).ViewComponent as LandedEstateMenuView; 

    }


    private void Update()
    {
       // Debug.Log(Input.mousePosition.y * screenHRate - 800 / 2.0f);
    }

    private void OnTriggerStay(Collider other)
    {
        if (endDrag && other.gameObject.tag == TagName.SELECTBUILDING3DUI)
        {
          

             
            if (view.IsInvoke)
            {
                selectBuildingIDCache = LandedEstateMenuView.buildingSelectItem;
                LandedEstateMenuView.buildingSelectItem = buildingID;
                view.resetTipWinPosi = ResetPosi;
                view.cancelChangeModel = CancelChangeModel;
                view.ChaneModelTipWindow.SetActive(true);
            }
        }

    }



    private void OnTriggerEnter(Collider other)
    {


        if (other.tag == TagName.SELECTBUILDING3DUI)
        {
            triggerEnter = true;
        }


    }


    private void OnTriggerExit(Collider other)
    {
        triggerEnter = false;
    }




 


    public void CancelChangeModel()
    {
        LandedEstateMenuView.buildingSelectItem = selectBuildingIDCache;
    }

    public void ResetPosi()
    {
        Debug.Log(posi);
        gameObject.transform.parent.localPosition = posi;
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        endDrag = false;
        gameObject.transform.parent.localPosition = new Vector3(Input.mousePosition.x * screenWRate - 1280 / 2.0f, Input.mousePosition.y * screenHRate - realHeight / 2.0f, onMouseDragPosiZ);
       
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!eventData.dragging)
        {

            view.Click3DItemChangeInfo(buildingID);
            Debug.Log("Click");
        }
       
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //endDrag = false;
        //gameObject.transform.parent.localPosition = new Vector3(Input.mousePosition.x* screenWRate - 1280 / 2.0f,Input.mousePosition.y*screenHRate- realHeight / 2.0f, onMouseDragPosiZ);
        Debug.Log("beginDrag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        endDrag = true;
        if (!triggerEnter)
        {
            gameObject.transform.parent.localPosition = posi;
        }
        Debug.Log("endDrag");
    }
}
