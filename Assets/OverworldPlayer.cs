using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldPlayer : MonoBehaviour
{
    public string pclass = "toad";
    public bool remote = false;

    const int PPU = 32; // pixels per unit

    float hspeed;
    float vspeed;
    char direction = 'D';
    char state = 'W';

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    char getDirection(float hspeed, float vspeed) {
        const float deadzone = 0.05f;

        bool nh = hspeed < deadzone; // Negative Hspeed
        bool ph = hspeed > deadzone; // Positive Hspeed
        bool nv = vspeed < deadzone; // Negative Vspeed
        bool pv = vspeed > deadzone; // Positive Vspeed

        char o = 'x'; // 'x' => unchanged

        if (nh) o = 'A'; // Left
        if (ph) o = 'B'; // Right
        if (nv) o = 'C'; // Up
        if (pv) o = 'D'; // Down

        if (nh && pv) o = 'E'; // Northwest
        if (nh && nv) o = 'F'; // Southwest
        if (ph && pv) o = 'G'; // Northeast
        if (ph && nv) o = 'H'; // Southeast

        return o;
    }

    // PHYSICS
    const float movespeed = 1f / PPU;
                         // (1px / 32 PPU) per frame

    // Update is called once per frame
    void Update()
    {
        // Move character around
        hspeed = Input.GetAxis("Horizontal") * movespeed;
        vspeed = Input.GetAxis("Vertical") * movespeed;

        // Animation code
        char ld = direction; // last direction
        direction = getDirection(hspeed, vspeed);

        if (direction == 'x') direction = ld; // x means no change

        animator.speed = Mathf.Abs((hspeed + vspeed) * PPU);

        animator.Play(pclass + state + direction);
        
        // if not moving, set to frame 0
        if (animator.speed == 0)
            animator.Play(0, -1, 0);

        // Apply hspeed/vspeed
        transform.position += new Vector3(
            hspeed, vspeed
        );
    }
}
