using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Button[] pitButtons;
    public TMP_Text[] pitTexts;
    public TMP_Text turnText;

    private BoardState state;

    void Start()
    {
        state = new BoardState();
        UpdateUI();
    }

    public void OnPitClick(int pit)
    {
        if (!state.isPlayer1Turn || state.pits[pit] == 0) return;

        state = MinimaxAI.SimulateMove(state, pit);
        UpdateUI();

        if (!state.isPlayer1Turn && !state.IsGameOver())
            Invoke(nameof(AIMove), 1f);
    }

    void AIMove()
    {
        int move = MinimaxAI.GetBestMove(state, 6);
        if (move == -1) return;

        state = MinimaxAI.SimulateMove(state, move);
        UpdateUI();

        if (!state.isPlayer1Turn && !state.IsGameOver())
            Invoke(nameof(AIMove), 1f);
    }

    void UpdateUI()
    {
        for (int i = 0; i < 14; i++)
        {
            pitTexts[i].text = state.pits[i].ToString();
        }

        turnText.text = state.isPlayer1Turn ? "Your Turn" : "AI Thinking...";
    }
}
