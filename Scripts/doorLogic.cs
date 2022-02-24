using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorLogic : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;

    private Vector3 openPos;
    private Vector3 closePos;

    void Start()
    {
        closePos = transform.position;
        openPos = new Vector3(closePos.x,closePos.y-6,closePos.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(button1.GetComponent<ButtonLogic>().pressed && button2.GetComponent<ButtonLogic>().pressed){
            transform.position = Vector3.MoveTowards(transform.position, openPos, 1.5f*Time.deltaTime);
        }
        else{
            transform.position = Vector3.MoveTowards(transform.position, closePos, 1.5f*Time.deltaTime);
        }
    }
}
