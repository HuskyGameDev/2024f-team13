using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamreaControl : MonoBehaviour
{
   public Rigidbody2D player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
    }
}