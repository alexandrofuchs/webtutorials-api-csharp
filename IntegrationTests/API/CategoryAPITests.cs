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
    public class CategoryAPITests
    {
        #region INSTANCIAR SERVER;
        private readonly HttpClient _client;
        private TestServer server;

        public CategoryAPITests()
        {
            server = new TestServer(new WebHostBuilder()
            .UseEnvironment("Development")
            .UseStartup<Startup>());
        }
        #endregion INSTANCIAR SERVER;


        [Theory]
        [InlineData("GET")]
        public async Task TestGetCategoriesMethod(string method)
        {
            var response = await server.CreateRequest("/categories").SendAsync(method);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("GET", 1)]
        public async Task TestGetCategoryMethod(string method, int? id = null)
        {
            var response = await server.CreateRequest($"/category/{id}").SendAsync(method);
           
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

    }        
}




