using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class to save all animation state name.To use in PlayerAnimationController methods
/// <seealso cref="PlayerAnimationController"/>
/// </summary>
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
