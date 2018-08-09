using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour {

    [SerializeField] private GameObject m_Clouds;
    [SerializeField] private GameObject m_Raining;
    [SerializeField] private Light m_Light;

    private float m_ShadowStrengthForRain;
    private Color32 m_LightColorForRain;

    private void Awake()
    {
         m_ShadowStrengthForRain = 0.15f;
         m_LightColorForRain = new Color32(175, 175, 175, 255);
    }

    public void rainign(bool enable)
    {
        m_Clouds.SetActive(enable);
        m_Raining.SetActive(enable);
        if(enable)
        {
            m_Light.shadowStrength = m_ShadowStrengthForRain;
            m_Light.color = m_LightColorForRain;
        }
    }


}
