using UnityEngine;

public static class Utilities
{
    public static bool HasReachedTime(float time, ref float timer)
    {
        timer += Time.deltaTime;
        if (timer >= time)
        {
            timer = 0f;
            return true;
        }
        return false;
    }
}