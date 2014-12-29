# README #

This README would normally document whatever steps are necessary to get your application up and running.

### What is this repository for? ###

* Quick summary
* Version
* [Learn Markdown](https://bitbucket.org/tutorials/markdowndemo)

### How do I get set up? ###

* Summary of set up
* Configuration
* Dependencies
* Database configuration
* How to run tests
* Deployment instructions

### Contribution guidelines ###

* Writing tests
* Code review
* Other guidelines

### Who do I talk to? ###

* Repo owner or admin
* Other community or team contact

<a class="anchor" id="versioning"></a>
	<h2>API Versioning</h2>
	<p>
		The <code>Accept</code> header is used to specify in what format the content should be received. If no <code>Accept</code> header is specified the content will be returned in json with the last version of the resource.
		Version and content type are optional and will default to latest version of the resource and json if not specified.
		<br /><br />The format for the custom header should follow:<pre><code class="hljs github json hljs">application/vnd.urbit[.<b>domain</b>][+<b>format</b>];[ version=<b>version</b>;]</code></pre>
		Regular media types are supported:<pre><code class="hljs github json hljs">application/json</code></pre>
		as well as custom media types:<pre><code class="hljs github json hljs">application/vnd.urb-it.com;
application/vnd.urb-it.com+json;
application/vnd.urb-it.se+json; version=1;</code></pre>
	<p class="api-note"><strong>Important</strong>: Please note that the latest version of the API may change, therefore it's recommended that you always specify a specific version of the API in the <code>Accept</code> header.</p>
	<p>To check in what format the content was received check the <code>X-Urbit-Media-Type</code> response header</p>
	<pre><code class="hljs github json hljs ">HTTP/1.1 200 OK
X-Urbit-Media-Type: urbit.v1; format=json</code></pre>
	<hr />