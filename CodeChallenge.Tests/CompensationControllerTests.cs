using System;
using System.Net;
using System.Net.Http;
using System.Text;

using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        [DataRow("b7839309-3348-463b-a7e3-5de1c168beb3", "1,000,000", "2021-01-01T00:00:00")]
        public void CreateCompensation_Returns_Created(string employeeId, string salary, string effectiveDate)
        {
            // Arrange
            var compensation = new Compensation
            {
                EmployeeId = employeeId,
                Salary = salary,
                EffectiveDate = DateTime.Parse(effectiveDate),
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompensation = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newCompensation.EmployeeId);
            Assert.AreEqual(compensation.Salary, newCompensation.Salary);
            Assert.AreEqual(compensation.EffectiveDate, newCompensation.EffectiveDate);
        }
        
        [TestMethod]
        [DataRow("16a596ae-edd3-4847-99fe-c4518e82c86f", "100,000", "2020-01-01T00:00:00")]
        [DataRow("03aa1462-ffa9-4978-901b-7c001562cf6f", "200,000", "2022-01-01T00:00:00")]
        public void GetCompensationByEmployeeId_Returns_Ok(string employeeId, string expectedSalary, string expectedEffectiveDate)
        {
            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var compensation = response.DeserializeContent<Compensation>();
            Assert.AreEqual(expectedSalary, compensation.Salary);
            Assert.AreEqual(DateTime.Parse(expectedEffectiveDate), compensation.EffectiveDate);
        }
        
        [TestMethod]
        [DataRow("c0c2293d-16bd-4603-8e08-638a9d18b22c")]
        public void GetCompensationByEmployeeId_Returns_NotFound(string employeeId)
        {
            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
