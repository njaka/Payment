﻿using System;

namespace Payment.Api.Configuration
{
    /// <summary>
    /// Third party Configuration Http Model
    /// </summary>
    public class ThirdPartyModel
    {
        /// <summary>
        /// Base Address
        /// </summary>
        public Uri BaseAddress { get; set; }

        /// <summary>
        /// Time out
        /// </summary>
        public TimeSpan Timeout { get; set; }
    }
}
