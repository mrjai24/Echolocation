using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Script eliminates the water bug by activating only the puddle on which the player entered

public class HideWater : MonoBehaviour
{

    private int fieldCount;
    void Start()
    {
        fieldCount = transform.childCount;
        Deactvate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Activate();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Deactvate();
        }
    }
    public void Deactvate()
    {
        for (int i = 0; i < fieldCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<SpriteMask>().enabled = false;
        }

    }
    public void Activate()
    {
        for (int i = 0; i < fieldCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<SpriteMask>().enabled = true;
        }

    }
}
