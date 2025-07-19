<script setup lang="ts">
import { reactive } from "vue";

const { padding } = defineProps<{
	padding?: "none" | "minimal" | "standard";
}>();

const classes = reactive({
	none: padding == "none" || !padding,
	minimal: padding == "minimal",
	standard: padding == "standard",
});
</script>

<template>
	<article :class="classes">
		<slot />
	</article>
</template>

<style scoped>
article {
	overflow-y: auto;
}

.minimal {
	padding-top: calc(4rem + 1px);
}

.standard {
	padding: 1rem;
	padding-top: calc(5rem + 1px);
}
</style>
