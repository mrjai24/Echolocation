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

    public LayerMask allGroundTypes;
    public string currentGroundType = "Normal";
    public int steps = 0;

    public bool isMoving = false;
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
        stepPosition = (Vector2) transform.position + movementVect * stepLength;
        currentGroundType = GetGroundType(stepPosition);
        if(movementDirection == "left" || movementDirection == "right")
            Instantiate(footstepHorizontal, stepPosition, Quaternion.identity);
        else if(movementDirection == "up" || movementDirection == "down")
            Instantiate(footstepVertical, stepPosition, Quaternion.identity);
        rb.MovePosition(stepPosition);

        
        

            yield return new WaitForSeconds(movementDelay);

        finishedStep = true;
    }

    string GetGroundType(Vector2 stepPosition)
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(stepPosition, 0.5f, allGroundTypes);
        if(hitCollider)
        {
            int hitLayer = hitCollider.gameObject.layer;
            if(hitLayer == 4)
            {
                Debug.Log("Walking on Water");
                return "Water";
            }
            if (hitLayer == 9)
            {
                Debug.Log("Walking on Ice");
                return "Ice";
            }
            if (hitLayer == 10)
            {
                Debug.Log("Walking on Mud");
                return "Mud";
            }
            else return "Normal";
        }
        else
            return "Normal";
        
    }
}
