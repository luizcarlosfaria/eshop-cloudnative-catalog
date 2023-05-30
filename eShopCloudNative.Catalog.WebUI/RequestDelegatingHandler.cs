using eShopCloudNative.Architecture.Logging;

public class RequestDelegatingHandler : DelegatingHandler
{
    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        using (new EnterpriseApplicationLogContext(request.RequestUri.ToString(), request.Method.ToString(), it => it.AddProperty("Refit", true).AddProperty("Async", false)))
        {
            return base.Send(request, cancellationToken);
        }
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        using (new EnterpriseApplicationLogContext(request.RequestUri.ToString(), request.Method.ToString(), it => it.AddProperty("Refit", true).AddProperty("Async", true)))
        {
            return await base.SendAsync(request, cancellationToken);
        }
    }
}