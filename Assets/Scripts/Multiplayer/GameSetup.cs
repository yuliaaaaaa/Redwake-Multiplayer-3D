using UnityEngine;
using Photon.Pun;

public class GameSetup : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // Спавн гравця при вході в сцену
        PhotonNetwork.Instantiate("Player", new Vector3(78, 12, 1), Quaternion.identity);
    }
}
