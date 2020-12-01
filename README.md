# signal-gen

Script for generating Typescript client code for SignalR hubs.
The C# code provides a single extension method for the IApplicationBuilder interface that uses reflection to generate a JSON document describing the hubs in the project, the methods they declare and potential custom types that are used as parameters. The Typescript part of the project is a script that takes this document and parses it into Typescript code containing clients for each of the hubs and their methods.

Still a very early version. Planned features include:
1. Add command line arguments for specifying where to save the generated clients.
2. Automatic route discovery. Right now routes for the hub methods have to be added manually.
3. UI for testing out hub methods, much like the UI made available by SwashBuckle for testing out controller methods.
4. Support for other languages than Typescript. Regular Javascript would be an easy next possibility.

More features might come up along the way.

Heavily inspired by the awesome work at nSwag: https://github.com/RicoSuter/NSwag
