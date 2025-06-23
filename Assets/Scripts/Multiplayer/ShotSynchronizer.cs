using UnityEngine;
using Photon.Pun;

public class ShotSynchronizer : MonoBehaviourPun
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform hostFirePoint;
    [SerializeField] private Transform clientFirePoint;

    public void SendFireAt(Vector2Int gridPos)
    {
        if (!PhotonNetwork.IsConnected || !PhotonNetwork.InRoom) return;

        photonView.RPC("RPC_FireCannonAt", RpcTarget.Others, gridPos.x, gridPos.y);

        Tile enemyTile = GameManager.Instance.enemyGrid.Grid[gridPos.x, gridPos.y];
        GameManager.Instance.playerGun.FireAt(enemyTile.transform.position, enemyTile);
    }

    [PunRPC]
    public void RPC_FireCannonAt(int x, int y, PhotonMessageInfo info)
    {
        if (!photonView.IsMine) return;

        Vector2Int gridPos = new Vector2Int(x, y);
        Tile targetTile = GameManager.Instance.playerGrid.Grid[gridPos.x, gridPos.y];

        if (targetTile != null)
        {
            GameManager.Instance.enemyGun.FireAt(targetTile.transform.position, targetTile);
        }
    }
}