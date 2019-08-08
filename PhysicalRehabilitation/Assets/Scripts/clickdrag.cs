using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class clickdrag : MonoBehaviour
{
    //宣告GameObject
    public GameObject arml;
    public GameObject armr;
    public GameObject calfl;
    public GameObject calfr;
    public GameObject footl;
    public GameObject footr;
    //旋轉角度
    private float mY;
    private float mX;
    //旋轉速度
    public float speed = 1;

    void Start()
    {
        //預設動作
        arml.transform.Rotate(0,0,60);
        armr.transform.Rotate(0, 0, -60);

    }
    // Happens every frame
    void Update()
    {
        mY = 0.0f;mX = 0.0f;
        if (Input.GetMouseButton(0))
        {
            //記錄滑鼠點選瞬間的點            
            Vector3 mousepos = Input.mousePosition;
            float x = mousepos.x;
            float y = mousepos.y;
            Debug.Log(x+", "+y);
            mY = Input.GetAxis("Mouse Y") * speed;
            mX = Input.GetAxis("Mouse X") * speed*10;
            if(x >=500 && y>330) arml.transform.Rotate(0, 0, -mY);  
            else if (x < 450 && y>330) armr.transform.Rotate(0, 0, mY);
            else if(x>=480 && y>200) calfl.transform.Rotate(0, -mX, 0);
            else if(x <= 480 && y > 200) calfr.transform.Rotate(-mX, 0, 0);
            else if (x >= 480 && y < 200) footl.transform.Rotate(0, mX, 0);
            else if(x <= 480 && y < 200)  footr.transform.Rotate(0, mX, 0);
        }
        if (Input.GetMouseButton(1))
        {    //not stable    
            mY = Input.GetAxis("Mouse Y") * speed;
            armr.transform.Rotate(0, 0, mY);

        }
        //####Reset####
        if (Input.GetKeyDown(KeyCode.R))
        {
            armr.transform.eulerAngles = new Vector3(0, 0, 0);
            arml.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        //####Test Code####
        if (Input.GetKeyDown(KeyCode.Z)) {
            armr.transform.Rotate(0, 0, 10);
            //calfr.transform.Rotate(0, 0, 10);
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            armr.transform.Rotate(0, 0, -10);
            //calfr.transform.Rotate(0, 0, -10);
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            arml.transform.Rotate(0, 0, 10);
        }
        if (Input.GetKeyDown(KeyCode.V)) {
            arml.transform.Rotate(0, 0, -10);
        }
    }
}