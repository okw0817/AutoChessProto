using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : IPickup
{
    #region Members : Private
    private GameObject pickupObject;
    #endregion

    #region Members : Property
    public GameObject PickupObject { get => pickupObject; }
    #endregion

    #region Methods : Interface
    public void Pickup(GameObject obj)
    {
        pickupObject = obj;
    }
    public void DropOff(Transform dropPosition)
    {
        pickupObject.transform.position = dropPosition.position;
        pickupObject = null;
    }

    public void Attach(Vector3 target)
    {
        pickupObject.transform.position = target;
    }
    #endregion

    #region Methods : Public
    #endregion
}
