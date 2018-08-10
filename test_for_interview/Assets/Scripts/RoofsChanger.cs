using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofsChanger : MonoBehaviour {

    [SerializeField] private Material RoofInSnow;
    [SerializeField] private Material RoofDefault;
	
    public void changeRoof(bool isSnowing)
    {
        GameObject[] roofs = GameObject.FindGameObjectsWithTag("Roof");
       if(roofs != null)
        foreach(GameObject roof in roofs)
        {
            roof.GetComponent<Renderer>().material = isSnowing ? RoofInSnow : RoofDefault;
        }
    }
}
