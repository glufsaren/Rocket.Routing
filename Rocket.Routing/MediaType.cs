// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaType.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the MediaType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Text;

using Rocket.Core.Extensions;

namespace Rocket.Routing
{
    public class MediaType
    {
        private const string MediaTypeHeaderName = "X-{0}-Media-Type";
        private const string RequestIdHeaderName = "X-{0}-Request-Id";

        public virtual double? RequestedVersion { get; set; }

        public virtual double ActualVersion { get; set; }

        public virtual ContentType ContentType { get; set; }

        public virtual Guid RequestId { get; set; }

        public virtual bool Matched { get; set; }

        public static string GetMediaTypeHeaderName(string vendorName)
        {
            return GeteaderName(
                MediaTypeHeaderName, vendorName);
        }

        public static string GetRequestIdHeaderName(string vendorName)
        {
            return GeteaderName(
                RequestIdHeaderName, vendorName);
        }

        public string GetMediaTypeString(string vendorName)
        {
            var stringBuilder = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(vendorName))
            {
                stringBuilder.AppendFormat(
                                "{0}.v{1};",
                                vendorName,
                                ActualVersion.ToInvariantString());
            }
            else
            {
                stringBuilder.AppendFormat(
                    "v{0};", ActualVersion.ToInvariantString());
            }

            if (ContentType != ContentType.Unspecified)
            {
                stringBuilder.AppendFormat(" format={0};", ContentType.ToString().ToLower());
            }

            return stringBuilder.ToString();
        }

        private static string GeteaderName(string headerName, string vendorName)
        {
            return string.IsNullOrWhiteSpace(vendorName)
                            ? headerName.Replace("-{0}", string.Empty)
                            : string.Format(headerName, vendorName);
        }
    }
}