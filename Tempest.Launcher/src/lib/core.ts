import { Command } from "@tauri-apps/plugin-shell";

const createDevCommand = (args: string[]) =>
	Command.create("dotnet", ["run", "--project", "../../Tempest.CLI/Tempest.CLI.csproj", "--", ...args]);

const createProdCommand = (args: string[]) => Command.create("tempest-cli", args);

export const createCommand = import.meta.env.DEV ? createDevCommand : createProdCommand;
