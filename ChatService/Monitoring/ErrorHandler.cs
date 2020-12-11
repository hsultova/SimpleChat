using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace ChatService.Monitoring
{
	public class ErrorHandler : IErrorHandler, IServiceBehavior
	{
		public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
		{
			
		}

		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			IErrorHandler errorHandler = new ErrorHandler();

			foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
			{
				ChannelDispatcher channelDispatcher = channelDispatcherBase as ChannelDispatcher;

				if (channelDispatcher != null)
				{
					channelDispatcher.ErrorHandlers.Add(errorHandler);
				}
			}
		}

		public bool HandleError(Exception error)
		{
			Trace.TraceError(error.ToString());

			return true;
		}

		public void ProvideFault(Exception error, MessageVersion version, ref System.ServiceModel.Channels.Message fault)
		{
			FaultException faultException = new FaultException("Server error encountered. All details have been logged.");
			MessageFault messageFault = faultException.CreateMessageFault();

			fault = System.ServiceModel.Channels.Message.CreateMessage(version, messageFault, faultException.Action);
		}

		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			throw new NotImplementedException();
		}
	}
}
