using ARFC;
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
            character.GetComponent<FPController>().enabled = false;
            camera.enabled = false;
        }
	}


    void OnGUI()
    {
        if(photonView.isMine)
            GUILayout.Label("player:" + character.name + "   object:" + gameObject.name);
    }
    // Update is called once per frame
    private Vector3 oldPosition = Vector3.zero; 
    private Vector3 newPosition = Vector3.zero; 
    private Quaternion oldPlayerRot = Quaternion.identity; 
    private Quaternion newPlayerRot = Quaternion.identity;
    private Quaternion oldCamerarRot = Quaternion.identity;
    private Quaternion newCameraRot = Quaternion.identity;

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
                camera.transform.rotation = oldCamerarRot = newCameraRot;
            }
            else
            {
                offsetTime += Time.deltaTime * 9f;
                transform.position = Vector3.Lerp(oldPosition, newPosition, offsetTime);
                transform.rotation = Quaternion.Lerp(oldPlayerRot, newPlayerRot, offsetTime);
                camera.transform.rotation = Quaternion.Lerp(oldCamerarRot, newCameraRot, offsetTime);
            }
        }
    }

        void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Vector3 playerPos = character.transform.position;
        Quaternion playerRot = character.transform.rotation;
        
        Quaternion cameraRot = camera.transform.rotation;


        stream.Serialize(ref playerPos);
        stream.Serialize(ref playerRot);
        stream.Serialize(ref cameraRot);
        if(stream.isReading)
        {
            oldPosition = transform.position;
            oldPlayerRot = transform.rotation;
            newPosition = playerPos;
            newPlayerRot = playerRot;
            offsetTime = 0;
            isSync = true;

            oldCamerarRot = camera.transform.rotation;
            newCameraRot = cameraRot;

        //    character.transform.position = playerPos;
         //   character.transform.rotation = playerRot;

         //   camera.transform.rotation = cameraRot;
        }
    }

}
