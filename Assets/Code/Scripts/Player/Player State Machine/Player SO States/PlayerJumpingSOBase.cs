using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player States/Jumping State", fileName = "PlayerJumpingSO")]
public class PlayerJumpingSOBase : PlayerGroundedSOBase
{
    [SerializeField, Range(0f, 10f)] private float airDrag;
    [SerializeField] private float mass;
    private float startDrag, startMass;


    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        Jump();
        player.animationController.PlayAnimation(AnimationNames.JUMP);

        //Rigidbody settings
        startMass = player.rb.mass;
        startDrag = player.rb.drag;
        player.rb.mass = mass;
        player.rb.drag = airDrag;
    }

    public override void DoUpdateLogic()
    {
        base.DoUpdateLogic();
    }

    public override void DoFixedUpdateLogic()
    {
        base.DoFixedUpdateLogic();
        MovementInAir();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        player.animationController.FadeToAnim(AnimationNames.GROUNDED_LOCOMOTION,.5f);
    }

    public override void HandleChangeState()
    {

        if (player.Input.IsAttacking())
        {
            player.stateMachine.ChangeState(player.playerCombatState);
        }
        else if (player.IsGrounded())
            player.stateMachine.ChangeState(player.playerIdleState);
    }

    public override void ResetValues()
    {
        //Rigidbody Reset
        player.rb.mass = startMass;
        player.rb.drag = startDrag;
    }

    void Jump()
    {
        player.rb.AddForce(Vector3.up * player.playerStats.jumpForce, ForceMode.Impulse);
    }

    void MovementInAir()
    {

        Vector3 movement = player.InputDirection * player.playerStats.walkingSpeed;
        player.rb.velocity = new Vector3(movement.x, player.rb.velocity.y, movement.z);
    }

}
