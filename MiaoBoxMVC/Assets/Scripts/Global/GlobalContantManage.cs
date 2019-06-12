using UnityEngine;
using System.Collections;


/// <summary>
/// the global use manage all contant quantity
/// </summary>
namespace Global
{
    #region 全局常量管理
    public class NotiConst
    {
        /* UI窗体名称*/
        public const string LOGIN_VIEW = "LoginView";
        public const string REGISTER_VIEW = "NewRegisterView";

        /* 路径常量 */
        public const string SYS_PATH_CANVAS = "UIPrefabs/Canvas";
        public const string SYS_PATH_UIFORMS_CONFIG_INFO = "ConfigJsonInfo/UIFormsConfigInfo";
        public const string SYS_PATH_CONFIG_INFO = "ConfigJsonInfo/SysConfigInfo";

        /* 标签常量 */
        public const string SYS_TAG_CANVAS = "_TagCanvas";
        /* 节点常量 */
        public const string SYS_NORMAL_NODE = "Normal";
        public const string SYS_FIXED_NODE = "Fixed";
        public const string SYS_POPUP_NODE = "PopUp";
        public const string SYS_SCRIPTMANAGER_NODE = "_ScriptMgr";
        /* 遮罩管理器中，透明度常量 */
        public const float SYS_UIMASK_LUCENCY_COLOR_RGB = 255 / 255F;
        public const float SYS_UIMASK_LUCENCY_COLOR_RGB_A = 0F / 255F;

        public const float SYS_UIMASK_TRANS_LUCENCY_COLOR_RGB = 220 / 255F;
        public const float SYS_UIMASK_TRANS_LUCENCY_COLOR_RGB_A = 50F / 255F;

        public const float SYS_UIMASK_IMPENETRABLE_COLOR_RGB = 50 / 255F;
        public const float SYS_UIMASK_IMPENETRABLE_COLOR_RGB_A = 200F / 255F;



        //LayerMask
        public const string Layer_EmployeeCat = "EmployeeCat";
        public const string Layer_Cats = "Cats";


        //Init MVC
        public const string STARTUP = "STARTUP";

        //Init View
        public const string INIT_MAIN_MENU_VIEW = "INIT_MAIN_MENU_VIEW";
        public const string INIT_CAT_GROUP_MENU_VIEW = "INIT_CAT_GROUP_MENU_VIEW";
        public const string INIT_CUSTOMER＿VIEW = "INIT_CUSTOMER＿VIEW";
        public const string INIT_EMPLOYEE_VIEW = "INIT_EMPLOYEE_VIEW";
        public const string INIT_TASK_MENU_VIEW = "INIT_TASK_MENU_VIEW";


        /// <summary>
        /// 初始化建筑蓝图界面
        /// </summary>
        public const string INIT_BUILDING_BLUEPRINT = "INIT_BUILDING_BLUEPRINT";

        //Login & Register
        public const string LOGIN_REQUEST = "LOGIN_REQUEST";
        public const string REGISTER_REQUEST = "REGISTER_REQUEST";

        //User Info
        public const string GET_USER_INFO_VALUE = "GET_USER_INFO_VALUE";
        public const string SET_GOLD = "SET_GOLD";
        public const string SET_EXP = "SET_EXP";

        //Neighbor User Info
        public const string GET_NEIGHBOR_INFO_VALUE = "GET_NEIGHBOR_USER_INFO_VALUE";

        //Cat Group
        /// <summary>
        /// 请求所有猫组信息
        /// </summary>
        public const string GET_CAT_GROUP_DATA = "GET_CAT_GROUP_DATA";

        /// <summary>
        /// 请求所有猫组名字和ID信息
        /// </summary>
        public const string GET_CAT_GROUP_TITLE = "GET_CAT_GROUP_TITLE";
        /// <summary>
        /// 请求某个猫组信息
        /// </summary>
        public const string GET_CAT_GROUP_INFO = "GET_CAT_GROUP_INFO";   //

        /// <summary>
        /// 删除猫
        /// </summary>
        public const string CAT_DELETE = "CAT_DELETE";


