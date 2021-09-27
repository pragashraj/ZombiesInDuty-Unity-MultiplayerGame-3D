using Photon.Pun;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    private StarterManager starterManager;

    void Start()
    {
        starterManager = FindObjectOfType<StarterManager>();
        PhotonNetwork.ConnectUsingSettings(); 
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();   
    }

    public override void OnJoinedLobby()
    {
        starterManager.HandleLoadComplete();
    }
}
