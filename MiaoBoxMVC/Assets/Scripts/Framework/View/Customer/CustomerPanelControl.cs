using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerPanelControl : MonoBehaviour
{
    /// <summary>
    /// 静态单例；
    /// </summary>
    public static CustomerPanelControl Instance;
    /// <summary>
    /// 是否激活；
    /// </summary>
    public bool IsInvoke
    {
        get
        {
            return transform.localScale != Vector3.zero;
        }
        set
        {
            transform.localScale = value ? Vector3.one : Vector3.zero;
        }
    }


    private void Awake()
    {
        Instance = this;
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
