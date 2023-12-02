using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedSOBase : ScriptableObject
{
    protected PlayerController player;
    protected GameObject gameObject;
    protected Transform transform;

    public virtual void Initialize(GameObject gameObject, PlayerController playerController)
    {
        this.gameObject = gameObject;
        this.player = playerController;
        transform = gameObject.transform;
    }

    public virtual void DoEnterLogic() { }
    public virtual void DoExitLogic() { ResetValues(); }

    /// <summary>
    /// Se si esegue l'override, non rimuovere il base.Update(), altrimenti non viene eseguito il controllo sul cambio di stati
    /// </summary>
    public virtual void DoUpdateLogic() { HandleChangeState(); }
    public virtual void DoFixedUpdateLogic() { }

    public virtual void ResetValues() { }
    public virtual void HandleChangeState() { }

}
