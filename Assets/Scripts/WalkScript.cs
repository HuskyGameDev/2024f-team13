using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;

public class WalkScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject player;
    public Animator animator;
    public int speed, runSpeed; // look_angle_offset;
    private int yVel, xVel;
    //private float angle;
    private bool inW, inA, inS, inD;
    //private Vector2 aim, prevMousePosition;
    public static Vector2 playerPos;

    Vector2 movement;
    public Vector2 targetPosition;


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

        animator.SetFloat("MoveHorizontal", movement.x);
        animator.SetFloat("MoveVertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (animator != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Swipe");
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Shoot");
            }
        }

        inW = Input.GetKey(KeyCode.W);
        inA = Input.GetKey(KeyCode.A);
        inS = Input.GetKey(KeyCode.S);
        inD = Input.GetKey(KeyCode.D);

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

        //TURNING
        //aim = Input.mousePosition;
        //Vector3 object_pos = Camera.main.WorldToScreenPoint(transform.position);
        //if (inW || inA || inS || inD)
        //{
        //    angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        //    transform.rotation = Quaternion.Euler(0, 0, angle + look_angle_offset);

        //}
        //else if (prevMousePosition != aim)
        //{
        //    //Only aim at mouse if mouse moved since last frame other wise keep current rotation
        //    angle = Mathf.Atan2((aim.y - object_pos.y), (aim.x - object_pos.x)) * Mathf.Rad2Deg;
        //    transform.rotation = Quaternion.Euler(0, 0, angle + look_angle_offset);
        //}

        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 targetDirection = new Vector2(targetPosition.x - transform.position.x, targetPosition.y - transform.position.y);
        animator.SetFloat("Horizontal", targetDirection.x);
        animator.SetFloat("Vertical", targetDirection.y);

        //WALK ANIMATION
        //if (rb.velocity != Vector2.zero)
        //{
        //    animator.SetBool("walking", true);
        //}
        //else
        //{
        //    animator.SetBool("walking", false);
        //}
        //playerPos = rb.transform.position;
        //prevMousePosition = Input.mousePosition;

    }

}
