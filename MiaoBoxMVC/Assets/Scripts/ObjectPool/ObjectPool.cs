using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Global;

public class ObjectPool
{


    /*
     * 对象池的键值按照其在Resources文件夹下的路径来表示。
     * 每一个不同的对象都有一个单独的对象池存在字典中；
     * 获取对象池或者对象都可以通过此处的静态方法来取得；
     */


    #region 对象池中的对象类型；

    /// <summary>
    /// 猫
    /// </summary>
    public const string Cats = "Characters/";
    /// <summary>
    /// 建筑物；
    /// </summary>
    public const string Bulidings = "Built/";

    #endregion

    #region 公用对象池部分；

    /// <summary>
    /// 所有的对象池；
    /// </summary>
    static Dictionary<string, ObjectPool> DicAllObjectPool = new Dictionary<string, ObjectPool>();
    /// <summary>
    /// 所有的父对象池；
    /// </summary>
    static Dictionary<string, Transform> DicAllRoots = new Dictionary<string, Transform>();
    
    /// <summary>
    /// 获得一个对象的方法；
    /// </summary>
    /// <param name="type">对象类型</param>
    /// <param name="name">对象名字</param>
    /// <returns></returns>
    public static GameObject GetObjectFromPool(string type, string name = "1")
    {
        //该对象的键值；
        string key = type + name;
        if (DicAllObjectPool.ContainsKey(key))
        {
            return DicAllObjectPool[key].GetPrefab();
        }
        else
        {
            GetNewObjectPool(type, name);
            return GetObjectFromPool(type, name);
        }
    }

    /// <summary>
    /// 通过键值来获取对象池；
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static ObjectPool GetObjectPoolByKey(string key)
    {
        //Debug.Log("加载路径："+key);
        if (DicAllObjectPool.ContainsKey(key))
        {
            return DicAllObjectPool[key];
        }
        else
        {
            GetNewObjectPool(key);
            return GetObjectPoolByKey(key);
        }
    }

    /// <summary>
    /// 根据类型获得的对象池的字典；保存该类型对象池的键值列表；
    /// </summary>
    static Dictionary<string, List<string>> DicAllObjectPoolByType = new Dictionary<string, List<string>>();

    /// <summary>
    /// 将某种类型的物品全部加进对象池中；
    /// </summary>
    /// <param name="type"></param>
    public static void CreateObjectPool(string type)
    {
        Object[] objs = Resources.LoadAll(type);
        //制作一个列表；
        List<string> ListKey = new List<string>();
        foreach (var obj in objs)
        {
            string key = (obj as GameObject).name;
            GetNewObjectPool(type, key);
            //获取键值；
            ListKey.Add(key);
        }
        //之后添加进字典中；
        if (DicAllObjectPoolByType.ContainsKey(type))
        {
            DicAllObjectPoolByType[type] = ListKey;
        }
        else
        {
            DicAllObjectPoolByType.Add(type, ListKey);
        }
    }

    /// <summary>
    /// 根据类型和名字来获取新对象池；并且返回这个对象池
    /// </summary>
    /// <param name="type"></param>
    /// <param name="name"></param>
    static ObjectPool GetNewObjectPool(string type, string name)
    {
        string path = type + name;
        GameObject obj = (Resources.Load(path)) as GameObject;
        //之后进行造池并存储；
        ObjectPool op = new ObjectPool();
        op.Init(path, obj, 0, 0);
        op.doNotDestruct = true;
        op.SetRoot(GetObjectPoolRoot(path));
        //存字典；
        DicAllObjectPool.Add(path, op);
        //返回这个对象池；
        return op;
    }

    /// <summary>
    /// 根据对象池类型批量销毁对象池中的活动对象；
    /// </summary>
    /// <param name="type"></param>
    public static void DestructObjectPoolsByType(string type)
    {
        if (DicAllObjectPoolByType.ContainsKey(type))
        {
            var ListKey = DicAllObjectPoolByType[type];
            for (int i = 0; i < ListKey.Count; i++)
            {
                Debug.Log(ListKey[i]);
                if (DicAllObjectPool.ContainsKey(ListKey[i]))
                {
                    Debug.Log(111);
                    DicAllObjectPool[ListKey[i]].DestructAll();
                }
               
            }
        }
    }

    /// <summary>
    /// 获取一个新的对象池；
    /// </summary>
    /// <param name="path"></param>
    static void GetNewObjectPool(string path)
    {
        GameObject obj = (Resources.Load(path)) as GameObject;
        //之后进行造池并存储；
        ObjectPool op = new ObjectPool();
        op.Init(path, obj, 0, 0);
        op.doNotDestruct = true;
        op.SetRoot(GetObjectPoolRoot(path));
        //存字典；
        DicAllObjectPool.Add(path, op);
    }

    /// <summary>
    /// 获取一个根节点的方法；
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    static GameObject GetObjectPoolRoot(string key)
    {
        if (DicAllRoots.ContainsKey(key))
        {
            return DicAllRoots[key].gameObject;
        }
        else
        {
            GameObject obj = new GameObject(key);
            DicAllRoots.Add(key, obj.transform);
            GameObject.DontDestroyOnLoad(obj);
            return obj.transform.gameObject;
        }
    }

