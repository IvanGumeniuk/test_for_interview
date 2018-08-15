using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FirstPersonNet : Photon.PunBehaviour {

    [SerializeField]
    private GameObject character;
    [SerializeField]
    private Camera camera;

    // Use this for initialization

    private void Awake()
    {
        character = gameObject;
        camera = gameObject.GetComponentInChildren<Camera>();
        character.name = character.name + photonView.viewID;
    }

    void Start () { 
            
        if(!photonView.isMine)
        {
            character.GetComponent<FirstPersonController>().enabled = false;
            camera.enabled = false;
        }
	}


    void OnGUI()
    {
        if(photonView.isMine)
        GUILayout.Label("player:" + character.name + "   object:" + gameObject.name);
    }
    // Update is called once per frame
    private Vector3 oldPosition = Vector3.zero; //We lerp towards this
    private Vector3 newPosition = Vector3.zero; //We lerp towards this
    private Quaternion oldPlayerRot = Quaternion.identity; //We lerp towards this
    private Quaternion newPlayerRot = Quaternion.identity; //We lerp towards this
    private float offsetTime = 0f;
    bool isSync = false;

    void Update()
    {
        if(!photonView.isMine && isSync)
        {
            if((Vector3.Distance(oldPosition, newPosition) > 3f) || (Quaternion.Angle(oldPlayerRot, newPlayerRot)>30f))
            {
                transform.position = oldPosition = newPosition;
                transform.rotation = oldPlayerRot = newPlayerRot;
            }
            else
            {
                offsetTime += Time.deltaTime * 9f;
                transform.position = Vector3.Lerp(oldPosition, newPosition, offsetTime);
                transform.rotation = Quaternion.Lerp(oldPlayerRot, newPlayerRot, offsetTime);
            }
        }
    }

        void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Vector3 pos = character.transform.position;
        Quaternion rot = character.transform.rotation;
        stream.Serialize(ref pos);
        stream.Serialize(ref rot);
        if(stream.isReading)
        {
            oldPosition = transform.position;
            oldPlayerRot = transform.rotation;
            newPosition = pos;
            newPlayerRot = rot;
            offsetTime = 0;
            isSync = true; 

            character.transform.position = pos;
            character.transform.rotation = rot;
        }
    }

}
