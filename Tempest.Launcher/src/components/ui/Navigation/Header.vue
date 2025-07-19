<template>
	<p>
		<slot />
	</p>
</template>

<style scoped>
p {
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
}
</style>
