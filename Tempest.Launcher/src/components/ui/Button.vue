<script setup lang="ts">
import type { ButtonHTMLAttributes } from "vue";

defineProps</* @vue-ignore */ ButtonHTMLAttributes>();
</script>
<template>
	<button v-bind="$props">
		<slot />
	</button>
</template>

<style scoped>
button {
	background: linear-gradient(#0f92cecc, #1f72a4cc, #137da9cc);
	box-shadow:
		rgba(0, 0, 0, 0.6) 0px 3px 6px 0px,
		rgba(0, 0, 0, 0.3) 0px 3px 6px 0px;
	color: var(--c-white);
	border: 2px solid var(--c-brand-3);
	border-radius: 2px;
	padding: 0.5rem 1rem;
	cursor: pointer;
	font-weight: 900;
	transition: box-shadow 0.3s ease-in-out;
}

button:hover {
	background: linear-gradient(#1cc6fbcc, #0194d4cc, #00a2dacc);
	box-shadow: var(--c-brand-1) 0px 0px 1rem 0.4rem;
	border-color: #1cc6fb;
}

button:active {
	background: linear-gradient(#0f92cecc, #1f72a4cc, #137da9cc);
	box-shadow: var(--c-brand-1) 0px 0px 1rem 0.4rem;
	border-color: var(--c-brand-3);
}
</style>
