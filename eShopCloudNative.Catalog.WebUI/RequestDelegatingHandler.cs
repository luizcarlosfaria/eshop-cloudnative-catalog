using eShopCloudNative.Architecture.Logging;

public class RequestDelegatingHandler : DelegatingHandler
{
    protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        using (var context = new EnterpriseApplicationLogContext())
        {
            context.SetIdentity(request.RequestUri.ToString(), request.Method.ToString());

            context
                .AddProperty("Refit", true)
                .AddProperty("Async", false);

            return base.Send(request, cancellationToken);
        }
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        using (var context = new EnterpriseApplicationLogContext())
        {
            context.SetIdentity(request.RequestUri.ToString(), request.Method.ToString());

            context
                .AddProperty("Refit", true)
                .AddProperty("Async", false);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}