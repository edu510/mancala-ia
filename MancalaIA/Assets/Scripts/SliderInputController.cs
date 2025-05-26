using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Controla um Slider, mostra seu valor num InputField não interativo,
/// armazena o valor selecionado em uma variável estática ao clicar no botão,
/// e troca de cena.
/// </summary>
public class SliderInputController : MonoBehaviour
{
    [Header("Referências UI")]
    [SerializeField] private Slider valueSlider;
    [SerializeField] private TMP_InputField valueInputField;
    [SerializeField] private Button submitButton;

    [Header("Configurações")]
    [Tooltip("Nome da cena para trocar ao clicar no botão")]
    [SerializeField] private string sceneToLoad;

    /// <summary>
    /// Armazena o valor selecionado (1 a 15) após o botão ser pressionado.
    /// </summary>
    public static int SelectedValue = 1;

    private void Awake()
    {
        // Garante que o InputField seja somente leitura
        if (valueInputField != null)
            valueInputField.interactable = false;

        // Configura o Slider para usar números inteiros e faixa 1-15
        if (valueSlider != null)
        {
            valueSlider.wholeNumbers = true;
            valueSlider.minValue = 1;
            valueSlider.maxValue = 15;
            // Atualiza o InputField com o valor inicial
            UpdateInputField(valueSlider.value);
        }

        // Registra callbacks
        if (valueSlider != null)
            valueSlider.onValueChanged.AddListener(UpdateInputField);
        if (submitButton != null)
            submitButton.onClick.AddListener(OnSubmitClicked);
    }

    /// <summary>
    /// Atualiza o texto do InputField conforme o Slider muda.
    /// </summary>
    /// <param name="val">Novo valor do Slider</param>
    private void UpdateInputField(float val)
    {
        int intVal = Mathf.RoundToInt(val);
        if (valueInputField != null)
            valueInputField.text = intVal.ToString();
    }

    /// <summary>
    /// Ao clicar no botão: armazena o valor do Slider e troca de cena.
    /// </summary>
    private void OnSubmitClicked()
    {
        if (valueSlider != null)
            SelectedValue = Mathf.RoundToInt(valueSlider.value);

        Debug.Log(SelectedValue);
        
        if (!string.IsNullOrEmpty(sceneToLoad))
            SceneManager.LoadScene(sceneToLoad);
        else
            Debug.LogWarning("Cena não definida em SliderInputController.");
    }
}