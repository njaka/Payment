namespace Payment.Api.Configuration
{
    using System.Collections.Generic;

    /// <summary>
    /// Configuration model for Swagger
    /// </summary>
    public class SwaggerConfigurationModel
    {
        /// <summary>
        /// Gets or sets the version collection.
        /// </summary>
        /// <value>
        /// The versions.
        /// </value>
        public IEnumerable<VersionConfigurationModel> Versions { get; set; }

        /// <summary>
        /// Gets or sets the documentation files used by SwaggerGen
        /// </summary>
        public IEnumerable<string> XmlDocumentation { get; set; }
    }

    /// <summary>
    /// Defines the configuration data for an Api version
    /// </summary>
    public class VersionConfigurationModel
    {
        /// <summary>
        /// Gets or sets the version (format m.n.p).
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the title of the current version.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the title of the current description.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the contact email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }
    }
}
