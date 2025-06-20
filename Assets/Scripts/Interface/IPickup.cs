using UnityEngine;

public interface IPickup
{
    public void Pickup(GameObject obj);

    public void Attach(Vector3 target);
    public void DropOff(Transform dropPosition);
}
