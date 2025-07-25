<script>
	import Button from "$lib/components/ui/Button.svelte";

</script>
<header class="header">
	<div class="section">
		<Button icon>{"<"}</Button>
		<Button icon>{">"}</Button>
	</div>
	<div class="section">right</div>
</header>

<style>
	.header {
		background-color: var(--bg-surface);
		padding: 0 var(--spacing-lg);
		min-height: 4rem;
		justify-content: space-between;
		width: 100vw;
	}

	.section {
		gap: var(--spacing-sm);
	}

	.header, .section {
		display: flex;
		align-items: center;
	}
</style>
