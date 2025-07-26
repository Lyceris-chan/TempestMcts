<script lang="ts">
	interface BreadcrumbItem {
		label: string;
		href?: string;
		isCurrentPage?: boolean;
	}

	interface EllipsisItem {
		label: string;
		isEllipsis: true;
	}

	type DisplayItem = BreadcrumbItem | EllipsisItem;

	interface Props {
		items: BreadcrumbItem[];
		separator?: string;
		maxItems?: number;
		class?: string;
	}

	let { items, separator = "/", maxItems, class: className = "" }: Props = $props();

	const displayItems = $derived((): DisplayItem[] => {
		if (!maxItems || items.length <= maxItems) {
			return items;
		} else if (maxItems <= 1) {
			return [items[items.length - 1]];
		} else if (maxItems === 2) {
			return [items[0], items[items.length - 1]];
		} else {
			const firstItem = items[0];
			const lastItems = items.slice(-(maxItems - 2));
			return [
				firstItem,
				{ label: "...", isEllipsis: true },
				...lastItems,
			];
		}
	});

	function isEllipsis(item: DisplayItem): item is EllipsisItem {
		return "isEllipsis" in item;
	}
</script>

<nav class="breadcrumb {className}" aria-label="Breadcrumb">
	<ol class="breadcrumb-list">
		{#each displayItems() as item, index}
			<li class="breadcrumb-item">
				{#if isEllipsis(item)}
					<span class="breadcrumb-ellipsis" aria-hidden="true">…</span>
				{:else if item.href && !item.isCurrentPage}
					<a href={item.href} class="breadcrumb-link">
						{item.label}
					</a>
				{:else}
					<span class="breadcrumb-current" aria-current={item.isCurrentPage ? "page" : undefined}>
						{item.label}
					</span>
				{/if}

				{#if index < displayItems().length - 1 && !isEllipsis(displayItems()[index + 1])}
					<span class="breadcrumb-separator" aria-hidden="true">{separator}</span>
				{/if}
			</li>
		{/each}
	</ol>
</nav>

<style>
	.breadcrumb {
		display: flex;
		align-items: center;
		font-size: 0.875rem;
		color: var(--text-secondary);
	}

	.breadcrumb-list {
		display: flex;
		align-items: center;
		flex-wrap: wrap;
		gap: var(--spacing-xs);
		list-style: none;
		padding: 0;
		margin: 0;
	}

	.breadcrumb-item {
		display: flex;
		align-items: center;
		gap: var(--spacing-xs);
	}

	.breadcrumb-link {
		color: var(--text-secondary);
		text-decoration: none;
		padding: var(--spacing-xs) var(--spacing-sm);
		border-radius: var(--border-radius);
		transition: all 0.3s var(--curve);
		display: inline-block;
		position: relative;
		font-weight: 600;
		border: 1px solid transparent;
		margin: 1px;
	}

	.breadcrumb-link:hover {
		background: linear-gradient(135deg, #1cc6fbaa, #0194d4aa, #00a2daaa);
		box-shadow: 
			#0f92ce 0px 0px 12px 0.1rem,
			rgba(0, 0, 0, 0.3) 0px 2px 8px 0px,
			inset rgba(255, 255, 255, 0.2) 0px 1px 0px 0px;
		border-color: #1cc6fb;
		color: var(--text-primary);
		text-decoration: none;
		transform: translateY(-1px);
	}

	.breadcrumb-link:focus-visible {
		outline: 2px solid var(--color-primary);
		outline-offset: 2px;
		color: var(--text-primary);
		background: linear-gradient(#0f92ce22, #1f72a422, #137da922);
		border-color: var(--color-primary);
	}

	.breadcrumb-current {
		color: var(--text-primary);
		font-weight: 700;
		padding: var(--spacing-xs) var(--spacing-sm);
		border-radius: var(--border-radius);
		background: linear-gradient(#0f92ce22, #1f72a422, #137da922);
		border: 1px solid rgba(15, 146, 206, 0.3);
	}

	.breadcrumb-separator {
		color: var(--text-muted);
		font-size: 0.875rem;
		font-weight: 600;
		user-select: none;
		opacity: 0.7;
	}

	.breadcrumb-ellipsis {
		color: var(--text-muted);
		padding: var(--spacing-xs) var(--spacing-sm);
		user-select: none;
	}
</style>
