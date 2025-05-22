using System;

public class BoardState
{
    public int[] pits = new int[14]; // 0–5: P1, 6: P1 store, 7–12: P2, 13: P2 store
    public bool isPlayer1Turn;

    public BoardState()
    {
        for (int i = 0; i < 14; i++)
        {
            if (i != 6 && i != 13)
                pits[i] = 4; // default 4 stones per pit
        }
        isPlayer1Turn = true;
    }

    public BoardState Clone()
    {
        return new BoardState
        {
            pits = (int[])this.pits.Clone(),
            isPlayer1Turn = this.isPlayer1Turn
        };
    }

    public bool IsGameOver()
    {
        bool p1Empty = true, p2Empty = true;
        for (int i = 0; i < 6; i++) if (pits[i] != 0) p1Empty = false;
        for (int i = 7; i < 13; i++) if (pits[i] != 0) p2Empty = false;
        return p1Empty || p2Empty;
    }
}
