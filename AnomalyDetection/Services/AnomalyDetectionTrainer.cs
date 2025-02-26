using System;
using AnomalyDetection.Models;
using Microsoft.ML;

namespace AnomalyDetection.Services;

public class AnomalyDetectionTrainer
{
    private readonly MLContext _mlContext;
    private ITransformer _model;

    public AnomalyDetectionTrainer()
    {
        _mlContext = new MLContext(seed: 0);
        TrainModel();
    }

    private void TrainModel()
    {
        // Simulated training data (in practice, load from a file or database)
        var data = GetTrainingData();

        var dataView = _mlContext.Data.LoadFromEnumerable(data);

        // Define anomaly detection pipeline
       var pipeline = _mlContext.Transforms.DetectIidSpike(
                outputColumnName: "Prediction",
                inputColumnName: nameof(LogData.ErrorCount),
                confidence: 95,
                pvalueHistoryLength: 5
            );

        // Train the model
        var model = pipeline.Fit(dataView);
        
    }

    public List<AnomalyResult> DetectAnomalies(List<LogData> logs)
        {
            var dataView = _mlContext.Data.LoadFromEnumerable(logs);
            var transformedData = _model.Transform(dataView);
            var predictions = _mlContext.Data.CreateEnumerable<AnomalyPrediction>(transformedData, reuseRowObject: false);

            return logs.Zip(predictions, (log, pred) => new AnomalyResult
            {
                Timestamp = log.Timestamp,
                ErrorCount = log.ErrorCount,
                IsAnomaly = pred.Prediction[0] == 1,
                ConfidenceScore = pred.Prediction[1]
            }).ToList();
        }

    //dummy training data
    private List<LogData> GetTrainingData()
    {
        return new List<LogData>(){
            new() { Timestamp = DateTime.Now.AddHours(-5), ErrorCount = 2 },
            new() { Timestamp = DateTime.Now.AddHours(-4), ErrorCount = 3 },
            new() { Timestamp = DateTime.Now.AddHours(-3), ErrorCount = 2 },
            new() { Timestamp = DateTime.Now.AddHours(-2), ErrorCount = 50 }, // Anomaly: Spike!
            new() { Timestamp = DateTime.Now.AddHours(-1), ErrorCount = 4 },
            new() { Timestamp = DateTime.Now.AddHours(-6), ErrorCount = 2 }
        };
    }
}
