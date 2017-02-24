using System.Collections.Generic;
using System.Text;
using Decision.Common.Extensions;
using Decision.Common.SEO.OpenGraph.Enums;
using Decision.Common.SEO.OpenGraph.Media;
using Decision.Common.SEO.OpenGraph.Structs;

namespace Decision.Common.SEO.OpenGraph.ObjectTypes.Facebook
{
    /// <summary>
    ///     This object type represents the user's activity contributing to a particular run, walk, or bike course. This object
    ///     type is not part of the
    ///     Open Graph standard but is used by Facebook.
    ///     See https://developers.facebook.com/docs/reference/opengraph/object-type/fitness.course/
    /// </summary>
    internal class OpenGraphFitnessCourse : OpenGraphMetadata
    {
        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="OpenGraphFitnessCourse" /> class.
        /// </summary>
        /// <param name="title">The title of the object as it should appear in the graph.</param>
        /// <param name="image">The default image.</param>
        /// <param name="url">The canonical URL of the object, used as its ID in the graph.</param>
        public OpenGraphFitnessCourse(string title, OpenGraphImage image, string url = null)
            : base(title, image, url)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Appends a HTML-encoded string representing this instance to the <paramref name="stringBuilder" /> containing the
        ///     Open Graph meta tags.
        /// </summary>
        /// <param name="stringBuilder">The string builder.</param>
        public override void ToString(StringBuilder stringBuilder)
        {
            base.ToString(stringBuilder);

            stringBuilder.AppendMetaPropertyContent("fitness:calories", Calories);

            if (CustomUnitEnergy != null)
            {
                stringBuilder.AppendMetaPropertyContent("fitness:custom_unit_energy:value", CustomUnitEnergy.Value);
                stringBuilder.AppendMetaPropertyContent("fitness:custom_unit_energy:units", CustomUnitEnergy.Units);
            }

            if (Distance != null)
            {
                stringBuilder.AppendMetaPropertyContent("fitness:distance:value", Distance.Value);
                stringBuilder.AppendMetaPropertyContent("fitness:distance:units", Distance.Units);
            }

            if (Duration != null)
            {
                stringBuilder.AppendMetaPropertyContent("fitness:duration:value", Duration.Value);
                stringBuilder.AppendMetaPropertyContent("fitness:duration:units", Duration.Units);
            }

            stringBuilder.AppendMetaPropertyContentIfNotNull("fitness:live_text", LiveText);

            if (Metrics != null)
            {
                foreach (var metric in Metrics)
                {
                    stringBuilder.AppendMetaPropertyContentIfNotNull("fitness:metrics:calories", metric.Calories);

                    if (metric.CustomUnitEnergy != null)
                    {
                        stringBuilder.AppendMetaPropertyContent("fitness:metrics:custom_unit_energy:value",
                            metric.CustomUnitEnergy.Value);
                        stringBuilder.AppendMetaPropertyContent("fitness:metrics:custom_unit_energy:units",
                            metric.CustomUnitEnergy.Units);
                    }

                    if (metric.Distance != null)
                    {
                        stringBuilder.AppendMetaPropertyContent("fitness:metrics:distance:value", metric.Distance.Value);
                        stringBuilder.AppendMetaPropertyContent("fitness:metrics:distance:units", metric.Distance.Units);
                    }

                    if (metric.Location != null)
                    {
                        stringBuilder.AppendMetaPropertyContent("fitness:metrics:location:latitude",
                            metric.Location.Latitude);
                        stringBuilder.AppendMetaPropertyContent("fitness:metrics:location:longitude",
                            metric.Location.Longitude);
                        stringBuilder.AppendMetaPropertyContentIfNotNull("fitness:metrics:location:altitude",
                            metric.Location.Altitude);
                    }

                    stringBuilder.AppendMetaPropertyContentIfNotNull("fitness:metrics:steps", metric.Steps);

                    if (metric.Speed != null)
                    {
                        stringBuilder.AppendMetaPropertyContent("fitness:metrics:speed:value", metric.Speed.Value);
                        stringBuilder.AppendMetaPropertyContent("fitness:metrics:speed:units", metric.Speed.Units);
                    }

                    stringBuilder.AppendMetaPropertyContentIfNotNull("fitness:metrics:timestamp", metric.Timestamp);

                    if (metric.Pace != null)
                    {
                        stringBuilder.AppendMetaPropertyContent("fitness:metrics:pace:value", metric.Pace.Value);
                        stringBuilder.AppendMetaPropertyContent("fitness:metrics:pace:units", metric.Pace.Units);
                    }
                }
            }

            if (Pace != null)
            {
                stringBuilder.AppendMetaPropertyContent("fitness:pace:value", Pace.Value);
                stringBuilder.AppendMetaPropertyContent("fitness:pace:units", Pace.Units);
            }

            if (Speed != null)
            {
                stringBuilder.AppendMetaPropertyContent("fitness:speed:value", Speed.Value);
                stringBuilder.AppendMetaPropertyContent("fitness:speed:units", Speed.Units);
            }

            if (Splits != null)
            {
                foreach (var split in Splits)
                {
                    stringBuilder.AppendMetaPropertyContent("fitness:splits:unit", split.IsMiles ? "mi" : "km");
                    foreach (var value in split.Values)
                    {
                        stringBuilder.AppendMetaPropertyContent("fitness:splits:values:value", value.Value);
                        stringBuilder.AppendMetaPropertyContent("fitness:splits:values:units", value.Units);
                    }
                }
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets an integer representing the number of calories used during the course.
        /// </summary>
        public int? Calories { get; set; }

        /// <summary>
        ///     Gets or sets the energy used during the course.
        /// </summary>
        public OpenGraphQuantity CustomUnitEnergy { get; set; }

        /// <summary>
        ///     Gets or sets the distance of the course.
        /// </summary>
        public OpenGraphQuantity Distance { get; set; }

        /// <summary>
        ///     Gets or sets the duration taken on the course.
        /// </summary>
        public OpenGraphQuantity Duration { get; set; }

        /// <summary>
        ///     Gets or sets a string value displayed in stories if the associated action's end_time has not passed, such as an
        ///     encouragement to friends to cheer the user on. The value is not rendered once the course has been completed.
        /// </summary>
        public string LiveText { get; set; }

        /// <summary>
        ///     Gets or sets the metrics about the course.
        /// </summary>
        public IEnumerable<OpenGraphFitnessActivityDataPoint> Metrics { get; set; }

        /// <summary>
        ///     Gets the namespace of this open graph type.
        /// </summary>
        public override string Namespace => "fitness: http://ogp.me/ns/fitness#";

        /// <summary>
        ///     Gets or sets the pace achieved on the course.
        /// </summary>
        public OpenGraphQuantity Pace { get; set; }

        /// <summary>
        ///     Gets or sets the split times during the course.
        /// </summary>
        public IEnumerable<OpenGraphSplit> Splits { get; set; }

        /// <summary>
        ///     Gets or sets the speed achieved on the course.
        /// </summary>
        public OpenGraphQuantity Speed { get; set; }

        /// <summary>
        ///     Gets the type of your object. Depending on the type you specify, other properties may also be required.
        /// </summary>
        public override OpenGraphType Type => OpenGraphType.FitnessCourse;

        #endregion
    }
}