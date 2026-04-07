using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using UnityEditor.Overlays;
using UnityEngine;

public static class Timer
{
    private static readonly Stopwatch stopwatch = new();
    private static List<long> steps = new();

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
        string savePath = Application.dataPath + "/save.txt";

        

        if (File.Exists(savePath))
        {

            string[] lines = File.ReadAllLines(savePath);

            if (steps[steps.Count - 1] < long.Parse(lines[lines.Length - 1]) )
            {
                FileStream streamed = new FileStream(savePath, FileMode.Create);
                StreamWriter saveFile = new StreamWriter(streamed);


                foreach (long step in steps)
                {
                    saveFile.WriteLine(step);
                }


                saveFile.Close();
                streamed.Close();

                UnityEngine.Debug.Log("saved!");
            }

        }
            

        



    }

    public static void Load()
    {
        // TODO : load our time steps from a file (if we have any)
        // and store them inside our steps variable (line 7 of this script)
        // to show them to the player before starting a race.

        string savePath = Application.dataPath + "/save.txt";
        if (File.Exists(savePath))
        {
            steps.Clear();

            FileStream streamed = new FileStream(savePath, FileMode.Open);
            StreamReader saveFile = new StreamReader(streamed);

            while (!saveFile.EndOfStream)
            {
                string line = saveFile.ReadLine();

                if (long.TryParse(line, out long value))
                {
                    steps.Add(value);
                }
            }

            saveFile.Close();
            streamed.Close();

        }

    }
}
