using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject footstepVertical;

    [SerializeField]
    private GameObject footstepHorizontal;



    Vector2 movementVect;

    public float movementDelay = 1f;
    public float stepLength = 10f;
    public string movementDirection;

    public int steps = 0;

    private bool isMoving = false;
    private bool finishedStep = true;
    private Vector2 stepPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {

        movementVect.y = Input.GetAxisRaw("Vertical");
        movementVect.x = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", movementVect.sqrMagnitude);

        if (Mathf.Abs(movementVect.x) > 0 && Mathf.Abs(movementVect.y) > 0)
            movementVect.x = 0;



        if (movementVect.x > 0)
            movementDirection = "right";
        if (movementVect.x < 0)
            movementDirection = "left";
        if (movementVect.y > 0)
            movementDirection = "up";
        if (movementVect.y < 0)
            movementDirection = "down";



        if (movementVect.sqrMagnitude > 0)
            isMoving = true;
        else
            isMoving = false;

        if (isMoving && finishedStep)
            StartCoroutine(MakeStep());
    }

    //void FixedUpdate()
    //{
        

    //}



    IEnumerator MakeStep()
    {
        steps++;
        finishedStep = false;

        stepPosition = rb.position + movementVect * stepLength;

        if(movementDirection == "left" || movementDirection == "right")
            Instantiate(footstepHorizontal, stepPosition, Quaternion.identity);
        else if(movementDirection == "up" || movementDirection == "down")
            Instantiate(footstepVertical, stepPosition, Quaternion.identity);

        rb.MovePosition(stepPosition);

        yield return new WaitForSeconds(movementDelay);

        finishedStep = true;
    }
}
