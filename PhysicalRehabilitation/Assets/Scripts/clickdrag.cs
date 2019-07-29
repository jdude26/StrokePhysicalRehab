using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class clickdrag : MonoBehaviour
{
    //宣告兩個GameObject
    public GameObject arml;
    public GameObject armr;
    //旋轉角度
    private float mY = 0.0F;
    //旋轉速度
    public float speed = 10.0f;

    void Start()
    {
        //預設動作
        arml.transform.Rotate(0,0,60);
        armr.transform.Rotate(0, 0, -60);
    }
    // Happens every frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //記錄滑鼠點選瞬間的點            
            mY -= Input.GetAxis("Mouse Y") * speed * 0.02f;
            arml.transform.Rotate(0, 0, mY);         
            
        }
        if (Input.GetMouseButton(1))
        {    //not stable    
            mY -= Input.GetAxis("Mouse Y") * speed * 0.02f;
            armr.transform.Rotate(0, 0, -mY);

        }
        if (Input.GetKeyDown(KeyCode.Z)) {
            armr.transform.Rotate(0, 0, 10);
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            armr.transform.Rotate(0, 0, -10);
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            arml.transform.Rotate(0, 0, 10);
        }
        if (Input.GetKeyDown(KeyCode.V)) {
            arml.transform.Rotate(0, 0, -10);
        }
    }
}