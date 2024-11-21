using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

/** For anyone who comes here in a later semester good luck I have no clue
 * what is going on here someone was in our team wrote the movement WalkScript 
 * and dropped the course and I never used unity before so I didn't dare touch
 * any of this and just been trying to impliment things enough to show an idea
 * of a game
 */ 



public class WalkScript : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    public Rigidbody2D rb;
    public static Vector3 playerPos;
    public int speed, runSpeed;
    [SerializeField] AudioSource sandSounds;
    public Vector2 targetPosition;
    Vector2 movement;
    
    private int yVel, xVel;
    private bool inW ,inA, inS, inD;
    
    // Start is called before the first frame update
    void Start()
    {
       UnityEngine.Cursor.lockState = CursorLockMode.Confined;
      animator = gameObject.GetComponent<Animator>();
    }
      
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        inW = Input.GetKey(KeyCode.W);
        inA = Input.GetKey(KeyCode.A);
        inS = Input.GetKey(KeyCode.S);
        inD = Input.GetKey(KeyCode.D);

        if (animator != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Swipe");
            }

            if (Input.GetMouseButtonDown(1))
            {
                animator.SetTrigger("Shoot");
            }
        }

        if (movement.x > 0 && movement.y > 0)
        {
            animator.SetFloat("MoveHorizontal", movement.x);
            animator.SetFloat("MoveVertical", movement.y);
        }
        else if (movement.x > 0 && movement.y == 0)
        {
            animator.SetFloat("MoveHorizontal", movement.x);
            animator.SetFloat("MoveVertical", movement.y);
        }
        else if (movement.x > 0 && movement.y < 0)
        {
            animator.SetFloat("MoveHorizontal", movement.x);
            animator.SetFloat("MoveVertical", movement.y);
        }
        else if (movement.x == 0 && movement.y < 0)
        {
            animator.SetFloat("MoveHorizontal", movement.x);
            animator.SetFloat("MoveVertical", movement.y);
        }
        else if (movement.x < 0 && movement.y < 0)
        {
            animator.SetFloat("MoveHorizontal", movement.x);
            animator.SetFloat("MoveVertical", movement.y);
        }
        else if (movement.x < 0 && movement.y == 0)
        {
            animator.SetFloat("MoveHorizontal", movement.x);
            animator.SetFloat("MoveVertical", movement.y);
        }
        else if (movement.x < 0 && movement.y > 0)
        {
            animator.SetFloat("MoveHorizontal", movement.x);
            animator.SetFloat("MoveVertical", movement.y);
        }
        else if (movement.x == 0 && movement.y > 0)
        {
            animator.SetFloat("MoveHorizontal", movement.x);
            animator.SetFloat("MoveVertical", movement.y);
        }

        animator.SetFloat("Speed", movement.sqrMagnitude);

        //MOVE
        if (inW ^ inS)
        { //upDown
            yVel = (inW) ? 1 : -1;
        }
        else
        {
            yVel = 0;
        }

        if (inA ^ inD)
        { // leftRight
            xVel = (inD) ? 1 : -1;
        }
        else
        {
            xVel = 0;
        }

        if (!((xVel == 0) && (yVel == 0)))
        {// SET VELOCITY+SPRINT
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                // Walk
                rb.velocity = (new Vector2(xVel, yVel) / Mathf.Sqrt((xVel * xVel) + (yVel * yVel))) * speed;
            }
            else
            {
                // Run
                rb.velocity = (new Vector2(xVel, yVel) / Mathf.Sqrt((xVel * xVel) + (yVel * yVel))) * runSpeed;
            }
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }

        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 targetDirection = new Vector2(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y);
        animator.SetFloat("Horizontal", targetDirection.x);
        animator.SetFloat("Vertical", targetDirection.y);

        //prevMousePosition = Input.mousePosition;
        playerPos = rb.transform.position;  
    }

}
