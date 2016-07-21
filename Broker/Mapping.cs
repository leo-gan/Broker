using System.Collections.Generic;

namespace Broker
{
    /// <summary>
    ///     It defines 1 to M mapping from Odyssey (Source) to ISAB (Target)
    /// </summary>
    public class Mapping
    {
        /// <summary>
        ///     Now it is the Odyssey Control Point as "SAVE-CR-CASE"
        /// </summary>
        public string ControlPoint { get; set; }

        /// <summary>
        ///     Now it is a name of interface as PetionAddUpdate.
        ///     I assume it is an endpoint name in a Cloverleaf web-service.
        ///     There could be several such names related to a single Odyssey Control Point.
        ///     This relation defined as several Mapping objects.
        /// </summary>
        public string Operation { get; set; }

        /// <summary>
        ///     Important! Here is it the reverse mapping (sort of), Key is a target not a source.
        ///     Key: It is a name of the field in the ISAB database as it appears in documentation.
        ///     An example: fj_petn_cno
        ///     Value: It is an XPath expression applied to the Odyssey CIP package message.
        ///     An example: /Integration/Case/CaseNumber
        /// </summary>
        public Dictionary<string, string> Fields { get; set; }
    }
}