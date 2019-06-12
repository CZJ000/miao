using UnityEngine;
using System.Collections;
using Global;
using UnityEngine.EventSystems;
public class ShopMenu3DItem : MonoBehaviour,IPointerClickHandler,IDragHandler,IEndDragHandler
{



    public int id;


    Vector3 posi;



    bool endDrag;
    bool triggerEnter;

    float onMouseDragPosiZ;
    float screenRate;
    float realHeight;
    float screenHRate;
    float screenWRate;

    ShopMenuView view;

    /// <summary>
    /// 建筑蓝图数据
    /// </summary>
    /// <value>The blueprint data.</value>
    public stat_blueprintRow blueprintData
    {
        get;
        set;
    }
    // Use this for initialization
    void Start()
    {
        view = AppFacade.Instance.RetrieveMediator(ShopMenuViewMediator.NAME).ViewComponent as ShopMenuView; 
        posi = gameObject.transform.parent.localPosition;
        onMouseDragPosiZ = gameObject.transform.parent.localPosition.z - 70;

        screenRate = (float)Screen.height / Screen.width;
        realHeight = 1280 * screenRate;
        screenHRate = (float)realHeight / Screen.height;
        screenWRate = (float)1280 / Screen.width;
    }




    private void OnTriggerStay(Collider other)
    {
        if (endDrag && other.gameObject.tag == TagName.SHOPMENUBUYFIELD)
        {

            ShopMenuView view = AppFacade.Instance.RetrieveMediator(ShopMenuViewMediator.NAME).ViewComponent as ShopMenuView;
            if (view.IsInvoke)
            {
                //selectBuildingIDCache = LandedEstateMenuView.buildingSelectItem;
               // LandedEstateMenuView.buildingSelectItem = buildingID;
                view.resetTipWinPosi = ResetPosi;
                //view.cancelChangeModel = CancelChangeModel;
                view.GoodsInBuyField(id);
            }
        }

    }

    public void ResetPosi()
    {

        gameObject.transform.parent.localPosition = posi;

    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.tag == TagName.SHOPMENUBUYFIELD)
        {
            triggerEnter = true;
        }


    }


    private void OnTriggerExit(Collider other)
    {
        triggerEnter = false;
    }


    //private void OnMouseDown()
    //{
    //    ShopMenuView view = AppFacade.Instance.RetrieveMediator(ShopMenuViewMediator.NAME).ViewComponent as ShopMenuView;

    //    view.SetTextInfoWhenSelect(id);

    //}

    //private void OnMouseUp()
    //{

    //    mouseUp = true;
    //    if (!triggerEnter)
    //    {
    //        gameObject.transform.parent.localPosition = posi;
    //    }

    //}


    //private void OnMouseDrag()
    //{

    //    mouseUp = false;
    //    



    //}





    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!eventData.dragging)
        {
            view.SetTextInfoWhenSelect(id);
        }
    }




    public void OnDrag(PointerEventData eventData)
    {
        endDrag = false;
        gameObject.transform.parent.localPosition = new Vector3(Input.mousePosition.x * screenWRate - 1280 / 2.0f, Input.mousePosition.y * screenHRate - realHeight / 2.0f, onMouseDragPosiZ);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        endDrag = true;
        if (!triggerEnter)
        {
            gameObject.transform.parent.localPosition = posi;
        }
    }
}
