using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HITscript : MonoBehaviour

{
    // Start is called before the first frame update

    private RaycastHit2D hit;
    public GameObject obj;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hit) { Destroy(obj); }
    }
}