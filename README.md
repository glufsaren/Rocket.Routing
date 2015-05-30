# Rocket.Routing #

Rocket.Routing uses a route constraint in order to enable versioning of REST APIs. If no version is specified the route marked with `isLatest`.

## Quick summary ##

* Verision 1.0.0.0
* [Repository](https://bitbucket.org/glufsaren/rocket.routing/)

## Configuration ##

### Inversion of Control ###

IoC is controlled used [MEF](https://msdn.microsoft.com/en-us/library/dd460648%28v=vs.110%29.aspx). Either configure all dependencies or use one of the exiting IoC implementations.
At the moment implementations for [Autofac](https://www.nuget.org/packages/Rocket.Routing.IoC.Autofac/) and [StructureMap](https://www.nuget.org/packages/Rocket.Routing.IoC.StructureMap) is available on NuGet.

### Request id ###

To track your requests a request id is created for each request when handled. The request id is available during the entire request and is also added to the response headers and sent to the client.
By default the request id will be fetched from the `HttpRequestMessage.GetCorrelationId()` if you want to override this functionality implement the `IRequestIdService` and set up your IoC container to use an implementation that provides your id of choice.

The information about the request is stored in `HttpRequestMessage.Properties` if you want to store the information in another place  implement the `IAcceptHeaderStoreService` and set up your IoC container to use an implementation that uses your store of choice.

### Vendor name ###
To specify what domain to match, implement `IVendorNameService` and configure your implementation in the IoC container. It is recommended to inherit the `DefaultVendorNameService` and override the `GetName` method to specify your vendor name.

## Usage ##

Use the `Accept` header to specify in what format the content should be received. If no `Accept` header is specified the content will be returned in json with the last version of the resource. Version and content type are optional and will default to latest version of the resource and json if not specified.

The format for the custom header should follow:

	application/vnd.[VENDOR_NAME]([.DOMAIN])([+FORMAT];)([ version=VERSION;])

Regular media types are supported:

	application/json

as well as custom media types:

	application/vnd.rocket.se;
	application/vnd.rocket.se+json;
	application/vnd.rocket.se+json; version=1;

To check in what format the content was received check the `X-[VENDOR_NAME]-Media-Type` response header:

Example:
	
	X-Rocket-Media-Type: rocket.v1; format=json;

The request id will be added to the `X-[VENDOR_NAME]-Request-Id`

Example:
	
	X-Rocket-Request-Id: a381cf7d-7502-4da2-b50c-4e9e8a24c97f
