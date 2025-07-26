<script lang="ts">
	import { page } from "$app/state";
	import Breadcrumb from "$lib/components/ui/Breadcrumb.svelte";
	import Button from "$lib/components/ui/Button.svelte";
	import { toReadablePathSegment } from "$lib/pages";

	let canNavigateBack = $state(false);
	let canNavigateForward = $state(false);
	let lastPage = $state<string | null>(null);

	const goBack = () => window.history.back();
	const goForward = () => window.history.forward();

	$effect(() => {
		canNavigateBack = page.url.pathname !== "/";
		canNavigateForward = lastPage !== null && lastPage !== page.url.pathname;

		lastPage = page.url.pathname;
	});

	const breadcrumbItems = $derived(() => {
		const segments = page.route.id?.split("/").filter(Boolean) ?? [];
		const items = [];

		const homeItem = segments.length === 0
			? { label: "Home", isCurrentPage: true }
			: { label: "Home", href: "/" };
		items.push(homeItem);

		let currentPath = "";
		segments.forEach((segment, index) => {
			currentPath += `/${segment}`;
			const isLast = index === segments.length - 1;

			const label = toReadablePathSegment(segment);

			if (isLast) {
				items.push({
					label,
					isCurrentPage: true,
				});
			} else {
				items.push({
					label,
					href: currentPath,
				});
			}
		});

		return items;
	});
</script>
<header class="header">
	<div class="glow">{""}</div>
	<div class="section">
		<div class="controls">
			<Button
				icon
				onclick={goBack}
				disabled={!canNavigateBack}
				title="Go back"
			>
				{"<"}
			</Button>
			<Button
				icon
				onclick={goForward}
				disabled={!canNavigateForward}
				title="Go forward"
			>
				{">"}
			</Button>
		</div>
		<Breadcrumb items={breadcrumbItems()} />
	</div>
	<div class="section"></div>
</header>

<style>
	.glow {
		background: radial-gradient(50% 50% at 50% 50%, rgba(15, 146, 206, 0.25) 0%, rgba(15, 146, 206, 0) 100%);
		width: 800px;
		height: 250px;
		top: -125px;
		left: -402px;
		position: fixed;
		z-index: -100;
	}

	.header {
		background-color: var(--bg-surface);
		padding: 0 var(--spacing-md);
		min-height: 4rem;
		justify-content: space-between;
		width: 100vw;
	}

	.controls {
		gap: var(--spacing-sm);
	}

	.section {
		gap: var(--spacing-md);
	}

	.header, .section, .controls {
		display: flex;
		align-items: center;
	}
</style>
