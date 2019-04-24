using System;
using System.Net;
using System.Threading;
using STH.BiometricIdentityService.Domain.BiometricDataServices.Request;
using STH.BiometricIdentityService.Domain.BiometricDataServices.Response;
using STH.BiometricIdentityService.Domain.Interfaces;
using STH.BiometricIdentityService.Domain.PaymentService.Interfaces;

namespace STH.BiometricIdentityService.Domain.PaymentService
{
    public class CardStreamPaymentService : IPaymentService
    {
        public MakePaymentResponse MakePayment(MakePaymentRequest request)
        {

            var transactionStartDateTime = DateTime.UtcNow;
            //Thread.Sleep(2000); // sleep 2 seconds to mimic payment.
            var responseUniqueReference = Guid.NewGuid().ToString();
            // talk to cardstreams
            return new MakePaymentResponse()
            {
                Success = true,
                MerchantId = request.MerchantId,
                Message = $"Authorised Payment Transaction: {request.TransactionId} as UniqueReferenceId: {responseUniqueReference}",
                StatusCode = (int)HttpStatusCode.OK,
                TotalAmount = request.TotalAmount,
                TransactionEndDateTime = DateTime.UtcNow,
                TransactionStartDateTime = transactionStartDateTime,
                TransactionId = request.TransactionId,
                UniqueReferenceId = responseUniqueReference
            };
        }

        public RefundPaymentResponse RefundPayment(RefundPaymentRequest request)
        {
            var transactionStartDateTime = DateTime.UtcNow;
            //Thread.Sleep(2000); // sleep 2 seconds to mimic payment.
            var responseUniqueReference = Guid.NewGuid().ToString();
            // talk to cardstreams
            return new RefundPaymentResponse()
            {
                Success = true,
                MerchantId = request.MerchantId,
                Message = $"Refund Payment Transaction: {request.TransactionId} as UniqueReferenceId: {responseUniqueReference}",
                StatusCode = (int)HttpStatusCode.OK,
                TotalAmount = request.TotalAmount,
                TransactionEndDateTime = DateTime.UtcNow,
                TransactionStartDateTime = transactionStartDateTime,
                TransactionId = request.TransactionId,
                UniqueReferenceId = responseUniqueReference
            };
        }
    }
}