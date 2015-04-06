# Rocket.Routing #

Rocket.Routing uses a route constraint in order to enable versioning of REST APIs. If no version is specified the route marked with `isLatest`.

## Quick summary ##

* Verision 1.0.0.0
* [Repository](https://bitbucket.org/glufsaren/rocket.routing/)

### How do I get set up? ###

* Summary of set up
* Configuration
* Dependencies
* Database configuration
* How to run tests
* Deployment instructions

## Configuration ##

### Inversion of Control ###

Rocket.Routing is configured with the [Autofac](http://autofac.org/) Container. You need to register the `RoutingModule` in your application, add this to your Autofac bootstrap code:

	builder.RegisterModule(new RoutingModule(config));

You should be able to use an other IoC container by replacing the configuration in `RoutingModule`.

### Request id ###

To track your request a request id is created when the request is handled. The request is available during the request and is also added to the response headers and sent to the client.
by default the request id will be fetched from the `HttpRequestMessage.GetCorrelationId()` if you want to override this functionality implement the `IRequestIdService` and set up your IoC container to use your implementation that provides your id of choice.
The information about the request is stored in `HttpRequestMessage.Properties` if you want to store the information in another place  implement the `IAcceptHeaderStoreService` and set up your IoC container to use your implementation that provides your id of choice.

### Vendor name ###
To specify what domain to match, implement `IVendorNameService` and configure your implementation in the IoC container. It is recommended to inherit the `DefaultVendorNameService` and override the `GetName` method.

## Usage ##

<a class="anchor" id="versioning"></a>
	<h2>API Versioning</h2>
	<p>
		The <code>Accept</code> header is used to specify in what format the content should be received. If no <code>Accept</code> header is specified the content will be returned in json with the last version of the resource.
		Version and content type are optional and will default to latest version of the resource and json if not specified.
		<br /><br />The format for the custom header should follow:<pre><code class="hljs github json hljs">application/vnd.rocket[.<b>domain</b>][+<b>format</b>];[ version=<b>version</b>;]</code></pre>
		Regular media types are supported:<pre><code class="hljs github json hljs">application/json</code></pre>
		as well as custom media types:<pre><code class="hljs github json hljs">application/vnd.rocket.com;
application/vnd.rocket.com+json;
application/vnd.rocket.se+json; version=1;</code></pre>
	<p class="api-note"><strong>Important</strong>: Please note that the latest version of the API may change, therefore it's recommended that you always specify a specific version of the API in the <code>Accept</code> header.</p>
	<p>To check in what format the content was received check the <code>X-Rocket-Media-Type</code> response header</p>
	<pre><code class="hljs github json hljs ">HTTP/1.1 200 OK
X-Rocket-Media-Type: rocket.v1; format=json</code></pre>
	<hr />