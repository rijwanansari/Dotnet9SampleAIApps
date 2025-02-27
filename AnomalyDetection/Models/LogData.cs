using System;

namespace AnomalyDetection.Models;

public record LogData
{
    public DateTime Timestamp { get; set; }
    public float ErrorCount { get; set; }

}
