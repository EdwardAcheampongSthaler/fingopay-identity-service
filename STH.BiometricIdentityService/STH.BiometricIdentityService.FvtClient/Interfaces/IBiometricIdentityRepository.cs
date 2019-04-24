using STH.BiometricIdentityService.FvtClient.Model;

namespace STH.BiometricIdentityService.FvtClient.Interfaces
{
    public interface IBiometricIdentityRepository2
    {
        BiometricRepositoryResponse AddBiometricUuidToAccount(string accountId, string uuid);
        BiometricRepositoryResponse GetAccountIdByBiometricUuid(string accountId, string uuid);
        BiometricRepositoryResponse GetAccountIdByEnrollmentPin(string accountId, string uuid);

    }


}