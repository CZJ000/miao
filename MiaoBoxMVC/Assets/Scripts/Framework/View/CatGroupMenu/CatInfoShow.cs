using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 猫信信息的展示方法，抽象类；
/// </summary>
public abstract class CatInfoShow : MonoBehaviour
{

    protected abstract void AfterStart();

    public abstract void ShowMethod(CatInfo cat); 

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
