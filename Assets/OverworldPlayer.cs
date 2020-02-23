using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text debugoutput;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    char getDirection(float hspeed, float vspeed) {
        const float deadzone = 0f;

        bool nh = hspeed < deadzone; // Negative Hspeed
        bool ph = hspeed > deadzone; // Positive Hspeed
        bool nv = vspeed < deadzone; // Negative Vspeed
        bool pv = vspeed > deadzone; // Positive Vspeed

        char o = 'x'; // 'x' => unchanged

        if (nh) o = 'A'; // Left
        if (ph) o = 'B'; // Right
        if (nv) o = 'C'; // Up
        if (pv) o = 'D'; // Down

        if (nh && pv) o = 'E'; // Northwest ( Left + Up )
        if (nh && nv) o = 'F'; // Southwest ( Left + Down )
        if (ph && pv) o = 'G'; // Northeast ( Right + Up )
        if (ph && nv) o = 'H'; // Southeast ( Right + Down )

        return o;
    }

    // PHYSICS
    const float movespeed = 1.5f  / PPU;
                        // (1.5px / 32 PxPerUnit) per frame

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

        animator.speed = (Mathf.Abs(hspeed) + Mathf.Abs(vspeed)) * PPU;

        // don't speed up diagonal animations
        if ("EFGH".IndexOf(direction) != -1)
            animator.speed = (Mathf.Abs(hspeed) + Mathf.Abs(vspeed)) / 2 * PPU;

        debugoutput.text = pclass + state + direction;
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
