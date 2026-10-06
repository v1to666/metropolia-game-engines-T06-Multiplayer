using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkConnection : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        JoinOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        Vector3 spawnPosition = new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f));

        PhotonNetwork.Instantiate("Player", spawnPosition, Quaternion.identity);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
    }

    private void JoinOrCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 20;

        PhotonNetwork.JoinOrCreateRoom("test", roomOptions, TypedLobby.Default);
    }
}
