using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Napier_Bank
{
    class Sir
    {
        public string sortcode { get; set; }
        public string incident { get; set; }


        public Sir(string headerIn, string subjIn, string sortIn, string incidentIn)
        {
            sortcode = sortIn;
            incident = incidentIn;
        }
    }

    class IncidentReportList
    {
        public List<Sir> Incidents { get; set; }
    }

}
