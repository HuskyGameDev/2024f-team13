using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;

/** For anyone who comes here in a later semester good luck I have no clue
 * what is going on here someone was in our team wrote the movement WalkScript 
 * and dropped the course and I never used unity before so I didn't dare touch
 * any of this and just been trying to impliment things enough to show an idea
 * of a game
 */ 



public class WalkScript : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;
    public static Vector3 playerPos;
    public int speed, runSpeed;
    [SerializeField] AudioSource sandSounds;
    
    private int yVel, xVel;
    private bool inW ,inA, inS, inD;
    
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
        if (inW ^ inS) { //upDown
            yVel = (inW) ? 1 : -1; 
        } else { 
           yVel = 0; 
        }

        if (inA ^ inD) { // leftRight
           xVel = (inD) ? 1 : -1;
        } else {
           xVel = 0; 
        }

        if (!((xVel == 0) && (yVel == 0))) {// SET VELOCITY+SPRINT
            if (!Input.GetKey(KeyCode.LeftShift)) {
                // Walk
                rb.velocity = (new Vector2(xVel, yVel) / Mathf.Sqrt((xVel * xVel) + (yVel * yVel))) * speed;
            } else { 
               // Run
               rb.velocity = (new Vector2(xVel, yVel) / Mathf.Sqrt((xVel * xVel) + (yVel * yVel))) * runSpeed; 
            }
        } else { 
           rb.velocity = new Vector2(0,0); 
        }
         
      playerPos = rb.transform.position;  
    }
}
