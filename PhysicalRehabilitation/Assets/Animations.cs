using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private string[] states = new string[] { "Welcome", "Sit", "HandHeart", "ExtendLeft", "Armrest", "ScootFeet", "LeanForward", "Stand", "ShoulderFeet", "LungeRight", "LungeLeft" };
    private int currentIndex = -1;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (null != anim)
            {
                // play Bounce but start at a quarter of the way though
                if(currentIndex < states.Length - 1)
                {
                    currentIndex++;
                    anim.Play(states[currentIndex]);
                }
                
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (null != anim)
            {
                // play Bounce but start at a quarter of the way though
                if (currentIndex - 1 > -1)
                {
                    currentIndex--;
                    anim.Play(states[currentIndex]);
                }

            }
        }

    }
}
