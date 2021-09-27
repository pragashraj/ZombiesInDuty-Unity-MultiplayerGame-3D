using UnityEngine;

public class StarterManager : MonoBehaviour
{
    [SerializeField] private GameObject loadingUI;
    [SerializeField] private GameObject starterUI;
    [SerializeField] private GameObject newGameUI;
    [SerializeField] private GameObject startGameUI;

    private bool createOrJoin;

    public bool CreateOrJoin { get => createOrJoin; set => createOrJoin = value; }

    void Start()
    {
        Cursor.visible = true;
        starterUI.SetActive(false);
        newGameUI.SetActive(false);
        startGameUI.SetActive(false);
        PlayAudio("Theme");
    }

    private void PlayAudio(string name)
    {
        FindObjectOfType<AudioManager>().Play(name);
    }

    public void HandleLoadComplete()
    {
        loadingUI.SetActive(false);
        starterUI.SetActive(true);
    }

    public void HandleNewGame()
    {
        PlayAudio("Click");
        starterUI.SetActive(false);
        newGameUI.SetActive(true);
        startGameUI.SetActive(false);
        createOrJoin = true;
    }

    public void HandleStartGame()
    {
        PlayAudio("Click");
        starterUI.SetActive(false);
        newGameUI.SetActive(false);
        startGameUI.SetActive(true);
        createOrJoin = true;
    }

    public void HandleQuit()
    {
        PlayAudio("Click");
        Application.Quit();
    }

    public void GoBack()
    {
        starterUI.SetActive(true);
        newGameUI.SetActive(false);
        startGameUI.SetActive(false);
        createOrJoin = false;
    }

    public void StoreUserName(string name)
    {
        PlayerPrefs.SetString("UserName", name);
    }
}
