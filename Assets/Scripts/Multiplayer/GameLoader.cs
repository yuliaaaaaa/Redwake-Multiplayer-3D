/*using UnityEngine;
using Photon.Pun;

public class GameLoader : MonoBehaviour
{
    void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            //PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
            PhotonNetwork.Instantiate("GameSetup", Vector3.zero, Quaternion.identity);
        }
    }
}
*/