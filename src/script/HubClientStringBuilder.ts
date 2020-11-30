export interface IHubClientStringBuilder {
  withNewLine: (n?: number) => IHubClientStringBuilder;
  setTabSize: (n: number) => IHubClientStringBuilder;
  increaseTabs: () => IHubClientStringBuilder;
  decreaseTabs: () => IHubClientStringBuilder;
  withLine: (lineStr: string) => IHubClientStringBuilder;
  build: () => string;
}

export class HubClientStringBuilder implements IHubClientStringBuilder {
  private _clientCode: string = "";
  private _tabSize: number = 2;
  private _tabs: number = 0;

  withNewLine(n: number = 1): IHubClientStringBuilder {
    for (let i = 0; i < n; i++) {
      this._clientCode = this._clientCode.concat("\n");
    }

    return this;
  }
  setTabSize(n: number): IHubClientStringBuilder {
    this._tabSize = n;

    return this;
  }
  increaseTabs(): IHubClientStringBuilder {
    this._tabs++;

    return this;
  }
  decreaseTabs(): IHubClientStringBuilder {
    if (this._tabs > 0) {
      this._tabs--;
    }

    return this;
  }
  withLine(lineStr: string): IHubClientStringBuilder {
    this._clientCode = this._clientCode
      .concat(" ".repeat(this._tabSize).repeat(this._tabs))
      .concat(lineStr);
    return this;
  }

  build(): string {
    return this._clientCode;
  }
}
