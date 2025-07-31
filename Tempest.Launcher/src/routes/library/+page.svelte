<script>
	import Button from "$lib/components/ui/Button.svelte";
	import plus from "@iconify-icons/mdi/plus";
</script>

{#if true}
	<div class="empty-library">
		<h1>Nothing here...</h1>
		<p>But you can get started by adding an instance of Paladins.</p>
		<Button href="/library/add-instance" icon={plus}>Add Instance</Button>
	</div>
{/if}

<style>
	.empty-library {
		display: flex;
		flex-direction: column;
		align-items: center;
		justify-content: center;
		text-align: center;
		gap: var(--spacing-sm);
		height: 100%;
		width: 100%;
	}
</style>
