﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Web.HttpAggregator.Models;

namespace Web.HttpAggregator.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;

        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task UpdateAsync(BasketData currentBasket, string accessToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/v1/basket")
            {
                Content = JsonContent.Create(currentBasket)
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
    }
}