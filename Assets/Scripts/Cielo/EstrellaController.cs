using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstrellaController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isColliding = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       // print("COLISION");
        isColliding = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isColliding = false;
    }
}
