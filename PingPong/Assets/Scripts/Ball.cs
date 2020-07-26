using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    public float size;

    private Controller controller;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>();

        //Change ball parameters at Restart
        speed += Random.Range(-1f, 1f);
        size += Random.Range(0f, 0.25f);
        transform.localScale = new Vector3(size, size, size);

        //Set random direction
        direction = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-1f, 1f), 0).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isPlaying)
        {
            transform.position += (direction * speed) * Time.deltaTime;

            //Side collision
            if (transform.position.x < controller.screenBounds.x || transform.position.x > controller.screenBounds.width)
            {
                Bounce(Vector3.right);
            }
            //Gate collision
            else if (transform.position.y < controller.screenBounds.y || transform.position.y > controller.screenBounds.height)
            {
                controller.EndLevel();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Bounce(collision.GetContact(0).normal);
            controller.score++;
        }
    }
    
    void Bounce(Vector3 surfaceNormal)
    {
        direction = Vector3.Reflect(direction, surfaceNormal);
    }
}
