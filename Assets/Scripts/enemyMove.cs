using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour
{

    private Rigidbody2D rb;
    private float moveSpeed, distance;
    public int look_angle_offset;
    public Animator animator;
    public enemyType type;

    [SerializeField] GameObject player;
    int animLayer = 0;

    public enum enemyType
    {
        small,
        medium,
        large
    }

    bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(animLayer).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(animLayer).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        switch (type)
        {
            case enemyType.large:
                moveSpeed = 3f;
                break;
            case enemyType.medium:
                moveSpeed = 1f;
                break;
            case enemyType.small:
            default:
                moveSpeed = 2.0f;
                break;
        }
    }

    void Awake()
    {
    }

    // TODO Make movement stop if stuck 
    // Update is called once per frame
    void Update() {
        // Move towards player 

        distance = Vector2.Distance(player.transform.position, transform.position);

        switch (type)
        {
            case enemyType.large:

                if (distance < 10)
                {

                    if (distance < 2)
                    {
                        animator.SetTrigger("Attack");


                    } else {

                        if(!isPlaying(animator, "Medium_hide"))
                        {
                            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
                            transform.up = player.transform.position - transform.position;
                        }
                        
                        animator.ResetTrigger("Attack");
                    }
                }

                    break;
            case enemyType.medium:

                if (distance < 7)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
                    transform.up = player.transform.position - transform.position;

                    if (distance < 1.5)
                    {
                        animator.SetTrigger("Attack");

                        animator.SetTrigger("Hide");
                    }
                    else
                    {
                        animator.ResetTrigger("Attack");

                    }
                }

                    break;
            case enemyType.small:

                if (distance < 10)
                {
                    animator.SetBool("PlayerDetected", true);
                    transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
                    transform.up = player.transform.position - transform.position;
                    
                } else
                {
                    animator.SetBool("PlayerDetected", false);
                }
                break;
        }

        //if (WalkScript.playerPos.y < gameObject.transform.position.y){
        //   transform.position = transform.position - new Vector3(0f, moveSpeed, 0);
        //}
        //if (WalkScript.playerPos.y > gameObject.transform.position.y){
        //   transform.position = transform.position + new Vector3(0f, moveSpeed, 0);
        //}
        //if (WalkScript.playerPos.x > gameObject.transform.position.x){
        //   transform.position = transform.position + new Vector3(moveSpeed, 0f, 0);
        //}
        //if (WalkScript.playerPos.x < gameObject.transform.position.x){
        //   transform.position = transform.position - new Vector3(moveSpeed, 0f, 0);
        //}


        //TODO Figure out angel offset
        // Look at player this was working then stopped I love it so much 
        // Quaternion rotation = Quaternion.LookRotation(
        //  WalkScript.playerPos - transform.position ,
        //transform.TransformDirection(Vector3.up)
        //);
        //transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        //transform.right = WalkScript.playerPos - transform.position;
        //Vector3 dir = WalkScript.playerPos - transform.position;
        //angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle + look_angle_offset, Vector3.forward);
    }
}
