using UnityEngine;

using UnityEngine.EventSystems;
public class SelectBuilding3DItem : MonoBehaviour,IPointerClickHandler
{

  public  int buildingId;




    public void OnPointerClick(PointerEventData eventData)
    {

       
        LandedEstateMenuView view = AppFacade.Instance.RetrieveMediator(LandedEstateMediator.NAME).ViewComponent as LandedEstateMenuView;

        view.Click3DItemChangeInfo(buildingId);

    }
}