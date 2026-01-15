namespace Ravm.Api.Utils.OpenXml;

public interface IWordpoccessingWorkerModel
{
    Stream AsWordStream(string sourcePath, string qrCodePath);
}