    #endregion
    
    //public bool activeNextFrame = false;
    public bool doNotDestruct = false;
    protected GameObject mPrefab;
    /// <summary>
    /// 池中对象集合
    /// </summary>
    protected List<GameObject> objects = new List<GameObject>();
    protected List<GameObject> mActiveObjects = new List<GameObject>();
    

    protected Dictionary<GameObject, float> createdTime = new Dictionary<GameObject, float>();
    protected float life;
    protected bool hasAnimation = false;
    protected bool hasParticleEmitter = false;
    protected GameObject folderObject;

    private List<GameObject> mRemoveList = new List<GameObject>();
    private List<GameObject> mRemoveActiveList = new List<GameObject>();

    public bool IsInitialized()
    {
        return mPrefab != null;
    }
    
    /// <summary>
    /// 建池
    /// </summary>
    /// <param name="poolName">池名</param>
    /// <param name="prefab">池中的对象</param>
    /// <param name="initNum">对象个数</param>
    /// <param name="life"></param>
    public void Init(string poolName, GameObject prefab, int initNum, float life)
    {
        mPrefab = prefab;

        this.life = life;



        folderObject = new GameObject(poolName);

        folderObject.tag = TagName.OBJECT_POOL;

        if (mPrefab == null)
            return;

        for (int i = 0; i < initNum; i++)
        {

            GameObject obj = GameObject.Instantiate(mPrefab) as GameObject;
            objects.Add(obj);

            createdTime[obj] = 0f;
            
            obj.transform.parent = folderObject.transform;
            obj.name = mPrefab.name;



            if (obj.GetComponent<UnityEngine.Animation>() != null)
            {
                hasAnimation = true;
            }
            if (obj.GetComponent<ParticleEmitter>() != null)
            {
                hasParticleEmitter = true;
            }
            obj.SetActive(false);
        }



        ////Debug.Log("pool objects count:"+objects.Count);

      
    }

    public void ResetPrefab(GameObject go)
    {
        mPrefab = go as GameObject;
    }
    /// <summary>
    /// 设置对象池节点的父节点
    /// </summary>
    /// <param name="rootObject">Root object.</param>
    public void SetRoot(GameObject rootObject)
    {
        if (folderObject != null && rootObject != null)
        {
            folderObject.transform.parent = rootObject.transform;
            folderObject.transform.localPosition = Vector3.zero;
            folderObject.transform.localScale = Vector3.one;
            
        }
    }

    /*
    public GameObject CreateObject(Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (!objects[i].active)
            {

                objects[i].SetActive(true);
                transforms[i].position = position;
                objects[i].transform.rotation = rotation;


                createdTime[i] = Time.time;
                return objects[i];
            }

        }

        GameObject obj = SwResources.Instantiate(objects[0]) as GameObject;
        objects.Add(obj);
        transforms.Add(obj.transform);
        createdTime.Add(0f);
        obj.name = objects[0].name;
        obj.transform.parent = folderObject.transform;
        
        if (obj.animation != null)
        {
            hasAnimation = true;
        }
        if (obj.particleEmitter != null)
        {
            hasParticleEmitter = true;
        }
        obj.SetActive(true);


        return obj;
    }
    */

