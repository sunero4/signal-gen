export interface SignalGenDiscoveryDoc {
  HubInfo: HubInfo[];
  Types: TypeInfo[];
}

export interface TypeInfo {
  TypeName: string;
  PropertyName: string;
  Fields: TypeInfo[];
  IsCollection: boolean;
}

export interface HubInfo {
  Name: string;
  Methods: MethodInfo[];
}

export interface MethodInfo {
  Name: string;
  Arguments: ArgumentInfo[];
}

export interface ArgumentInfo {
  Type: string;
  Name: string;
  IsCollection: boolean;
}
