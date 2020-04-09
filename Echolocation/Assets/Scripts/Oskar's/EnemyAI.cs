using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyAI : MonoBehaviour
{

   

    [SerializeField]
    private GameObject footstepVertical;

    [SerializeField]
    private GameObject footstepHorizontal;

    public Vector3 target;

    public int steps=0;
    public float speed; 
    public float nextWaypointDistance = 3f;
    public float movementDelay = 1f;
    public float stepLength = 10f;
    public string movementDirection;

    public LayerMask allGroundTypes;
    public string currentGroundType = "Normal";

    public bool isMoving = false;
    private bool finishedStep = true;
    private Vector2 stepPosition;


    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Vector2 direction;

    Seeker seeker;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, .5f);
        
        
    }

    public void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target, OnPathComplete);
    }



    void OnPathComplete(Path p)
    {
        if (!p.error)
            path = p;
        currentWaypoint = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count || Vector2.Distance(transform.position,target) < 0.4f)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        direction = ((Vector2)path.vectorPath[1] - rb.position).normalized;


     
        if (direction.x > 0.5)
            movementDirection = "right";
        if (direction.x < -0.5)
            movementDirection = "left";
        if (direction.y > 0.5)
            movementDirection = "up";
        if (direction.y < -0.5)
            movementDirection = "down";



        if (direction.sqrMagnitude > 0)
            isMoving = true;
        else
            isMoving = false;

        if (isMoving && finishedStep)
            StartCoroutine(MakeStep());


        //float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);




        //if(distance < nextWaypointDistance)
        //{
        //    currentWaypoint++;
        //}
    }
    IEnumerator MakeStep()
    {
        steps++;
        finishedStep = false;
        stepPosition = (Vector2)transform.position + direction * stepLength;
        currentGroundType = GetGroundType(stepPosition);
        if (movementDirection == "left" || movementDirection == "right")
            Instantiate(footstepHorizontal, stepPosition, Quaternion.identity);
        else if (movementDirection == "up" || movementDirection == "down")
            Instantiate(footstepVertical, stepPosition, Quaternion.identity);
        rb.MovePosition(stepPosition);
        yield return new WaitForSeconds(movementDelay);
        finishedStep = true;
    }

    string GetGroundType(Vector2 stepPosition)
    {
        Collider2D hitCollider = Physics2D.OverlapCircle(stepPosition, 0.2f, allGroundTypes);
        if (hitCollider)
        {
            int hitLayer = hitCollider.gameObject.layer;
            if (hitLayer == 4)
            {
                return "Water";
            }
            if (hitLayer == 9)
            {
                return "Ice";
            }
            if (hitLayer == 10)
            {
                return "Mud";
            }
            else return "Normal";
        }
        else
            return "Normal";

    }

}
