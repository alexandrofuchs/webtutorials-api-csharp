using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using WebTutorialsApp.Api;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Net.Http.Headers;

namespace IntegrationTests.API
{
    public class UserAPITests
    {
        #region Instanciar server;
        private readonly HttpClient _client;
        private TestServer server;

        public UserAPITests()
        {
            server = new TestServer(new WebHostBuilder()
            .UseEnvironment("Development")
            .UseStartup<Startup>());            
        }
        #endregion Instanciar server;

        //[Theory]
        //[InlineData("GET", 1)]
        //public async Task GetUser(string method, int? id = null)
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Post, $"/user/{id}");

        //    request.Content = new StringContent(JsonConvert.SerializeObject(new Dictionary<string, string>
        //{
        //   {"Name", "MyLeague"}
        //}), Encoding.Default, "application/json");

        //    // PAREI AQUI ARRUMANDO O JSON PRA REQUISIÇÃO
        //    var response = await _client. .SendAsync(request);

        //    response.EnsureSuccessStatusCode();
        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        //}

        //[Theory]
        //[InlineData("POST", )]
        //public async Task RegisterUser(string method, int? id = null)
        //{

        //    var request = new HttpRequestMessage(new HttpMethod(method), $"/user/{id}");

        //    var response = await _client.SendAsync(request);

        //    response.EnsureSuccessStatusCode();
        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        //}

        [Fact]
        public async Task TestRegisterUser()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/user");

            request.Content = new StringContent(JsonConvert.SerializeObject(new Dictionary<string, string>
        {
            {"firstName", "Teste"},
            {"lastName", "Teste"},
            {"email","teste@mail.com"},
            {"password","abc12345678"}
        }), Encoding.Default, "application/json");

            var client = server.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.SendAsync(request);

            System.Console.WriteLine(response.Content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);


            
           

            
        }



    }
}
