using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCotroller : MonoBehaviour
{
    private int count = 3;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(count-- > 0)
        {
            float position = Random.Range(-3f, 0);
            GameObject qv = (GameObject) Instantiate(Resources.Load("Prefabs/quai"), 
                new Vector3(position, -0.91f, 0), Quaternion.identity);
            qv.GetComponent<Enemy>().SetStart(position - 3);
            qv.GetComponent<Enemy>().SetEnd(position + 5);
            qv.GetComponent<Enemy>().SetPlayer(player);
        }
        
    }
}
