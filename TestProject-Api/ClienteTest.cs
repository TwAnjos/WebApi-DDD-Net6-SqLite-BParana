using Entities.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace TestProject_Api
{
    [TestClass]
    public class ClienteTest
    {
        public List<Cliente> cliente { get; set; }
        public Task<HttpResponseMessage> TaskResponse { get; set; }

        [TestMethod]
        public void GetAllClientes()
        {
            string url = "https://localhost:5000/api/GetAllClientes";
            string query = "";
            GetData(url, query);

            Assert.IsTrue(cliente.Any());
        }

        private async void GetData(string url, string query)
        {
            using (var request = new HttpClient())
            {
                request.DefaultRequestHeaders.Clear();
                TaskResponse = request.GetAsync(url + query);
                TaskResponse.Wait();

                if (TaskResponse.Result.IsSuccessStatusCode)
                {
                    var result = await TaskResponse.Result.Content.ReadAsStringAsync();
                    cliente = JsonConvert.DeserializeObject<List<Cliente>>(result);
                }
            }
        }
    }
}