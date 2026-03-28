using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasabiBox : MonoBehaviour, ICollectable
{
    public void OnPickup()
    {
        Destroy(this.gameObject);
    }
}
