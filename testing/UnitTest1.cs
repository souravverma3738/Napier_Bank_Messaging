using System;
using Xunit;
using Napier_Bank;

namespace testing
{
    public class UnitTest1
    {
        [Fact]
        public  void Sender_pass()
        {
            sms sm = new sms();
            sm.Sender = "07824047235";



        }
        [Fact]
        public void Sender_fail()
        {
            sms sm = new sms();

            sm.Sender = "1";

        }
        [Fact]
        public  void Text_pass()
        {
            sms sm = new sms();
            sm.Text = "this is our software ";

        }

    }
}
