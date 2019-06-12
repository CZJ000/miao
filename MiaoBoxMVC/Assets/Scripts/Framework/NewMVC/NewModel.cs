/*****************************************************
/** 类名：NewModel.cs
/** 作者：Tearix
/** 日期：2018-03-07
/** 描述：
*******************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MB.MVC
{
    public class NewModel : NewNotifier
    {
        // 模型名是只读的，只在构造函数处生成，用来区分不同model的事件
        protected string m_modeName;

        public string GetModelName()
        {
            if (string.IsNullOrEmpty(m_modeName))
            {
                m_modeName = this.GetHashCode().ToString();
            }
            return m_modeName;
        }

        // 抛出事件，可携带不定数量数据
        public void Refresh(Enum attribute, params object[] e)
        {
            RaiseEvent(string.Format("{0}{1}", GetModelName(), attribute), e);
        }

        public virtual void Destory()
        {

        }

    }
}
