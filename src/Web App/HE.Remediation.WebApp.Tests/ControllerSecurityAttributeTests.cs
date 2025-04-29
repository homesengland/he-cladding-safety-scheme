using System.Reflection;
using HE.Remediation.WebApp.Areas;
using HE.Remediation.WebApp.Attributes.Authorisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HE.Remediation.WebApp.Tests
{
    public class ControllerSecurityAttributeTests : TestBase
    {
        [Fact]
        public void CountControllersWithoutAnySecurity()
        {
            // Arrange
            var assembly = Assembly.GetAssembly(typeof(StartController));
            var baseType = typeof(Controller);
            var expectedControllersWithoutSecurityCount = 6;

            // Act
            var controllersWithoutAnySecurityAttributes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(baseType) && !t.IsSubclassOf(typeof(StartController))
                && (!t.GetCustomAttributes(typeof(CookieAuthoriseAttribute), true).Any() &&
                    !t.GetCustomAttributes(typeof(CookieApplicationAuthoriseAttribute), true).Any() &&
                    !t.GetCustomAttributes(typeof(AuthorizeAttribute), true).Any())
                );

            /************************** NOTE *************************************/
            /* If this fails a controller has been added without security.       */
            /* If this was intended increase the expected count.                 */
            /* If it wasn't intended add some security i.e. the above attributes.*/
            /*********************************************************************/

            // Assert
            Assert.Equal(expectedControllersWithoutSecurityCount, controllersWithoutAnySecurityAttributes.Count()); 

        }
    }
}
