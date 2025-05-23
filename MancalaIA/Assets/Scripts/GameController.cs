using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Button[] pitButtons;
    public TMP_Text[] pitTexts;
    public TMP_Text turnText;

    [SerializeField] private float thinkingDelay = 2;
    [SerializeField] private int depth = 6;
    
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
        
        _state = MinimaxAI.SimulateMove(_state, pit, true);
        UpdateUI();

        if (_state.IsGameOver())
        {
            Debug.Log("Player PAROU");
        }
        
        if (!_state.isPlayer1Turn && !_state.IsGameOver())
            Invoke(nameof(AIMove), thinkingDelay);
    }

    void AIMove()
    {
        int move = MinimaxAI.GetBestMove(_state, depth);
        if (move == -1) return;

        _state = MinimaxAI.SimulateMove(_state, move, true);
        UpdateUI();

        if (_state.IsGameOver())
        {
            Debug.Log("IA PAROU");
        }
        
        if (!_state.isPlayer1Turn && !_state.IsGameOver())
            Invoke(nameof(AIMove), thinkingDelay);
    }

    void UpdateUI()
    {
        for (int i = 0; i < 14; i++)
        {
            pitTexts[i].text = _state.pits[i].ToString();
        }

        turnText.text = _state.isPlayer1Turn ? "Your Turn" : "AI Thinking...";
    }
}
