// using Xunit;
// using Bunit;
// using Microsoft.Extensions.DependencyInjection;
// using Client.Pages;
// using Client.Services;

// public class LiveMatchTests : TestContext
// {
//     [Fact]
//     public void SubscribeButton_ShowsEvents()
//     {
//         Services.AddSingleton(new PatternApiClient(new System.Net.Http.HttpClient(new FakeHandler()), new RequestLogService()));
//         var cut = RenderComponent<LiveMatch>();
//         cut.Find("button.btn-success").Click();
//         Assert.Contains("Subscribed!", cut.Markup);
//     }
//     class FakeHandler : System.Net.Http.HttpMessageHandler
//     {
//         protected override Task<System.Net.Http.HttpResponseMessage> SendAsync(System.Net.Http.HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
//             => Task.FromResult(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new System.Net.Http.StringContent("Goal!") });
//     }
// }
