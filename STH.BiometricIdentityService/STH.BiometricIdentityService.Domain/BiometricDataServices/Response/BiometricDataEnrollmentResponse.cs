using System;
using STH.BiometricIdentityService.Data.Entity;

namespace STH.BiometricIdentityService.Domain.BiometricDataServices.Response
{
    public class BiometricDataEnrollmentResponse : ResponseBase
    {
        public Account Account { get; set; }

    }



}
