using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace Global
{
    public class FadeInOut : MonoBehaviour
    {
        public GameObject ObjRawImage;
        private RawImage _RawImage;
        public float FadeSpeed=1.5f;
        private bool _BoolSceneToClear=true ;
        private bool _BoolSceneToBlack=false ;
        public static FadeInOut _instance;

        void Awake()
        {

            _instance = this;
        }
        void Start()
        {

            if (ObjRawImage)
            {
                _RawImage = ObjRawImage.GetComponent<RawImage>();
            }

        }
        /// <summary>
        /// the scnen to clear
        /// </summary>
        private  void SceneToClear()
        {
            FadeToClear();
            if (_RawImage.color.a < 0.05f)
            {
                _RawImage.color = Color.clear;
                _BoolSceneToClear = false;
                _RawImage.enabled = false;

            }
        }
        /// <summary>
        /// the scne to black
        /// </summary>
        private  void SceneToBlack()
        {
            _RawImage.enabled = true;
            FadeToBlack();
            if (_RawImage.color.a > 0.98f)
            {
                _RawImage.color= Color.black;
                _BoolSceneToBlack = false;

            }


        }
        /// <summary>
        /// effect to clear
        /// </summary>
        private void FadeToClear()
        {

            _RawImage.color = Color.Lerp(_RawImage.color, Color.clear, FadeSpeed*Time .deltaTime );

        }
        /// <summary>
        /// effect to black
        /// </summary>
        private void FadeToBlack()
        {

            _RawImage.color = Color.Lerp(_RawImage.color, Color.black, FadeSpeed*Time .deltaTime);

        }
        internal void   SetSceneToClear()
        {
            _BoolSceneToBlack = false;
            _BoolSceneToClear = true;

        }
        internal void SetSceneToBlack()
        {
            _BoolSceneToBlack = true ;
            _BoolSceneToClear = false ;

        }
      
        void Update()
        {

            if (_BoolSceneToClear)
            {
                SceneToClear();
            }
            else if (_BoolSceneToBlack)
            {
                SceneToBlack();

            }


        }
        
    }
}
