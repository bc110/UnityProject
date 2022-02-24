using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : MonoBehaviour
{

    public GameObject onColour;
    public bool pressed = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Box"){
            onColour.GetComponent<MeshRenderer>().enabled = true;
            pressed = true;
            this.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Box"){
            onColour.GetComponent<MeshRenderer>().enabled = false;
            pressed = false;
            this.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
