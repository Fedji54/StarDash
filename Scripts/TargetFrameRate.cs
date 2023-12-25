using UnityEngine;

public static class TargetFrameRate
{
    public static void SetLimit(int value)
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = value;
    }
}