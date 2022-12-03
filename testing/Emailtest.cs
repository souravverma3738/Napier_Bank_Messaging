using System;
using System.Collections.Generic;
using System.Text;
using Napier_Bank;
using Xunit;

namespace testing
{
    public class Emailtest
    {

        email e1 = new email();
        [Fact]
        public void SenderPass()
        {
            e1.Sender = "mluqueanguita@gmail.com";
            string expected = "mluqueanguita@gmail.com";

            Assert.Equal(expected, e1.Sender);
        }
        [Fact]
        public void SubjectPass()
        {
            e1.Subject = "subject < 21 chars";
            string expected = "subject < 21 chars";

            Assert.Equal(expected, e1.Subject);
        }
        [Fact]
        public void TextPass()
        {
            e1.Text = "subject less than 1029 chars";
            string expected = "subject less than 1029 chars";

            Assert.Equal(expected, e1.Text);
        }

    }
}
