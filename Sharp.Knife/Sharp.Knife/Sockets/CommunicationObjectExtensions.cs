using System;
using System.ServiceModel;

namespace Sharp.Knife.Sockets
{
    public static class CommunicationObjectExtensions
    {
        /// <summary>
        /// Executes the method on the ICommunicationObject method. 
        /// Will close or abort the client based on the exception and communication state.
        /// </summary>
        /// <typeparam name="TClient">ICommunicationObject type.</typeparam>
        /// <typeparam name="TResponse">The return of the method.</typeparam>
        /// <param name="client">ICommunicationObject implementer.</param>
        /// <param name="method">The method from the ICommunicationObject object to execute.</param>
        /// <returns>The value of TypeResponse from the method parameter.</returns>
        /// <exception cref="CommunicationException">Error thrown if client is unable to communicate to endpoint.</exception>
        /// <exception cref="TimeoutException">Error thrown if the alotted time to connect is exceeded.</exception>
        public static TResponse Execute<TClient, TResponse>
            (this TClient client, Func<TClient, TResponse> method)
            where TClient : class, ICommunicationObject
        {
            if ((client == null) || (method == null))
                return default(TResponse);

            try
            {
                return method(client);
            }
            catch (CommunicationException)
            {
                client.Abort();
                throw;
            }
            catch (TimeoutException)
            {
                client.Abort();
                throw;
            }
            catch (Exception)
            {
                if (client.State == CommunicationState.Faulted)
                    client.Abort();

                throw;
            }
            finally
            {
                if (client.State != CommunicationState.Faulted)
                {
                    client.Close();
                }
                else
                {
                    client.Abort();
                }
            }
        }
    }
}