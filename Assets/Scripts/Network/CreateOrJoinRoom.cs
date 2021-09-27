using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CreateOrJoinRoom : MonoBehaviourPunCallbacks
{
    [Header("create")]
    [SerializeField] private InputField createId;
    [SerializeField] private InputField creatorUserName;

    [Header("join")]
    [SerializeField] private InputField joinId;
    [SerializeField] private InputField joinerUserName;

    [Header("join")]
    [SerializeField] private GameObject loader;


    private AudioManager audioManager;
    private StarterManager starterManager;

    private bool btnPressed;
    private bool connected;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        starterManager = FindObjectOfType<StarterManager>();
    }

    private void Update()
    {
        if (starterManager.CreateOrJoin && btnPressed)
        {
            loader.SetActive(true);
        }
    }

    public void CreateRoom()
    {
        btnPressed = true;
        audioManager.Play("Click");
        starterManager.StoreUserName(creatorUserName.text);
        connected = PhotonNetwork.CreateRoom(createId.text);
    }

    public void JoinRoom()
    {
        btnPressed = true;
        audioManager.Play("Click");
        starterManager.StoreUserName(joinerUserName.text);
        connected = PhotonNetwork.JoinRoom(joinId.text);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
