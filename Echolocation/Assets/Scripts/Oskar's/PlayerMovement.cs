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

    public LayerMask groundType;
    public string currentGroundType = "Normal";
    public int steps = 0;

    private bool isMoving = false;
    private bool finishedStep = true;
    private Vector2 stepPosition;


    private bool touchingWater = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
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

        currentGroundType = GetGroundType(stepPosition);
        

            yield return new WaitForSeconds(movementDelay);

        finishedStep = true;
    }

    string GetGroundType(Vector2 stepPosition)
    {
        if (Physics2D.OverlapCircle(stepPosition, 0.1f, groundType))
            return "Water";
        else
            return "Normal;";
        
    }
}
