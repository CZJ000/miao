using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Global;
using LitJson;
using System.Collections.Generic;

public class Cat3DModelInGroup : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler,IPointerClickHandler
{

    public bool triggerEnter;

    float screenRate;
    float realHeight;
    float screenHRate;
    float screenWRate;

   public Vector3 parent_posi;


    bool enterField;

    bool enterCat;

    public CatInGroupInfoVO infoVO;

    CharacterController character;
    BoxCollider collider;

   // public layer
    bool endDrag;
    List<Collider> colliderList=new List<Collider>();

    //Collider catInGroup;
    //Collider emptyField;
    //Collider catBg;
    //Collider dismissField;

    Collider head;

    Collider headFlag;
    // Use this for initialization
    void Start()
    {
        parent_posi = transform.parent.localPosition;
        screenRate = (float)Screen.height / Screen.width;
        realHeight = 1280 * screenRate;
        screenHRate = (float)realHeight / Screen.height;
        screenWRate = (float)1280 / Screen.width;
        character = transform.GetComponent<CharacterController>();
        character.enabled = false;
        collider = transform.gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = true;
        collider.center = new Vector3(0, 0.6f, 0f);
    }

    // Update is called once per frame
    void Update()
    {



    }


    private void OnTriggerExit(Collider other)
    {
        if (character.enabled == true)
        {

            triggerEnter = false;

            Debug.Log(other.tag + "  exit");

            //if (other.tag == TagName.CATINGROUP)
            //{
            //   // enterCat = true;

            //    catInGroup = null;
            //}
            //else if (other.tag == TagName.EMPTYGROUPFIELD)
            //{
            //  //  enterField = true;

            //    emptyField = null;
            //}
            //else if (other.tag == TagName.CATGROUPCATBG)
            //{
            //    catBg = null;
            //}
            //else if (other.tag == TagName.DIMISSFIELD)
            //{
            //    dismissField = null;
            //}
            //if (other.tag == TagName.CATINGROUP || other.tag == TagName.EMPTYGROUPFIELD || other.tag == TagName.CATGROUPCATBG || other.tag == TagName.DIMISSFIELD)
            //{ head = null; }
            if (colliderList.Contains(other))
            {
                colliderList.Remove(other);
            }
        }

    }



    private void OnTriggerEnter(Collider other)
    {

        if (character.enabled == true)
        {

            colliderList.Add(other);
        }
        
      

     
        //if (other.tag == TagName.CATINGROUP)
        //{
        //    //enterCat = true;
        //    //catInGroup = other;
        //    //emptyField = null;
        //    //catBg = null;
        //    //dismissField = null;

        //    head = other;

        //}
        //else if (other.tag == TagName.EMPTYGROUPFIELD)
        //{
        //    //enterField = true;

        //    //emptyField = other;
        //    //catInGroup = null;
        //    //catBg = null;
        //    //dismissField = null;

        //    head = other;

        //}
        //else if (other.tag == TagName.CATGROUPCATBG)
        //{
        //    //catBg = other;
        //    //emptyField = null;
        //    //catInGroup = null;
        //    //dismissField = null;

        //    head = other;
        //}
        //else if (other.tag == TagName.DIMISSFIELD)
        //{
        //    //catBg = null;
        //    //emptyField = null;
        //    //catInGroup = null;
        //    //dismissField = other;
        //    head = other;

        //}
    }

