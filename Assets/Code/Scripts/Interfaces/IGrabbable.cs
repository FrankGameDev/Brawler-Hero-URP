

using UnityEngine;

public interface IGrabbable
{
    float grabResistence { get; set; }


    void IsGrabbed();

    void IsDropped();

    bool IsGrabbable(float myForce)
    {
        return myForce > grabResistence;
    }
}
