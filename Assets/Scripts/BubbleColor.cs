using UnityEngine;

[System.Serializable]
public enum BubbleColor
{
    Red,
    Green,
    Blue,
    Yellow
}

public static class BubbleColorExtensions
{
    // Helper method to map BubbleColor to Unity Color
    public static Color ToUnityColor(this BubbleColor bubbleColor)
    {
        return bubbleColor switch
        {
            BubbleColor.Red => Color.red,
            BubbleColor.Green => Color.green,
            BubbleColor.Blue => Color.blue,
            BubbleColor.Yellow => Color.yellow,
            _ => Color.white, // Default color if needed
        };
    }
}