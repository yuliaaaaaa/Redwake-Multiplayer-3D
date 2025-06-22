using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Collections.Generic;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("UI Elements")]
    public TMP_InputField nameInput;
    public Button connectButton;
    public TextMeshProUGUI playerListText;
    public Button startGameButton;

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        connectButton.onClick.AddListener(Connect);
        startGameButton.gameObject.SetActive(false);
    }

    void Connect()
    {
        string nickname = nameInput.text.Trim();

        if (string.IsNullOrEmpty(nickname))
        {
            Debug.LogWarning("❌ Ім’я не може бути порожнім");
            return;
        }

        PhotonNetwork.NickName = nickname;
        PhotonNetwork.ConnectUsingSettings();
        connectButton.interactable = false;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("✅ Підключено до Photon Master Server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("📥 Приєдналися до лобі. Створюємо або підключаємось до кімнати...");
        PhotonNetwork.JoinOrCreateRoom("Room_1", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("🚪 Увійшли в кімнату!");
        UpdatePlayerList();

        startGameButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
        startGameButton.onClick.RemoveAllListeners(); // уникаємо дублювання
        startGameButton.onClick.AddListener(() => {
            PhotonNetwork.LoadLevel("SampleScene");
        });
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdatePlayerList();
    }

    void UpdatePlayerList()
    {
        List<string> names = new List<string>();
        foreach (var player in PhotonNetwork.PlayerList)
        {
            string isMaster = player.IsMasterClient ? " (Host)" : "";
            names.Add(player.NickName + isMaster);
        }

        playerListText.text = "Players in the room:\n" + string.Join("\n", names);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
