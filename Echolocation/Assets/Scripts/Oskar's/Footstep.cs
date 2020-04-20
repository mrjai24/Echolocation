using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    public GameObject player;
    public GameObject waterWave;
    public GameObject waveStartPosition;
    public float waterMovementDelay = 0.45f;
    public float iceMovementDelay = 0.4f;
    public string currentGroundType;
    public float fadingTime = 0.3f;
    private string movementDirection;
    private SpriteRenderer spriteRenderer;
    private Transform myTransform;
    public List<AudioClip> stepSounds;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        currentGroundType = player.GetComponent<PlayerMovement>().currentGroundType;
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        if(currentGroundType == "Water")
        {
            player.GetComponent<PlayerMovement>().movementDelay = waterMovementDelay;
            Instantiate(waterWave, new Vector3(waveStartPosition.transform.position.x,waveStartPosition.transform.position.y), Quaternion.identity);
            SoundManager.PlaySound(stepSounds[Random.Range(10, 14)]);
        }
        if (currentGroundType == "Ice")
        {
            player.GetComponent<PlayerMovement>().movementDelay = iceMovementDelay;
            SoundManager.PlaySound(stepSounds[Random.Range(5, 9)]);
        }
        if (currentGroundType == "Normal")
        {

            player.GetComponent<PlayerMovement>().movementDelay = 0.3f;
            SoundManager.PlaySound(stepSounds[Random.Range(0,4)]);
        }
        if (currentGroundType == "Mud")
        {
            player.GetComponent<PlayerMovement>().movementDelay = 0.65f;
            SoundManager.PlaySound(stepSounds[Random.Range(15, 19)]);
        }
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        float fadeChunk = fadingTime / 10;
        for(int i = 10; i>0; i -= 1)
        {
            spriteRenderer.material.color = new Color(1f, 1f, 1f, (float)i/10);
            yield return new WaitForSeconds(fadeChunk);
        }
        Destroy(this.gameObject);
    }

}
