using UnityEngine;
using System.Collections;
using Global;

public class BattleDisplayAnimator : MonoBehaviour
{
 
 
  
    public   bool AttackEnd { get { return _AttackEnd; } }
    public  animastate manimatate
    {
        set
        {
            
            _manimastate = value;

        }
    }

    private float time = 0;


    private bool _isAttack = false;
    private bool _ismove = false;
    private bool _AttackEnd = false;
   
    private Vector3 targetpos;
    private Vector3 cachepos;
    private Animation _Animation;
    /// <summary>
    /// 猫信息；
    /// </summary>
    private BattleCatInfo _catInfo;
    private animastate _manimastate = animastate.idle;

    // Use this for initialization
    void Start()
    {
        _Animation = this.GetComponent<Animation>();
        _catInfo = GetComponent<BattleCatInfo>();
    }


    void FixedUpdate()
    {  
        if (_isAttack)
        {
            if (_ismove)
            {
                 manimatate = animastate.run;
                _ismove = !MoveTarget(targetpos);
            }
            else
            {
                animastate atk= (animastate)_catInfo.AttackType;
                 if (time<GetAnimationLenth(atk.ToString()))
                {
                    time += Time.deltaTime;
                    manimatate =atk;
                    
                }else
                {   
                    if (MoveTarget(cachepos))
                    {
                        Reset();
                        BattleCamera._instance.currentbackbat();
                           
                    }
                    else
                    {
                        manimatate = animastate.run;
                    };
                }
               
               
            }

        }
        switch (_manimastate)
        {
            case animastate.idle:
                _Animation.CrossFade("idle", 0.1f);
                
                break;
            case animastate.attack01:
                 
                _Animation.CrossFade("attack01", 0.1f);
                
                
                break;
            case animastate.attack02:
                _Animation.CrossFade("attack02", 0.1f);
                
                break;
            case animastate.attack03:
                _Animation.CrossFade("attack03", 0.1f);
               
                break;
            case animastate.attack04:
                _Animation.CrossFade("attack04", 0.1f);
                
                break;
            case animastate.clap:
                _Animation.CrossFade("idle", 0.1f);
                
                break;
            case animastate.dance:
                _Animation.CrossFade("dance", 0.1f);
                break;
            case animastate.die:
                _Animation.CrossFade("attack02", 0.1f);
                
                break;
            case animastate.dig:
                _Animation.CrossFade("dance", 0.1f);
                
                break;
            case animastate.lucky:
                _Animation.CrossFade("lucky", 0.1f);
                
                break;
            case animastate.run:
                _Animation.CrossFade("run", 0.1f);
 
                break;
            case animastate.walk:
                _Animation.CrossFade("attack02", 0.1f);
                
                break;

            default: break;
        }


    }

    bool IsStartMove = false;
    Vector3 StartDri = Vector3.one;


    /// <summary>
    /// 执行攻击动画
    /// </summary>
    /// <param name="Target"></param>
    /// <returns></returns>
    private  bool  MoveTarget(Vector3  Target )
    {
        if (!IsStartMove)
        {
            IsStartMove = true;
            StartDri = transform.forward;
            transform.forward = Target - transform.position;
        }
        Vector3 initialPosition = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, Target , Time.deltaTime * 10f);
        var distanceSquared = (transform.position - Target  ).sqrMagnitude;
        if (distanceSquared < 0.1f * 0.1f)
        {
            //恢复原始朝向；
            IsStartMove = false;
            return true;
        }
        return false;
       
         
    }

    /// <summary>
    /// 攻击位移动画
    /// </summary>
    /// <param name="target"></param>
    public void AttackMove(Transform target)
    {
       
        cachepos = transform.position;
        targetpos = target.position;
        if (cachepos.z<targetpos.z)
        {
            targetpos.z -= 1f;
        }else
        {
            targetpos.z += 1f;
        }
        _isAttack = true;
        _ismove = true;
        _AttackEnd = false;

    }
   
    private float GetAnimationLenth(string anima)
    {
        AnimationClip clip = _Animation.GetClip(anima);
        if (clip != null)
        {
            return clip.length;
        }
        else
        {
            return 0;
        }
    }
     

    /// <summary>
    /// 重置属性
    /// </summary>
    private void Reset()
    {

        _isAttack = false;
        _AttackEnd = true;
        _ismove = false;
        _manimastate = animastate.idle;
        time = 0f;
        //将旋转设置为正确的格式；
        transform.forward = StartDri;
    }



    public  void ChangeAniamstate(animastate state)
    {
        manimatate = state;
    }

    void OnDisable()
    {
        Destroy(this);
    }
}
