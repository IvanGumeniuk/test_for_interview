using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour {
    [SerializeField] private TerrainTextureChanger TerrainTexture;
    [SerializeField] private GameObject m_Cloud;
    [SerializeField] private GameObject m_Snow;
    public bool Snow;
    [SerializeField] private GameObject m_Wind;
    public bool Wind;
    [SerializeField] private GameObject m_Rain;
    public bool Rain;
  
    void Start () {
       
        if(Rain)
        {
            m_Rain.SetActive(Rain);
            m_Cloud.SetActive(Rain);
            TerrainTexture.changeToNotWinter();
        }

        if(Wind)
            m_Wind.SetActive(Wind);

        if(Snow)
        {
            m_Snow.SetActive(Snow);
            m_Cloud.SetActive(Snow);
            TerrainTexture.changeToWinter();
        }
    }
	
}
