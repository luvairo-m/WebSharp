using System.Net;
using AchievementServiceConnectionLib.Contracts;
using AchievementServiceConnectionLib.Models;
using HttpLogic.Contracts;
using HttpLogic.Models;
using Polly;

namespace AchievementServiceConnectionLib.Services;

internal class AchievementConnectionService : IAchievementConnectionService
{
    private const string achievementEndpoint = "http://localhost:5004/api/v1/users";

    private readonly IAsyncPolicy<HttpResponseMessage> defaultPolicy =
        Policy<HttpResponseMessage>
            .HandleResult(message => message.StatusCode == HttpStatusCode.InternalServerError)
            .WaitAndRetryAsync(3, step => TimeSpan.FromSeconds(Math.Pow(2, step)));

    private readonly IHttpRequestService requestService;

    public AchievementConnectionService(IHttpRequestService requestService)
    {
        this.requestService = requestService;
    }

    public async Task<IEnumerable<AchievementDto>> GetUserAchievementsAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var requestMessage = new HttpRequestData
        {
            Method = HttpMethod.Get,
            Uri = new Uri($"{achievementEndpoint}/{userId}/achievements")
        };

        var responseMessage = await requestService
            .SendRequestAsync<List<AchievementDto>>(
                requestMessage,
                resiliencePolicy: defaultPolicy,
                cancellationToken: cancellationToken);

        return !responseMessage.IsSuccessStatusCode ? new List<AchievementDto>() : responseMessage.Body!;
    }
}