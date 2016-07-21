using System;
using Contracts;

namespace Broker
{
    public class Caller
    {
        public static string Call(string callAddress, CallRequest request, Log log)
        {
            var client = new WS_Ref.ServiceClient();
            return client.PetitionAddUpdate(request); 
        }
    }
}