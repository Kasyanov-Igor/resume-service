using System.Text.Json;
using Application.DTO;
using Application.IService;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace Infrastructure.Services
{
    public class ResumeService : IResumeService
    {
        private readonly Kernel _kernel;

        public ResumeService()
        {
            var builder = Kernel.CreateBuilder();

            builder.AddOpenAIChatCompletion(
                modelId: "llama3",
                apiKey: "not-needed", 
                endpoint: new Uri("http://host.docker.internal:11434/v1") 
            );

            _kernel = builder.Build();
        }

        public async Task<ResumeDTO> ResumeGeneration(VacancyDTO vacancy)
        {
            try
            {
                var chatService = _kernel.GetRequiredService<IChatCompletionService>();

                // Ваш промпт с небольшой оптимизацией под локальные модели (они любят строгие инструкции)
                string prompt = $@"Generate resume in Russian for: {vacancy.Title}. 
                Vacancy: {vacancy.Description}.
                Return ONLY valid JSON format without any markdown wrappers or comments.
                Required structure:
                {{ ""Title"": ""..."", ""Bio"": ""..."", ""Description"": ""..."", ""ProgressWork"": ""..."" }}";

                var result = await chatService.GetChatMessageContentAsync(prompt);
                var responseText = result?.Content;

                if (string.IsNullOrWhiteSpace(responseText))
                    throw new Exception("AI returned empty content");

                // Очистка от Markdown разметки ```json ... ``` (Ваш код)
                var cleanJson = responseText;
                if (cleanJson.Contains("```"))
                {
                    cleanJson = cleanJson.Split("```").FirstOrDefault(s => s.Trim().StartsWith("{") || s.Trim().StartsWith("json")) ?? cleanJson;
                    cleanJson = cleanJson.Replace("json", "").Trim();
                }

                return JsonSerializer.Deserialize<ResumeDTO>(cleanJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SK Error: {ex.Message}");
                throw;
            }
        }
    }
}
