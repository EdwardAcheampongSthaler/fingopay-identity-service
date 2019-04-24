using System;
using System.Collections.Generic;
using STH.BiometricIdentityService.FvtClient.Model;

namespace STH.BiometricIdentityService.FvtClient.Interfaces
{
    public interface IFvtClientRepository
    {
        BiometricFvtResult Enroll(string uuid, byte[] eTemplate, byte[] vTemplate);
        BiometricFvtResult ReEnroll(string uuid, byte[] data);
        BiometricFvtResult Identify(byte[] data);
        BiometricFvtResult Verify(string uuid, byte[] data);
        BiometricFvtResult Delete(string uuid);

        BiometricServiceTransactionLogResult GetTransactions(DateTime startDate, DateTime endDate);
        BiometricServiceTransactionLogResult GetUserTransactions(string uuid, DateTime startDate, DateTime endDate);
        bool DeleteTransactionLog(DateTime startDate, DateTime endDate);
        GetMetricsResult GetIdMetrics(DateTime startDate, DateTime endDate);
        GetMetricsResult GetVerMetrics(DateTime startDate, DateTime endDate);
        IEnumerable<Guid> GetUsers(int noOfUsers);
        GetVersionResult DeleteAllUsers();
        BiometricFvtResult GetVersion();
        bool SetLicense(string license);

    }
}
