using System.ServiceModel;
using Contracts;
////using System.Text;

namespace WebServiceStub
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        string PetitionAddUpdate(CallRequest value);

        [OperationContract]
        string AddMinute(CallRequest value);
    }
}