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
    public int speed, runSpeed, look_angle_offset;
    private bool inW ,inA, inS, inD;
    private int yVel, xVel;
    private Vector2 aim;
    private float angle;
    
    

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        inW = Input.GetKey(KeyCode.W);
        inA = Input.GetKey(KeyCode.A);
        inS = Input.GetKey(KeyCode.S);
        inD = Input.GetKey(KeyCode.D);

        //MOVE
        if (inW ^ inS) //upDown
        {
            if (inW) { yVel = 1; }
            else { yVel = -1; }
        }
        else { yVel = 0; }

        if (inA ^ inD) //leftRight
        {
            if (inD) { xVel = 1; }
            else { xVel = -1; }
        }
        else { xVel = 0; }

        if (!((xVel == 0) && (yVel == 0)))//SET VELOCITY+SPRINT
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                //sprint
                rb.velocity = (new Vector2(xVel, yVel) / Mathf.Sqrt((xVel * xVel) + (yVel * yVel))) * speed;
            }
                //walk
            else { rb.velocity = (new Vector2(xVel, yVel) / Mathf.Sqrt((xVel * xVel) + (yVel * yVel))) * runSpeed; }
        }
        else { rb.velocity = new Vector2(0,0); }

        //TURNING
        Vector3 object_pos = Camera.main.WorldToScreenPoint(transform.position);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle+look_angle_offset);
        }
        else
        {
            aim = Input.mousePosition;
            angle = Mathf.Atan2((aim.y - object_pos.y), (aim.x - object_pos.x)) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + look_angle_offset);
        }

        //WALK ANIMATION
        if (rb.velocity != Vector2.zero) { animator.SetBool("walking", true); }
        else { animator.SetBool("walking", false); }

        
        
    }
}
