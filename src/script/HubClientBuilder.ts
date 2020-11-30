import {
  IHubClientStringBuilder,
  HubClientStringBuilder,
} from "./HubClientStringBuilder";
import { HubInfo, TypeInfo, MethodInfo } from "./models";
import { mapType } from "./typeMapper";

export class HubClientBuilder {
  private _builder: IHubClientStringBuilder = new HubClientStringBuilder().setTabSize(
    2
  );

  constructor() {
    this.withImportSignalR();
  }

  build(): string {
    return this._builder.build();
  }

  withHubs(hubs: HubInfo[]): HubClientBuilder {
    hubs.forEach((hub) => {
      this.withHub(hub);
    });
    return this;
  }

  withHub(hub: HubInfo): HubClientBuilder {
    this.withSignalRConnection(hub).withGetConnection(hub);
    hub.Methods.forEach((method) => {
      this.withHubMethod(method);
    });
    this._builder.decreaseTabs().withLine("}").withNewLine(2);

    return this;
  }

  withTypes(types: TypeInfo[]): HubClientBuilder {
    types.forEach((t) => {
      this.withType(t);
    });

    return this;
  }

  withType(type: TypeInfo): HubClientBuilder {
    this._builder.withLine(`interface ${type.TypeName} {`).increaseTabs();

    type.Fields.forEach((t) => {
      const typeName = mapType(t.TypeName, t.IsCollection) ?? t.TypeName;
      console.log(typeName);
      this._builder.withNewLine().withLine(`${t.PropertyName}: ${typeName}`);
    });

    this._builder.decreaseTabs().withNewLine().withLine("}").withNewLine(2);

    return this;
  }

  private withImportSignalR(): HubClientBuilder {
    this._builder
      .withLine(
        "import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr'"
      )
      .withNewLine(2);

    return this;
  }

  private withSignalRConnection(hub: HubInfo): HubClientBuilder {
    this._builder
      .withLine(`class ${hub.Name}Client {\n`)
      .increaseTabs()
      .withNewLine()
      .withLine("private _connection = new HubConnectionBuilder()")
      .increaseTabs()
      .withNewLine()
      .withLine(`.withUrl(${hub.Name.toUpperCase()}_URL_PLACEHOLDER)`)
      .withNewLine()
      .withLine(".withAutomaticReconnect()")
      .withNewLine()
      .withLine(".build();")
      .decreaseTabs()
      .withNewLine(2);

    return this;
  }

  private withGetConnection(hub: HubInfo): HubClientBuilder {
    this._builder
      .withLine(`public get${hub.Name}Connection(): HubConnection {`)
      .increaseTabs()
      .withNewLine()
      .withLine("return this._connection;")
      .decreaseTabs()
      .withNewLine()
      .withLine("}")
      .withNewLine(2);

    return this;
  }

  private withHubMethod(method: MethodInfo): HubClientBuilder {
    this._builder
      .withLine(
        `public async ${method.Name}(${method.Arguments.map(
          (arg) =>
            `${arg.Name}: ${mapType(arg.Type, arg.IsCollection) ?? arg.Type}`
        ).join(", ")}): Promise<void> {`
      )
      .increaseTabs()
      .withNewLine()
      .withLine(
        `await this._connection.invoke("${method.Name}", ${method.Arguments.map(
          (a) => a.Name
        ).join(", ")});`
      )
      .decreaseTabs()
      .withNewLine()
      .withLine("}")
      .withNewLine(2);

    return this;
  }
}
