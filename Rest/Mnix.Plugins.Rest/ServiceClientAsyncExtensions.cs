using ServiceStack.Service;
using ServiceStack.ServiceHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cirrious.CrossCore;

namespace Mnix.Plugins.Rest
{
    // To use this class, the project must have the Microsoft Async NuGet package
    public static class ServiceClientAsyncExtensions
    {
        private delegate void AsyncRequest<TResponse>(IReturn<TResponse> request, Action<TResponse> onSuccess, Action<TResponse, Exception> onError);

        private static TResponse AsyncHandler<TResponse>(AsyncRequest<TResponse> asyncRequest, IReturn<TResponse> request)
        {
            TResponse response = default(TResponse);
            Exception exc = null;
            ManualResetEvent handle = null;

            asyncRequest(request,
                (r) => { response = r; handle.Set(); },
                (r, e) => { response = r; exc = e; handle.Set(); });
            // TODO: Fanta- verify need for timeout
            using (handle = new ManualResetEvent(false))
            {
                handle.WaitOne();
            }
            if (exc != null)
            {
                Mvx.Error(exc.ToString());
                throw exc;
            }
            return response;
        }

        public static Task<TResponse> GetAsync<TResponse>(this IServiceClient client, IReturn<TResponse> request)
        {
            return TaskEx.Run(() => AsyncHandler(client.GetAsync, request));
        }

        public static Task<TResponse> PostAsync<TResponse>(this IServiceClient client, IReturn<TResponse> request)
        {
            return TaskEx.Run(() => AsyncHandler(client.PostAsync, request));
        }

        public static Task<TResponse> PutAsync<TResponse>(this IServiceClient client, IReturn<TResponse> request)
        {
            return TaskEx.Run(() => AsyncHandler(client.PutAsync, request));
        }

        public static Task<TResponse> DeleteAsync<TResponse>(this IServiceClient client, IReturn<TResponse> request)
        {
            return TaskEx.Run(() => AsyncHandler(client.DeleteAsync, request));
        }
    }
}
