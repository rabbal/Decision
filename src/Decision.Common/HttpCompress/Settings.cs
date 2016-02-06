using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Xml;

namespace Decision.Common.HttpCompress
{
    public sealed class Settings
    {

        private Algorithms _preferredAlgorithm;
        private CompressionLevels _compressionLevel;
        private readonly StringCollection _excludedTypes;
        private readonly StringCollection _excludedPaths;

        /// <summary>
        /// Create an HttpCompressionModuleSettings from an XmlNode
        /// </summary>
        /// <param name="node">The XmlNode to configure from</param>
        public Settings(XmlNode node)
            : this()
        {
            AddSettings(node);
        }


        private Settings()
        {
            _preferredAlgorithm = Algorithms.Default;
            _compressionLevel = CompressionLevels.Default;
            _excludedTypes = new StringCollection();
            _excludedPaths = new StringCollection();
            InitTypes();
        }

        private void InitTypes()
        {
            _excludedPaths.Add(".axd");
            _excludedTypes.Add("image/jpeg");
            _excludedTypes.Add("image/png");
            _excludedTypes.Add("image/gif");
            _excludedTypes.Add("image/jpg");
        }

        /// <summary>
        /// Suck in some more changes from an XmlNode.  Handy for config file parenting.
        /// </summary>
        /// <param name="node">The node to read from</param>
        public void AddSettings(XmlNode node)
        {
            if (node == null)
                return;

            var preferredAlgorithm = node.Attributes?["preferredAlgorithm"];
            if (preferredAlgorithm != null)
            {
                try
                {
                    _preferredAlgorithm = (Algorithms)Enum.Parse(typeof(Algorithms), preferredAlgorithm.Value, true);
                }
                catch (ArgumentException) { }
            }

            var compressionLevel = node.Attributes?["compressionLevel"];
            if (compressionLevel != null)
            {
                try
                {
                    _compressionLevel = (CompressionLevels)Enum.Parse(typeof(CompressionLevels), compressionLevel.Value, true);
                }
                catch (ArgumentException) { }
            }

            ParseExcludedTypes(node.SelectSingleNode("excludedMimeTypes"));
            ParseExcludedPaths(node.SelectSingleNode("excludedPaths"));

        }


        /// <summary>
        /// Get the current settings from the xml config file
        /// </summary>
        public static Settings GetSettings()
        {
#pragma warning disable 618
            var settings = (Settings)ConfigurationSettings.GetConfig("blowery.web/httpCompress");
#pragma warning restore 618
            return settings ?? Default;
        }

        /// <summary>
        /// The default settings.  Deflate + normal.
        /// </summary>
        public static Settings Default => new Settings();

        /// <summary>
        /// The preferred algorithm to use for compression
        /// </summary>
        public Algorithms PreferredAlgorithm => _preferredAlgorithm;


        /// <summary>
        /// The preferred compression level
        /// </summary>
        public CompressionLevels CompressionLevel => _compressionLevel;


        /// <summary>
        /// Checks a given mime type to determine if it has been excluded from compression
        /// </summary>
        /// <param name="mimetype">The MimeType to check.  Can include wildcards like image/* or */xml.</param>
        /// <returns>true if the mime type passed in is excluded from compression, false otherwise</returns>
        public bool IsExcludedMimeType(string mimetype)
        {
            return mimetype == null || _excludedTypes.Contains(mimetype.ToLower());
        }

        /// <summary>
        /// Looks for a given path in the list of paths excluded from compression
        /// </summary>
        /// <param name="relUrl">the relative url to check</param>
        /// <returns>true if excluded, false if not</returns>
        public bool IsExcludedPath(string relUrl)
        {
            return _excludedPaths.Contains(relUrl.ToLower());
        }

        private void ParseExcludedTypes(XmlNode node)
        {
            if (node == null) return;

            for (var i = 0; i < node.ChildNodes.Count; ++i)
            {
                switch (node.ChildNodes[i].LocalName)
                {
                    case "add":
                        var xmlAttributeCollection = node.ChildNodes[i].Attributes;
                        if (xmlAttributeCollection?["type"] != null)
                        {
                            var collection = node.ChildNodes[i].Attributes;
                            if (collection != null)
                                _excludedTypes.Add(collection["type"].Value.ToLower());
                        }
                        break;
                    case "delete":
                        var attributeCollection = node.ChildNodes[i].Attributes;
                        if (attributeCollection?["type"] != null)
                        {
                            var attributes = node.ChildNodes[i].Attributes;
                            if (attributes != null)
                                _excludedTypes.Remove(attributes["type"].Value.ToLower());
                        }
                        break;
                }
            }
        }

        private void ParseExcludedPaths(XmlNode node)
        {
            if (node == null) return;

            for (var i = 0; i < node.ChildNodes.Count; ++i)
            {
                switch (node.ChildNodes[i].LocalName)
                {
                    case "add":
                        var xmlAttributeCollection = node.ChildNodes[i].Attributes;
                        if (xmlAttributeCollection?["path"] != null)
                        {
                            var attributes = node.ChildNodes[i].Attributes;
                            if (attributes != null)
                                _excludedPaths.Add(attributes["path"].Value.ToLower());
                        }
                        break;
                    case "delete":
                        var attributeCollection = node.ChildNodes[i].Attributes;
                        if (attributeCollection?["path"] != null)
                        {
                            var collection = node.ChildNodes[i].Attributes;
                            if (collection != null)
                                _excludedPaths.Remove(collection["path"].Value.ToLower());
                        }
                        break;
                }
            }
        }
    }
}
