using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Global;
public class SpawnRandomCatView : MonoBehaviour
{


    public GameObject[] bornPoints;
    public GameObject[] disappearPoints;


    //雇员刷新时间数据 暂时不写到Proxy中
    private float refreshTime = 0f;
    private float currentTime = 0f;
    private float lastTime = 0f;

    // Use this for initialization
    void Start()
    {
        refreshTime = Random.Range(1f, 5f);

        currentTime = Time.realtimeSinceStartup;
        lastTime = Time.realtimeSinceStartup;

    }

    // Update is called once per frame
    void Update()
    {

    }

    //10~15秒 生出来一只小雇员
    void FixedUpdate()
    {
        currentTime = Time.realtimeSinceStartup;
        if (currentTime - lastTime > refreshTime)
        {
            SendAddEmployeeCommand();

            refreshTime = Random.Range(1f, 5f);
            lastTime = Time.realtimeSinceStartup;
        }
    }


    public void GenerateEmployee(EmployeeInfoVO employeeVO)
    {

        //设置出生点和消失点
        int bornPointIndex = Random.Range(0, 4);
        Vector3 bornPoint = bornPoints[bornPointIndex].transform.position;
        Vector3 disappearPoint = disappearPoints[bornPointIndex].transform.position;

        GameObject employee = CatPool.GetInstance().GetCatPool(employeeVO.Id).CreateObject(bornPoint);

        EmployeeCtl employeeCtl = employee.AddComponent<EmployeeCtl>();
        employeeCtl.bornPosition = bornPoint;
        employeeCtl.disappearPosition = disappearPoint;
        employee.GetComponent<CharacterController>().enabled = true;

        EmployeeInfo employeeInfo = employee.AddComponent<EmployeeInfo>();
        employeeInfo.Id = employeeVO.Id;
        employeeInfo.Name = employeeVO.Name;
        employeeInfo.Level = employeeVO.Level;
        employeeInfo.Evo = employeeVO.Evo;
        employeeInfo.Iq = employeeVO.Iq;
        employeeInfo.Power = employeeVO.Power;
        employeeInfo.React = employeeVO.React;
        employeeInfo.Skill = employeeVO.Skill;
        employeeInfo.About = employeeVO.About;
        employeeInfo.Hireprice = employeeVO.HirePrice;
    }

    private void SendAddEmployeeCommand()
    {
        
        AppFacade.getInstance.SendNotification(NotiConst.ADD_EMPLOYEE_MODEL);
    }

}
