using PaymentApplyProject.Application.Dtos;
using PaymentApplyProject.Application.Dtos.GrandPashaBetDtos;
using PaymentApplyProject.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PaymentApplyProject.Infrastructure.Services
{
    public class GrandPashaBetService : IGrandPashaBetService
    {
        private readonly HttpClient _httpClient;

        public GrandPashaBetService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<Response<NoContent>> ApplyDepositAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<NoContent>> ApplyWithdrawAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<NoContent>> CancelDepositAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<NoContent>> CancelWithdrawAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Response<CreateDepositResponseDto>> CreateDepositAsync(CreateDepositDto createDeposit)
        {
            string path = "/api/payment";
            var responseMessage = await _httpClient.PostAsJsonAsync(path, createDeposit);

            if (!responseMessage.IsSuccessStatusCode)
            {
                var errorMessage = JsonSerializer.Deserialize<string>(await responseMessage.Content.ReadAsStringAsync());
                return Response<CreateDepositResponseDto>.Error(responseMessage.StatusCode, errorMessage);
            }

            var response = await responseMessage.Content.ReadFromJsonAsync<CreateDepositResponseDto>();
            return Response<CreateDepositResponseDto>.Success(responseMessage.StatusCode, response);
        }
    }
}
