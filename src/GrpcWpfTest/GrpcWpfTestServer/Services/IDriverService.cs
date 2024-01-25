
namespace GrpcWpfTestServer.Services
{
	public interface IDriverService
	{
		event Func<SendMessage, Task>? Send;
	}
}