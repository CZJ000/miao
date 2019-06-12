using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulitPool : MonoBehaviour
{

    public static BulitPool mInstance = null;

    public static BulitPool GetInstance()
    {
        return mInstance;
    }

    private void Awake()
    {
        mInstance = this;
    }

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        //设置一次对象池；
        ObjectPool.CreateObjectPool(ObjectPool.Bulidings);
    }

    void Update()
    {

    }

    /// <summary>
    /// 获得一个建筑物对象池；
    /// </summary>
    /// <param name="Built"></param>
    /// <returns></returns>
    public ObjectPool GetBulitPool(int Built)
    {

        return ObjectPool.GetObjectPoolByKey(ObjectPool.Bulidings + "Built" + Built);

    }
    //public void DestructAll()
    //{
    //    foreach (ObjectPool pool in mEffectPools.Values)
    //    {
    //        pool.DestructAll();
    //    }
    //}

    /// <summary>
    /// 删除所有的建筑物的活动对象；
    /// </summary>
    public void DestructAll()
    {
        ObjectPool.DestructObjectPoolsByType(ObjectPool.Bulidings);
    }

}
