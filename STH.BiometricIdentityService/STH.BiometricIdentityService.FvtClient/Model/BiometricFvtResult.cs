using System;

namespace STH.BiometricIdentityService.FvtClient.Model
{
    public class BiometricFvtResult: BiometricResultBase
    {
        public string Uuid { get; set; }
        public dynamic Data { get; set; }
    }

    public class BiometricRepositoryResponse: BiometricResultBase
    {
    }

}