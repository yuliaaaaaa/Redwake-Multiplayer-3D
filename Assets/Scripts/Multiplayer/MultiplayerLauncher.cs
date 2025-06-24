using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // підключення до Photon
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server");

        PhotonNetwork.JoinLobby(); // приєднання до лобі
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Приєднано до лобі. Створюємо або підключаємось до кімнати...");
        PhotonNetwork.JoinOrCreateRoom("Room_1", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Увійшли в кімнату!");
        PhotonNetwork.LoadLevel("SampleScene"); // переходиш на сцену гри
    }
}
