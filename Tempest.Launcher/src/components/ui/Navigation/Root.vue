<template>
	<nav>
		<slot />
	</nav>
</template>

<style scoped>
nav {
	background-color: var(--c-nav-bg);
	-webkit-backdrop-filter: blur(4px);
	backdrop-filter: blur(4px);
	box-shadow: rgba(100, 100, 111, 0.2) 0px 7px 29px 0px;
	border-bottom: 2px solid var(--c-nav-border);
	display: flex;
	justify-content: space-between;
	height: 4rem;
	gap: 1rem;
	width: 100vw;
	position: fixed;
	top: 0;
	left: 0;
	z-index: 100;
}
</style>
