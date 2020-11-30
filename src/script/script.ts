import fetch from "node-fetch";
import * as fs from "fs";
import { HubClientBuilder } from "./HubClientBuilder";
import { SignalGenDiscoveryDoc } from "./models";

const fetchDiscoveryDoc = async (): Promise<void> => {
  const response = await fetch(
    "http://localhost:5000/signalgen/discovery.json"
  );
  const json = await response.json();

  const hubs = json as SignalGenDiscoveryDoc;

  const builder = new HubClientBuilder();
  builder.withTypes(hubs.Types);
  builder.withHubs(hubs.HubInfo);
  const result = builder.build();
  fs.writeFileSync("./SignalGenClients2.ts", result);
};

fetchDiscoveryDoc();
