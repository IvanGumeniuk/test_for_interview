using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour {

    [SerializeField] private ParticleSystem m_Snowing;
    [SerializeField] private GameObject m_Wind;
    public bool Wind;
    [SerializeField] private RainController m_RainController;
    public bool Rain;
    // Use this for initialization
    void Start () {
        if(m_RainController)
          m_RainController.rainign(Rain);
        m_Wind.SetActive(Wind);
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
