using System.Collections.Generic;
using System.Diagnostics;

public static class Timer
{
    private static readonly Stopwatch stopwatch = new();
    private static readonly List<long> steps = new();

    public static bool IsRunning
    {
        get => stopwatch.IsRunning;
    }

    public static double ElapsedSeconds
    {
        get => stopwatch.ElapsedMilliseconds * 0.001f;
    }

    public static int StepsCount
    {
        get => steps.Count;
    }

    public static double GetStepElapsedSeconds(int index)
    {
        return steps[index] * 0.001f;
    }

    /// <summary>
    /// Reset the timer and remove any steps.
    /// </summary>
    public static void Reset()
    {
        stopwatch.Reset();
        steps.Clear();
    }

    public static void Start()
    {
        stopwatch.Start();
    }

    public static void Stop()
    {
        stopwatch.Stop();
    }

    public static void Step()
    {
        steps.Add(stopwatch.ElapsedMilliseconds);
    }

    public static void Save()
    {
        // TODO : save our time steps (line 7 of this script) inside a file.
    }

    public static void Load()
    {
        // TODO : load our time steps from a file (if we have any)
        // and store them inside our steps variable (line 7 of this script)
        // to show them to the player before starting a race.
    }
}
