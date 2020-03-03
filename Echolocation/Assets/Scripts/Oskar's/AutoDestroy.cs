using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float DestroyTime = 5f;
    void Start()
    {
        Destroy(this.gameObject, DestroyTime);
    }


}
