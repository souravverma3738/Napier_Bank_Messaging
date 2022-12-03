using System;
using System.Collections.Generic;
using System.Text;
using Napier_Bank;
using Xunit;

namespace testing
{
   public  class Twittertest
    {
        Twitter tw1 = new Twitter();

        [Fact]
        public void SenderPass()
        {

            tw1.Sender = "@sourav";
            string expected = "@sourav";

            Assert.Equal(expected, tw1.Sender);
        }
        [Fact]
        public  void Text_pass()
        {
            tw1.Text = "this is sourav";
            String expected = "this is sourav";
            Assert.Equal(expected, tw1.Text);
        }



    }
}
