using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Broker.Test
{
    [TestFixture]
    public class MappingTest
    {
        private static readonly string MappingFilePath = Environment.CurrentDirectory + @"\config.mappings.json";

        /// <summary>
        ///     It is used to generate a json for development.
        /// </summary>
        [Test]
        public void MappingGeneration()
        {
            var mappings = new List<Mapping>
            {
                new Mapping
                {
                    ControlPoint = "SAVE-CR-CASE",
                    Operation = "PetitionAddUpdate",
                    Fields = new Dictionary<string, string>
                    {
                        {"fj_petn_cno", @"/Integration/Case/CaseNumber"},
                        {"fj_petn_pdt", @"/Integration/Case/FiledDate"},
                        {"fj_petn_sct", @"/Integration/Case/CaseType"},
                        {
                            "fj_petn_cpn",
                            @"/Integration/Case/CaseCrossReference[CaseCrossReferenceType/@Word='COM']/CrossCaseNumber"
                        },
                        {
                            "fj_arrt_dcn",
                            @"/Integration/Case/CaseCrossReference[CaseCrossReferenceType/@Word='DA']/CrossCaseNumber"
                        },
                        {
                            "fj_arrt_bnu",
                            @"/Integration/Case/CaseCrossReference[CaseCrossReferenceType/@Word='BOO']/CrossCaseNumber"
                        },
                        {"fj_arrt_aoa", @"/Integration/Case/Charge/BookingAgency/Agency"},
                        {"fj_arrt_arn", @"/Integration/Case/Charge/BookingAgency/ControlNumber"},
                        {"fj_arrt_cdt", @"/Integration/Case/Charge/BookingAgency/ArrestDate"},
                        {"fj_arrt_ctm", @"/Integration/Case/Charge/BookingAgency/ArrestTime"}
                    }
                }
            };

            var json = JsonConvert.SerializeObject(mappings, Formatting.Indented);
            File.WriteAllText(MappingFilePath, json);
        }
    }
}