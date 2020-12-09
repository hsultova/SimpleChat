using System.ServiceModel;

namespace ChatService
{
	/// <summary>
	/// <see cref="IOperationContext"/> interface for the <see cref="OperationContext"/>
	/// </summary>
	public interface IOperationContext
	{
		// Summary:
		//     Gets a channel to the client instance that called the current operation.
		//
		// Type parameters:
		//   T:
		//     The type of channel used to call back to the client.
		//
		// Returns:
		//     A channel to the client instance that called the operation of the type specified
		//     in the System.ServiceModel.ServiceContractAttribute.CallbackContract property.
		T GetCallbackChannel<T>();

		// Summary:
		//     Commits the currently executing transaction.
		//
		// Exceptions:
		//   T:System.InvalidOperationException:
		//     There is no transaction in the context.
		void SetTransactionComplete();
	}

	/// <summary>
	/// Wrapper of the <see cref="OperationContext"/> for easier unit testing.
	/// </summary>
	public class OperationContextWrapper : IOperationContext
	{
		public T GetCallbackChannel<T>()
		{
			return OperationContext.Current.GetCallbackChannel<T>();
		}

		public void SetTransactionComplete()
		{
			OperationContext.Current.SetTransactionComplete();
		}
	}
}
