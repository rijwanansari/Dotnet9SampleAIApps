using System;
using Microsoft.ML.Data;

namespace AnomalyDetection.Models;

public class AnomalyPrediction
{
    [VectorType(3)]
    public double[] Prediction { get; set; } =  [];

}
