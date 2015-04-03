// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OrderController.cs" company="Borderline Studios">
//   Copyright © Borderline Studios. All rights reserved.
// </copyright>
// <summary>
//   Defines the OrderController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Web.Http;

namespace Rocket.Routing.Test.Component.TestDoubles
{
    [RoutePrefix("api/orders")]
    public class OrderController : ApiController
    {
        [HttpGet]
        [VersionedRoute("", 1, isLatest: false)]
        public IHttpActionResult OrdersV1()
        {
            return Json("{\"version\":\"1\",\"isLatest\":\"false\"}");
        }

        [HttpGet]
        [VersionedRoute("", 2, isLatest: true)]
        public IHttpActionResult OrdersV2()
        {
            return Json("{\"version\":\"2\",\"isLatest\":\"true\"}");
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Json("{version:X}");
        }
    }
}