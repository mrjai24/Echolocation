﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    public GameObject player;
    public GameObject waterWave;
    public GameObject waveStartPosition;
    public float waterMovementDelay = 0.6f;

    public float fadingTime = 0.3f;
    private string movementDirection;
    
    private SpriteRenderer sRenderer;
    private Transform myTransform;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        sRenderer = GetComponent<SpriteRenderer>();
        myTransform = GetComponent<Transform>();
        movementDirection = player.GetComponent<PlayerMovement>().movementDirection;


        if (player.GetComponent<PlayerMovement>().steps %2 == 0)
        {
            if (movementDirection == "left")
                myTransform.localScale = new Vector3(myTransform.localScale.x, -1, 1);
            if (movementDirection == "right")
                myTransform.localScale = new Vector3(myTransform.localScale.x, -1, 1);

            if (movementDirection == "down")
                myTransform.localScale = new Vector3(-1, myTransform.localScale.y, 1);
            if (movementDirection == "up")
            myTransform.localScale = new Vector3(-1, myTransform.localScale.y, 1);
        }


        if (movementDirection == "down")
            myTransform.localScale = new Vector3(myTransform.localScale.x, -1, 1);

        if (movementDirection == "left")
            myTransform.localScale = new Vector3(-1, myTransform.localScale.y, 1);

        if(player.GetComponent<PlayerMovement>().currentGroundType == "Water")
        {
            player.GetComponent<PlayerMovement>().movementDelay = waterMovementDelay;
            GetComponent<FootEcho>().enabled = false;
            Instantiate(waterWave, new Vector3(waveStartPosition.transform.position.x,waveStartPosition.transform.position.y), Quaternion.identity);
        }
        //if(player.GetComponent<PlayerMovement>().currentGroundType == "Ice")
        //{

        //}
        else
            player.GetComponent<PlayerMovement>().movementDelay = 0.3f;

        StartCoroutine(Fade());
        
    }

    IEnumerator Fade()
    {
        float fadeChunk = fadingTime / 10;
        for(int i = 10; i>0; i -= 1)
        {
            sRenderer.material.color = new Color(1f, 1f, 1f, (float)i/10);
            yield return new WaitForSeconds(fadeChunk);
        }
        Destroy(this.gameObject);

    }

}
