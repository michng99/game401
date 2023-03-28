using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private bool isNen;
    private Animator anim;
    public ParticleSystem psBui;
    public GameObject menu;
    private bool isPlaying;
    private int countCoin;
    public TMP_Text txtCoin;
    public AudioSource coinSound;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 scale = transform.localScale;
        anim.SetBool("isRunning", false);
        Quaternion rotation = psBui.transform.localRotation;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotation.y = 0;

            psBui.transform.localRotation = rotation;

            psBui.Play();

            anim.SetBool("isRunning", true);

            scale.x = -1;

            transform.Translate(Vector3.left * 5f * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotation.y = 2;

            psBui.transform.localRotation = rotation;

            psBui.Play();

            anim.SetBool("isRunning", true);

            scale.x = 1;

            transform.Translate(Vector3.right * 5f * Time.deltaTime);
        }

        transform.localScale = scale;

        if (Input.GetKey(KeyCode.Space))
        {
            if (isNen)
            {
                rb.AddForce(new Vector2(0, 300));
                isNen = false;
            }
        }

        //GetKey: Nhan giu nut
        //GetKeyDown: Nhan xuong 1 lan
        //GetKeyUp: Nhan xong tha
        
        //..Pause game và show menu
        if (Input.GetKey(KeyCode.P))
        {
            showMenu();
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "nen")
        {
            isNen = true;
        }
    }

    public void showMenu()
    {
        if (isPlaying)
        {
            menu.SetActive(true);
            Time.timeScale = 0;
            isPlaying = false;
        }
        else
        {
            menu.SetActive(false);
            Time.timeScale = 1;
            isPlaying = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "coins")
        {
            coinSound.Play();
            Destroy(collision.gameObject);
            countCoin += 10;
            txtCoin.text = countCoin + " x";
        }    
    }
}
