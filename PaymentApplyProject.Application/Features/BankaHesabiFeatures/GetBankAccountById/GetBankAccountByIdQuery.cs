using PaymentApplyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PaymentApplyProject.Application.Dtos;

namespace PaymentApplyProject.Application.Features.BankaHesabiFeatures.GetBankAccountById
{
    public class GetBankAccountByIdQuery : IRequest<Response<GetBankAccountByIdResult>>
    {
        public int Id { get; set; }
    }
}
