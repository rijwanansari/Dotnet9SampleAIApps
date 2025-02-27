using System;

namespace AnomalyDetection.Models;

public record AnomalyResult
{
    public DateTime Timestamp { get; set; }
    public float ErrorCount { get; set; }
    public bool IsAnomaly { get; set; }
    public double ConfidenceScore { get; set; }

}
