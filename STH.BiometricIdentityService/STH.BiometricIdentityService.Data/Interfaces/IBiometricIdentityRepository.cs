using STH.BiometricIdentityService.Data.Repositories;

namespace STH.BiometricIdentityService.Data.Interfaces
{
    public interface IBiometricIdentityRepository
    {
        BiometricAccountRepositoryResponse AddBiometricUuidToAccount(string accountId, string uuid);
        BiometricRepositoryResponse GetAccountIdByBiometricUuid(string uuid);
        BiometricRepositoryResponse IsEnrollmentPinValid(string accountId, string pin);

    }
}