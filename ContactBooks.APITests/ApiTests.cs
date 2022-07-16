using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace ContactBooks.APITests
{
    public class ApiTests
    {
        private const string url = "https://contactbook.nakov.repl.co/api/contacts";
       // private const string url = "http://localhost:8080/api/contacts";
        private RestClient client;
        private RestRequest request;

        [SetUp]
        public void Setup()
        {
            this.client = new RestClient();
        }

        [Test]
        public void Test_GetAllContacts_CheckFirstClient()
        {
            this.request = new RestRequest(url);
                        
            var response = this.client.Execute(request, Method.Get);
            var contacts = JsonSerializer.Deserialize<List<Contact>>(response.Content);

           

        }

        [Test]
        public void Test_GetFirstContacts_WithCurrentFirstName()
        {
            this.request = new RestRequest(url + "/search/albert");

            var response = this.client.Execute(request, Method.Get);
            var contacts = JsonSerializer.Deserialize<List<Contact>>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(contacts.Count, Is.GreaterThan(0));
            Assert.That(contacts[0].firstName, Is.EqualTo("Albert"));
            Assert.That(contacts[0].lastName, Is.EqualTo("Einstein"));

        }
        [Test]
        public void Test_GetFirstContacts_WithInvalidName()
        {
            this.request = new RestRequest(url + "/search/invalidName");

            var response = this.client.Execute(request, Method.Get);
            var contacts = JsonSerializer.Deserialize<List<Contact>>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(contacts.Count, Is.EqualTo(0));            

        }
        [Test]
        public void Test_CreateNewContact_WithInvalidFirstName()
        {
            this.request = new RestRequest(url);
            var body = new
            {
                firstName = "",
                lastName = "Ivanov",
                email = "marie67@gmail.com",
                phone = "+1 800 200 300",
                comments = "New friend"
            };
            request.AddBody(body);
            var errorMssg = "{\"errMsg\":\"First name cannot be empty!\"}";                    

            var response = this.client.Execute(request, Method.Post);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(response.Content, Is.EqualTo(errorMssg));

        }
        [Test]
        public void Test_CreateNewContact_WithInvalidLastName()
        {
            this.request = new RestRequest(url);
            var body = new
            {
                firstName = "Ivan",
                lastName = "",
                email = "marie67@gmail.com",
                phone = "+1 800 200 300",
                comments = "New friend"
            };
            request.AddBody(body);
            var errorMssg = "{\"errMsg\":\"Last name cannot be empty!\"}";

            var response = this.client.Execute(request, Method.Post);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(response.Content, Is.EqualTo(errorMssg));

        }
        [Test]
        public void Test_CreateNewContact_WithInvalidEmail()
        {
            this.request = new RestRequest(url);
            var body = new
            {
                firstName = "Ivan",
                lastName = "Ivanov",
                email = "",
                phone = "",
                comments = "New friend"
            };
            request.AddBody(body);
            var errorMssg = "{\"errMsg\":\"Invalid email!\"}";

            var response = this.client.Execute(request, Method.Post);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(response.Content, Is.EqualTo(errorMssg));

        }
        [Test]
        public void Test_CreateNewContact_WithValidData()
        {
            this.request = new RestRequest(url);
            var name = "Ivan" + DateTime.Now;
            var lastname = "Ivanov" + DateTime.Now;

            var body = new
            {
                firstName = name,
                lastName = lastname,
                email = "ivan@abv.bg",
                phone = "123456",
                comments = "New friend"
            };
            request.AddBody(body);
            
            var response = this.client.Execute(request, Method.Post);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            response = this.client.Execute(request, Method.Get);
            var contacts = JsonSerializer.Deserialize<List<Contact>>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(contacts.Count, Is.GreaterThan(0));
            Assert.That(contacts[contacts.Count - 1].firstName, Is.EqualTo(name));
            Assert.That(contacts[contacts.Count - 1].lastName, Is.EqualTo(lastname));
        }

    }
}