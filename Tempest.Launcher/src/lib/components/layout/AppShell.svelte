<script lang="ts">
	import BackgroundBlur from "./BackgroundBlur.svelte";
	import Header from "./Header.svelte";
	import Sidebar from "./Sidebar.svelte";
</script>

<div class="app-shell">
	<Header />
	<BackgroundBlur />
	<div class="main-area">
		<Sidebar />
		<div class="main-content">
			<slot />
		</div>
	</div>
</div>

<style>
	.app-shell {
		display: flex;
		flex-direction: column;
		height: 100vh;
	}

	.main-area {
		display: flex;
		flex: 1;
	}

	.main-content {
		border: 1px solid var(--border-primary);
		border-right: 0;
		border-bottom: 0;
		overflow-y: auto;
		overflow-x: hidden;
		min-height: 100%;
		flex: 1;
		padding: var(--spacing-sm);
	}
</style>
