// using Xunit;
// using System.Threading.Tasks;
// using Client.Services;
// using System.Net.Http;
// using System.Net;
// using System;

// public class PatternApiClientTests
// {
//     [Fact]
//     public async Task ConfigureTournamentAsync_ReturnsString()
//     {
//         var handler = new FakeHandler();
//         var http = new HttpClient(handler);
//         var log = new RequestLogService();
//         var api = new PatternApiClient(http, log);
//         handler.Response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Configured!") };
//         var result = await api.ConfigureTournamentAsync("Test");
//         Assert.Equal("Configured!", result);
//     }

//     class FakeHandler : HttpMessageHandler
//     {
//         public HttpResponseMessage? Response;
//         protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
//             => Task.FromResult(Response ?? new HttpResponseMessage(HttpStatusCode.OK));
//     }
// }
