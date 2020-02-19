using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // setup camera
        Camera.main.orthographicSize = Screen.height / 2;
    }

    // PHYSICS
    const float movespeed = 1f;

    // Update is called once per frame
    void Update()
    {
        // Move character around
        transform.position = new Vector3(
            transform.position.x + (Input.GetAxis("Horizontal") * movespeed),
            transform.position.y + (Input.GetAxis("Vertical")   * movespeed)
        );
    }
}
