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
using Newtonsoft.Json.Linq;

namespace IntegrationTests.API
{
    public class UserAPITests
    {
        #region Instanciar server;
        private TestServer server;

        public UserAPITests()
        {
            server = new TestServer(new WebHostBuilder()
            .UseEnvironment("Development")
            .UseStartup<Startup>());            
        }
        #endregion Instanciar server;

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

        [Fact]
        public async Task TestAuthUser()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "/authenticate");

            request.Content = new StringContent(JsonConvert.SerializeObject(new Dictionary<string, string>
        {
            {"email","teste@mail.com"},
            {"password","abc12345678"}
        }), Encoding.Default, "application/json");

            var client = server.CreateClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.SendAsync(request);

            //var body = await response.Content.ReadAsStringAsync();
            //var res = JObject.Parse(body);            

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            
        }
    }
}
