<script setup lang="ts">
const { text } = defineProps<{
    text: string
}>();
</script>

<template>
	<header>
        <h1>{{ text }}</h1>
		<section>
            <slot />
        </section>
	</header>
</template>

<style scoped>
header {
    display: flex;
    justify-content: space-between;
    align-items: center;
}

h1 {
    font-weight: 900;
    text-shadow: var(--c-black) 2px 2px 4px;
}

section {
    display: flex;
    align-items: center;
    gap: 1rem;
}
</style>