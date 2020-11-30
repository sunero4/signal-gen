const typeMap: { [key: string]: string } = {
  String: "string",
  Int16: "number",
  Int32: "number",
  Int64: "number",
  Guid: "string",
  Double: "number",
  Single: "number",
  Boolean: "boolean",
};

export function mapType(typeName: string, isCollection: boolean): string {
  if (typeMap[typeName] && !isCollection) return typeMap[typeName];

  if (isCollection)
    return typeMap[typeName] ? `${typeMap[typeName]}[]` : `${typeName}[]`;

  return typeName;
}
