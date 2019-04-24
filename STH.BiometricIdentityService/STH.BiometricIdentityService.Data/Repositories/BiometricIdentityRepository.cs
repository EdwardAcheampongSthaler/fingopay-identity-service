using System;
using STH.BiometricIdentityService.Data.Entity;
using STH.BiometricIdentityService.Data.Interfaces;

namespace STH.BiometricIdentityService.Data.Repositories
{
    public class BiometricIdentityRepository : IBiometricIdentityRepository
    {

        public BiometricIdentityRepository()
        {
            //TODO:// use context to do the work. // replace with UnitOfWork asaps
        }

        public BiometricAccountRepositoryResponse AddBiometricUuidToAccount(string accountId, string uuid)
        {
           return new BiometricAccountRepositoryResponse();
        }

        public BiometricRepositoryResponse GetAccountIdByBiometricUuid(string uuid)
        {
            return new BiometricRepositoryResponse();
        }

        public BiometricRepositoryResponse IsEnrollmentPinValid(string accountId, string pin)
        {
            return new BiometricRepositoryResponse();

        }
    }

    public class BiometricAccountRepositoryResponse: BiometricRepositoryResponse
    {
        public Account Account { get; set; }
    }

    public class BiometricRepositoryResponse 
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

    }
}
