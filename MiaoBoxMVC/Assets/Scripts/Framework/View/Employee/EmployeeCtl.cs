using UnityEngine;
using System.Collections;
using Global;
using SUIFW;
public class EmployeeCtl : MonoBehaviour {

    public enum EmployeeState
    {
        FALL_OFF,
        MOVE
    }

    public Vector3 bornPosition;
    public Vector3 disappearPosition;

    private float fallSpeed = -0.3f;
    private float moveSpeed = 0.5f;

    private Animation m_animation;
    private CharacterController m_characterController;
    private EmployeeState m_employeeState;
    private EmployeeView _employeeview;

    // Use this for initialization
    void Start () {
        m_animation = this.GetComponent<Animation>();
        m_characterController = this.GetComponent<CharacterController>();
        //禁用
       // UIEventListener.Get(gameObject).onClick += ShowEmployeeInfo;

    }



    private void OnMouseUpAsButton()
    {

        /// 在执行点击事件之前，需要判定是否点击在UI上，如果在UI上则返回不执行下面的程序，防止穿透 ；
        if (CanvasUIMediator.Instance.IsInterceptFromUI) return;


        ShowEmployeeInfo();
    }

    void OnEnable()
    {
        m_employeeState = EmployeeState.FALL_OFF;
    }

    void OnDisable()
    {
        Destroy(this);
      //  UIEventListener.Get(gameObject).onClick -= ShowEmployeeInfo;
    }

	// Update is called once per frame
	void Update () {
        switch (m_employeeState)
        {
            case EmployeeState.FALL_OFF:
                if (transform.position.y <= -0.4f)
                {
                    m_employeeState = EmployeeState.MOVE;
                }
                else
                {
                    Gravity();
                }
                break;
            case EmployeeState.MOVE:
                TransMove();
                if ((transform.position - disappearPosition).sqrMagnitude < 0.5f)
                {
                    gameObject.SetActive(false); //隐掉 回收
                }
                break;
            default:break;
        }
        if (_employeeview && _employeeview.employeesussce)    //处理雇佣成功后模型的隐藏
        {

            gameObject.SetActive(false );
        }
	}

    private void Gravity()
    {
        this.gameObject.transform.Translate(new Vector3(0, fallSpeed * Time.deltaTime, 0));
    }

    private void TransMove()
    {
        if (disappearPosition != null && bornPosition != null)
        {
            Vector3 direction = (disappearPosition - bornPosition).normalized;
            gameObject.transform.Translate(direction * Time.deltaTime * moveSpeed, Space.World);
            Vector3 lookAtDir = Vector3.RotateTowards(transform.forward, direction, 5f * Time.deltaTime, 10000);
            transform.LookAt(transform.position + lookAtDir);
            m_animation.CrossFade("run", 0.1f);
        }
    }

    private void ShowEmployeeInfo()
    {

        UIManager.GetInstance().ShowUIForms("EmployeeView");
        UIBaseBehaviour<EmployeeMediator>.CreateUI<EmployeeView>();
        EmployeeInfo employeeInfo = this.GetComponent<EmployeeInfo>();
        EmployeeMediator employeeMediator = AppFacade.getInstance.RetrieveMediator(EmployeeMediator.NAME) as EmployeeMediator;
        _employeeview = employeeMediator.employeeView;

        _employeeview.employeesussce = false;
        _employeeview.currentClickCatInfo = employeeInfo;

       // _employeeview.IsInvoke = true;
       
        GameObject employee = CatPool.GetInstance().GetCatPool(employeeInfo.Id).CreateObject(_employeeview.catPosition.localPosition);
        employee.AddComponent<EmployeeRandomAnimation>();



        int layer = LayerMask.NameToLayer(NotiConst.Layer_EmployeeCat);
        employee.layer = layer;
        //GameObjectUtils.SetLayerRecursively(employee, layer);
        employee.transform.SetChildLayer(layer);
       
       employee.transform.rotation = _employeeview.catPosition.transform.rotation;
        employee.transform.parent = _employeeview.catPosition.transform;
        employee.transform.localPosition = Vector3.zero;
       employee.transform.localScale = new Vector3(32,32,32);
        employee.transform.parent.localScale = new Vector3(5,5,5);
        employee.transform.parent.localRotation = Quaternion.Euler(new Vector3(0,180,0));


        _employeeview.eName.text = employeeInfo.Name ;
        _employeeview.eLevel.text =  employeeInfo.Level.ToString();
        _employeeview.eAtk.text =  employeeInfo.Power.ToString();
        _employeeview.eSkill.text =  employeeInfo.Skill.ToString();
        _employeeview.eHireprice.text = employeeInfo.Hireprice.ToString();
       
        
    }

}
