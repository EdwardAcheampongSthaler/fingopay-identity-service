using System;

namespace STH.BiometricIdentityService.FvtClient.Interfaces
{
    public interface IClientChannelWrapper<T> where T : class
    {
        IAsyncResult BeginInvoke(Func<T, IAsyncResult> function);
        void Dispose();
        void EndInvoke(Action<T> action);
        TResult EndInvoke<TResult>(Func<T, TResult> function);
    }
}