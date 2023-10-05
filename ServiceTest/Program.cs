// See https://aka.ms/new-console-template for more information
using ServiceTest;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Json;

Console.WriteLine("Hello, World!");
 HttpClient client = new HttpClient();

// Update port # in the following line.
client.BaseAddress = new Uri("https://localhost:7111");
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


var path = "FleetTransport/Employee";

List<EmployeeDto>? result = new();
HttpResponseMessage response = await client.GetAsync(path);
if (response.IsSuccessStatusCode)
{
    result = await response.Content.ReadFromJsonAsync<List<EmployeeDto>>();
}

foreach (var item in result! )
{
    Console.WriteLine($"{item.Nif}\t\t{item.Name}");
}

Console.ReadLine();