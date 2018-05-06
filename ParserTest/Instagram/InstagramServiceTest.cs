using NUnit.Framework;
using Parser.Instagram;
using Parser.Instagram.Interfaces;

namespace ParserTest.Instagram
{
    [TestFixture(Category = TestCategory.UNIT_TEST)]
    public class InstagramServiceTest
    {
        private IInstagramService instagramServce;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            //instagramServce = new InstagramService();
        }
        
    }
}
