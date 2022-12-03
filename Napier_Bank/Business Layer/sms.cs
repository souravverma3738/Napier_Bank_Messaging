using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Napier_Bank
{
    public class sms
    {

       public string _sender;
        private string _text;
       public String smsSenderRegex = @"^(\+[1-9][0-9]*(\([0-9]*\)|-[0-9]*-))?[0]?[1-9][0-9\- ]*$";


        public string Sender
        {
            get { return _sender; }
            set
            {
                if (!Regex.IsMatch(value, smsSenderRegex))
                    throw new ArgumentException("Telephone number is in wrong format");
                else
                    _sender = value;
            }

        }

        public string Text
        {
            get { return _text; }
            set
            {
                if ((value.Length > 0) && (value.Length < 141))
                    _text = value;
                else
                    throw new ArgumentException("SMS has to have between 0 and 140 characters.");
            }
        }
    }
}
