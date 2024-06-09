using dotenv.net;
using Azure.AI.OpenAI;
using Azure;
namespace AppEase.Service
{
    public class GPTService
    {
        public string GetPromptResults(string prompt) 
        {
            DotEnv.Load();
            var key = Environment.GetEnvironmentVariable("KEY");
            var endpoint = Environment.GetEnvironmentVariable("ENDPOINT");
            var deployment = Environment.GetEnvironmentVariable("DEPLOYMENT");

            OpenAIClient aIClient = new(new Uri(endpoint), new Azure.AzureKeyCredential(key));
            ChatCompletionsOptions chatCompletionsOptions = new ChatCompletionsOptions
            {
                Messages ={
                        new ChatRequestSystemMessage("You are a helpful AI bot for generating cover letter for jobs"),
                        new ChatRequestUserMessage(prompt)
                }

            };
            chatCompletionsOptions.DeploymentName = deployment;

            ChatCompletions response = aIClient.GetChatCompletions(chatCompletionsOptions);

            return response.Choices[0].Message.Content.ToString();
          

        }
    }
}
