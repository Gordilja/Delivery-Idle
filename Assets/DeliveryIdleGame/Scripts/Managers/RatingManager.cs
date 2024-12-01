using UnityEngine;
using System;

public static class RatingManager
{
    // Default parameters (can be modified in calls)
    public static float MaxRating = 3f;          // Maximum rating
    public static float TimeThreshold = 10f;     // Base time threshold in seconds
    public static float DeductionInterval = 5f;  // Time interval for deductions
    public static float DeductionRate = 0.5f;    // Rating deduction per interval

    // Save the current time and return it
    public static DateTime SaveCurrentTime()
    {
        DateTime startTime = DateTime.Now;
        Debug.Log("Start time saved: " + startTime);
        return startTime;
    }

    // Calculate the rating based on elapsed time
    public static float CalculateRating(DateTime startTime)
    {
        if (startTime == default(DateTime))
        {
            Debug.LogWarning("Start time has not been set!");
            return 0f;
        }

        TimeSpan elapsed = DateTime.Now - startTime;
        float elapsedSeconds = (float)elapsed.TotalSeconds;

        // Calculate rating
        if (elapsedSeconds <= TimeThreshold)
        {
            return MaxRating;
        }

        float overThresholdTime = elapsedSeconds - TimeThreshold;
        float deductions = Mathf.Floor(overThresholdTime / DeductionInterval) * DeductionRate;
        float rating = Mathf.Max(0f, MaxRating - deductions);

        return rating;
    }

    // Update the cumulative rating based on a new rating
    public static float UpdateRating(float newRating, float currentRating, int ratingCount)
    {
        if (ratingCount == 0)
        {
            // First rating directly sets the current rating
            currentRating = newRating;
        }
        else
        {
            // Weighted average for cumulative rating
            currentRating = ((currentRating * ratingCount) + newRating) / (ratingCount + 1);
        }

        Debug.Log($"Updated Rating: {currentRating} (After {ratingCount} updates)");
        return currentRating;
    }
}