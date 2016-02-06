using System;
using Decision.Common.OpenGraph.ObjectTypes.Standard;

namespace Decision.Common.OpenGraph.Structs
{
    /// <summary>
    /// Represents an actor in a video.
    /// </summary>
    public class OpenGraphActor
    {
        private readonly string _actorUrl;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphActor"/> class.
        /// </summary>
        /// <param name="actorUrl">The URL to the page about the actor. This URL must contain profile meta tags <see cref="OpenGraphProfile"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actorUrl"/> is <c>null</c>.</exception>
        public OpenGraphActor(string actorUrl)
        {
            if (actorUrl == null) { throw new ArgumentNullException(nameof(actorUrl)); }

            this._actorUrl = actorUrl;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenGraphActor"/> class.
        /// </summary>
        /// <param name="actorUrl">The URL to the page about the actor. This URL must contain profile meta tags <see cref="OpenGraphProfile"/>.</param>
        /// <param name="role">The role the actor played.</param>
        /// <exception cref="ArgumentNullException"><paramref name="actorUrl"/> is <c>null</c>.</exception>
        public OpenGraphActor(string actorUrl, string role) : this(actorUrl)
        {
            this.Role = role;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the URL to the page about the actor. This URL must contain profile meta tags <see cref="OpenGraphProfile"/>.
        /// </summary>
        public string ActorUrl { get { return this._actorUrl; } }

        /// <summary>
        /// Gets or sets the role the actor played.
        /// </summary>
        public string Role { get; set; }

        #endregion
    }
}
