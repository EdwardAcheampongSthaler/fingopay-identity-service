using System.ServiceModel;
using STH.BiometricIdentityService.FvtClient.FvtProxy;

namespace STH.BiometricIdentityService.FvtClient.Interfaces
{
    [ServiceContract(
        Namespace = "http://hitachisoft.jp/aug/", 
        ConfigurationName = "FvtProxy.AuthenticationServiceSoap",
        Name = "AuthenticationServiceSoap")]
    public interface IAuthenticationServiceSoap
    {
        [OperationContract(Action = "http://hitachisoft.jp/aug/Remove", ReplyAction = "*")]
        RemoveResponse Remove(RemoveRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/Remove", ReplyAction = "*")]
        System.Threading.Tasks.Task<RemoveResponse> RemoveAsync(RemoveRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/DeleteAllUsers", ReplyAction = "*")]
        DeleteAllUsersResponse DeleteAllUsers(DeleteAllUsersRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/DeleteAllUsers", ReplyAction = "*")]
        System.Threading.Tasks.Task<DeleteAllUsersResponse> DeleteAllUsersAsync(DeleteAllUsersRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/Verify", ReplyAction = "*")]
        VerifyResponse Verify(VerifyRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/Verify", ReplyAction = "*")]
        System.Threading.Tasks.Task<VerifyResponse> VerifyAsync(VerifyRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/Enrol", ReplyAction = "*")]
        EnrolResponse Enrol(EnrolRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/Enrol", ReplyAction = "*")]
        System.Threading.Tasks.Task<EnrolResponse> EnrolAsync(EnrolRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/ReEnrol", ReplyAction = "*")]
        ReEnrolResponse ReEnrol(ReEnrolRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/ReEnrol", ReplyAction = "*")]
        System.Threading.Tasks.Task<ReEnrolResponse> ReEnrolAsync(ReEnrolRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/Identify", ReplyAction = "*")]
        IdentifyResponse Identify(IdentifyRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/Identify", ReplyAction = "*")]
        System.Threading.Tasks.Task<IdentifyResponse> IdentifyAsync(IdentifyRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/GetUsers", ReplyAction = "*")]
        GetUsersResponse GetUsers(GetUsersRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/GetUsers", ReplyAction = "*")]
        System.Threading.Tasks.Task<GetUsersResponse> GetUsersAsync(GetUsersRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/GetUserTransactions", ReplyAction = "*")]
        GetUserTransactionsResponse GetUserTransactions(GetUserTransactionsRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/GetUserTransactions", ReplyAction = "*")]
        System.Threading.Tasks.Task<GetUserTransactionsResponse> GetUserTransactionsAsync(GetUserTransactionsRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/GetVersion", ReplyAction = "*")]
        GetVersionResponse GetVersion(GetVersionRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/GetVersion", ReplyAction = "*")]
        System.Threading.Tasks.Task<GetVersionResponse> GetVersionAsync(GetVersionRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/SetLicence", ReplyAction = "*")]
        SetLicenceResponse SetLicence(SetLicenceRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/SetLicence", ReplyAction = "*")]
        System.Threading.Tasks.Task<SetLicenceResponse> SetLicenceAsync(SetLicenceRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/GetTransactions", ReplyAction = "*")]
        GetTransactionsResponse GetTransactions(GetTransactionsRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/GetTransactions", ReplyAction = "*")]
        System.Threading.Tasks.Task<GetTransactionsResponse> GetTransactionsAsync(GetTransactionsRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/DeleteTransactionLog", ReplyAction = "*")]
        DeleteTransactionLogResponse DeleteTransactionLog(DeleteTransactionLogRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/DeleteTransactionLog", ReplyAction = "*")]
        System.Threading.Tasks.Task<DeleteTransactionLogResponse> DeleteTransactionLogAsync(DeleteTransactionLogRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/GetIdMetrics", ReplyAction = "*")]
        GetIdMetricsResponse GetIdMetrics(GetIdMetricsRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/GetIdMetrics", ReplyAction = "*")]
        System.Threading.Tasks.Task<GetIdMetricsResponse> GetIdMetricsAsync(GetIdMetricsRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/GetVerMetrics", ReplyAction = "*")]
        GetVerMetricsResponse GetVerMetrics(GetVerMetricsRequest request);

        [OperationContract(Action = "http://hitachisoft.jp/aug/GetVerMetrics", ReplyAction = "*")]
        System.Threading.Tasks.Task<GetVerMetricsResponse> GetVerMetricsAsync(GetVerMetricsRequest request);
    }
}