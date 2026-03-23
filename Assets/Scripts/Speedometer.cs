using System.Collections.Generic;

using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour
{
    [SerializeField]
    private Transform target = null;
    private Vector3 previousPosition = Vector3.zero;

    [SerializeField]
    private float timeframe = 0.75f;
    private List<FrameData> buffer = new();

    [SerializeField]
    private TextMeshPro label = null;

    private void Start()
    {
        previousPosition = target.position;
    }

    private void LateUpdate()
    {
        // Compute distance.
        float distance = (previousPosition - target.position).magnitude;

        // Store it inside our buffer.
        buffer.Insert(0, new FrameData(Time.deltaTime, distance));

        // Store the position for next frame.
        previousPosition = target.position;

        // Compute our average speed over timeframe.
        float bufferTime = 0.0f;
        float bufferDistance = 0.0f;
        int bufferFrameCount = 0;
        for (int index = 0; index < buffer.Count; index++)
        {
            if (bufferTime > timeframe)
            {
                // Remove the end of the buffer (out of our timeframe).
                int lastIndex = buffer.Count - 1;
                if (index < lastIndex)
                {
                    buffer.RemoveRange(index, lastIndex - index);
                }

                break;
            }

            FrameData frameData = buffer[index];
            bufferTime += frameData.Time;
            bufferDistance += frameData.Distance;
            bufferFrameCount++;
        }

        int kmPerHour = 0;
        if (bufferFrameCount > 0)
        {
            // https://www.checkyourmath.com/convert/speed/per_second_hour/m_per_second_km_per_hour.php
            kmPerHour = Mathf.FloorToInt(bufferDistance / bufferTime * 3.6f);
        }

        // Update label.
        label.SetText($"{kmPerHour} km/h");
    }

    private struct FrameData
    {
        public readonly float Time;

        public readonly float Distance;

        public FrameData(float time, float distance)
        {
            Time = time;
            Distance = distance;
        }
    }
}
