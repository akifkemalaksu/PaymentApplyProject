﻿namespace PaymentApplyProject.Application.Dtos.Settings
{
    public class JwtSettings
    {
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required string SigningKey { get; set; }
        public double TokenTimeoutMinutes { get; set; }
    }
}
