using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BellFlowerText : MonoBehaviour
{
    [SerializeField]
    Text bellFlowersText;

    PickUp pickUp;

    int bellFlowersCollected;
    int bellFlowersNeeded;

    void Start()
    {
        GameObject Player = GameObject.Find("Player");
        pickUp = Player.GetComponent<PickUp>();

        bellFlowersNeeded = GameObject.FindGameObjectsWithTag("BellFlower").Length;
    }

    void Update()
    {
        bellFlowersCollected = pickUp.bellFlowers;

        string bellFlowersTextFormat = string.Format("{0:0}/{1:0}", bellFlowersCollected, bellFlowersNeeded);

        bellFlowersText.text = bellFlowersTextFormat;
    }
}
