<script lang="ts">
	import { onNavigate } from "$app/navigation";
	import { page } from "$app/state";
	import Breadcrumb from "$lib/components/ui/Breadcrumb.svelte";
	import Button from "$lib/components/ui/Button.svelte";
	import { toReadablePathSegment } from "$lib/pages";

	let historyStack = $state<string[]>([page.route.id || ""]);
	let historyIndex = $state(0);
	let navigating = false;

	onNavigate((navigation) => {
		if (!navigating) {
			if (historyIndex < historyStack.length - 1) {
				historyStack = historyStack.slice(0, historyIndex + 1);
			}
			historyStack.push(navigation.to?.route.id || "");
			historyIndex++;
		}
		navigating = false;
	});

	const goBack = () => {
		if (historyIndex > 0) {
			navigating = true;
			historyIndex--;
			window.history.back();
		}
	};

	const goForward = () => {
		if (historyIndex < historyStack.length - 1) {
			navigating = true;
			historyIndex++;
			window.history.forward();
		}
	};

	const canNavigateBack = $derived(historyIndex > 0);
	const canNavigateForward = $derived(historyIndex < historyStack.length - 1);

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
				kind="icon"
				onclick={goBack}
				disabled={!canNavigateBack}
				title="Go back"
			>
				{"<"}
			</Button>
			<Button
				kind="icon"
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
