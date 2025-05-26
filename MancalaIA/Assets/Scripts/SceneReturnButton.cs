using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Ao clicar no botão configurado, carrega a cena especificada no Inspector.
/// </summary>
public class SceneReturnButton : MonoBehaviour
{
    [Header("Referências UI")]
    [Tooltip("Botão que ao ser clicado retorna à cena desejada")]
    [SerializeField] private Button returnButton;

    [Header("Configurações")]
    [Tooltip("Nome da cena a ser carregada ao clicar no botão")]
    [SerializeField] private string sceneToLoad;

    private void Awake()
    {
        if (returnButton != null)
            returnButton.onClick.AddListener(OnReturnClicked);
        else
            Debug.LogWarning("ReturnButton não foi atribuído em SceneReturnButton.");
    }

    private void OnReturnClicked()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogWarning("Nenhuma cena definida para retorno em SceneReturnButton.");
        }
    }
}