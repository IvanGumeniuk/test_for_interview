using UnityEngine;
using System.Collections;
using Invector.CharacterController;

public class ThirdPersonNet : Photon.PunBehaviour
{
    [SerializeField]
    private GameObject character;
    [SerializeField]
    private GameObject camera;
    [SerializeField]
    private vThirdPersonInput inputManager;


    bool firstTake = false;

    void OnEnable()
    {
        firstTake = true;
    }

    void Awake()
    {
        //  cameraScript = GetComponent<ThirdPersonCamera>();
        //  controllerScript = GetComponent<ThirdPersonController>();
        /*
                if(photonView.isMine)
                {
                    //MINE: local player, simply enable the local scripts
                    cameraScript.enabled = true;
                    controllerScript.enabled = true;
                }
                else
                {
                    cameraScript.enabled = false;

                    controllerScript.enabled = true;
               //     controllerScript.isControllable = false;
                }
                */
        gameObject.name = gameObject.name + photonView.viewID;

      //  character = GameObject.Find("vThirdPersonController") as GameObject;
        character.name = character.name + photonView.viewID;

        inputManager = character.GetComponent<vThirdPersonInput>();

    //    camera = GameObject.Find("vThirdPersonCamera") as GameObject;
        camera.name = camera.name + photonView.viewID;

     //   camera = GameObject.Find("vThirdPersonCamera"+ photonView.viewID) as GameObject;

    }

    void OnGUI()
    {
        GUILayout.Label("player:" + gameObject.name+"   camera:" + camera.name + "    target:" + character.name);
    }
    
  

    private void Start()
    {
       // if(photonView.isMine)
       //     camera.GetComponent<vThirdPersonCamera>().SetTarget(character.transform);

        if(!photonView.isMine)
        {
            camera.GetComponent<Camera>().enabled = false;
            //character.SetActive(false);
            inputManager.enabled = false;
            
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
            character.transform.position = pos;
            character.transform.rotation = rot;
        }     
    }

    private Vector3 correctPlayerPos = Vector3.zero; //We lerp towards this
    private Quaternion correctPlayerRot = Quaternion.identity; //We lerp towards this

    void Update()
    {
        if(!photonView.isMine)
        {
            //Update remote player (smooth this, this looks good, at the cost of some accuracy)
          //  transform.position = Vector3.Lerp(transform.position, correctPlayerPos, Time.deltaTime * 5);
         //   transform.rotation = Quaternion.Lerp(transform.rotation, correctPlayerRot, Time.deltaTime * 5);
        }
    }

}