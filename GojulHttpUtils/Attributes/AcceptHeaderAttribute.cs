using Conditions;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GojulHttpUtils.Attributes
{
    /// <summary>
    /// Class <code>AcceptHeaderAttribute</code> is an ASP Net Core filter
    /// which enables you to filter a route based on the <code>Accept</code>
    /// header of the request. Sadly Microsoft does not provide a reliable way to 
    /// perform this filtering as of now.
    /// </summary>
    public class AcceptHeaderAttribute : ActionMethodSelectorAttribute
    {
        // Note : this code has been vastly inspired from :
        // https://github.com/ChrisKlug/AspNetCoreMvcAcceptHeaderRouting/blob/master/Code/AcceptHeaderRoutingDemo.Web/Routing/AcceptHeaderAttribute.cs

        internal const string AcceptHeader = "Accept";

        private readonly string _acceptType;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="acceptType">the accept type which must be a MIME type. No check
        /// is performed at this stage on the actual MIME type.</param>
        /// <exception cref="ArgumentNullException">if <code>acceptType</code> is <code>null</code>.</exception>
        /// <exception cref="ArgumentException">if <code>acceptType</code> is blank.</exception>
        public AcceptHeaderAttribute(string acceptType)
        {
            Condition.Requires(acceptType).IsNotNullOrWhiteSpace();
            this._acceptType = acceptType.Trim();
        }

        public override bool IsValidForRequest(RouteContext routeContext, ActionDescriptor action)
        {
            Condition.Requires(routeContext).IsNotNull();

            var accept = routeContext.HttpContext.Request.Headers
                .FirstOrDefault(x => AcceptHeader.Equals(x.Key, StringComparison.InvariantCultureIgnoreCase)).Key;
            if (accept == null) return false;

            var acceptWithoutFormat = accept;
            if (accept.Contains("+"))
            {
                acceptWithoutFormat = accept.Substring(0, accept.IndexOf("+"));
            }

            return _acceptType.Equals(acceptWithoutFormat, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
