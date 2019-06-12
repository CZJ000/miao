using UnityEngine;
using System.Collections;
using System.Collections.Generic;






/// <summary>
/// 全局场景变量
/// </summary>
namespace Global
{
    public class GlobalVarTrans : MonoBehaviour
    {

        internal  static EnumScene NextEnumScene=EnumScene.SceneLogin;

        private static Dictionary<EnumScene, string> cachescenedic = new Dictionary<EnumScene, string>();
        public static  void  addLoadSceneName(EnumScene type)
        {

            if (!cachescenedic.ContainsKey(type))
            {
                cachescenedic.Add(type, GlobalEnumConverString.GetInsatnce().GetScenestr(type));
            }
          
        }
        public static bool isLoaded(EnumScene type)
        {
            if (cachescenedic.ContainsKey(type))
            {

                return true;
            }
            return false;
        }
    }
}