    void ResetPosi()
    {
       // triggerEnter = false;
        gameObject.transform.parent.localPosition = parent_posi;
        colliderList.Clear();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        character.enabled = true;
        collider.enabled = false;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        character.enabled = false;
        collider.enabled = true;
        collider.isTrigger = true;
        endDrag = true;

        Debug.Log(triggerEnter);

        //if (!triggerEnter)
        //{
        //    ResetPosi();
            
        //}


        //if (catInGroup != null)
        //{
        //    CatInGroupInfoVO otherInfo = catInGroup.GetComponent<Cat3DModelInGroup>().infoVO;
        //    if (otherInfo.GroupId == infoVO.GroupId)
        //    {
        //        gameObject.transform.parent.localPosition = parent_posi;
        //    }
        //    else
        //    {
        //        ChangeCatInfoVO changeCatInfoVO = new ChangeCatInfoVO();

        //        changeCatInfoVO.OneType = infoVO.Type;
        //        changeCatInfoVO.OtherType = otherInfo.Type;

        //        Debug.Log(changeCatInfoVO.OneType);
        //        Debug.Log(changeCatInfoVO.OtherType);

        //        changeCatInfoVO.OneCatID = infoVO.StoreId;
        //        changeCatInfoVO.OneCatGroupID = infoVO.GroupId;
        //        changeCatInfoVO.OtherCatID = otherInfo.StoreId;
        //        changeCatInfoVO.OtherCatGroupID = otherInfo.GroupId;

        //        Debug.Log("111");
        //        if (SwitchCheck(infoVO, otherInfo))
        //        {
        //            Debug.Log("swtich");
        //            AppFacade.Instance.SendNotification(NotiConst.CAT_CHANGE_GROUP, changeCatInfoVO);
        //        }


        //    }
        //}
        //else if (catInGroup == null && emptyField != null)
        //{
        //    if (infoVO.GroupId == 5)
        //    {
        //        gameObject.transform.parent.localPosition = parent_posi;
        //    }
        //    else
        //    {
        //        ChangeCatInfoVO changeCatInfoVO = new ChangeCatInfoVO();
        //        changeCatInfoVO.OneCatID = infoVO.StoreId;
        //        changeCatInfoVO.OneType = infoVO.Type;
        //        changeCatInfoVO.OneCatGroupID = infoVO.GroupId;
        //        changeCatInfoVO.OtherCatID = -1;
        //        changeCatInfoVO.OtherCatGroupID = 5;

        //        CatGroupProxy catgroupproxy = AppFacade.Instance.RetrieveProxy(CatGroupProxy.NAME) as CatGroupProxy;
        //        bool islimt = catgroupproxy.limitCatGroupNum(5, 1);
        //        if (islimt)
        //        {
        //            MessagePanelContorler.Instance.ShowMessage("第五组人数已满");

        //        }
        //        else
        //        {
        //            AppFacade.Instance.SendNotification(NotiConst.CAT_CHANGE_GROUP, changeCatInfoVO);
        //        }


        //    }

        //}
        //else if (catBg != null)
        //{
        //    CatInGroupInfoVO otherInfo = catBg.transform.parent.GetComponent<CatInGroupItem>().catInGroupInfo;
        //    if (otherInfo != null)
        //    {
        //        if (otherInfo.GroupId == infoVO.GroupId)
        //        {
        //            gameObject.transform.parent.localPosition = parent_posi;
        //        }
        //        else
        //        {
        //            ChangeCatInfoVO changeCatInfoVO = new ChangeCatInfoVO();
        //            changeCatInfoVO.OneType = otherInfo.Type;
        //            changeCatInfoVO.OtherType = infoVO.Type;
        //            changeCatInfoVO.OneCatID = infoVO.StoreId;
        //            changeCatInfoVO.OneCatGroupID = infoVO.GroupId;
        //            changeCatInfoVO.OtherCatID = -1;
        //            changeCatInfoVO.OtherCatGroupID = otherInfo.GroupId;

        //            if (SwitchCheck(infoVO, otherInfo))
        //            {
        //                Debug.Log("swtich");
        //                AppFacade.Instance.SendNotification(NotiConst.CAT_CHANGE_GROUP, changeCatInfoVO);
        //            }
        //        }

        //    }
        //    else
        //    {
        //        otherInfo = new CatInGroupInfoVO();
        //        otherInfo.GroupId = catBg.transform.parent.GetComponent<CatInGroupItem>().groupId;
        //        ChangeCatInfoVO changeCatInfoVO = new ChangeCatInfoVO();
        //        changeCatInfoVO.OneType = infoVO.Type;
        //        changeCatInfoVO.OneCatID = infoVO.StoreId;
        //        changeCatInfoVO.OneCatGroupID = infoVO.GroupId;
        //        changeCatInfoVO.OtherCatID = -1;
        //        changeCatInfoVO.OtherCatGroupID = otherInfo.GroupId;

        //        if (otherInfo.GroupId == infoVO.GroupId)
        //        {
        //            gameObject.transform.parent.localPosition = parent_posi;
        //            return;
        //        }


        //        Debug.Log("other ID" + otherInfo.GroupId);

        //        AppFacade.Instance.SendNotification(NotiConst.CAT_CHANGE_GROUP, changeCatInfoVO);
        //    }

        //}
        //else if (dismissField != null)
        //{

        //    JsonData data = new JsonData();
        //    data["groupId"] = infoVO.GroupId;
        //    data["id"] = infoVO.StoreId;
        //    AppFacade.Instance.SendNotification(NotiConst.CAT_DELETE, data);

        //}
     
        if (colliderList.Count > 0)
        {
            head = colliderList[colliderList.Count - 1];
        }
        else
        {
            head = null;
            ResetPosi();
        }
        
        if (head != null)
        {
            if (head.tag == TagName.CATINGROUP)
            {
                CatInGroupInfoVO otherInfo = head.GetComponent<Cat3DModelInGroup>().infoVO;
                if (otherInfo.GroupId == infoVO.GroupId)
                {
                    ResetPosi();
                }
                else
                {              
                    ChangeCatInfoVO changeCatInfoVO = new ChangeCatInfoVO();

                    changeCatInfoVO.OneType = infoVO.Type;
                    changeCatInfoVO.OtherType = otherInfo.Type;

                    Debug.Log(changeCatInfoVO.OneType);
                    Debug.Log(changeCatInfoVO.OtherType);

                    changeCatInfoVO.OneCatID = infoVO.StoreId;
                    changeCatInfoVO.OneCatGroupID = infoVO.GroupId;
                   
                    if (otherInfo.GroupId == 5)
                    {
                        changeCatInfoVO.OtherCatID = -1;

                    }
                    else
                    {
                        changeCatInfoVO.OtherCatID = otherInfo.StoreId;
                    }
                    changeCatInfoVO.OtherCatGroupID = otherInfo.GroupId;

                    Debug.Log("111");
                    if (SwitchCheck(infoVO, otherInfo))
                    {
                        Debug.Log("swtich");
                        AppFacade.Instance.SendNotification(NotiConst.CAT_CHANGE_GROUP, changeCatInfoVO);
                    }


                }
            }
            else if (head.tag == TagName.EMPTYGROUPFIELD)
            {
                if (infoVO.GroupId == 5)
                {
                    ResetPosi();
                }
                else
                {
                    ChangeCatInfoVO changeCatInfoVO = new ChangeCatInfoVO();
                    changeCatInfoVO.OneCatID = infoVO.StoreId;
                    changeCatInfoVO.OneType = infoVO.Type;
                    changeCatInfoVO.OneCatGroupID = infoVO.GroupId;
                    changeCatInfoVO.OtherCatID = -1;
                    changeCatInfoVO.OtherCatGroupID = 5;

                    CatGroupProxy catgroupproxy = AppFacade.Instance.RetrieveProxy(CatGroupProxy.NAME) as CatGroupProxy;
                    bool islimt = catgroupproxy.limitCatGroupNum(5, 1);
                    if (islimt)
                    {
                        MessageView.GetInstance().ShowMessage("第五组人数已满");

                    }
                    else
                    {
                        AppFacade.Instance.SendNotification(NotiConst.CAT_CHANGE_GROUP, changeCatInfoVO);
                    }


                }

            }
            else if (head.tag == TagName.CATGROUPCATBG)
            {
                CatInGroupInfoVO otherInfo = head.transform.parent.GetComponent<CatInGroupItem>().catInGroupInfo;
                if (otherInfo != null)
                {
                    if (otherInfo.GroupId == infoVO.GroupId)
                    {
                        ResetPosi();
                    }
                    else
                    {
                     

                        ChangeCatInfoVO changeCatInfoVO = new ChangeCatInfoVO();
                        changeCatInfoVO.OneType = otherInfo.Type;
                        changeCatInfoVO.OtherType = infoVO.Type;
                        changeCatInfoVO.OneCatID = infoVO.StoreId;
                        changeCatInfoVO.OneCatGroupID = infoVO.GroupId;
                        changeCatInfoVO.OtherCatID = -1;
                        changeCatInfoVO.OtherCatGroupID = otherInfo.GroupId;

                        if (SwitchCheck(infoVO, otherInfo))
                        {
                            Debug.Log("swtich");
                            AppFacade.Instance.SendNotification(NotiConst.CAT_CHANGE_GROUP, changeCatInfoVO);
                        }
                    }

                }
                else
                {
                    otherInfo = new CatInGroupInfoVO();
                    otherInfo.GroupId = head.transform.parent.GetComponent<CatInGroupItem>().groupId;
                    ChangeCatInfoVO changeCatInfoVO = new ChangeCatInfoVO();
                    changeCatInfoVO.OneType = infoVO.Type;
                    changeCatInfoVO.OneCatID = infoVO.StoreId;
                    changeCatInfoVO.OneCatGroupID = infoVO.GroupId;
                    changeCatInfoVO.OtherCatID = -1;
                    changeCatInfoVO.OtherCatGroupID = otherInfo.GroupId;

                    if (otherInfo.GroupId == infoVO.GroupId)
                    {
                        ResetPosi();
                        return;
                    }


                    Debug.Log("other ID" + otherInfo.GroupId);

                    AppFacade.Instance.SendNotification(NotiConst.CAT_CHANGE_GROUP, changeCatInfoVO);
                }

            }
            else if (head.tag == TagName.DIMISSFIELD)
            {

                JsonData data = new JsonData();
                data["groupId"] = infoVO.GroupId;
                data["id"] = infoVO.StoreId;
                AppFacade.Instance.SendNotification(NotiConst.CAT_DELETE, data);

            }
        }
      

    }




