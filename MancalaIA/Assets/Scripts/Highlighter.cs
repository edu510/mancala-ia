using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Highlighter : MonoBehaviour
{
    public Image image;
    public float highlightTime;
    public Color highlightedColor;
    private Color _oldColor;

    private void Start()
    {
        _oldColor = image.color;
    }

    public void Highlight(int index=0)
    {
        StartCoroutine(HighlightCoroutine(index));
    }
    
    private IEnumerator HighlightCoroutine(int index=0)
    {
        yield return new WaitForSeconds(highlightTime * index);
        image.color = highlightedColor;
        yield return new WaitForSeconds(highlightTime);
        image.color = _oldColor;
    }
}