        /// <summary>
        /// 交换猫
        /// </summary>
        public const string CAT_SWITCH_GROUP = "CAT_SWITCH_GROUP";
        public const string CAT_CHANGE_GROUP = "CAT_CHANGE_GROUP";
        
        /// <summary>
        /// 保存到数据库
        /// </summary>
        public const string CAT_GROUP_CLOSE = "CAT_GROUP_CLOSE";

        //Customer
        public const string GET_CUSTOMER_SPAWN_VALUE = "GET_CUSTOMER_SPAWN_VALUE";
        public const string SET_CURRENT_CUSTOMER = "SET_CURRENT_CUSTOMER";
        public const string ADD_CUSTOMER_MODEL = "ADD_CUSTOMER_MODEL";

        //Employee
        public const string ADD_EMPLOYEE_MODEL = "ADD_EMPLOYEE_MODEL";
        public const string EMPLOY_EMPLOYEE = "EMPLOY_EMPLOYEE";
        //BuildingBlueprint&& LandedEstateMenuView
        public const string GEI_RECUITTYPEDIC_DATA = "GEI_RECUITTYPEDIC_DATA";
        public const string GET_BUILDINGBLUEPRINT_DATA = "GET_BUILDINGBLUEPRINT_DATA";
        public const string GET_STATBUILD_DATA = "GET_STATBUILD_DATA";
        public const string GET_BUILDING_DATA = "GET_BUILDING_DATA";
        /// <summary>
        /// 购买蓝图
        /// </summary>
        public const string PURCHASE_BLUEPRINT = "PURCHASE_BLUEPRINT";
        public const string SALE_BLUEPRINT = "SALE_BLUEPRINT";
        /// <summary>
        /// 建筑模型建造命令
        /// </summary>
        public const string INITBUILDINGCHANGE = "INITBUILDINGCHANGE";
        public const string GET_BUILDING_MODEL_DATA = "GET_BUILDING_MODEL_DATA";
        public const string GET_CHNAGE_MODEL_DATA = "GET_CHNAGE_MODEL_DATA";
        public const string SET_BUILDING_MODEL_DATA = "SET_BUILDING_MODEL_DATA";

        ///战斗场景
        ///
        public const string INITBATTLEVIEW = "INITBATTLEVIEW";
        public const string GET_BATTLE_CAT_GROUP_INFO = "GET_BATTLE_CAT_GROUP_INFO";
        public const string GET_BATTLE_RANDOM_CAT_INFO = "GET_BATTLE_RANDOM_CAT_INFO";
        public const string SET_BATTLE_RESULT_EXP = "SET_BATTLE_RESULT_EXP";

        ///店长
        ///
        public const string INIT_ASSISTANT_VIEW = "INITASSISTANTVIEW";
        public const string SET_ASSISTANT = "SET_ASSISTANT";

        /// 店员
        /// 
        public const string INIT_CLERK_VIEW = "INITCLERKVIEW";
        public const string SET_CLERK = "SET_CLERK";
        public const string LEVEL_UP_CLERK = "LEVEL_UP_CLERK";


        ///任务系统
        ///
        public const string C_CHANGE_BATTLE_AI_USER = "C_CHANGE_BATTLE_AI_USER";
    }
    public class JsonConst
    {
        public const string USER_NAME = "USER_NAME";
        public const string PASSWORD = "PASSWORD";
        public const string ID = "ID";
        public const string ErrorCode = "ErrorCode";
    }

    public class ErrorCode
    {
        public const int SUCCESS = 0;

        public const int INVALID_LOGIN_INFO = 1;
    }

    public class AppConst
    {
        //是否使用缓存数据
        public const bool USER_CACHE = true;

        //顾客刷新时间
        public const float CUSTOM_REFRESH_TIME = 15f;

        //顾客生成数
        public const int DEFALUT_SPAWN_NUM = 5;

        //顾客可见数
        public const int VISIBLE_CUSTOMER = 20;

        //默认最大顾客数
        public const int DEFAULT_MAX_CUSTOMER = 5;
    }

    public class GlobalContantManage
    {
        
