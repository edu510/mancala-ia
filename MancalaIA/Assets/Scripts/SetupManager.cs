using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SetupManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Dropdown player1Dropdown;
    [SerializeField] private TMP_Dropdown player2Dropdown;
    [SerializeField] private Button startButton;
    [SerializeField] private string gameSceneName = "GameScene";

    void Awake()
    {
        if (startButton == null)
        {
            Debug.LogWarning("Start Button não atribuído no Inspector, tentando GetComponent<Button>()");
            startButton = GetComponent<Button>();
        }

        startButton.onClick.AddListener(OnStartClicked);
    }

    private void OnStartClicked()
    {
        // Define quem é Human ou AI
        SelectedGameConfig.Player1Kind = 
            player1Dropdown.value == 0 
                ? SelectedGameConfig.Kind.Human 
                : SelectedGameConfig.Kind.AI;

        SelectedGameConfig.Player2Kind = 
            player2Dropdown.value == 0 
                ? SelectedGameConfig.Kind.Human 
                : SelectedGameConfig.Kind.AI;

        // Carrega a cena do jogo
        SceneManager.LoadScene(gameSceneName);
    }
}