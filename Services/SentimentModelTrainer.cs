using System;
using dotnet9SentimentApi.Models;
using Microsoft.ML;

namespace dotnet9SentimentApi.Services;

public class SentimentModelTrainer
{
    private readonly MLContext _mlContext;
    private ITransformer _model;

    public SentimentModelTrainer()
    {
        _mlContext = new MLContext();
        TrainModel();

    }

    private void TrainModel()
    {
        // Simulated training data (in practice, load from a file or database)
        var data = BuildTrainingData();

        var dataView = _mlContext.Data.LoadFromEnumerable(data);
        // Enhanced pipeline with text preprocessing
        var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", new Microsoft.ML.Transforms.Text.TextFeaturizingEstimator.Options
        {
            WordFeatureExtractor = new Microsoft.ML.Transforms.Text.WordBagEstimator.Options(),
            StopWordsRemoverOptions = new Microsoft.ML.Transforms.Text.StopWordsRemovingEstimator.Options()
        }, nameof(SentimentTrainingData.Text))
            .Append(_mlContext.BinaryClassification.Trainers.LbfgsLogisticRegression(labelColumnName: nameof(SentimentTrainingData.Sentiment)));

        // Train the model
        _model = pipeline.Fit(dataView);

        // Optional: Save the model for reuse
        _mlContext.Model.Save(_model, dataView.Schema, "sentiment_model.zip");
    }

    public SentimentPrediction Predict(string text)
    {
        var predictionEngine = _mlContext.Model.CreatePredictionEngine<SentimentData, SentimentPrediction>(_model);
        return predictionEngine.Predict(new SentimentData { Text = text });
    }

    //build training data
    private List<SentimentTrainingData> BuildTrainingData()
    {
        return new List<SentimentTrainingData>
        {
            new() { Text = "I love this product!", Sentiment = true },
            new() { Text = "This is terrible.", Sentiment = false },
            new() { Text = "The weather is nice!", Sentiment = true },
            new() { Text = "Horrible service, never again.", Sentiment = false },
            new() { Text = "Absolutely fantastic experience!", Sentiment = true },
            new() { Text = "It’s a complete disaster.", Sentiment = false },
            new() { Text = "I’m so happy with this!", Sentiment = true },
            new() { Text = "Disappointing and awful.", Sentiment = false },
            new() { Text = "I’m so impressed!", Sentiment = true },
            new() { Text = "I’m never coming back.", Sentiment = false },
            new() { Text = "I’m so excited!", Sentiment = true },
            new() { Text = "I’m so disappointed.", Sentiment = false },
            new() { Text = "I’m so pleased with this!", Sentiment = true },
            new() { Text = "I’m so upset.", Sentiment = false },
            new() { Text = "I’m so satisfied with this!", Sentiment = true },
            new() { Text = "I’m so angry.", Sentiment = false },
            new() { Text = "I’m so grateful for this!", Sentiment = true },
            new() { Text = "I’m so annoyed.", Sentiment = false },
            new() { Text = "I’m so thankful for this!", Sentiment = true }
        };
    }
    

}
public record SentimentTrainingData
{
    public string Text { get; set; } = string.Empty;
    public bool Sentiment { get; set; }
}
