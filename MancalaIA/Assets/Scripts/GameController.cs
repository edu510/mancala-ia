using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Button[] pitButtons;
    public TMP_Text[] pitTexts;
    public TMP_Text turnText;

    [SerializeField] private float highlightTime = .4f;
    
    private BoardState _state;

    void Start()
    {
        _state = new BoardState();

        UpdateUI();
    }

    public void OnPitClick(int pit)
    {
        if (!_state.isPlayer1Turn || _state.pits[pit] == 0) return;

        int start = _state.isPlayer1Turn ? 0 : 7;
        int end = _state.isPlayer1Turn ? 6 : 13;
        
        if (!(pit >= start && pit <= end)) return;

        int oldPitCount = _state.pits[pit];
        _state = MinimaxAI.SimulateMove(_state, pit, true, highlightTime);
        UpdateUI();

        if (_state.IsGameOver())
        {
            Debug.Log("Player PAROU");
            ShowWinner();
        }
        
        if (!_state.isPlayer1Turn && !_state.IsGameOver())
            Invoke(nameof(AIMove), highlightTime * (oldPitCount + 3));
    }

    void AIMove()
    {
        int move = MinimaxAI.GetBestMove(_state, SliderInputController.SelectedValue);
        if (move == -1) return;

        int oldPitCount = _state.pits[move];
        _state = MinimaxAI.SimulateMove(_state, move, true, highlightTime);
        UpdateUI();

        if (_state.IsGameOver())
        {
            Debug.Log("IA PAROU");
            ShowWinner();
        }
        
        if (!_state.isPlayer1Turn && !_state.IsGameOver())
            Invoke(nameof(AIMove), highlightTime * (oldPitCount + 3));
    }

    void UpdateUI()
    {
        for (int i = 0; i < 14; i++)
        {
            pitTexts[i].text = _state.pits[i].ToString();
        }
        
        turnText.text = _state.isPlayer1Turn ? "Your Turn" : "AI Thinking...";
    }

    void ShowWinner()
    {
        if (_state.pits[6] > _state.pits[13])
        {
            turnText.text = "Você ganhou!";
        }
        else if (_state.pits[6] == _state.pits[13])
        {
            turnText.text = "Empatou!";
        }
        else
        {
            turnText.text = "IA ganhou!";
        }
    }
}
