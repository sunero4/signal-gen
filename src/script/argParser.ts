const knownArguments = ["url", "outDir"];

export function parseCommandLineArgs(argv?: string[]): CommandLineArgument[] {
  const args = argv && argv.length > 0 ? argv : process.argv;

  return knownArguments.map((a) => getArgument(a, args));
}

function getArgument(name: string, args: string[]): CommandLineArgument {
  const indexOfArgName = args.indexOf(`--${name}`);

  if (indexOfArgName < 0) {
    return {
      name: name,
      value: null,
      exists: false,
    };
  }

  const value =
    args.length > indexOfArgName + 1 ? args[indexOfArgName + 1] : null;

  return {
    name: name,
    value: value,
    exists: Boolean(value),
  };
}

interface CommandLineArgument {
  name: string;
  value: string | null;
  exists: boolean;
}
