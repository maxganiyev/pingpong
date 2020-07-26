using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Controller controller;
    private Camera camera;

    private Vector3 deltaPosition;
    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<Controller>();

        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isPlaying)
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastPosition = new Vector3(camera.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, transform.position.z);
            }

            if (Input.GetMouseButton(0))
            {
                deltaPosition = new Vector3(camera.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, transform.position.z) - lastPosition;

                transform.position = new Vector3(Mathf.Clamp(transform.position.x + deltaPosition.x, controller.screenBounds.x, controller.screenBounds.width), transform.position.y, transform.position.z);

                lastPosition = new Vector3(camera.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, transform.position.z);
            }

            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * 4);
        }
    }
}
