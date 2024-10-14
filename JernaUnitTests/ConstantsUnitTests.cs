using JernaClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JernaUnitTests
{
    public class ConstantsUnitTests
    {
        [Fact]
        public void Test_FrontLoadedInGradeSearch()
        {
            string result = Constants.FrontLoadedInGradeSearch("A123");
            Assert.Equal("A123-", result);

            result = Constants.FrontLoadedInGradeSearch(string.Empty);
            Assert.Equal("-", result);

            result = Constants.FrontLoadedInGradeSearch(null);
            Assert.Equal("-", result);
        }

        [Fact]
        public void Test_IsValidEmail()
        {
            Assert.True(Constants.IsValidEmail("example@example.com"));
            Assert.True(Constants.IsValidEmail("user.name+tag+sorting@example.com"));

            Assert.False(Constants.IsValidEmail("example.com"));
            Assert.False(Constants.IsValidEmail("example@.com"));
            Assert.False(Constants.IsValidEmail("example@com"));

            Assert.False(Constants.IsValidEmail(null));
            Assert.False(Constants.IsValidEmail(string.Empty));
        }

        [Fact]
        public void Test_SanitizeHtml()
        {
            string result = Constants.SanitizeHtml("<p>Hello, <b>World</b>!</p>");
            Assert.Equal("Hello, World!", result);

            result = Constants.SanitizeHtml("Hello, World!");
            Assert.Equal("Hello, World!", result);

            result = Constants.SanitizeHtml(string.Empty);
            Assert.Equal(string.Empty, result);

            result = Constants.SanitizeHtml("<div><span>Text</span></div>");
            Assert.Equal("Text", result);

            result = Constants.SanitizeHtml("<a href='https://example.com'>Link</a>");
            Assert.Equal("Link", result);
        }
    }
}
