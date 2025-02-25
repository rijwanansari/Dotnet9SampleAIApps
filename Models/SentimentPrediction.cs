using System;

namespace dotnet9SentimentApi.Models;

public record SentimentPrediction
{
    public bool Prediction { get; set; } // True = Positive, False = Negative
    public float Probability { get; set; }
}
