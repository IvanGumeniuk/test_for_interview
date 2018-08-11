using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Seasons
{
    Spring,
    Summer,
    Autumn,
    Winter
};

public class WeatherController : MonoBehaviour {
    [SerializeField] private TerrainTextureChanger TerrainTexture;
    [SerializeField] private GameObject m_Cloud;
    [SerializeField] private GameObject m_Wind;
    [SerializeField] private GameObject m_Snow;
    [SerializeField] private GameObject m_Rain;
    public bool Clouds;
    public bool Wind;
    public bool Snow;
    public bool Rain;
    public Seasons Season;

    void Start () {
        TerrainTexture.changeSeason(Season);

        m_Rain.SetActive(Rain);
        m_Wind.SetActive(Wind);
        m_Snow.SetActive(Snow);
        m_Cloud.SetActive(Clouds);

    }
	
}
