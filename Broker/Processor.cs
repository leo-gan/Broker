using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using Contracts;

namespace Broker
{
    public class Processor
    {
        /// <summary>
        ///     It extract a control point name from the CIP message.
        ///     It should be in the /Integration/ControlPoint/text() xpath.
        /// </summary>
        /// <param name="msg">CIP message as an XDocument</param>
        /// <returns>A control point. An example: "SAVE-CR-CASE"</returns>
        public static string GetControlPoint(XDocument msg)
        {
            return msg.Descendants(XName.Get("ControlPoint")).First().Value;
        }

        /// <summary>
        ///     It takes an Odyssey IXML message and maps it into the ISAB Cloverleaf requests.
        ///     There can be several requests per one Control Point but only one per one Control Point / Operation pair).
        /// </summary>
        /// <param name="config"></param>
        /// <param name="msg">An Odyssey IXML message</param>
        /// <param name="controlPoint">An Odyssey Control Point. For example: SAVE-CR-CASE</param>
        /// <param name="operation">An ISAB operation/interface name. For example: PetitionAddUpdate</param>
        /// <returns>A CallRequest.</returns>
        public static CallRequest CreateCallRequest(Config config, XDocument msg, string controlPoint, string operation)
        {
            CallRequest callRequest = null;
            // find a mapping for the controlPoint and Operation pair:
            foreach (var mapping in config.Mappings)
            {
                if (mapping.ControlPoint != controlPoint && mapping.Operation != operation) continue;
                callRequest = new CallRequest {Operation = mapping.Operation, Fields = new List<Field>()};
                // Map the msg into a CallRequest
                foreach (var mappingForAField in mapping.Fields)
                {
                    var fieldValue = msg.XPathSelectElement(mappingForAField.Value) == null
                        ? null
                        : msg.XPathSelectElement(mappingForAField.Value).Value;
                    var fieldAction = msg.XPathSelectElement(mappingForAField.Value) == null
                        ? null
                        : (msg.XPathSelectElement(mappingForAField.Value).Attribute("Op") == null
                            ? null
                            : msg.XPathSelectElement(mappingForAField.Value).Attribute("Op").Value);
                    var field = new Field
                    {
                        Name = mappingForAField.Key,
                        Value = fieldValue,
                        Action = fieldAction
                    };
                    callRequest.Fields.Add(field);
                }
            }
            return callRequest;
        }
    }
}