using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkManager : MonoBehaviour, IPunObservable
{

    public PhotonView photonView;

    public GameManager gameManager;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        photonView = PhotonView.Get(this);
    }


    public void NotifySelectBoardPiece(GameObject gameObject)
    {
        if ((int) gameManager.currentActivePlayer.id == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            //allow the player to select a board item
            photonView.RPC("RPC_NotifySelectBoardPiece", RpcTarget.All, gameObject.name);
            Debug.Log("sent rpc " + gameObject.name);
        }
    }

    [PunRPC]
    public void RPC_NotifySelectBoardPiece(string gameObjectName)
    {
        GetComponent<GameManager>().SelectBoardPiece(GameObject.Find(gameObjectName));
    }

}