        public const float TIME_0DOT1 = 0.1f;
        public const float TIME_0DOT2 = 0.2f;
        public const float TIME_0DOT5 = 0.5f;
        public const float TIME_1 = 1f;
        public const float TIME_1DOT5 = 1.5f;
        public const float TIME_2 = 2f;
        public const float TIME_2DOT5 = 2.5f;

    }

    public class TagName
    {

        public static string CATINGROUP = "CatInGroup";
        public static string EMPTYGROUPFIELD = "EmptyGroupField";
        public static string CATGROUPCATBG= "CatGroupCatBg";

        public static string DIMISSFIELD = "DismissField";

        public static string START_POS = "StartPos";
        public static string END_POS = "EndPos";
        public static string MIAOLIANG = "miaoliang";
        public static string SPAWN_POINT = "SpawnPoint";
        public static string OBJECT_POOL = "ObjectPool";
        public static string GO_AWAY_POS = "GoAwayPos";
        public static string CENTER_POS = "CenterPos";
        public static string MAIN_SCRIPT = "MainScript";

        public static string CUSTOMER_VIEW = "CustomerView";
        public static string EMPLOYEE_VIEW = "EmployeeView";
        public static string ASSISTANT_VIEW = "AssistantView";
        public static string CLERK_VIEW = "ClerkView";
        

        public static string SELECTBUILDING3DUI = "SelectBuilding3DUI";
        public static string SHOPMENUBUYFIELD = "ShopMenuBuyField";
    }
    

    #endregion
    #region 全局枚举类型
    public enum MenuType
    {
        Null,
        BuildingBlueprintMenu,
        CatGroupMenu,

    }
    public enum FailType
    {
        Money,
        LimitNum

    }
    public enum RoleType
    {
        NUll,
        Player,
        Enemy
    }
    public enum EnumScene
    {
        SceneLogin,
         SceneMain,
        SceneLoading,
         SceneBattle,
        SceneStrategy

    }
    public enum animastate
    {
        attack01=0,
        attack02 = 1,
        attack03 = 2,
        attack04 = 3,
        idle,
        walk,
        run,
        dance,
        lucky,
        die,
        dig,
        acatch,
        clap
    }
    
    public enum AttackPatten
    {
        Normal,
        Captain

    }

    public enum SkillPatten
    {
        Normal,
        Special,
    }



    /// <summary>
    /// UI窗体（位置）类型
    /// </summary>
    public enum UIFormType
    {
        //普通窗体
        Normal,
        //固定窗体                              
        Fixed,
        //弹出窗体
        PopUp
    }

    /// <summary>
    /// UI窗体的显示类型
    /// </summary>
    public enum UIFormShowMode
    {
       
        /// <summary>
        ///  窗体与其他窗体可以并列显示
        /// </summary>
        Normal,
        /// <summary>
        ///    窗体主要应用与"弹出窗体"，维护多个弹出窗体的层级关系。
        /// </summary>
        ReverseChange,

        //隐藏其他
        HideOther
    }

    /// <summary>
    /// UI窗体透明度类型
    /// </summary>
    public enum UIFormLucenyType
    {
        /// <summary>
        ///  完全透明，不能穿透
        /// </summary>
        Lucency,
        /// <summary>
        /// 半透明，不能穿透
        /// </summary>
        Translucence,
        /// <summary>
        /// 低透明度，不能穿透
        /// </summary>
        ImPenetrable,
        /// <summary>
        /// 可以穿透
        /// </summary>
        Pentrate
    }

    #endregion


  
    #region 全局委托
    public delegate void CtrHeroDelegate(string eventstr);
    public delegate void PlayerKerneldatedel(KeyAndValue kv);
    public delegate void playerExtenalDatadel(KeyAndValue kv);
    public class KeyAndValue 
    {
        private string _Key;
        private float _Value;
        public KeyAndValue(string key,float value)
        {
            this._Key = key;
            this._Value = value;

        }
        //只读属性
        public string Key
        {
            get
            {
                return _Key;
            }
        }
        public float Value
        {
            get
            {
                return _Value;
            }
        }
    }
    #endregion
}
