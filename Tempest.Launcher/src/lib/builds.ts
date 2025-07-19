import { useLocalStorage } from "@vueuse/core";
import { computed, readonly } from "vue";

export type Build = {
	id: string;
	label: string;
	path: string;
};

export const useBuilds = () => {
	const builds = useLocalStorage<Build[]>("builds", []);
	const selectedBuildId = useLocalStorage<string | null>("selectedBuild", null);

	const selectedBuild = computed(() => builds.value.find(b => b.id == selectedBuildId.value));

	return {
		builds: readonly(builds),
		selectedBuild,

		addBuild: (build: Omit<Build, "id">) => builds.value = [{ id: crypto.randomUUID(), ...build }, ...builds.value],
		selectBuild: (id: string) => selectedBuildId.value = id,
	};
};
