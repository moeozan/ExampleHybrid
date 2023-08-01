using System;

public static class GameUtilities
{
    public static float FloatHandler(float value)
    {
        value = (float)Math.Round(value, 2);
        return value;
    }
}
