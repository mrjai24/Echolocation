using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : MonoBehaviour
{
    private bool playerIsMoving;
    private bool isStomping = false;
    public float stompWaitTime = 3f;
    
    public GameObject stomp;

     void LateUpdate()
    {
        playerIsMoving = GetComponent<PlayerMovement>().isMoving;
    }

    void Update()
    {
        if (!playerIsMoving)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isStomping)
            {
                StartCoroutine(StompDelay());
                MakeStomp();
                isStomping = true;
            }
        }
    }
    IEnumerator StompDelay()
    {
        yield return new WaitForSeconds(stompWaitTime);
        isStomping = false;
    }
    void MakeStomp()
    {
        Instantiate(stomp, transform.position, Quaternion.identity);
    }
}
