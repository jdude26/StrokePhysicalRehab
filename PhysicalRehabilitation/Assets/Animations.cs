using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private string[] states = new string[] { "Welcome", "Sit", "HandHeart", "LiftElbow", "ExtendLeft", "Armrest", "SitInstructions", "ScootFeet", "LeanForward", "Stand", "ShoulderFeet", "LungeLeft", "StepBackward", "LungeRight" };
    private int currentIndex = -1;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void PlaySound(string resource)
    {
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        AudioClip clip = (AudioClip)Resources.Load(resource);
        if (clip != null)
        {
            audio.PlayOneShot(clip);
        }
        //else
        //{
            //Debug.Log("Attempted to play missing audio clip by name" + resource);​
        //}​
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
                    PlaySound(states[currentIndex]);
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
                    PlaySound(states[currentIndex]);
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (null != anim)
            {
                if (currentIndex == 2)
                {
                    PlaySound("HandHigher");
                }
                else if (currentIndex == 3)
                {
                    PlaySound("ElbowHigher");
                }
                else
                {
                    PlaySound("StepForward");
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (null != anim)
            {
                if (currentIndex == 2)
                {
                    PlaySound("HandLeft");
                }
                else
                {
                    PlaySound("StepLeft");
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (null != anim)
            {
                if (currentIndex == 2)
                {
                    PlaySound("HandLower");
                }
                else if (currentIndex == 3)
                {
                    PlaySound("ElbowLower");
                }
                else
                {
                    PlaySound("StepBackward");
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (null != anim)
            {
                if (currentIndex == 2)
                {
                    PlaySound("HandRight");
                }
                else
                {
                    PlaySound("StepRight");
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (null != anim)
            {
                int randInt = Random.Range(0, 6);
                if (randInt == 0)
                {
                    PlaySound("Pos1");
                }
                else if (randInt == 1)
                {
                    PlaySound("Pos2");
                }
                else if (randInt == 2)
                {
                    PlaySound("Pos3");
                }
                else if (randInt == 3)
                {
                    PlaySound("Pos4");
                }
                else
                {
                    PlaySound("Pos5");
                }
            }
        }

    }
}
