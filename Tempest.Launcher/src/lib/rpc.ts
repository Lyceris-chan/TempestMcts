import { GrpcWebFetchTransport } from "@protobuf-ts/grpcweb-transport";
import { fetch } from "@tauri-apps/plugin-http";
import { ServerListServiceClient } from "./rpc/server_list_service.client";

const transport = new GrpcWebFetchTransport({
	baseUrl: "http://localhost:5197",
	format: "binary",
	fetch,
});

export const serverList = new ServerListServiceClient(transport);

export * from "./rpc/server";
