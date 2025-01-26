using UnityEngine;

[System.Serializable]
public enum BubbleColor
{
    Red = 0,
    Blue = 1,
    Green = 2,
    Yellow = 3
}

public static class BubbleColorExtensions
{
    // Helper method to map BubbleColor to Unity Color
    public static Color ToUnityColor(this BubbleColor bubbleColor)
    {
        return bubbleColor switch
        {
            BubbleColor.Red => Color.red,
            BubbleColor.Blue => Color.blue,
            BubbleColor.Green => Color.green,
            BubbleColor.Yellow => Color.yellow,
            _ => Color.white, // Default color if needed
        };
    }
}