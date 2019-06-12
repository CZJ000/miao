/*****************************************************
/** 类名：NewView.cs
/** 作者：Tearix
/** 日期：2018-03-07
/** 描述：
*******************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MB.MVC
{
    public class NewView : MonoBehaviour
    {
        protected NewModel m_bindModel;
        Dictionary<string, NewNotifier.StandardDelegate> m_AllFun = new Dictionary<string, NewNotifier.StandardDelegate>();

        public virtual void Init(NewModel model)
        {
            m_bindModel = model;
        }

        protected void BindModel(Enum attribute, NewNotifier.StandardDelegate fun)
        {
            if (null != m_bindModel)
            {
                string keyName = string.Format("{0}{1}", m_bindModel.GetModelName(), attribute);
                if (m_AllFun.ContainsKey(keyName))
                {
                    m_bindModel.RemoveEventHandler(keyName, m_AllFun[keyName]);
                    m_AllFun.Remove(keyName);
                }
                m_bindModel.AddEventHandler(keyName, fun);
                m_AllFun.Add(keyName, fun);
            }
            else
            {
                Debug.LogError("没有绑定数据模型");
            }
        }

        protected void UnBindModel(Enum attribute, NewNotifier.StandardDelegate fun)
        {
            if (null != m_bindModel)
            {
                string keyName = string.Format("{0}{1}", m_bindModel.GetModelName(), attribute);
                if (m_AllFun.ContainsKey(keyName))
                {
                    m_bindModel.RemoveEventHandler(keyName, fun);
                    m_AllFun.Remove(keyName);
                }
            }
            else
            {
                Debug.LogError("没有绑定数据模型");
            }
        }

        protected void UnAllBindModel(Enum attribute)
        {
            if (null == m_bindModel)
            {
                return;
            }
            m_bindModel.RemoveAllEventHandler(m_bindModel.GetModelName() + attribute);
        }

        private void ClearModelAndBind()
        {
            if (m_bindModel != null)
            {
                if (m_AllFun.Count > 0)
                {
                    var iter = m_AllFun.GetEnumerator();
                    while (iter.MoveNext())
                    {
                        m_bindModel.RemoveEventHandler(iter.Current.Key, iter.Current.Value);
                    }
                    iter.Dispose();
                }
                m_bindModel = null;
            }
        }

        public virtual void F_Reset()
        {
            ClearModelAndBind();
        }

        // monobehavior method
        protected virtual void Awake()
        {

        }

        protected virtual void OnDestroy()
        {
            ClearModelAndBind();
        }
    }
}
