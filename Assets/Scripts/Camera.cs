using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject player;
    public float start, end;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //lay vi tri x cua player
        var playerX = player.transform.position.x;
        // lay vi tri x cua cam 
        var camX = transform.position.x;
        if(playerX > start && playerX < end)
        {
            camX = playerX;
        }
        else
        {
            if(playerX < start) { camX = start; }
            if(playerX > end) { camX = end; }
        }
        transform.position = new Vector3(camX,0,-10);
    }
    
}
