using AI.OpenAI;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace OriginTest.Controllers;

[ApiController]
[Route("api/v1/settings")]
public class SettingsController
{
  private readonly bool _isOpenAIEnabled;

  public SettingsController(IOpenAIClient openAIClient)
  {
    _isOpenAIEnabled = openAIClient.IsEnabled;
  }

  [HttpGet]
  public Settings GetSettings()
  {
    return new Settings {OpenAIEnabled = _isOpenAIEnabled};
  }
}