using System;

namespace dotnet9SentimentApi.Models;

public record SentimentData
{
    public string Text { get; set; } = string.Empty;
}
