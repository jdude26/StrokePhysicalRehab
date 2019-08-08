using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubetestdrag : MonoBehaviour{

    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //記錄滑鼠點選瞬間的點            
            Vector3 mousepos = Input.mousePosition;
            cube.transform.position = mousepos;
        }
    }
}
