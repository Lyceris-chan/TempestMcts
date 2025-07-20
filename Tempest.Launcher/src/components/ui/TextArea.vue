<script setup lang="ts">
import type { TextareaHTMLAttributes } from "vue";

defineProps</* @vue-ignore */ TextareaHTMLAttributes>();
</script>
<template>
	<textarea></textarea>
</template>

<style scoped>
textarea {
	background: linear-gradient(#0f92cecc, #1f72a4cc, #137da9cc);
	box-shadow:
		rgba(0, 0, 0, 0.6) 0px 3px 6px 0px,
		rgba(0, 0, 0, 0.3) 0px 3px 6px 0px;
	color: var(--c-white);
	border: 2px solid var(--c-brand-3);
	border-radius: 2px;
	padding: 0.5rem 1rem;
	font-weight: 600;
	font-size: 16px;
	width: 225px;
	height: 45px;
	resize: none;
	overflow: hidden;
	transition: box-shadow 0.3s ease-in-out;
}

textarea::placeholder {
	font-size: 16px;
	font-weight: 600;
}

textarea:hover {
	background: linear-gradient(#1cc6fbcc, #0194d4cc, #00a2dacc);
	box-shadow: var(--c-brand-1) 0px 0px 1rem 0.4rem;
	border-color: #1cc6fb;
}

textarea:active {
	background: linear-gradient(#0f92cecc, #1f72a4cc, #137da9cc);
	box-shadow: var(--c-brand-1) 0px 0px 1rem 0.4rem;
	border-color: var(--c-brand-3);
}
</style>
