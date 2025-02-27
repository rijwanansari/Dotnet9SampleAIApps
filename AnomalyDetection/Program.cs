// See https://aka.ms/new-console-template for more information
using AnomalyDetection.Models;
using AnomalyDetection.Services;

//Console.WriteLine("Hello, World!");

//create a dummy log data
var logs = new List<LogData>(){
    new() { Timestamp = DateTime.Now.AddHours(-5), ErrorCount = 2 },
    new() { Timestamp = DateTime.Now.AddHours(-4), ErrorCount = 3 },
    new() { Timestamp = DateTime.Now.AddHours(-3), ErrorCount = 2 },
    new() { Timestamp = DateTime.Now.AddHours(-2), ErrorCount = 50 },
    new() { Timestamp = DateTime.Now.AddHours(-1), ErrorCount = 4 },
    new() { Timestamp = DateTime.Now.AddHours(-6), ErrorCount = 2 }
};

//create an instance of the AnomalyDetectionTrainer
var trainer = new AnomalyDetectionTrainer();

//detect anomalies
var results = trainer.DetectAnomalies(logs);

//print the results
foreach (var result in results)
{
    Console.WriteLine($"Timestamp: {result.Timestamp}, ErrorCount: {result.ErrorCount}, IsAnomaly: {result.IsAnomaly}, ConfidenceScore: {result.ConfidenceScore}");
}

