using PureMVC.Patterns;
using PureMVC.Interfaces;
using System.Collections.Generic;
using Global;
using UnityEngine;
public class SpawnRandomCatMediator : Mediator, IMediator
{

    public new const string NAME = "SpawnRandomCatMediator";

    public const string GENERATE_EMPLOYEE = "GENERATE_EMPLOYEE";
    //public const string EMPLOY_SUCCESS = "EMPLOY_SUCCESS";
    //public const string EMPLOY_FAILURE = "EMPLOY_FAILURE";


    public SpawnRandomCatMediator() : base(NAME)
    {
    }

    public SpawnRandomCatView spawnRandomCatView
    {
        get
        {
            return ViewComponent as SpawnRandomCatView;
        }
    }

    public override IEnumerable<string> ListNotificationInterests
    {
        get
        {
            List<string> list = new List<string>();
            list.Add(SpawnRandomCatMediator.GENERATE_EMPLOYEE);
            //list.Add(EmployeeMediator.EMPLOY_SUCCESS);
            //list.Add(EmployeeMediator.EMPLOY_FAILURE);
            return list;
        }
    }
    public override void HandleNotification(INotification notification)
    {
       
        switch (notification.Name)
        {
            case SpawnRandomCatMediator.GENERATE_EMPLOYEE:

                spawnRandomCatView.GenerateEmployee((EmployeeInfoVO)notification.Body);
                break;
            //case EmployeeMediator.EMPLOY_SUCCESS:
            //    employeeView.EmploySuccess();
            //    break;
            //case EmployeeMediator.EMPLOY_FAILURE:

            //    employeeView.EmployFailure((FailType)notification.Body);
            //    break;
        }
    }
}
