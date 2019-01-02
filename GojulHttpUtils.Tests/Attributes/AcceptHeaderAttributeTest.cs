﻿using GojulHttpUtils.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

using Xunit;

namespace GojulHttpUtils.Tests.Attributes
{
    public class AcceptHeaderAttributeTest
    {

        [Fact]
        public void TestConstructorWithNullAcceptHeaderThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new AcceptHeaderAttribute(null));
        }

        [Fact]
        public void TestConstructorWithBlankAcceptHeaderThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new AcceptHeaderAttribute("     "));
        }

        [Fact]
        public void TestIsValidForRequestWithNullRouteContextThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new AcceptHeaderAttribute("windowssucks").IsValidForRequest(null, new ActionDescriptor()));
        }

        [Fact]
        public void TestIsValidForRequestWithoutAcceptHeaderReturnFalse()
        {
            var headerDictionary = new HeaderDictionary();
            headerDictionary["foo"] = "bar";
            headerDictionary["linux"] = "rocks";

            var routeContext = CreateRouteContext(headerDictionary);
            Assert.False(new AcceptHeaderAttribute("windowssucks").IsValidForRequest(routeContext, new ActionDescriptor()));
        }

        [Fact]
        public void TestIsValidForRequestWithAcceptHeaderNotMatchingExpectedReturnFalse()
        {
            var headerDictionary = new HeaderDictionary();
            headerDictionary["foo"] = "bar";
            headerDictionary["linux"] = "rocks";
            headerDictionary["accept"] = "javabetterthandotnet";

            var routeContext = CreateRouteContext(headerDictionary);
            Assert.False(new AcceptHeaderAttribute("windowssucks").IsValidForRequest(routeContext, new ActionDescriptor()));
        }

        [Fact]
        public void TestIsValidForRequestWithAcceptHeaderWithAttributeCaseInsensitivityReturnTrue()
        {
            var headerDictionary = new HeaderDictionary();
            headerDictionary["foo"] = "bar";
            headerDictionary["linux"] = "rocks";
            headerDictionary["accept"] = "windowssucks";

            var routeContext = CreateRouteContext(headerDictionary);
            Assert.False(new AcceptHeaderAttribute("windowssucks").IsValidForRequest(routeContext, new ActionDescriptor()));
        }

        [Fact]
        public void TestIsValidForRequestWithExpectedAcceptHeaderReturnTrue()
        {
            var headerDictionary = new HeaderDictionary();
            headerDictionary["foo"] = "bar";
            headerDictionary["linux"] = "rocks";
            headerDictionary[AcceptHeaderAttribute.AcceptHeader] = "windowssucks";

            var routeContext = CreateRouteContext(headerDictionary);
            Assert.False(new AcceptHeaderAttribute("windowssucks").IsValidForRequest(routeContext, new ActionDescriptor()));
        }

        [Fact]
        public void TestIsValidForRequestWithUnxpectedAcceptHeaderWithFormatReturnFalse()
        {
            var headerDictionary = new HeaderDictionary();
            headerDictionary["foo"] = "bar";
            headerDictionary["linux"] = "rocks";
            headerDictionary[AcceptHeaderAttribute.AcceptHeader] = "microsoft+dotnetsucks";

            var routeContext = CreateRouteContext(headerDictionary);
            Assert.False(new AcceptHeaderAttribute("   windowssucks   ").IsValidForRequest(routeContext, new ActionDescriptor()));
        }

        [Fact]
        public void TestIsValidForRequestWithExpectedAcceptHeaderWithFormatReturnTrue()
        {
            var headerDictionary = new HeaderDictionary();
            headerDictionary["foo"] = "bar";
            headerDictionary["linux"] = "rocks";
            headerDictionary[AcceptHeaderAttribute.AcceptHeader] = "microsoft+windowssucks";

            var routeContext = CreateRouteContext(headerDictionary);
            Assert.False(new AcceptHeaderAttribute("windowssucks").IsValidForRequest(routeContext, new ActionDescriptor()));
        }

        [Fact]
        public void TestIsValidForRequestWithExpectedAcceptHeaderWithFormatAndBadCaseReturnTrue()
        {
            var headerDictionary = new HeaderDictionary();
            headerDictionary["foo"] = "bar";
            headerDictionary["linux"] = "rocks";
            headerDictionary[AcceptHeaderAttribute.AcceptHeader] = "microsoft+WindowsSucks";

            var routeContext = CreateRouteContext(headerDictionary);
            Assert.False(new AcceptHeaderAttribute("windowssucks").IsValidForRequest(routeContext, new ActionDescriptor()));
        }

        private RouteContext CreateRouteContext(IHeaderDictionary dictionary)
        {
            var httpContext = new Mock<HttpContext>();
            var routeContext = new RouteContext(httpContext.Object);
            var httpRequest = new Mock<HttpRequest>();

            httpContext.Setup(e => e.Request).Returns(httpRequest.Object);
            httpRequest.Setup(e => e.Headers).Returns(dictionary);

            return routeContext;
        }
    }
}