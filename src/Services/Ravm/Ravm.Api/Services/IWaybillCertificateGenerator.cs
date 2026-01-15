namespace Ravm.Api.Services;

public interface IWaybillCertificateGenerator
{
    Task<string> GenerateWaybillAsync(Guid waybillId);
}
