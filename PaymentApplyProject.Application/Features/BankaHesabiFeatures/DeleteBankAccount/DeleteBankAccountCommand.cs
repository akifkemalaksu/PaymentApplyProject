﻿using MediatR;
using PaymentApplyProject.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentApplyProject.Application.Features.BankaHesabiFeatures.DeleteBankAccount
{
    public class DeleteBankAccountCommand : IRequest<Response<NoContent>>
    {
        public int Id { get; set; }
    }
}
