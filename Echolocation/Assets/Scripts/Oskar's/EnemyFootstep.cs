using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFootstep : MonoBehaviour
{
    public GameObject enemy;
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
        enemy = GameObject.Find("Zombie");
        currentGroundType = enemy.GetComponent<EnemyAI>().currentGroundType;
        spriteRenderer = GetComponent<SpriteRenderer>();
        myTransform = GetComponent<Transform>();
        movementDirection = enemy.GetComponent<EnemyAI>().movementDirection;


        if (enemy.GetComponent<EnemyAI>().steps % 2 == 0)
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

        if (currentGroundType == "Water")
        {
            enemy.GetComponent<EnemyAI>().movementDelay = waterMovementDelay;
            Instantiate(waterWave, new Vector3(waveStartPosition.transform.position.x, waveStartPosition.transform.position.y), Quaternion.identity);
            SoundManager.PlaySound(stepSounds[Random.Range(10, 14)]);
        }
        if (currentGroundType == "Ice")
        {
            enemy.GetComponent<EnemyAI>().movementDelay = iceMovementDelay;
            SoundManager.PlaySound(stepSounds[Random.Range(5, 9)]);
        }
        if (currentGroundType == "Normal")
        {

            enemy.GetComponent<EnemyAI>().movementDelay = 0.6f;
            SoundManager.PlaySound(stepSounds[Random.Range(0, 4)]);
        }
        if (currentGroundType == "Mud")
        {
            enemy.GetComponent<EnemyAI>().movementDelay = 1.2f;
            SoundManager.PlaySound(stepSounds[Random.Range(15, 19)]);
        }
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        float fadeChunk = fadingTime / 10;
        for (int i = 10; i > 0; i -= 1)
        {
            spriteRenderer.material.color = new Color(1f, 1f, 1f, (float)i / 10);
            yield return new WaitForSeconds(fadeChunk);
        }
        Destroy(this.gameObject);

    }
}
