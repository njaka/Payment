namespace Payment.Api.Configuration.Versioning
{
    /// <summary>
    /// Defines the configuration for Api versioning
    /// </summary>
    public class VersioningConfigurationModel
    {
        /// <summary>
        /// Gets or sets the default version (format m.n.p).
        /// </summary>
        /// <value>
        /// The default version.
        /// </value>
        public string Default { get; set; }

        /// <summary>
        /// Gets or sets the name of the route constraint.
        /// </summary>
        /// <value>
        /// The name of the route contraint.
        /// </value>
        public string RouteConstraintName { get; set; }
    }
}
