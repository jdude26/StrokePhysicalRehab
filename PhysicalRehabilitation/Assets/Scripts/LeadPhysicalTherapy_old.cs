using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LeadPhysicalTherapy_Old: MonoBehaviour {


    [Tooltip("Path to the file used to record or replay the recorded data.")]
    public string filePath = "Assets\\ExcerciseAnimations\\BodyRecording.csv";

    private bool isPlaying = false;

    // reference to the KM
    private KinectManager manager = null;

    // time variables used for recording and playing
    private long liRelTime = 0;
    private float fStartTime = 0f;
    private float fCurrentTime = 0f;
    private int fCurrentFrame = 0;

    // player variables
    private StreamReader fileReader = null;
    private float fPlayTime = 0f;
    private string sPlayLine = string.Empty;

    // Use this for initialization
    void Start () {




        Debug.Log("Playing started.");


        // initialize times
        fStartTime = fCurrentTime = Time.time;
        fCurrentFrame = -1;

        // open the file and read a line
#if !UNITY_WSA
        fileReader = new StreamReader(filePath);
#endif
        ReadLineFromFile();

        // enable the play mode
        if (manager)
        {
            manager.EnablePlayMode(true);
        }
        StartPlaying();
    }

	
	// Update is called once per frame
	void Update () {
        //bones[1].position.Set(bones[1].position.x + 1, bones[1].position.y + 1, bones[1].position.z + 1);
        //bones[1].Rotate(2, 2, 2);

        //


        if (isPlaying)
        {/*
            // wait for the right time
            fCurrentTime = Time.time;
            float fRelTime = fCurrentTime - fStartTime;
            //Debug.Log(sPlayLine);

                    var line = sPlayLine.Split(',');
                    var index = 0;
                    for(var i = 0; i < 24; i++)
                    {
                        float x = 0;
                        float y = 0;
                        float z = 0;
                float w = 0;
                        float.TryParse(line[index], out x);
                        float.TryParse(line[index + 1], out y);
                        float.TryParse(line[index + 2], out z);
                float.TryParse(line[index + 2], out w);

                //float old_w = bones[i].rotation.w;
                //bones[i].Rotate(new Vector3(diff_x, diff_y, diff_z));
                //bones[i].SetPositionAndRotation(new Vector3(x, y, z), new Quaternion(0, 0, 0, 0));
                //bones[i].position.Set(x, y, z);
                //bones[i].rotation.Set(x, y, z, 0);
                if (bones[i] != null)
                {
                    Transform boneTransform = bones[i];
                    boneTransform.rotation = new Quaternion(x, y, z, w);
                    
                }
                


                index += 4;
                    }

                // and read the next line
                ReadLineFromFile();

            if (sPlayLine == null)
            {
                // finish playing, if we reached the EOF
                StopRecordingOrPlaying();
            }
            */
            fCurrentTime = Time.time;
            float fRelTime = fCurrentTime - fStartTime;

            if (sPlayLine != null && fRelTime >= fPlayTime)
            {
                // then play the line
                if (manager && sPlayLine.Length > 0)
                {
                    manager.GetBodyIndexByUserId(2);

                    
                    manager.SetBodyFrameData(sPlayLine);
                    Debug.Log(sPlayLine);
                }

                // and read the next line
                ReadLineFromFile();
            }

            if (sPlayLine == null)
            {
                // finish playing, if we reached the EOF
                StopRecordingOrPlaying();
            }
        }

    }
    private void CloseFile()
    {
        // close the file
        if (fileReader != null)
        {
            fileReader.Dispose();
            fileReader = null;
        }

    }
    // reads a line from the file
    private bool ReadLineFromFile()
    {
        if (fileReader == null)
            return false;

        // read a line
        sPlayLine = fileReader.ReadLine();
        if (sPlayLine == null)
            return false;

        // extract the unity time and the body frame
        char[] delimiters = { '|' };
        string[] sLineParts = sPlayLine.Split(delimiters);

        if (sLineParts.Length >= 2)
        {
            float.TryParse(sLineParts[0], out fPlayTime);
            sPlayLine = sLineParts[1];
            fCurrentFrame++;


            return true;
        }

        return false;
    }
    public void StopRecordingOrPlaying()
    {


        if (isPlaying)
        {
            // close the file, if it is playing
            CloseFile();
            isPlaying = false;

            Debug.Log("Playing stopped.");
        }


    }
    public bool StartPlaying()
    {
        if (isPlaying)
            return false;

        isPlaying = true;

        // avoid recording an playing at the same time

        // stop playing if there is no file name specified
        if (filePath.Length == 0 || !File.Exists(filePath))
        {
            isPlaying = false;
            Debug.LogError("No file to play.");


        }

        if (isPlaying)
        {
            Debug.Log("Playing started.");


            // initialize times
            fStartTime = fCurrentTime = Time.time;
            fCurrentFrame = -1;

            // open the file and read a line
#if !UNITY_WSA
            fileReader = new StreamReader(filePath);
#endif
            ReadLineFromFile();

            // enable the play mode
            if (manager)
            {
                manager.EnablePlayMode(true);
            }
        }

        return isPlaying;
    }
}
