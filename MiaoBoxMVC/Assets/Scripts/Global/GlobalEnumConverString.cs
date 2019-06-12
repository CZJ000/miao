using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Global
{
    public class GlobalEnumConverString  
    {
        private Dictionary<EnumScene, string> _DirEnumString;
        private static GlobalEnumConverString _insatnce;

        private GlobalEnumConverString()
        {
            _DirEnumString = new Dictionary<EnumScene, string>();
            _DirEnumString.Add(EnumScene.SceneLogin, "LoginSence_Chief");  
            _DirEnumString.Add(EnumScene.SceneMain, "MainScene");
            _DirEnumString.Add(EnumScene.SceneLoading, "LoadingScene");
            _DirEnumString.Add(EnumScene.SceneBattle, "BattleScene");
            _DirEnumString.Add(EnumScene.SceneStrategy, "StrategyScence");

        }
        public static  GlobalEnumConverString GetInsatnce()
        {
            if (_insatnce == null)
            {

                _insatnce = new GlobalEnumConverString();
            }
             
            
             return _insatnce;
             

        }
        public  string GetScenestr(EnumScene enumscene)
        {
            if (_DirEnumString!=null&&_DirEnumString .Count >=1)
            {
                return _DirEnumString[enumscene];
            }else
            {

                Debug.LogWarning(GetType() + "_DirEnumString=null&&_DirEnumString .Count <1");
                return null;
            }

        }
        

       
    }
}