import { useLocalStorage } from "@vueuse/core";

export type Build = {
    
};

export const useBuilds = () => {
    const builds = useLocalStorage<Build[]>("builds", []);
};