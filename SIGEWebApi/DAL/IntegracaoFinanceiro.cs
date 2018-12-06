﻿using SIGEWebApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace SIGEWebApi.DAL
{
    public class IntegracaoFinanceiro
    {
        static HttpClient client = new HttpClient();

        public static async Task<List<InformacaoFinanceiroDTO>> GetAsync(string path)
        {
            List<InformacaoFinanceiroDTO> product = new List<InformacaoFinanceiroDTO>();
            client.BaseAddress = new Uri("http://trabalhosige.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));


            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<List<InformacaoFinanceiroDTO>>();
            }
            return product;
        }

        //static async Task<Uri> CreateProductAsync(Object product)
        //{
        //    HttpResponseMessage response = await client.PostAsJsonAsync(
        //        "api/products", product);
        //    response.EnsureSuccessStatusCode();

        //    // return URI of the created resource.
        //    return response.Headers.Location;
        //}


        //static async Task<Object> UpdateProductAsync(Object product)
        //{
        //    HttpResponseMessage response = await client.PutAsJsonAsync(
        //        $"api/products/{product}", product);
        //    response.EnsureSuccessStatusCode();

        //    // Deserialize the updated product from the response body.
        //    product = await response.Content.ReadAsAsync<Object>();
        //    return product;
        //}

        //static async Task<HttpStatusCode> DeleteProductAsync(string id)
        //{
        //    HttpResponseMessage response = await client.DeleteAsync(
        //        $"api/products/{id}");
        //    return response.StatusCode;
        //}

        //static void Main()
        //{
        //    RunAsync().GetAwaiter().GetResult();
        //}

        //static async Task RunAsync()
        //{
        //    // Update port # in the following line.
        //    client.BaseAddress = new Uri("http://localhost:64195/");
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/json"));

        //    try
        //    {
        //        // Create a new product
        //        Object product = new 
        //        {
        //            Name = "Gizmo",
        //            Price = 100,
        //            Category = "Widgets"
        //        };

        //        var url = await CreateProductAsync(product);
        //        Console.WriteLine($"Created at {url}");

        //        // Get the product
        //        product = await GetAsync(url.PathAndQuery);

        //        // Update the product
        //        Console.WriteLine("Updating price...");
        //        product = 80;
        //        await UpdateProductAsync(product);

        //        // Get the updated product
        //        product = await GetAsync(url.PathAndQuery);


        //        // Delete the product
        //        var statusCode = await DeleteProductAsync("1");
        //        Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }

        //    Console.ReadLine();
        //}
    }
}