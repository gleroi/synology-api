﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Synology.Api.Tests
{
    public static class CheckResponse
    {
        /// <summary>
        /// A succes response is:
        /// * is not null
        /// * has its Success property to true
        /// * has its Error property to Null
        /// </summary>
        /// <param name="response"></param>
        public static void HasSucceeded(IResponseStatus response) 
        {
            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Null(response.Error);
        }
    }
}