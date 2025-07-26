<script>
	import { page } from "$app/state";
	import { mainPages } from "$lib/pages";
</script>
<div class="sidebar">
	<div class="sidebar-section link-list">
		{#each mainPages as mainPage}
			<a class="sidebar-link" aria-current={page.url.pathname == mainPage.path} href={mainPage.path}>{mainPage.label}</a>
		{/each}
	</div>
	<div class="sidebar-section">
	</div>
</div>

<style>
	.sidebar {
		background-color: var(--bg-surface);
		display: flex;
		flex-direction: column;
		justify-content: space-between;
		width: 200px;
	}

	.sidebar-section {
		display: flex;
		flex-direction: column;
	}

	.link-list {
		padding: 0 var(--spacing-md);
		gap: var(--spacing-sm);
	}

	.sidebar-link {
		border-radius: var(--border-radius);
		color: inherit;
		text-decoration: none;
		display: flex;
		align-items: center;
		justify-content: center;
		text-align: center;
		font-weight: 600;
		padding: var(--spacing-md);
		text-transform: uppercase;
		transition: all 0.3s var(--curve);
		border: 1px solid transparent;
		position: relative;
	}

	.sidebar-link:hover {
		background: linear-gradient(135deg, #1cc6fbaa, #0194d4aa, #00a2daaa);
		box-shadow:
			#0f92ce 0px 0px 12px 0.1rem,
			rgba(0, 0, 0, 0.3) 0px 2px 8px 0px,
			inset rgba(255, 255, 255, 0.2) 0px 1px 0px 0px;
		border-color: #1cc6fb;
		text-decoration: none;
		transform: translateY(-1px);
	}

	.sidebar-link:focus-visible {
		outline: 2px solid var(--color-primary);
		outline-offset: 2px;
		background: linear-gradient(#0f92ce22, #1f72a422, #137da922);
		border-color: var(--color-primary);
	}

	.sidebar-link[aria-current="true"] {
		background: linear-gradient(135deg, #0f92ce88, #1f72a488, #137da988);
		border-color: var(--color-primary);
		/* box-shadow:
			#0f92ce 0px 0px 8px 0.05rem,
			rgba(0, 0, 0, 0.2) 0px 1px 4px 0px,
			inset rgba(255, 255, 255, 0.1) 0px 1px 0px 0px; */
		font-weight: 700;
	}

	.sidebar-link[aria-current="true"]:hover {
		background: linear-gradient(135deg, #1cc6fbcc, #0194d4cc, #00a2dacc);
		box-shadow:
			#1cc6fb 0px 0px 16px 0.15rem,
			rgba(0, 0, 0, 0.4) 0px 3px 12px 0px,
			inset rgba(255, 255, 255, 0.3) 0px 1px 0px 0px;
		border-color: #1cc6fb;
		transform: translateY(-2px);
	}
</style>
