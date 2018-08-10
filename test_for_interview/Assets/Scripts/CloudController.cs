using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour {

    [SerializeField]
    private GameObject m_Clouds;
    [SerializeField]
    private Light m_Light;

    private float m_ShadowStrengthForRain;
    private Color32 m_LightColorForRain;

    private void Awake()
    {
        m_ShadowStrengthForRain = 0.15f;
        m_LightColorForRain = new Color32(175, 175, 175, 255);
    }

    private void Start()
    {
        clouding();
    }

    private void clouding()
    {
        m_Clouds.SetActive(true);
        m_Light.shadowStrength = m_ShadowStrengthForRain;
        m_Light.color = m_LightColorForRain;
    }
}