    bool SwitchCheck(CatInGroupInfoVO one, CatInGroupInfoVO other)
    {

        if (other.GroupId != 5&& other.GroupId !=4)
        {
            if (one.Type != other.Type)
            {
                MessageView.GetInstance().ShowMessage("只有队长猫才能和队长猫交换！");
                ResetPosi();
                return false;
            }
            else if (one.Attribute != other.Attribute && one.Attribute != null && other.Attribute != null)
            {
                MessageView.GetInstance().ShowMessage("属性不符，不能交换");
                ResetPosi();
                return false;
            }
            else
            {
                return true;
            }

        }
        else 
        {
            return true;
        }
        


           
        
       

    }





    public void OnDrag(PointerEventData eventData)
    {
        transform.parent.localPosition= new Vector3(Input.mousePosition.x * screenWRate - 1280 / 2.0f, Input.mousePosition.y * screenHRate - realHeight / 2.0f, transform.parent.localPosition.z);

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!eventData.dragging)
        {

            CatGroupViewMediator catGroupViewMediator= AppFacade.Instance.RetrieveMediator(CatGroupViewMediator.NAME) as CatGroupViewMediator;
            CatGroupView catGroupView=catGroupViewMediator.CatGroupView;

            catGroupView.catName.enabled = true;

            catGroupView.level.enabled = true;
            catGroupView.energy.enabled = true;
            catGroupView.introduce.enabled = true;


            catGroupView.catName.text = infoVO.Name;
            catGroupView.energy.text = infoVO.Power+"";
            catGroupView.introduce.text = infoVO.About;
            catGroupView.level.text = infoVO.Level+"";
            catGroupView.ShowSelectCat3DModel(infoVO.CatTypeId);
        }
       
    }
}
