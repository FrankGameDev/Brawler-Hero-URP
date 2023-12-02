using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

public static class AnimationNames
{
    #region Locomotion

    public static readonly string GROUNDED_LOCOMOTION = "Grounded Locomotion";
    public static readonly string JUMP = "Jumping up";
    public static readonly string FALLING_POSE = "Falling pose";
    public static readonly string LANDING = "Land2";

    #endregion 

    #region Combat 

    public static readonly string COMBAT_IDLE = "Combat idle";
    public static readonly string HOOK_PUNCH_RX = "Hook punch Rx";
    public static readonly string HOOK_PUNCH_LX = "Hook punch Lx";
    public static readonly string HEAD_BUTT = "Head butt";
    public static readonly string KICK = "Kick";
    public static readonly string MARTELO_KICK = "Martelo Kick";
    public static readonly string DROP_KICK = "Drop kick";

    #endregion
}

public class PlayerAnimationController : MonoBehaviour
{

    private static PlayerAnimationController _instance;

    public static PlayerAnimationController Instance
    {
        get
        {
            return _instance;
        }
    }

    Animator animator;

    private int _velocityHash;

    #region Animation reference

    private string _currentAnimation;
    private Dictionary<string, int> _animationHashes = new();

    #endregion
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        PopulateAnimationHashesDictionary();

        // Locomotive
        _velocityHash = Animator.StringToHash("Velocity");
    }

    private void PopulateAnimationHashesDictionary()
    {
        FieldInfo[] fields = typeof(AnimationNames).GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (FieldInfo field in fields)
        {
            _animationHashes.Add(field.GetValue(null).ToString(),
                                 Animator.StringToHash(field.GetValue(null)
                                                            .ToString()));
        }
    }

    /// <summary>
    /// Play the requested animation
    /// </summary>
    /// <param name="animName"> Name of the animation. Get the name from class <see cref="AnimationNames"/></param>
    public void PlayAnimation(string animName)
    {
        if (_currentAnimation == animName) return;

        if (_animationHashes.TryGetValue(animName, out int hash))
        {
            animator.Play(hash);
            _currentAnimation = animName;
        }
        else
            Debug.LogError($"Animation {animName} doesn't exist");

    }

    /// <summary>
    /// Fade the current animation to the requested one
    /// </summary>
    /// <param name="animName"> Requested animation. Get the name from class <see cref="AnimationNames"/></param>
    /// <param name="fadeTime">Fade time. Default value = 1 </param>
    public void FadeToAnim(string animName, float fadeTime = 1f)
    {
        if (_currentAnimation == animName) return;

        if (_animationHashes.TryGetValue(animName, out int hash))
        {
            animator.CrossFade(hash, fadeTime);
            _currentAnimation = animName;
        }
        else
            Debug.LogError($"Animation {animName} doesn't exist");
    }

    public void SetGroundedLocomotionVelocity(float velocity) => animator.SetFloat(_velocityHash, velocity);


    #region Animation times

    public float GetAnimationLength() => animator.GetCurrentAnimatorStateInfo(0).length;
    public bool AnimatorIsPlaying() =>  animator.GetCurrentAnimatorStateInfo(0).normalizedTime < GetAnimationLength();
    public async Task WaitForAnimationToFinish()
    {
        while (AnimatorIsPlaying())
        {
            //Debug.Log($"Animation calculated length {GetAnimationLength()}; current animation time: {animator.GetCurrentAnimatorStateInfo(0).normalizedTime}");
            await Task.Yield();
        }
    }

    #endregion

    // ======= UTILITY =========

    public void StopAnimation() => animator.speed = 0;
    public void ResumeAnimation() => animator.speed = 1;
}







