
using API.Models;
using API.ViewModel;
using Client.Base.Urls;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Client.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
        }

        public async Task<List<EmployeeVM>> GetRegister() {
            List<EmployeeVM> entities = new List<EmployeeVM>();

            using (var response = await httpClient.GetAsync(request+"getregister")) {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<EmployeeVM>>(apiResponse);
            }
            return entities;
        }
        public HttpStatusCode Register(RegisterVM registerVM)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(registerVM), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + request+"Register", content).Result;
            return result.StatusCode;
        }
    }
}