    /// <summary>
    /// 1.对象池中某对象被销毁会补充
    /// 2.对象池中有隐藏的对象（闲置对象），就让其显示
    /// 3.前两者都不符合就创建新的对象
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public GameObject CreateObject(Vector3 position, bool normalizescale = false)
    {
        return CreateObject(position, Vector3.zero, Quaternion.identity, normalizescale);
    }
    public GameObject CreateObject(Vector3 position, Vector3 lookAtRotation, Quaternion rotation,bool normalizescale = false)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i] == null)
            {
                GameObject newobj = GameObject.Instantiate(mPrefab) as GameObject;
                objects[i] = newobj;
                createdTime[newobj] = Time.time;
                newobj.name = mPrefab.name;
                Debug.Log(newobj.name + "missed! Reinstantiate!");
                newobj.transform.parent = folderObject.transform;

                newobj.transform.position = position;
                if (lookAtRotation != Vector3.zero)
                {
                    newobj.transform.rotation = Quaternion.LookRotation(lookAtRotation);
                }

                if (rotation != Quaternion.identity)
                {
                    newobj.transform.rotation = rotation;
                }

                if (newobj.GetComponent<UnityEngine.Animation>() != null)
                {
                    hasAnimation = true;
                }
                if (newobj.GetComponent<ParticleEmitter>() != null)
                {
                    hasParticleEmitter = true;
                }
//                 if (activeNextFrame)
//                 {
//                     newobj.SetActive(false);
//                     EffectPool.GetInstance().SetEffectActive(newobj);
//                 }
//                 else
                {
                    newobj.SetActive(true);
                }

                if (!mActiveObjects.Contains(newobj))
                {
                    mActiveObjects.Add(newobj);
                }
                return newobj;
            } 
            else if (!mActiveObjects.Contains(objects[i]))
            {
//                 if (activeNextFrame)
//                 {
//                     objects[i].SetActive(false);
//                     EffectPool.GetInstance().SetEffectActive(objects[i]);
//                 }
//                 else
                
                    objects[i].SetActive(true);
                
                objects[i].transform.position = position;
                if (lookAtRotation != Vector3.zero)
                {
                    objects[i].transform.rotation = Quaternion.LookRotation(lookAtRotation);
                }

                if (rotation != Quaternion.identity)
                {
                    objects[i].transform.rotation = rotation;
                }

                createdTime[objects[i]] = Time.time;
              
                    mActiveObjects.Add(objects[i]);
                
                return objects[i];
            }
        }

        GameObject obj = GameObject.Instantiate(mPrefab) as GameObject;
       
        objects.Add(obj);
        createdTime[obj] = Time.time;
        obj.name = mPrefab.name;
        obj.transform.parent = folderObject.transform;
        if (normalizescale)
        {
            obj.transform.localScale = Vector3.one;
        }
        obj.transform.position = position;

     
        if (lookAtRotation != Vector3.zero)
        {
            obj.transform.rotation = Quaternion.LookRotation(lookAtRotation);
        }

        if (rotation != Quaternion.identity)
        {
            obj.transform.rotation = rotation;
        }

        if (obj.GetComponent<UnityEngine.Animation>() != null)
        {
            hasAnimation = true;
        }
        if (obj.GetComponent<ParticleEmitter>() != null)
        {
            hasParticleEmitter = true;
        }
//         if (activeNextFrame)
//         {
//             obj.SetActive(false);
//             EffectPool.GetInstance().SetEffectActive(obj);
//         }
//         else
        {
            obj.SetActive(true);
        }

        if (!mActiveObjects.Contains(obj))
        {
            mActiveObjects.Add(obj);
        }
        return obj;
    }

    /// <summary>
    /// 
    /// 
    /// </summary>
    public void AutoDestruct()
    {
//         if (mActiveObjects.Count != 0)
//         {
//             Debug.Log(folderObject +" "+ mActiveObjects.Count);
//         }
        if (objects == null)
        {     
            return;
        }
        mRemoveList.Clear();
        mRemoveActiveList.Clear();
        for (int i = 0; i < mActiveObjects.Count; i++)
        {
            if (mActiveObjects[i] == null)
            {
                Debug.Log("The Object has been destroyed!" + folderObject.name);

                mRemoveList.Add(mActiveObjects[i]);
                mRemoveActiveList.Add(mActiveObjects[i]);
                continue;
            }

            //if (!mActiveObjects[i].activeSelf && !EffectPool.GetInstance().IsInSetActiveList(mActiveObjects[i]))
            if (!mActiveObjects[i].activeSelf)
            {
                if (mActiveObjects[i].transform.parent != folderObject.transform)
                {
                    mActiveObjects[i].transform.parent = folderObject.transform;
                }

                mRemoveActiveList.Add(mActiveObjects[i]);
            }

            if (mActiveObjects[i].activeSelf && Time.time - createdTime[mActiveObjects[i]] > life && !doNotDestruct)
            {
                mActiveObjects[i].SetActive(false);
                mActiveObjects[i].transform.parent = folderObject.transform;
                mRemoveActiveList.Add(mActiveObjects[i]);
            }
        }

        foreach (GameObject go in mRemoveList)
        {
            objects.Remove(go);
            if (go != null)
            {
                createdTime.Remove(go);
                GameObject.Destroy(go);
            }
        }

        foreach (GameObject go in mRemoveActiveList)
        {
            mActiveObjects.Remove(go);
        }
    }
    /// <summary>
    /// 清除objects 中活跃的 Go ，清除mActiveObjects 中所有的对象
    /// </summary>
    public void DestructAll()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i] != null)
            {
                if ((objects[i].activeSelf))
                {
                    objects[i].SetActive(false);
                    if (folderObject != null)
                    {
                        objects[i].transform.parent = folderObject.transform;
                    }
                }
            }
        }
        mActiveObjects.Clear();
    }

//     public GameObject DeleteObject(GameObject obj)
//     {
//         obj.SetActive(false);
//         obj.transform.parent = folderObject.transform;
//         return obj;
//     }

    public void ReleaseAll()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i] != null)
                GameObject.Destroy(objects[i]);
        }

        objects.Clear();
        mActiveObjects.Clear();
    }

    public GameObject FindObjectByName(string objectName)
    {
        if (folderObject == null)
            return null;
        Transform findTrans = folderObject.transform.Find(objectName);
        if (findTrans != null)
            return findTrans.gameObject;
        else
            return null;
    }
    public void SetLife(float newLife)
    {
        life = newLife;
    }
    public GameObject GetPrefab()
    {
        return mPrefab;
    }
}
