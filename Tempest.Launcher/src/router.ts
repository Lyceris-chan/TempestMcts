import { createMemoryHistory, createRouter, RouteRecordRaw } from "vue-router";

export const routes: RouteRecordRaw[] = [
	{ path: "/", component: () => import("./pages/Index.vue") },
	{ path: "/builds", component: () => import("./pages/Builds.vue") },
	{ path: "/multiplayer", component: () => import("./pages/Multiplayer.vue") },
	{ path: "/servers", component: () => import("./pages/Servers.vue") },
];

export const router = createRouter({
	history: createMemoryHistory(),
	routes,
});
