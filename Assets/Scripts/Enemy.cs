using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float start, end;
    private bool isRight;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var poEnemy = transform.position.x;

        //di theo player
        if(player!= null)
        {
            var poPlayer = player.transform.position.x;
            if (poPlayer > start && poPlayer < end)
            {
                if (poPlayer < poEnemy) isRight = false;
                if (poPlayer > poEnemy) isRight = true;
            }
        }
        


        if(poEnemy < start)
        {
            isRight = true;
        }
        if (poEnemy > end)
        {
            isRight = false;
        }
        Vector2 scale = transform.localScale;
        if (isRight)
        {
            scale.x = -1;
            transform.Translate(Vector3.right * 2f * Time.deltaTime);
        }
        else
        {
            scale.x = 1;
            transform.Translate(Vector3.left * 2f * Time.deltaTime);
        }
        transform.localScale = scale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "trai")
        {
            isRight = isRight ? false : true;
        }
    }

    public void SetStart(float start)
    {
        this.start = start;
    }

    public void SetEnd(float end)
    {
        this.end = end;
    }
    public void SetPlayer(GameObject player)
    {
        this.player = player;
    }
}
