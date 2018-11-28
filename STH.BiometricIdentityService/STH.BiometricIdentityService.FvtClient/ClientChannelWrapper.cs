using System;
using System.ServiceModel;
using STH.BiometricIdentityService.FvtClient.Interfaces;

namespace STH.BiometricIdentityService.FvtClient
{
    /// <summary>
    /// Wrapper class ensures proper life-cycle handling for IClientChannel 
    /// objects by implementing proper Open, Close and Dispose techniques
    /// based on recommended practices.
    /// </summary>
    /// <remarks>
    /// The idea for BeginInvoke and EndInvoke methods was derived from
    /// this <see cref="http://msdn.microsoft.com/en-us/magazine/ee309512.aspx">MSDN article</see>
    /// </remarks>
    /// <typeparam name="T">An interface which defines an async ServiceContract</typeparam>
    public class ClientChannelWrapper<T> : IDisposable, IClientChannelWrapper<T> where T : class
    {
        private ChannelFactory<T> m_Factory;
        private T m_Service;
        private Object m_SyncRoot = new Object();

        public ClientChannelWrapper(String endpointName)
        {
            m_Factory = new ChannelFactory<T>(endpointName);
        }

        public ClientChannelWrapper(T service)
        {
            m_Service = service;
        }

        public IAsyncResult BeginInvoke(Func<T, IAsyncResult> function)
        {
            try
            {
                return function.Invoke(Current);
            }
            catch (Exception)
            {
                CloseChannel();
                throw;
            }
        }

        public TResult EndInvoke<TResult>(Func<T, TResult> function)
        {
            try
            {
                return function.Invoke(Current);
            }
            catch (Exception)
            {
                CloseChannel();
                throw;
            }
        }

        public void EndInvoke(Action<T> action)
        {
            try
            {
                action.Invoke(Current);
            }
            catch (Exception)
            {
                CloseChannel();
                throw;
            }
        }

        protected void CloseChannel()
        {
            if (m_Service != null)
            {
                lock (m_SyncRoot)
                {
                    if (m_Service != null)
                    {
                        IClientChannel channel = m_Service as IClientChannel;
                        if (channel != null)
                        {
                            try
                            {
                                channel.Faulted -= IClientChannel_Faulted;
                                if (channel.State == CommunicationState.Faulted)
                                    channel.Abort();
                                else
                                    channel.Close();
                            }
                            catch (CommunicationException) { channel.Abort(); }
                            catch (TimeoutException) { channel.Abort(); }
                            catch (Exception) { channel.Abort(); throw; }
                            finally { m_Service = null; }
                        }
                    }
                }
            }
        }

        protected T Current
        {
            get
            {
                if (m_Service != null)
                    return m_Service;

                lock (m_SyncRoot)
                {
                    if (m_Service == null)
                    {
                        m_Service = m_Factory.CreateChannel();
                        ((IClientChannel)m_Service).Faulted += IClientChannel_Faulted;
                    }
                }

                return m_Service;
            }
        }

        private void IClientChannel_Faulted(Object sender, EventArgs e)
        {
            CloseChannel();
        }

        #region IDisposable Members

        private Boolean m_IsDisposed = false;

        public void Dispose()
        {
            if (m_IsDisposed)
                throw new ObjectDisposedException("ClientChannelWrapper");

            try
            {
                CloseChannel();
            }
            finally
            {
                m_IsDisposed = true;
            }
        }

        #endregion
    }
}