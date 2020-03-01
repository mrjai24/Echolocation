using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public int bellFlowers = 0;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("BellFlower"))
        {
            bellFlowers++;
            Destroy(col.gameObject);
            Debug.Log("BellFlower was picked up");
        }
    }
}
