using System;
using UnityEngine;

public class ColorCycle
{
    private Color[] colors;
    private int currentIndex = 0;

    public ColorCycle(Color[] colors)
    {
        this.colors = colors;
    }

    public Color NextColor(Color currentColor)
    {
        int colorIndex = Array.IndexOf(colors, currentColor);
        currentIndex = (colorIndex + 1) % colors.Length;
        return colors[currentIndex];
    }
}
