// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VersionedRouteAttribute.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the VersionedRouteAttribute type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Web.Http.Routing;

namespace Rocket.Routing
{
    public class VersionedRouteAttribute : RouteFactoryAttribute
    {
        private readonly double _version;
        private readonly bool _isLatest;

        public VersionedRouteAttribute(
                    string template,
                    double version,
                    bool isLatest = false)
            : base(template)
        {
            _version = version;
            _isLatest = isLatest;
        }

        public override IDictionary<string, object> Constraints
        {
            get
            {
                return new HttpRouteValueDictionary
                           {
                               { 
                                   "version",
                                   new VersionConstraint(_version, _isLatest) 
                               }
                           };
            }
        }
    }
}