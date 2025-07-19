<script setup lang="ts">
import { playSound } from "@/lib/sounds";
import { RouterLink } from "vue-router";

const { to } = defineProps<{
	to: string;
	title: string;
}>();
</script>

<template>
	<RouterLink activeClass="active" :to="to" @click="() => playSound('clickSwitch')">
		{{ title }}
	</RouterLink>
</template>

<style scoped>
a {
	color: var(--c-text-1);
	font-weight: 900;
	font-size: 1.4rem;
	padding: 0 2rem;
	height: 100%;
	display: flex;
	align-items: center;
	justify-content: center;
	text-decoration: none;
	text-shadow: var(--c-black) 2px 2px 4px;
	clip-path: polygon(18px 0, 100% 0, calc(100% - 18px) 100%, 0 100%);
	text-transform: uppercase;
}

a:not(.active):hover {
	background-color: var(--c-bg-soft);
}

a.active {
	background-color: var(--c-nav-border);
}

a:not(:first-child) {
	margin-left: -18px;
}
</style>
