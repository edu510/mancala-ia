using System.Collections.Generic;
using UnityEngine;

public static class MinimaxAI
{
    private static int Evaluate(BoardState state)
    {
        return state.pits[13] - state.pits[6];
    }
    
    public static int GetBestMove(BoardState state, int depth)
    {
        int bestValue = int.MinValue;
        int bestMove = -1;

        foreach (int move in GetValidMoves(state))
        {
            BoardState next = SimulateMove(state, move);
            int value = Minimax(next, depth - 1, int.MinValue, int.MaxValue, false);
            if (value > bestValue)
            {
                bestValue = value;
                bestMove = move;
            }
        }

        return bestMove;
    }

    private static int Minimax(BoardState state, int depth, int alpha, int beta, bool maximizingPlayer)
    {
        if (depth == 0 || state.IsGameOver())
            return Evaluate(state);

        int maxEval = int.MinValue;
        int minEval = int.MaxValue;
        if (maximizingPlayer)
        {
            foreach (int move in GetValidMoves(state))
            {
                BoardState newState = SimulateMove(state, move);
                if (newState.isPlayer1Turn != state.isPlayer1Turn)
                {
                    int eval = Minimax(newState, depth - 1, alpha, beta, false);
                    maxEval = Mathf.Max(maxEval, eval);
                    alpha = Mathf.Max(alpha, eval);
                    if (beta <= alpha) break;
                }
                else
                {
                    int eval = Minimax(newState, depth - 1, alpha, beta, true);
                    minEval = Mathf.Min(minEval, eval);
                    beta = Mathf.Min(beta, eval);
                    if (beta <= alpha) break;
                }
                
            }
            return maxEval;
        }
        else
        {
            foreach (int move in GetValidMoves(state))
            {
                BoardState newState = SimulateMove(state, move);
                if (newState.isPlayer1Turn != state.isPlayer1Turn)
                {
                    int eval = Minimax(newState, depth - 1, alpha, beta, true);
                    minEval = Mathf.Min(minEval, eval);
                    beta = Mathf.Min(beta, eval);
                    if (beta <= alpha) break;
                }
                else
                {
                    int eval = Minimax(newState, depth - 1, alpha, beta, false);
                    maxEval = Mathf.Max(maxEval, eval);
                    alpha = Mathf.Max(alpha, eval);
                    if (beta <= alpha) break;
                }
                
            }
            return minEval;
        }
    }

    private static List<int> GetValidMoves(BoardState state)
    {
        List<int> valid = new List<int>();
        int start = state.isPlayer1Turn ? 0 : 7;
        int end = state.isPlayer1Turn ? 6 : 13;
        for (int i = start; i < end; i++)
            if (state.pits[i] > 0) valid.Add(i);
        return valid;
    }

    public static BoardState SimulateMove(BoardState state, int pit, bool highlight=false)
    {
        var gameController = Object.FindFirstObjectByType<GameController>();
        if(highlight) gameController.pitTexts[pit].GetComponentInParent<Highlighter>().Highlight();
        BoardState newState = state.Clone();
        int stones = newState.pits[pit];
        newState.pits[pit] = 0;
        int index = pit;

        int count = 0;
        
        while (stones > 0)
        {
            count++;
            index = (index + 1) % 14;
            if ((state.isPlayer1Turn && index == 13) || (!state.isPlayer1Turn && index == 6))
                continue;
            newState.pits[index]++;
            if(highlight) gameController.pitTexts[index].GetComponentInParent<Highlighter>().Highlight(count);
            stones--;
        }

        // Capture
        if (newState.pits[index] == 1 &&
            ((state.isPlayer1Turn && index >= 0 && index < 6) ||
            (!state.isPlayer1Turn && index >= 7 && index < 13)))
        {
            int opposite = 12 - index;
            int store = state.isPlayer1Turn ? 6 : 13;
            newState.pits[store] += newState.pits[opposite] + 1;
            newState.pits[opposite] = 0;
            newState.pits[index] = 0;
            if(highlight) Debug.Log("CAPTURE!");
        }

        // Extra turn
        bool extraTurn = (state.isPlayer1Turn && index == 6) || (!state.isPlayer1Turn && index == 13);
        if (!extraTurn) newState.isPlayer1Turn = !newState.isPlayer1Turn;

        if (newState.IsGameOver())
        {
            if(highlight) Debug.Log("JOGO ACABOU");
            
            for (int i = 0; i <= 5; i++)
            {
                newState.pits[6] += newState.pits[i];
                newState.pits[i] = 0;
            }
            for (int i = 7; i <= 12; i++)
            {
                newState.pits[13] += newState.pits[i];
                newState.pits[i] = 0;
            }
        }

        return newState;
    }
}
